using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
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

            var currencies = valCurs.ValType.Find(x => x.Type == "Xarici valyutalar")?.Valute;
            if (currencies == null || currencies.Count == 0)
            {
                Console.WriteLine("Currency data not found.");
                return;
            }

            Console.WriteLine("Select conversion type:");
            Console.WriteLine("1. AZN → Currency");
            Console.WriteLine("2. Currency → AZN");
            Console.Write("Choice (1 or 2): ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter amount in AZN: ");
                int amountInAzn;
                while (!int.TryParse(Console.ReadLine(), out amountInAzn) || amountInAzn < 0)
                {
                    Console.WriteLine("Error: Please enter a **positive whole number**.");
                    Console.Write("Enter amount in AZN: ");
                }

                Console.Write("Enter currency code (e.g., USD, EUR): ");
                var currencyCode = Console.ReadLine()?.ToUpper();

                var selectedCurrency = currencies.Find(x => x.Code == currencyCode);
                if (selectedCurrency == null || selectedCurrency.Value <= 0)
                {
                    Console.WriteLine("Currency not found or invalid exchange rate.");
                    return;
                }

                double result = amountInAzn / selectedCurrency.Value;
                Console.WriteLine($"{amountInAzn} AZN = {result:F2} {currencyCode}");
            }
            else if (choice == "2")
            {
                Console.Write("Enter currency code (e.g., USD, EUR): ");
                var currencyCode = Console.ReadLine()?.ToUpper();

                var selectedCurrency = currencies.Find(x => x.Code == currencyCode);
                if (selectedCurrency == null || selectedCurrency.Value <= 0)
                {
                    Console.WriteLine("Currency not found or invalid exchange rate.");
                    return;
                }

                Console.Write($"Enter amount in {currencyCode}: ");
                int foreignAmount;
                while (!int.TryParse(Console.ReadLine(), out foreignAmount) || foreignAmount < 0)
                {
                    Console.WriteLine("Error: Please enter a **positive whole number**.");
                    Console.Write($"Enter amount in {currencyCode}: ");
                }

                double result = foreignAmount * selectedCurrency.Value;
                Console.WriteLine($"{foreignAmount} {currencyCode} = {result:F2} AZN");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }
}
