namespace Notino.Converter.Core
{
    public interface IDeserializer<T>
    {
        Task<T> DeserializeData(Stream stream);
    }
}
