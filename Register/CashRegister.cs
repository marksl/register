using System;
using System.Collections.Generic;
using System.Linq;

namespace Register
{
    public class CashRegister
    {
        private readonly ItemPriceRepository _itemPriceRepository;
        private readonly List<Item> _items;
        private Coupon _coupon;

        public CashRegister()
        {
            _items = new List<Item>();
            _itemPriceRepository = new ItemPriceRepository();
        }

        public Item AddItem(ItemId itemId)
        {
            var item = new Item(itemId, new PricePerItem());

            return AddItem(item);
        }

        public Item AddItem(ItemId itemId, decimal weight)
        {
            var item = new Item(itemId, new PricePerWeight(weight));

            return AddItem(item);
        }

        private Item AddItem(Item item)
        {
            _items.Add(item);

            return item;
        }

        public void RemoveItem(Item item)
        {
            if (item == null) throw new ArgumentNullException("item");

            bool removed = _items.Remove(item);
            if (!removed)
            {
                throw new InvalidOperationException("Item failed to remove.");
            }
        }

        public void AddCoupon(Coupon coupon)
        {
            if (coupon == null) throw new ArgumentNullException("coupon");

            if (_coupon != null)
            {
                throw new InvalidOperationException(
                    "Only one coupon is allowed. Threshold coupons are always specified as 'this coupon can not be applied with any other coupon'.");
            }

            _coupon = coupon;
        }

        public decimal CalculateTotalBill()
        {
            IEnumerable<Item> itemsIncludedInBill = _itemPriceRepository.RemoveBulkDiscountedItems(_items);

            decimal itemTotal = itemsIncludedInBill.Sum(item => item.GetPrice());

            if (_coupon != null)
            {
                itemTotal = _coupon.Apply(itemTotal);
            }

            return itemTotal;
        }
    }
}