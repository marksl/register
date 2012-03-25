using System;

namespace Register
{
    public class WeighedItem : Item
    {
        public WeighedItem(ItemId id, decimal price, decimal weight)
            : base(id, price)
        {
            if(weight <= 0.0M)
            {
                throw new ArgumentOutOfRangeException("weight", "weight must be greater than 0.");
            }

            Weight = weight;
        }

        public decimal Weight { get; private set; }

        public override decimal GetPrice()
        {
            return Weight*Price;
        }
    }
}