using ConsoleApp1.Engines;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //SNIP - collect input (risk data from the user)

            var request = new PriceRequest(new RiskData("John", "Smith", 500, "Cool New Phone", DateTime.Parse("1980-01-01")));

            var priceEngine = new PriceEngine();

            try
            {
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
