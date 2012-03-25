using System;

namespace Register
{
    public class Item
    {
        public Item(ItemId id, decimal price)
        {
            if (id == ItemId.Invalid)
            {
                throw new InvalidOperationException("ItemId is invalid.");
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