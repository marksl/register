namespace Register
{
    public class PricePerItem : PricingStrategy
    {
        public override decimal GetPrice()
        {
            if (IsFree)
            {
                return new decimal(00.00);
            }

            throw new System.NotImplementedException();
        }

        // Can be free if it's a bulk discount
        public bool IsFree { get; set; }
    }
}