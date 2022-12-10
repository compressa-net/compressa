using System.Text.Json.Serialization;

namespace Compressa.API.Models.AssemblyAI
{
    // ./Models/TranscriptionResponse.cs
    public class TranscriptionResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("acoustic_model")]
        public string AcousticModel { get; set; }

        [JsonPropertyName("audio_duration")]
        public double? AudioDuration { get; set; }

        [JsonPropertyName("audio_url")]
        public string AudioUrl { get; set; }

        [JsonPropertyName("confidence")]
        public double? Confidence { get; set; }

        [JsonPropertyName("dual_channel")]
        public string DualChannel { get; set; }

        [JsonPropertyName("format_text")]
        public bool FormatText { get; set; }

        [JsonPropertyName("language_model")]
        public string LanguageModel { get; set; }

        [JsonPropertyName("punctuate")]
        public bool Punctuate { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("utterances")]
        public string Utterances { get; set; }

        [JsonPropertyName("webhook_status_code")]
        public string WebhookStatusCode { get; set; }

        [JsonPropertyName("webhook_url")]
        public string WebhookUrl { get; set; }

        [JsonPropertyName("words")]
        public List<Word> Words { get; set; }

        [JsonPropertyName("auto_chapters")]
        public bool AutoChapters { get; set; }
        [JsonPropertyName("summarization")]
        public bool Summarization { get; set; }
        [JsonPropertyName("summary_type")]
        public string SummaryType { get; set; }
        [JsonPropertyName("summary_model")]
        public string SummaryModel { get; set; }
        [JsonPropertyName("summary")]
        public string Summary { get; set; }

    }
}
