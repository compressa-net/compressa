using System.Text.Json.Serialization;

namespace Compressa.API.Models.Cohere
{
    public class GenerateResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("generations")]
        public Generation[] Generations { get; set; }

        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }
    }
}
