using ConsoleApp1.Models;

namespace ConsoleApp1.Engines
{
    public interface IPriceEngine
    {
        decimal GetPrice(PriceRequest request, out decimal tax, out string insurerName);
    }
}
