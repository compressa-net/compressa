using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Transactions;
using Compressa.API.Models.AssemblyAI;
using Compressa.API.Models.Audiobook;
using Compressa.API.Models.Cohere;
using FFMpegCore;
using FFMpegCore.Enums;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using Xabe.FFmpeg;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Compressa.API.Services
{
    public class CompressaService : ICompressaService
    {
        private readonly ILogger<CompressaService> _logger;
        private readonly IConfiguration _config;
        private readonly string _mediaFolder;
        private readonly string? _kindleActivationBytes;
        private readonly string? _assemblyAIKey;
        private readonly string? _assemblyAIUrl;
        private readonly string? _cohereKey;
        private readonly string? _cohereUrl;
        private readonly Dictionary<string, string> _audiobooks = new Dictionary<string, string>();

        public CompressaService(ILogger<CompressaService> logger, IConfiguration configRoot)
        {
            _logger = logger;
            _config = configRoot;

            var compressaSection = _config.GetSection("Compressa");

            _mediaFolder = compressaSection["MediaFolder"] ?? ".\\Media";
            _kindleActivationBytes = compressaSection["Kindle:ActivationBytes"];
            _assemblyAIKey = compressaSection["AssemblyAI:APIKey"];
            _assemblyAIUrl = compressaSection["AssemblyAI:APIURL"];
            _cohereKey = compressaSection["Cohere:APIKey"];
            _cohereUrl = compressaSection["Cohere:APIURL"];
        }

        public Dictionary<string, string> GetAllAudiobooks()
        {
            _audiobooks.Clear();

            foreach (string filename in Directory.GetFiles(_mediaFolder, "*.aax", SearchOption.AllDirectories))
            {
                _audiobooks.Add(Path.GetFileNameWithoutExtension(filename), filename);
                _logger.LogInformation($"Found audiobook file at '{filename}'.");
            }

            return _audiobooks;
        }

        public void ConvertAudiobookToM4B(string audiobookName)
        {
            _audiobooks.TryGetValue(audiobookName, out string filename);

            if ((String.IsNullOrEmpty(filename)) || (!File.Exists(filename)))
            {
                throw new Exception($"Audiobook file '{audiobookName}' was not found in the database.");
            }
            string m4bFilename = ChangeExtension(filename, ".m4b");

            FFMpegArguments
                .FromFileInput(filename)
                .OutputToFile(m4bFilename, false, options => options
                    .WithAudibleActivationBytes(_kindleActivationBytes))
                .NotifyOnOutput((data) => _logger.LogInformation(data))
                .NotifyOnError((data) => _logger.LogError(data))
                .ProcessSynchronously();

            //string ffmpegParameters = $"-y -activation_bytes {_kindleActivationBytes} -i {filename} -codec copy {m4bFilename}";

            //var ffmpegConversion = FFmpeg.Conversions.New();
            //ffmpegConversion.OnDataReceived += FfmpegConversion_OnDataReceived;

            //try
            //{
            //    var ffmpegConversionResult = ffmpegConversion.Start(ffmpegParameters).ContinueWith((cr) =>
            //    {
            //        _logger.LogInformation($"Conversion of '{audiobookName}' was successful.");
            //    });
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Conversion of '{audiobookName}' failed with error: {ex.Message}");
            //}
        }

        private static string ChangeExtension(string filename, string extension)
        {
            return Path.Combine(Path.GetDirectoryName(Path.GetFullPath(filename)), Path.GetFileNameWithoutExtension(filename) + extension);
        }

        private void FfmpegConversion_OnDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            _logger.LogInformation($"FFMPEG {e.Data}");
        }
        public Chapter[] ExtractChapterMetadata(string audiobookName)
        {
            _audiobooks.TryGetValue(audiobookName, out string filename);

            filename = ChangeExtension(filename, "_chapters.json");

            if ((String.IsNullOrEmpty(filename)) || (!File.Exists(filename)))
            {
                throw new Exception($"Audiobook chapters metadata file for '{audiobookName}' was not found.");
            }

            var chapters = JsonSerializer.Deserialize<ChaptersMetadata>(File.ReadAllText(filename));

            foreach (Chapter chapter in chapters.Chapters)
            {
                _logger.LogInformation($"Chapter {chapter.StartTime} - {chapter.EndTime} - {chapter.Tags.Title}");
            }

            return chapters.Chapters;
        }

        public void SaveChaptersAsMP3s(string audiobookName)
        {
            _audiobooks.TryGetValue(audiobookName, out string inputFilename);
            inputFilename = ChangeExtension(inputFilename, $".m4b");

            if ((String.IsNullOrEmpty(inputFilename)) || (!File.Exists(inputFilename)))
            {
                throw new Exception($"Audiobook file '{audiobookName}' was not found in the database.");
            }

            int chapterIndex = 0;
            foreach (Chapter chapter in ExtractChapterMetadata(audiobookName))
            {
                string chapterFilename = ChangeExtension(inputFilename, $"_ch{++chapterIndex:00}.mp3");

                TimeSpan startTime = TimeSpan.FromSeconds(Single.Parse(chapter.StartTime, CultureInfo.InvariantCulture));
                TimeSpan endTime = TimeSpan.FromSeconds(Single.Parse(chapter.EndTime, CultureInfo.InvariantCulture));

                TimeSpan duration = endTime - startTime;

                string ffmpegParameters = $"-i \"{inputFilename}\" -ss {startTime} -to {endTime} \"{chapterFilename}\"";

                var ffmpegConversion = FFmpeg.Conversions.New();
                //ffmpegConversion.OnDataReceived += FfmpegConversion_OnDataReceived;
                ffmpegConversion.OnProgress += FfmpegConversion_OnProgress;

                try
                {
                    _logger.LogInformation($"FFMPEG Running 'ffmpeg {ffmpegParameters}'...");
                    ffmpegConversion.Start(ffmpegParameters).Wait();
                    _logger.LogInformation($"Generated '{chapterFilename}' successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Generation of '{chapterFilename}' failed with error: {ex.Message}");
                }
            }
        }

        private void FfmpegConversion_OnProgress(object sender, Xabe.FFmpeg.Events.ConversionProgressEventArgs args)
        {
            _logger.LogInformation($"FFMPEG {args.Percent}");
        }

        public async Task<TranscriptionResponse> TranscribeChapter(string audiobookName, int chapterIndex)
        {
            _audiobooks.TryGetValue(audiobookName, out string uploadFilename);
            uploadFilename = ChangeExtension(uploadFilename, $"_ch{chapterIndex:00}.mp3");
            string jsonFilename = ChangeExtension(uploadFilename, $".json");

            if (File.Exists(jsonFilename))
            {
                return JsonSerializer.Deserialize<TranscriptionResponse>(File.ReadAllText(jsonFilename));
            }

            if ((String.IsNullOrEmpty(uploadFilename)) || (!File.Exists(uploadFilename)))
            {
                throw new Exception($"Chapter {chapterIndex} for the file '{audiobookName}' was not found.");
            }

            var client = new AssemblyAIApiClient(_assemblyAIKey, _assemblyAIUrl);

            // Upload file
            _logger.LogInformation($"Uploading '{uploadFilename}' to AssemblyAI.");
            var uploadResult = client.UploadFileAsync(uploadFilename).GetAwaiter().GetResult();

            TranscriptionResponse result = GetSummary(client, uploadResult, "informative", "bullets");

            File.WriteAllText(jsonFilename, JsonSerializer.Serialize<TranscriptionResponse>(result, new JsonSerializerOptions() { WriteIndented = true }));

            return result;
        }

        private TranscriptionResponse GetSummary(AssemblyAIApiClient client, UploadAudioResponse uploadResult, string summaryModel, string summaryType)
        {
            // Submit file for transcription
            var submissionResult = client.SubmitAudioFileAsync(uploadResult.UploadUrl, true, summaryModel, summaryType).GetAwaiter().GetResult();
            _logger.LogInformation($"File {submissionResult.Id} in status {submissionResult.Status}");

            // Query status of transcription until it's `completed`
            TranscriptionResponse result = client.GetTranscriptionAsync(submissionResult.Id).GetAwaiter().GetResult();
            while (!result.Status.Equals("completed"))
            {
                _logger.LogInformation($"File {result.Id} in status {result.Status}");
                Thread.Sleep(15000);
                result = client.GetTranscriptionAsync(submissionResult.Id).GetAwaiter().GetResult();
            }

            return result;
        }

        public async Task<GenerateResponse> SummarizeChapter(string audiobookName, int chapterIndex)
        {
            _audiobooks.TryGetValue(audiobookName, out string transcriptFilename);
            transcriptFilename = ChangeExtension(transcriptFilename, $"_ch{chapterIndex:00}.json");
            var summaryFilename = ChangeExtension(transcriptFilename, $"_ch{chapterIndex:00}_summary.json");

            if ((String.IsNullOrEmpty(transcriptFilename)) || (!File.Exists(transcriptFilename)))
            {
                throw new Exception($"The JSON file for chapter {chapterIndex} of '{audiobookName}' was not found.");
            }

            var transcriptionResponse = JsonSerializer.Deserialize<TranscriptionResponse>(File.ReadAllText(transcriptFilename));

            int maxTokens = 40;
            float temperature = 0.6f;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.cohere.ai/generate"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Cohere-Version", "2021-11-08" },
                    { "authorization", $"Bearer {_cohereKey}" },
                },
                Content = new StringContent("{\"model\":\"xlarge\",\"prompt\":\"" + transcriptionResponse.Text + "\",\"max_tokens\":"+maxTokens+",\"temperature\":" + temperature.ToString("0.0", CultureInfo.InvariantCulture) + ",\"k\":0,\"p\":0.75}")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var generateResponse = JsonSerializer.Deserialize<GenerateResponse>(responseBody);

                File.WriteAllText(summaryFilename, JsonSerializer.Serialize<GenerateResponse>(generateResponse, new JsonSerializerOptions() { WriteIndented = true }));

                return generateResponse;
            }
        }
    }
}
