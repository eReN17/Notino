using Notino.Converter.Core;
using Notino.Converter.Exceptions;
using Notino.Converter.Models;

namespace Notino.Converter.Helpers
{
    public interface IDeserializerFactory<T>
    {
        IDeserializer<T> GetDeserializer(string format);
    }
}
