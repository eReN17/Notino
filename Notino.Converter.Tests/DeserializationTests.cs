using Notino.Converter.Core;
using Notino.Converter.Models;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace Notino.Converter.Tests
{
    public class DeserializationTests
    {
        private XmlDeserializer<Document> _xmlDocumentSerializer;

        [SetUp]
        public void Setup()
        {
            _xmlDocumentSerializer = new XmlDeserializer<Document>();
        }

        [Test]
        public void DeserializationTest()
        {
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes($"<?xml version=\"1.0\" encoding=\"UTF-8\"?><document><title>title</title><text>text</text></document>")))
            {
                var deserializedDocument = _xmlDocumentSerializer.DeserializeData(memoryStream).GetAwaiter().GetResult();
                Assert.Multiple(() =>
                {
                    Assert.AreEqual("text", deserializedDocument.Text);
                    Assert.AreEqual("title", deserializedDocument.Title);
                });
            }
        }
    }
}
