using Newtonsoft.Json;
using Notino.Converter.Models;

namespace Notino.Converter.Core
{
    public class JsonSerializer<T> : ISerializer<T>
    {
        public async Task<Stream> SerializeData(Stream stream, T data)
        {
            using (var writer = new StreamWriter(stream, leaveOpen: true))
                await writer.WriteAsync(JsonConvert.SerializeObject(data));

            return stream;
        }
    }
}
