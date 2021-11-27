using ConsoleApp1.Models;

namespace ConsoleApp1.Engines
{
    public interface IPriceEngine
    {
        PriceResponse GetPrice(PriceRequest request);
    }
}
