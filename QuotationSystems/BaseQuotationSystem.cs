using ConsoleApp1.Models;

namespace ConsoleApp1.QuotationSystems
{
    public abstract class BaseQuotationSystem : IQuotationSystem
    {
        protected readonly string _url;
        protected readonly string _port;

        public BaseQuotationSystem()
        {}

        public BaseQuotationSystem(string url, string port)
        {
            _url = url;
            _port = port;
        }

        public abstract dynamic GetPrice(PriceRequest requegst);
    }
}
