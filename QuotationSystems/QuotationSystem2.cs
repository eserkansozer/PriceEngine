using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.QuotationSystems
{
    public class QuotationSystem2 : BaseQuotationSystem
    {
        public QuotationSystem2(string url, string port) : base(url, port)
        { }

        public override dynamic GetPrice(PriceRequest request)
        {
            dynamic requestData = new ExpandoObject();
            requestData.FirstName = request.RiskData.FirstName;
            requestData.Surname = request.RiskData.LastName;
            requestData.DOB = request.RiskData.DOB;
            requestData.Make = request.RiskData.Make;
            requestData.Amount = request.RiskData.Value;

            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(requestData);

            dynamic response = new ExpandoObject();
            response.Price = 234.56M;
            response.IsSuccess = true;
            response.Name = "qewtrywrh";
            response.Tax = 234.56M * 0.12M;

            return response;
        }
    }
}
