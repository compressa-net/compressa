using System.Text.Json.Serialization;

namespace Compressa.API.Models.Cohere
{
    public class Confidence
    {
        [JsonPropertyName("option")]
        public string Option { get; set; }

        [JsonPropertyName("confidence")]
        public double Value { get; set; }
    }
}