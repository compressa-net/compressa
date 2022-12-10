﻿using Compressa.API.Models.AssemblyAI;
using Compressa.API.Models.Audiobook;
using Compressa.API.Models.Cohere;

namespace Compressa.API.Services
{
    public interface ICompressaService
    {
        Dictionary<string, string> GetAllAudiobooks();
        void ConvertAudiobookToM4B(string audiobookName);
        Chapter[] ExtractChapterMetadata(string audiobookName);
        void SaveChaptersAsMP3s(string audiobookName);
        Task<TranscriptionResponse> TranscribeChapter(string audiobookName, int chapterIndex);
        Task<GenerateResponse> SummarizeChapter(string audiobookName, int chapterIndex);
    }
}