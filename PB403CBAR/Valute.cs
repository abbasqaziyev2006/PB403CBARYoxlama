using System.Xml.Serialization;

namespace PB403CBAR
{
    [XmlRoot(ElementName = "Valute")]
    public class Valute
    {
        [XmlElement(ElementName = "Nominal")]
        public string Nominal { get; set; }

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Value")]
        public double Value { get; set; }

        [XmlAttribute(AttributeName = "Code")]
        public string Code { get; set; }
    }
}
