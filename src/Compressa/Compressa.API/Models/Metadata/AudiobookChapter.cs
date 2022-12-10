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
        public Word[] Words { get; set; }
    }
}