using Notino.Converter.Core;

namespace Notino.Converter.Helpers
{
    public interface ISerializerFactory<T>
    {
        ISerializer<T> GetSerializer(string format);
    }
}
