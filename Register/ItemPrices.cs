using System;
using System.Collections.Generic;
using System.Linq;

namespace Register
{
    public class ItemPrices : IItemPrices
    {
        private readonly List<ItemPrice> _items = new List<ItemPrice>();
        private readonly List<ItemPrice> _weighedItems = new List<ItemPrice>();

        #region IItemPrices Members

        public decimal GetPrice(ItemId itemId)
        {
            return GetItemPrice(_items, itemId);
        }

        public decimal GetWeighedPrice(ItemId itemId)
        {
            return GetItemPrice(_weighedItems, itemId);
        }

        #endregion

        private static decimal GetItemPrice(IEnumerable<ItemPrice> items, ItemId itemId)
        {
            ItemPrice itemPrice = items.SingleOrDefault(x => x.Id == itemId);

            if (itemPrice == null)
            {
                throw new InvalidOperationException("ItemId was not found in list.");
            }

            return itemPrice.Price;
        }

        public void AddItem(ItemId boxOfCherrios, decimal price)
        {
            _items.Add(new ItemPrice(boxOfCherrios, price));
        }

        public void AddWeighedItem(ItemId boxOfCherrios, decimal price)
        {
            _weighedItems.Add(new ItemPrice(boxOfCherrios, price));
        }
    }
}