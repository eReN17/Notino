using Notino.Converter.Core;
using Notino.Converter.Exceptions;

namespace Notino.Converter.Helpers
{
    public class SerializerFactory<T> : ISerializerFactory<T>
    {
        public ISerializer<T> GetSerializer(string format)
        {
            if (format is null)
                throw new ArgumentNullException("format can't be null here");

            switch (format.ToLower())
            {
                case ".json":
                    return new JsonSerializer<T>();
                default:
                    throw new FileFormatNotSupportedException($"File format {format} is not supported for target stream.");
            }
        }
    }
}
