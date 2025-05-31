using System.Xml.Serialization;

namespace PB403CBAR
{
    [XmlRoot(ElementName = "ValType")]
    public class ValType
    {
        [XmlElement(ElementName = "Valute")]
        public required List<Valute> Valute { get; set; }

        [XmlAttribute(AttributeName = "Type")]
        public required string Type { get; set; }
    }
}
