//https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types
using Notino.Homework.Api.Controllers;

namespace Notino.Homework.Api.Helpers
{
    public class ConverterHelpers
    {
        public static string GetMime(string format)
        {
            switch (format)
            {
                case ".xml":
                    return "application/xml";
                case ".json":
                    return "application/json";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
