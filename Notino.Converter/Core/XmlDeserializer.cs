using System.Xml.Serialization;

namespace Notino.Converter.Core
{
    public class XmlDeserializer<T> : IDeserializer<T>
    {
        public Task<T> DeserializeData(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            return Task.FromResult((T)serializer.Deserialize(stream));
        }
    }
}
