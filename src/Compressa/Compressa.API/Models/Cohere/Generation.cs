using System.Text.Json.Serialization;

namespace Compressa.API.Models.Cohere
{
    public class Generation
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}