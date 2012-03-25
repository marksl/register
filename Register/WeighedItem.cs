namespace Register
{
    public class WeighedItem : Item
    {
        public WeighedItem(ItemId id, decimal price, decimal weight)
            : base(id, price)
        {
            Weight = weight;
        }

        public decimal Weight { get; private set; }

        public override decimal GetPrice()
        {
            return Weight*Price;
        }
    }
}