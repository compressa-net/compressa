using System.Text.Json.Serialization;

namespace Compressa.API.Models.Cohere
{
    public class Classification
    {
        [JsonPropertyName("input")]
        public string Input { get; set; }

        [JsonPropertyName("prediction")]
        public string Prediction { get; set; }

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("confidences")]
        public Confidence[] Confidences { get; set; }
    }
}