using System.Text.Json.Serialization;

namespace Compressa.API.Models.AssemblyAI
{
    // ./Models/TranscriptionRequest.cs
    public class TranscriptionRequest
    {
        [JsonPropertyName("audio_url")]
        public string AudioUrl { get; set; }
    }
}
