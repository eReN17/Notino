using Notino.Converter.Core;
using Notino.Converter.Exceptions;
using Notino.Converter.Helpers;
using Notino.Converter.Models;

namespace Notino.Converter
{
    public class FileConverter
    {        
        public async Task Convert(string sourceFile, string targetFile)
        {
            // Possible FileFormatNotSupportedException
            var deserializer = ConverterUtilities.GetDeserializer(sourceFile);
            var serialzier = ConverterUtilities.GetSerializer(targetFile);

            Document data;
            using (var sourceStream = File.OpenRead(sourceFile))
                data = await deserializer.DeserializeData(sourceStream);

            using (var targetStream = File.Create(targetFile))
                await serialzier.SerializeData(targetStream, data);
        }
    }
}
