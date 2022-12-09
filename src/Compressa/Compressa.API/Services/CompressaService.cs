using System.Globalization;
using System.Text.Json;
using Compressa.API.Models.Audiobook;
using FFMpegCore;
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
        private readonly Dictionary<string, string> _audiobooks = new Dictionary<string, string>();

        public CompressaService(ILogger<CompressaService> logger, IConfiguration configRoot)
        {
            _logger = logger;
            _config = configRoot;

            _mediaFolder = _config.GetSection("Compressa")["MediaFolder"] ?? ".\\Media";
            _kindleActivationBytes = _config.GetSection("Compressa")["Kindle:ActivationBytes"];
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

            if (String.IsNullOrEmpty(filename))
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

            if (String.IsNullOrEmpty(filename))
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

            if (String.IsNullOrEmpty(inputFilename))
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

                //FFMpegArguments
                //    .FromFileInput(inputFilename)
                //    .OutputToFile(chapterFilename, false, options => options
                //    .WithStartNumber(chapter.Start)
                //    .WithDuration(duration))
                //.NotifyOnOutput((data) => _logger.LogInformation(data))
                //.NotifyOnError((data) => _logger.LogError(data))
                //.ProcessSynchronously();

                string ffmpegParameters = $"-i \"{inputFilename}\" -ss {startTime} -to {endTime} \"{chapterFilename}\"";

                var ffmpegConversion = FFmpeg.Conversions.New();
                //ffmpegConversion.OnDataReceived += FfmpegConversion_OnDataReceived;
                //ffmpegConversion.OnProgress += FfmpegConversion_OnProgress;

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
    }
}
