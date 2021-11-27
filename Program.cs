using ConsoleApp1.Engines;
using ConsoleApp1.Models;
using ConsoleApp1.QuotationSystems;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //SNIP - collect input (risk data from the user)

            try
            {
                var quotationSytems = new List<BaseQuotationSystem> {
                    new QuotationSystem1("http://quote-system-1.com", "1234"),
                    new QuotationSystem2("http://quote-system-2.com", "1235"),
                    new QuotationSystem3("http://quote-system-3.com", "100")
                };
                var priceEngine = new PriceEngine(quotationSytems);
                var request = new PriceRequest(new RiskData("John", "Smith", 500, "Cool New Phone", DateTime.Parse("1980-01-01")));
                var response = priceEngine.GetPrice(request);
                Console.WriteLine(String.Format("You price is {0}, from insurer: {1}. This includes tax of {2}", response.Price, response.InsurerName, response.Tax));
            }
            catch(Exception ex)
            {
                Console.WriteLine(String.Format("There was an error - {0}", ex.Message));
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
