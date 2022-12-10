namespace Compressa.Models.Metadata
{
    public class Segment
    {
        public int Index { get; set; }
        public string ChatGPTPrompt { get; set; }
        public string ChatGPTResponse { get; set; }
        public override string ToString()
        {
            return $"{this.Index.ToString("00")}: {this.ChatGPTResponse?.Count(c => c == ' ')} words";
        }

    }
}