using Compressa.API.Models.AssemblyAI;
using Compressa.API.Models.Cohere;
using Compressa.API.Models.FFMPEG;
using Compressa.Models.Metadata;

namespace Compressa.API.Services
{
    public interface ICompressaService
    {
        Dictionary<string, string> GetAllAudiobooks();
        void ConvertAudiobookToM4B(string audiobookName);
        Chapter[] ExtractChapterMetadata(string audiobookName);
        void SaveChaptersAsMP3s(string audiobookName);
        Task<AudiobookChapter> TranscribeChapter(string audiobookName, int chapterIndex);
        Task<GenerateResponse> SummarizeChapter(string audiobookName, int chapterIndex);
        Audiobook TranscribeAudiobook(string audiobookName);
        MetadataRoot[] GetAllMetadata(bool includeSummaries);
        Task<Segment[]> GetSentiment(string audiobookName, int chapterIndex);
    }
}