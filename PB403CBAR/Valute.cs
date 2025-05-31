using System.Xml.Serialization;

namespace PB403CBAR
{
    [XmlRoot(ElementName = "Valute")]
    public class Valute
    {
        [XmlElement(ElementName = "Nominal")]
        public required string Nominal { get; set; }

        [XmlElement(ElementName = "Name")]
        public required string Name { get; set; }

        [XmlElement(ElementName = "Value")]
        public double Value { get; set; }

        [XmlAttribute(AttributeName = "Code")]
        public required string Code { get; set; }
    }
}
