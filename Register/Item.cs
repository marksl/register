using System;

namespace Register
{
    public class Item
    {
        public Item(ItemId id, decimal price)
        {
            if (id == ItemId.Invalid)
            {
                throw new ArgumentOutOfRangeException("id", "ItemId is invalid.");
            }

            if (price <= 0.0M)
            {
                throw new ArgumentOutOfRangeException("price", "price must be greater than $0.00.");
            }

            Id = id;
            Price = price;
        }

        public ItemId Id { get; private set; }
        protected decimal Price { get; set; }

        public virtual decimal GetPrice()
        {
            return Price;
        }
    }
}