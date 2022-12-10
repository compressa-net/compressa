using System.Text.Json.Serialization;

namespace Compressa.API.Models.AssemblyAI
{
    // ./Models/TranscriptionRequest.cs
    public class TranscriptionRequest
    {
        [JsonPropertyName("audio_url")]
        public string AudioUrl { get; set; }
        [JsonPropertyName("summarization")]
        public bool Summarization { get; set; }
        [JsonPropertyName("summary_model")]
        public string SummaryModel { get; set; }
        [JsonPropertyName("summary_type")]
        public string SummaryType { get; set; }
    }
}
