using ConsoleApp1.Models;
using ConsoleApp1.QuotationSystems;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace ConsoleApp1.Engines
{
    public class PriceEngine : IPriceEngine
    {
        private readonly List<BaseQuotationSystem> _quotationSytems;
        
        public PriceEngine(List<BaseQuotationSystem> quotationSytems)
        {
            _quotationSytems = quotationSytems;
        }

        //pass request with risk data with details of a gadget, return the best price retrieved from 3 external quotation engines
        public PriceResponse GetPrice(PriceRequest request)
        {
            //validation
            ValidatePriceRequest(request);

            //now call 3 external system and get the best price
            List<PriceResponse> responses = new List<PriceResponse>();

            //system 1 requires DOB to be specified
            if (request.RiskData.DOB != null)
            {
                var response1 = QuoteExternalSystem(_quotationSytems[0], request);
                if (response1.IsSuccess)
                {
                    responses.Add(new PriceResponse(response1.Price, response1.Tax, response1.Name));
                }
            }

            //system 2 only quotes for some makes
            if (request.RiskData.Make == "examplemake1" || 
                request.RiskData.Make == "examplemake2" ||
                request.RiskData.Make == "examplemake3")
            {
                var response2 = QuoteExternalSystem(_quotationSytems[1], request);
                if (response2.IsSuccess)
                {
                    responses.Add(new PriceResponse(response2.Price, response2.Tax, response2.Name));
                }
            }

            //system 3 is always called
            var response3 = QuoteExternalSystem(_quotationSytems[2], request);
            if (response3.IsSuccess)
            {
                responses.Add(new PriceResponse(response3.Price, response3.Tax, response3.Name));
            }

            return responses.OrderBy(r => r.Price).First();
        }

        private dynamic QuoteExternalSystem(BaseQuotationSystem system, PriceRequest request)
        {          
            return system.GetPrice(request);
        }

        private void ValidatePriceRequest(PriceRequest request)
        {
            if (request.RiskData == null)
            {
                var errorMessage = "Risk Data is missing";
                throw new ApplicationException(errorMessage);
            }

            if (String.IsNullOrEmpty(request.RiskData.FirstName))
            {
                var errorMessage = "First name is required";
                throw new ApplicationException(errorMessage);

            }

            if (String.IsNullOrEmpty(request.RiskData.LastName))
            {
                var errorMessage = "Surname is required";
                throw new ApplicationException(errorMessage);

            }

            if (request.RiskData.Value == 0)
            {
                var errorMessage = "Value is required";
                throw new ApplicationException(errorMessage);
            }
        }
    }
}
