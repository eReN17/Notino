using Notino.Converter.Core;
using Notino.Converter.Exceptions;
using Notino.Converter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notino.Converter.Helpers
{
    internal static class ConverterUtilities
    {
        public static IDeserializer<Document> GetDeserializer(string extension)
        {
            switch (extension.ToLower())
            {
                case ".xml":
                    return new XmlDocumentDeserializer();
                default:
                    throw new FileFormatNotSupportedException($"File format {extension} is not supported for source stream.");
            }
        }

        public static ISerializer<Document> GetSerializer(string extension)
        {
            switch (extension.ToLower())
            {
                case ".json":
                    return new JsonDocumentSerializer();
                default:
                    throw new FileFormatNotSupportedException($"File format {extension} is not supported for target stream.");
            }
        }
    }
}
