using System.Xml.Serialization;

namespace PB403CBAR
{
    [XmlRoot(ElementName = "ValCurs")]
    public class ValCurs
    {
        [XmlElement(ElementName = "ValType")]
        public required List<ValType> ValType { get; set; }

        [XmlAttribute(AttributeName = "Date")]
        public required string Date { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public required string Name { get; set; }

        [XmlAttribute(AttributeName = "Description")]
        public required string Description { get; set; }
    }
}
