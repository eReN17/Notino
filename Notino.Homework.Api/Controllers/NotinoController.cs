using Microsoft.AspNetCore.Mvc;
using Notino.Converter.Helpers;
using Notino.Converter.Models;
using Notino.Homework.Api.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Notino.Homework.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotinoController : ControllerBase
    {
        private readonly ILogger<NotinoController> _logger;
        private readonly ISimpleMailManager _mailManager;

        private readonly ISerializerFactory<Document> _documentSerializerFactory;
        private readonly IDeserializerFactory<Document> _documentDeserializerFactory;

        public NotinoController(ILogger<NotinoController> logger,
                                ISimpleMailManager mailManager,
                                ISerializerFactory<Document> serializerFactory,
                                IDeserializerFactory<Document> deserializerFactory)
        {
            _logger = logger;
            _mailManager = mailManager;

            _documentSerializerFactory = serializerFactory;
            _documentDeserializerFactory = deserializerFactory;
        }

        [HttpPost("Convert/Download/")]
        public async Task<IActionResult> ConvertFile([Required] IFormFile file, [Required] string targetFormat = ".json")
        {
            //konvertovat mezi formáty XML a JSON
            //odeslat zdrojový a stáhnout výsledný soubor z API

            _logger.LogInformation($"Converting {file.FileName} to {targetFormat}");

            using var targetStream = new MemoryStream();
            await ConvertSourceToStream(file, targetStream, targetFormat);

            return new FileContentResult(targetStream.ToArray(), ConverterHelpers.GetMime(targetFormat));
        }

        [HttpPost("Convert/Copy/")]
        public async Task<IActionResult> CopyFile([Required] IFormFile file, [Required] string targetFile)
        {
            //načíst a uložit data z/do libovolné cesty na disku (případně cloud-storage)

            _logger.LogInformation($"Converting source file {file.FileName} to {targetFile}");

            using var targetStream = new MemoryStream();
            await ConvertSourceToStream(file, targetStream, System.IO.Path.GetExtension(targetFile));

            using var fileStream = System.IO.File.Create(targetFile);
            targetStream.CopyTo(fileStream);

            return Ok($"File successfully converted and copied to {targetFile}");
        }

        [HttpPost("Convert/Send/")]
        public async Task<IActionResult> SendFile([Required] IFormFile file, [Required] string recipient, [Required] string fileName = "attachment.json")
        {
            //odeslat výsledný soubor e-mailem(stačí pouze nástřel)

            _logger.LogInformation($"Converting {file.FileName} to {System.IO.Path.GetExtension(fileName)} and sending it to {recipient}");
            
            using var targetStream = new MemoryStream();
            await ConvertSourceToStream(file, targetStream, System.IO.Path.GetExtension(fileName));

            await _mailManager.SendFile(recipient, fileName, targetStream);

            return Ok($"File {file.FileName} successfully converted to {System.IO.Path.GetExtension(fileName)} and sent as an email with attachment to the {recipient}");
        }

        private async Task ConvertSourceToStream(IFormFile sourceFile, Stream targetStream, string targetFormat)
        {
            using var sourceStream = sourceFile.OpenReadStream();

            var deserializer = _documentDeserializerFactory.GetDeserializer(Path.GetExtension(sourceFile.FileName).ToLower());
            var serializer = _documentSerializerFactory.GetSerializer(targetFormat);

            var data = await deserializer.DeserializeData(sourceStream);
            await serializer.SerializeData(targetStream, data);
        }
    }
}
