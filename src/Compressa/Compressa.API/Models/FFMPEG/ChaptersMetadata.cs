using System.Text.Json.Serialization;

namespace Compressa.API.Models.FFMPEG
{
    public class ChaptersMetadata
    {
        [JsonPropertyName("chapters")]
        public Chapter[] Chapters { get; set; }
    }
}