using System.Xml.Serialization;
namespace PB403CBAR
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var endpoint = "https://www.cbar.az/currencies/22.05.2025.xml";
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync(endpoint);
            XmlSerializer serializer = new XmlSerializer(typeof(ValCurs));
            ValCurs valCurs;
            using (StringReader reader = new StringReader(content))
            {
                valCurs = (ValCurs)serializer.Deserialize(reader);
            }
            Console.Write("AZN ile meblegi daxil edin: ");
            int balanceInAzn;
            while (!int.TryParse(Console.ReadLine(), out balanceInAzn) || balanceInAzn < 0)
            {
                Console.WriteLine("Xeta: Zehmet olmasa **musbet bir tam eded** daxil edin.");
                Console.Write("AZN ile meblegi daxil edin: ");
            }
            Console.Write("Valyuta kodunu daxil edin (meselen: USD, EUR): ");
            var convertCurrencyCode = Console.ReadLine()?.ToUpper();
            var currencies = valCurs.ValType.Find(x => x.Type == "Xarici valyutalar").Valute;
            var value=currencies.Find(x=>x.Code==convertCurrencyCode).Value;
            var convertedFromAzn = balanceInAzn/value;
            Console.WriteLine($"{balanceInAzn} azn={convertedFromAzn} {convertCurrencyCode}");
        }
    }

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

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "ValType")]
    public class ValType
    {

        [XmlElement(ElementName = "Valute")]
        public List<Valute> Valute { get; set; }

        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "ValCurs")]
    public class ValCurs
    {

        [XmlElement(ElementName = "ValType")]
        public List<ValType> ValType { get; set; }

        [XmlAttribute(AttributeName = "Date")]
        public string Date { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Description")]
        public string Description { get; set; }

        [XmlText]
        public string Text { get; set; }
    }


}
