using System.Text.Json.Serialization;

namespace Compressa.API.Models.Audiobook
{
    public class ChaptersMetadata
    {
        [JsonPropertyName("chapters")]
        public Chapter[] Chapters { get; set; }
    }
}