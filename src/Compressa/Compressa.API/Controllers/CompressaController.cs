using Compressa.Models.Metadata;
using Compressa.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using FFMpegCore.Builders.MetaData;
using System.Text.Json;

namespace Compressa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompressaController : ControllerBase
    {
        private readonly ILogger<CompressaController> _logger;
        private readonly IConfiguration _config;
        private readonly string _mediaFolder;
        private readonly string? _kindleActivationBytes;
        private readonly ICompressaService _compressaService;
        private readonly Dictionary<string, string> _audiobooks = new Dictionary<string, string>();

        public CompressaController(ILogger<CompressaController> logger, IConfiguration configRoot, ICompressaService compressaService)
        {
            _logger = logger;
            _config = configRoot;

            _mediaFolder = _config.GetSection("Compressa")["MediaFolder"] ?? ".\\Media";
            _kindleActivationBytes = _config.GetSection("Compressa")["Kindle:ActivationBytes"];
            _compressaService = compressaService;
            _compressaService.GetAllAudiobooks();
        }

        [HttpGet]
        [Route("getallaudiobooks")]
        public int GetAllAudiobooks()
        {
            int taskId = Task.Run(() =>
            {
                _compressaService.GetAllAudiobooks();
            }).Id;

            return taskId;
        }

        [HttpGet]
        [Route("convertaudiobooktom4b/{audiobookName}")]
        public int ConvertAudiobookToM4B(string audiobookName)
        {
            int taskId = Task.Run(() =>
            {
                _compressaService.ConvertAudiobookToM4B(audiobookName);
            }).Id;

            return taskId;
        }

        [HttpGet]
        [Route("extractchaptermetadata/{audiobookName}")]
        public int ExtractChapterMetadata(string audiobookName)
        {
            int taskId = Task.Run(() =>
            {
                _compressaService.ExtractChapterMetadata(audiobookName);
            }).Id;

            return taskId;
        }

        [HttpGet]
        [Route("savechaptersasmp3s/{audiobookName}")]
        public int SaveChaptersAsMP3s(string audiobookName)
        {
            int taskId = Task.Run(() =>
            {
                _compressaService.SaveChaptersAsMP3s(audiobookName);
            }).Id;

            return taskId;
        }

        [HttpGet]
        [Route("transcribechapter/{audiobookName}/{chapterIndex}")]
        public int TranscribeChapter(string audiobookName, int chapterIndex)
        {
            int taskId = Task.Run(() =>
            {
                _compressaService.TranscribeChapter(audiobookName, chapterIndex);
            }).Id;

            return taskId;
        }

        [HttpGet]
        [Route("transcribeaudiobook/{audiobookName}")]
        public int TranscribeAudiobook(string audiobookName)
        {
            int taskId = Task.Run(() =>
            {
                _compressaService.TranscribeAudiobook(audiobookName);
            }).Id;

            return taskId;
        }

        [HttpGet]
        [Route("summarizechapter/{audiobookName}/{chapterIndex}")]
        public int SummarizeChapter(string audiobookName, int chapterIndex)
        {
            int taskId = Task.Run(() =>
            {
                _compressaService.SummarizeChapter(audiobookName, chapterIndex);
            }).Id;

            return taskId;
        }

        [HttpGet]
        [Route("getallmetadata")]
        public MetadataRoot[] GetAllMetadata(bool includeSummaries)
        {
            var metadataCache = _compressaService.GetAllMetadata(includeSummaries);

            //System.IO.File.WriteAllText("getallmetadata.json", JsonSerializer.Serialize<MetadataRoot[]>(metadataCache, new JsonSerializerOptions() { WriteIndented = true }));

            return metadataCache;
        }
        
        [HttpGet]
        [Route("getsentiment/{audiobookName}/{chapterIndex}")]
        public int GetSentiment(string audiobookName, int chapterIndex)
        {
            int taskId = Task.Run(() =>
            {
                _compressaService.GetSentiment(audiobookName, chapterIndex);
            }).Id;

            return taskId;
        }
    }
}