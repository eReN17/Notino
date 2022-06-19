using Notino.Converter.Core;
using Notino.Converter.Exceptions;

namespace Notino.Converter.Helpers
{
    public class DeserializerFactory<T> : IDeserializerFactory<T>
    {
        public IDeserializer<T> GetDeserializer(string format)
        {
            if (format is null)
                throw new ArgumentNullException("format can't be null here");

            switch (format.ToLower())
            {
                case ".xml":
                    return new XmlDeserializer<T>();
                default:
                    throw new FileFormatNotSupportedException($"File format {format} is not supported for source stream.");
            }
        }
    }
}
