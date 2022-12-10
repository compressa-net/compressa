using System.Text.Json.Serialization;

namespace Compressa.Models.Metadata
{
    public class Word
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string Text { get; set; }
        public double Confidence { get; set; }
        public static Word FromAssemblyAI(int start, int end, string text, double confidence)
        {
            return new Word
            {
                Start = start,
                End = end,
                Text = text,
                Confidence = confidence
            };
        }        
    }
}