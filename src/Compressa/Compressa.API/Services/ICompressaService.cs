using Compressa.API.Models.Audiobook;

namespace Compressa.API.Services
{
    public interface ICompressaService
    {
        Dictionary<string, string> GetAllAudiobooks();

        void ConvertAudiobookToM4B(string audiobookName);

        Chapter[] ExtractChapterMetadata(string audiobookName);

        void SaveChaptersAsMP3s(string audiobookName);
    }
}