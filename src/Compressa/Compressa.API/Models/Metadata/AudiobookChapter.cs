using System.Text.Json.Serialization;

namespace Compressa.API.Models.Metadata
{
    public class AudiobookChapter
    {
        public int Index { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Title { get; set; }
        public string GistSummary { get; set; }
        public string BulletsSummary { get; set; }
        public string BulletsVerboseSummary { get; set; }
        public string ParagraphSummary { get; set; }
        public string Transcript { get; set; }
        public Segment[] Segments { get; set; }
        public Word[] Words { get; set; }
        public void GenerateSegmentsAndPrompts()
        {
            string[] sentences = Transcript.Split(". ");
            sentences = sentences.Select(s => s + ".").ToArray();

            int segmentCount = (sentences.Length / 50) + 1;
            int sentencesPerSegment = sentences.Length / segmentCount;

            Segments = new Segment[segmentCount];
            for (int i = 0; i < segmentCount; i++)
            {
                Segments[i] = new Segment();
                Segments[i].Index = i;
                Segments[i].ChatGPTPrompt = $"Summarize the following text with approximately {(sentencesPerSegment / 4) + 1} sentences: " + string.Join(" ", sentences.Skip(i * sentencesPerSegment).Take(sentencesPerSegment));
            }
        }
    }
}