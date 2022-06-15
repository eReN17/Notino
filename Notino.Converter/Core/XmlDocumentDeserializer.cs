using Notino.Converter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Notino.Converter.Core
{
    public class XmlDocumentDeserializer : IDeserializer<Document>
    {
        public async Task<Document> DeserializeData(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                var data = await streamReader.ReadToEndAsync();
                var parsedData = XDocument.Parse(data);
                return new Document()
                {
                    Title = parsedData.Root.Element("title").Value,
                    Text = parsedData.Root.Element("text").Value
                };
            }
        }
    }
}
