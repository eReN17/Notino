using Microsoft.AspNetCore.Mvc;
using Notino.Converter;
using Notino.Converter.Core;
using Notino.Converter.Exceptions;
using Notino.Homework.Api.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Notino.Homework.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotinoController : ControllerBase
    {
        private readonly ILogger<NotinoController> _logger;

        public NotinoController(ILogger<NotinoController> logger)
        {
            _logger = logger; 
        }

        [HttpPost()]
        public async Task<IActionResult> ConvertFile([Required] IFormFile file, string targetFormat)
        {
            try
            {
                // throws FileFormatNotSupportedException in case of unknown/unsupported source file format
                var deserializer = FileConverter.GetDeserializer(System.IO.Path.GetExtension(file.FileName));
                var serializer = FileConverter.GetSerializer(targetFormat);

                using var sourceStream = file.OpenReadStream();
                using var targetStream = new MemoryStream();

                var data = await deserializer.DeserializeData(sourceStream);

                await serializer.SerializeData(targetStream, data);

                return new FileContentResult(await targetStream.ReadAsByteArrayAsync(), ConverterHelpers.GetMime(targetFormat));
            }
            catch (FileFormatNotSupportedException ffse)
            {
                return BadRequest(ffse.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
