using System.Text.Json.Serialization;

namespace Compressa.API.Models.FFMPEG
{
    public class Tags
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}