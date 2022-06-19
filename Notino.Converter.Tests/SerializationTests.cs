using Notino.Converter.Core;
using Notino.Converter.Models;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace Notino.Converter.Tests
{
    public class SerializationTests
    {
        private ISerializer<Document> _jsonDocumentSerializer;

        [SetUp]
        public void Setup()
        {
            _jsonDocumentSerializer = new JsonSerializer<Document>();
        }

        [Test]
        public void JsonSerializationTest()
        {
            var document = new Document()
            {
                Title = "title",
                Text = "text"
            };

            using (var memoryStream = new MemoryStream())
            {
                _jsonDocumentSerializer.SerializeData(memoryStream, document).GetAwaiter().GetResult();
                Assert.AreEqual("{\"Title\":\"title\",\"Text\":\"text\"}", Encoding.UTF8.GetString(memoryStream.ToArray()));
            }
        }
    }
}