using System.Text.Json.Serialization;

namespace Compressa.API.Models.FFMPEG
{
    public class Chapter
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("time_base")]
        public string TimeBase { get; set; }
        [JsonPropertyName("start")]
        public int Start { get; set; }
        [JsonPropertyName("start_time")]
        public string StartTime { get; set; }
        [JsonPropertyName("end")]
        public int End { get; set; }
        [JsonPropertyName("end_time")]
        public string EndTime { get; set; }
        [JsonPropertyName("tags")]
        public Tags Tags { get; set; }

    }
}