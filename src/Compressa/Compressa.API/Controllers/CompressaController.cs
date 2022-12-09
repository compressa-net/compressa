using Compressa.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

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
    }
}