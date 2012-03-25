using System;
using System.Collections.Generic;
using System.Linq;

namespace Register
{
    public class ItemPrices : IItemPrices
    {
        private readonly List<Item> _items = new List<Item>();
        private readonly List<Item> _weighedItems = new List<Item>();

        #region IItemPrices Members

        public decimal GetPrice(ItemId id)
        {
            return GetItemPrice(_items, id);
        }

        public decimal GetWeighedPrice(ItemId id)
        {
            return GetItemPrice(_weighedItems, id);
        }

        #endregion

        private static decimal GetItemPrice(IEnumerable<Item> items, ItemId id)
        {
            Item itemPrice = items.SingleOrDefault(x => x.Id == id);

            if (itemPrice == null)
            {
                throw new InvalidOperationException("ItemId was not found in list.");
            }

            return itemPrice.GetPrice();
        }

        public void AddItem(ItemId id, decimal price)
        {
            _items.Add(new Item(id, price));
        }

        public void AddWeighedItem(ItemId id, decimal price)
        {
            _weighedItems.Add(new Item(id, price));
        }
    }
}