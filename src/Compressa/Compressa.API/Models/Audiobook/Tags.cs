using System.Text.Json.Serialization;

namespace Compressa.API.Models.Audiobook
{
    public class Tags
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}