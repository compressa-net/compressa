using System.Text.Json.Serialization;

namespace Compressa.API.Models.AssemblyAI
{
    // ./Models/UploadAudioResponse.cs
    public class UploadAudioResponse
    {
        [JsonPropertyName("upload_url")]
        public string UploadUrl { get; set; }
    }
}
