using ConsoleApp1.Models;
using System.Dynamic;

namespace ConsoleApp1.QuotationSystems
{
    public class QuotationSystem1 : BaseQuotationSystem
    {
        public QuotationSystem1(string url, string port):base (url, port)        
        {}

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
            response.Price = 123.45M;
            response.IsSuccess = true;
            response.Name = "Test Name";
            response.Tax = 123.45M * 0.12M; 

            return response;
        }
    }
}
