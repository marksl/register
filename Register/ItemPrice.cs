namespace Register
{
    public class ItemPrice
    {
        public ItemPrice(ItemId id, decimal price)
        {
            Id = id;
            Price = price;
        }

        public ItemId Id { get; private set; }
        public decimal Price { get; private set; }
    }
}