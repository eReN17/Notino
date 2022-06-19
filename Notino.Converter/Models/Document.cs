using System.Xml.Serialization;

namespace Notino.Converter.Models
{
    [XmlRoot("document")]
    public class Document
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("text")]
        public string Text { get; set; }
    }
}
