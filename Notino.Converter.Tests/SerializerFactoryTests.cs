using Notino.Converter.Core;
using Notino.Converter.Exceptions;
using Notino.Converter.Helpers;
using Notino.Converter.Models;
using NUnit.Framework;
using System;

namespace Notino.Converter.Tests
{
    public class SerializerFactoryTests
    {
        private SerializerFactory<Document> documentSerializerFactory;

        [SetUp]
        public void Setup()
        {
            documentSerializerFactory = new SerializerFactory<Document>();
        }

        [Test]
        public void GettingSerializaer()
        {
            var jsonSerializer = documentSerializerFactory.GetSerializer(".json");

            Assert.IsTrue(jsonSerializer is JsonSerializer<Document>);
        }

        [Test]
        public void GettingSerializerFileFormatException()
        {
            Assert.Throws<FileFormatNotSupportedException>(() => documentSerializerFactory.GetSerializer("."));
        }

        [Test]
        public void GettingSerializerArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => documentSerializerFactory.GetSerializer(null));
        }
    }
}
