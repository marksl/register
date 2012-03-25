namespace Register
{
    public class PricePerWeight : PricingStrategy
    {
        public PricePerWeight(decimal weight)
        {
            Weight = weight;
        }

        public decimal Weight { get; private set; }

        public override decimal GetPrice()
        {
            throw new System.NotImplementedException();
        }
    }
}