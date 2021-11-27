﻿using ConsoleApp1.Engines;
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

            decimal tax = 0;
            string insurer = "";
            string error = "";

            var priceEngine = new PriceEngine();
            var price = priceEngine.GetPrice(request, out tax, out insurer, out error);

            if (price == -1)
            {
                Console.WriteLine(String.Format("There was an error - {0}", error));
            }
            else
            {
                Console.WriteLine(String.Format("You price is {0}, from insurer: {1}. This includes tax of {2}", price, insurer, tax));
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
