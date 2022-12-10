using System.Text.Json.Serialization;

namespace Compressa.Models.Metadata
{
    public class Audiobook
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Author { get; set; }
        public string NarratedBy { get; set; }
        public string Summary { get; set; }
        public string CoverBase64 { get; set; }
        public StoreLink[] StoreLinks { get; set; }
        public AudiobookChapter[] Chapters { get; set; }
        public override string ToString()
        {
            return $"{this.Title} by {this.Author}";
        }

    }
}