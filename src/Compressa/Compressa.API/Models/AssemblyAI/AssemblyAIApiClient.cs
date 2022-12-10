using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Compressa.API.Models.AssemblyAI
{
    public class AssemblyAIApiClient
    {
        private readonly string _apiToken;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public AssemblyAIApiClient(string apiToken, string baseUrl)
        {
            _apiToken = apiToken;
            _baseUrl = baseUrl;
            _httpClient = new HttpClient() { BaseAddress = new Uri(_baseUrl) };
            _httpClient.DefaultRequestHeaders.Add("Authorization", _apiToken);
        }

        /// <summary>
        /// Helper method that sends the <see cref="HttpRequestMessage"/> using the configured <see cref="HttpClient"/>
        /// </summary>
        /// <typeparam name="TModel">The type to deserialized the response to.</typeparam>
        /// <param name="request">The http request message</param>
        /// <returns>The deserialized response object</returns>
        private async Task<TModel> SendRequestAsync<TModel>(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TModel>(json);
        }

        /// <summary>
        /// Uploads a local audio file to the API
        /// </summary>
        /// <param name="filePath">The file path of the audio file</param>
        /// <returns>A <see cref="Task{UploadAudioResponse}"/></returns>
        public Task<UploadAudioResponse> UploadFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            var request = new HttpRequestMessage(HttpMethod.Post, "v2/upload");
            request.Headers.Add("Transer-Encoding", "chunked");

            var fileReader = File.OpenRead(filePath);
            request.Content = new StreamContent(fileReader);

            return SendRequestAsync<UploadAudioResponse>(request);
        }

        /// <summary>
        /// Submits an audio file at the specified URL for transcription
        /// </summary>
        /// <param name="audioUrl">The URL where the file is hosted</param>
        /// <returns>A <see cref="Task{TranscriptionResponse}"/></returns>
        public async Task<TranscriptionResponse> SubmitAudioFileAsync(string audioUrl, bool summarization = false, string summaryModel = "catchy", string summaryType = "headline")
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", _apiToken);

                var json = new
                {
                    audio_url = audioUrl,
                    summarization = true,
                    summary_model = "informative",
                    summary_type = "bullets"
                };

                StringContent payload = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("https://api.assemblyai.com/v2/transcript", payload);
                response.EnsureSuccessStatusCode();

                var responseJson = response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<TranscriptionResponse>(responseJson.Result);
            }

            //var request = new HttpRequestMessage(HttpMethod.Post, "v2/transcript");
            //var requestBody = JsonSerializer.Serialize(new TranscriptionRequest { AudioUrl = audioUrl, Summarization = summarization, SummaryModel = summaryModel, SummaryType = summaryType });
            //request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            //requestBody = requestBody.Replace("true", "True");

            //return SendRequestAsync<TranscriptionResponse>(request);
        }

        /// <summary>
        /// Retrieves the transcription
        /// </summary>
        /// <param name="id">The id of the transcription</param>
        /// <returns>A <see cref="Task{TranscriptionResponse}"/></returns>
        public Task<TranscriptionResponse> GetTranscriptionAsync(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"v2/transcript/{id}");

            return SendRequestAsync<TranscriptionResponse>(request);
        }
    }
}
