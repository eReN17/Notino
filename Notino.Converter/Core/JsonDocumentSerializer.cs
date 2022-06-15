using Newtonsoft.Json;
using Notino.Converter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notino.Converter.Core
{
    public class JsonDocumentSerializer : ISerializer<Document>
    {
        public async Task<Stream> SerializeData(Stream stream, Document data)
        {
            using (var writer = new StreamWriter(stream))
                await writer.WriteAsync(JsonConvert.SerializeObject(data));

            return stream;
        }
    }
}
