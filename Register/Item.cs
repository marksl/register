namespace Register
{
    public class Item
    {
        public ItemId Id { get; private set; }
        public PricingStrategy PricingStrategy { get; private set; }

        public Item(ItemId id, PricingStrategy pricingStrategy)
        {
            Id = id;
            PricingStrategy = pricingStrategy;
        }

        public decimal GetPrice()
        {
            return PricingStrategy.GetPrice();
        }
    }

    // Perhaps this does make more sense....

    public class WeighedItem : Item
    {
        public WeighedItem(ItemId id, PricingStrategy pricingStrategy) : base(id, pricingStrategy)
        {
        }

        public decimal Weight { get; set; }
    }
}