using System;
using System.Collections.Generic;
using System.Linq;

namespace Register
{
    public class CashRegister
    {
        private readonly IItemPrices _itemPrices;
        private readonly List<Item> _items;

        public CashRegister(IItemPrices itemPrices)
        {
            if (itemPrices == null) throw new ArgumentNullException("itemPrices");

            _itemPrices = itemPrices;

            _items = new List<Item>();
        }

        public Item AddItem(ItemId itemId)
        {
            decimal price = _itemPrices.GetPrice(itemId);

            var item = new Item(itemId, price);

            return AddItem(item);
        }

        public Item AddItem(ItemId itemId, decimal weight)
        {
            decimal price = _itemPrices.GetWeighedPrice(itemId);

            var item = new WeighedItem(itemId, price, weight);

            return AddItem(item);
        }

        private Item AddItem(Item item)
        {
            _items.Add(item);

            return item;
        }

        public bool RemoveItem(Item item)
        {
            if (item == null) throw new ArgumentNullException("item");

            bool removed = _items.Remove(item);
            if (!removed)
            {
                throw new InvalidOperationException("Item failed to remove.");
            }

            return true;
        }

        public decimal CalculateTotalBill(IDiscounts discounts)
        {
            decimal total = _items.Sum(item => item.GetPrice());

            decimal discount = discounts.GetDiscount(_items);

            return total - discount;
        }
    }
}