using Notino.Converter.Core;
using Notino.Converter.Exceptions;
using Notino.Converter.Helpers;
using Notino.Converter.Models;
using NUnit.Framework;
using System;

namespace Notino.Converter.Tests
{
    public class DeserializersFactoryTests
    {
        private DeserializerFactory<Document> documentDeserializerFactory;

        [SetUp]
        public void Setup()
        {
            documentDeserializerFactory = new DeserializerFactory<Document>();
        }

        [Test]
        public void GettingDeserializaer()
        {
            var jsonSerializer = documentDeserializerFactory.GetDeserializer(".xml");

            Assert.IsTrue(jsonSerializer is XmlDeserializer<Document>);
        }

        [Test]
        public void GettingDeserializerFileFormatException()
        {
            Assert.Throws<FileFormatNotSupportedException>(() => documentDeserializerFactory.GetDeserializer("."));
        }

        [Test]
        public void GettingDeserializerArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => documentDeserializerFactory.GetDeserializer(null));
        }
    }
}
