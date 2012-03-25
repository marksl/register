﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Register
{
    public class ItemPrices : IItemPrices
    {
        private readonly List<ItemPrice> _items = new List<ItemPrice>();
        private readonly List<ItemPrice> _weighedItems = new List<ItemPrice>();

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

        private static decimal GetItemPrice(IEnumerable<ItemPrice> items, ItemId id)
        {
            ItemPrice itemPrice = items.SingleOrDefault(x => x.Id == id);

            if (itemPrice == null)
            {
                throw new InvalidOperationException("ItemId was not found in list.");
            }

            return itemPrice.Price;
        }

        public void AddItem(ItemId id, decimal price)
        {
            _items.Add(new ItemPrice(id, price));
        }

        public void AddWeighedItem(ItemId id, decimal price)
        {
            _weighedItems.Add(new ItemPrice(id, price));
        }
    }
}