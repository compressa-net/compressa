using System.Text.Json.Serialization;

namespace Compressa.API.Models.Cohere
{
    public class SentimentResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("classifications")]
        public Classification[] Classifications { get; set; }
    }
}
