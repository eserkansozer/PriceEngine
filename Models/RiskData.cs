using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class RiskData
    {
        public string FirstName { get; }
        public string LastName { get;  }
        public decimal Value { get;  }
        public string Make { get;  }
        public DateTime? DOB { get; }

        public RiskData(string firstName, string lastName, decimal value, string make, DateTime? dob)
        {
            FirstName = firstName;
            LastName = lastName;
            Value = value;
            Make = make;
            DOB = dob;
        }
    }
}
