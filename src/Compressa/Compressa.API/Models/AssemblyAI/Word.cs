using System.Text.Json.Serialization;

namespace Compressa.API.Models.AssemblyAI
{
    // ./Models/Word.cs
    public class Word
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("end")]
        public int End { get; set; }

        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
