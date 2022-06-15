using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml.Linq;

using Notino.Converter;
using Notino.Converter.Exceptions;

namespace Notino.Homework
{
    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine($"Expected 2 arguments to run the program. No of arguments {args.Length}");
                Console.WriteLine(string.Join(Environment.NewLine, args));
                Console.ReadKey();
                return;
            }

            Solution1(args[0], args[1]);
            //Solution2(args[0], args[1]);

            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }

        static void Solution2(string sourceFile, string targetFile)
        {
            try
            {
                new FileConverter().Convert(sourceFile, targetFile).GetAwaiter().GetResult();
            }
            catch(FileFormatNotSupportedException fex)
            {

            }
            catch(Exception ex)
            {

            }
        }

        static void Solution1(string sourceFile, string targetFile)
        {
            Debug.Assert(File.Exists(sourceFile));
            Debug.Assert(Path.GetExtension(sourceFile).ToLower().Equals(".xml"));
            Debug.Assert(Path.GetExtension(targetFile).ToLower().Equals(".json"));

            try
            {
                var sourceFileData = ReadSourceFile(sourceFile).GetAwaiter().GetResult();

                var sourceDocument = ParseSourceStringXml(sourceFileData);

                var serializedDocumentJson = SerializeDocumentToJson(sourceDocument);

                WriteDataToFile(serializedDocumentJson, targetFile).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }

            Console.ReadKey();
        }

        static async Task<string> ReadSourceFile(string sourceFile)
        {
            using (var stream = File.Open(sourceFile, FileMode.Open))
            using (var reader = new StreamReader(stream))
                return await reader.ReadToEndAsync();
        }

        static async Task WriteDataToFile(string data, string targetFile)
        {
            using (var stream = File.Open(targetFile, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(stream))
                await writer.WriteAsync(data);
        }

        static string SerializeDocumentToJson(Document document)
            => JsonConvert.SerializeObject(document);

        static Document ParseSourceStringXml(string data)
        {
            var xdoc = XDocument.Parse(data);
            return new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };
        }

        static void ShowError(Exception ex, bool printStackTrace = false)
        {
            var previousForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Error of type {ex.GetType()}");
            Console.WriteLine($"{ex.Message}");
            Console.WriteLine("------------------------------------------------");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner exception of type {ex.InnerException.GetType()}: {ex.InnerException.Message}");
            }

            if (printStackTrace)
            {
                Console.WriteLine($"Stack trace:{Environment.NewLine}{ex.StackTrace}");
            }

            Console.ForegroundColor = previousForeground;
        }
    }
}