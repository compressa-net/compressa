using System.Text.Json.Serialization;

namespace Compressa.API.Models.Metadata
{
    public class Word
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string Text { get; set; }
        public double Confidence { get; set; }
        public static Word FromAssemblyAI(AssemblyAI.Word w)
        {
            Word result = new Word()
            {
                Start = w.Start,
                End = w.End,
                Text = w.Text,
                Confidence = w.Confidence
            };

            return result;
        }
    }
}