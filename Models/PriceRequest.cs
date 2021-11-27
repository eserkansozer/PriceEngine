using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class PriceRequest
    {
        public RiskData RiskData { get; }
        public PriceRequest(RiskData riskData)
        {
            RiskData = riskData;
        }
        
    }
}
