namespace ConsoleApp1.Models
{
    public class PriceResponse
    {
        public decimal Price { get; }
        public decimal Tax { get; }
        public string InsurerName { get; }

        public PriceResponse(decimal price, decimal tax, string insurerName)
        {
            Price = price;
            Tax = tax;
            InsurerName = insurerName;
        }

    }
}
