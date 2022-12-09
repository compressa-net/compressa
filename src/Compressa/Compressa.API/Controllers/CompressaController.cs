using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Xabe.FFmpeg;

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
        private readonly Dictionary<string, string> _audiobooks = new Dictionary<string, string>();

        public CompressaController(ILogger<CompressaController> logger, IConfiguration configRoot)
        {
            _logger = logger;
            _config = configRoot;

            _mediaFolder = _config.GetSection("Compressa")["MediaFolder"] ?? ".\\Media";
            _kindleActivationBytes = _config.GetSection("Compressa")["Kindle:ActivationBytes"];
        }

        [HttpGet]
        [Route("getallaudiobooks")]
        public int GetAllAudiobooks()
        {
            int taskId = Task.Run(() =>
            {
                _audiobooks.Clear();
                foreach (string filename in Directory.GetFiles(_mediaFolder, "*.aax", SearchOption.AllDirectories))
                {
                    _audiobooks.Add(Path.GetFileNameWithoutExtension(filename), filename);
                    _logger.LogInformation($"Found audiobook file at '{filename}'.");
                }
            }).Id;

            return taskId;
        }

        [HttpGet]
        [Route("convertaudiobooktom4b/{audiobookName}")]
        public int ConvertAudiobookToM4B(string audiobookName)
        {
            int taskId = Task.Run(() =>
            {
                _audiobooks.TryGetValue(audiobookName, out string filename);

                if (String.IsNullOrEmpty(filename))
                {
                    throw new Exception($"Audiobook file '{audiobookName}' was not found in the database.");
                }

                string m4bFilename = Path.GetFileNameWithoutExtension(filename) + ".m4b";

                string ffmpegParameters = $"-y -activation_bytes {_kindleActivationBytes} -i {filename} -codec copy {m4bFilename}";

                var fffmpegConversion = FFmpeg.Conversions.New().Start(ffmpegParameters);
            }).Id;

            return taskId;
        }

        [HttpGet]
        [Route("extractchaptermetadata/{audiobookName}")]
        public int ExtractChapterMetadata(string audiobookName)
        {
            return 0;
        }

    }
}