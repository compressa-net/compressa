using Compressa.Models.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Compressa.GUI.Services
{
    public class CompressaClientService : ICompressaClientService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        private IConfiguration _config;
        string _compressaUrl;

        public MetadataRoot Audiobooks { get; private set; }

        public CompressaClientService(ILogger<CompressaClientService> logger, IConfiguration configRoot)
        {            
            _config = configRoot;
            var compressaSection = _config.GetSection("Compressa");
            _compressaUrl = compressaSection["APIURL"];

            _client = new HttpClient() { BaseAddress = new Uri(_compressaUrl) };
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<MetadataRoot[]> GetAllMetadataAsync()
        {
            var result = new MetadataRoot[0];

            Uri uri = new Uri(string.Format("getallmetadata", string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<MetadataRoot[]>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return result;
        }
    }
}
