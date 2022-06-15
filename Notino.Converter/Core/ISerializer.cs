namespace Notino.Converter.Core
{
    public interface ISerializer<T>
    {
        Task<Stream> SerializeData(Stream stream, T data);
    }
}
