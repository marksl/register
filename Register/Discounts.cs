using System;
using System.Collections.Generic;
using System.Linq;

namespace Register
{
    public class Discounts : IDiscounts
    {
        private readonly List<BulkDiscount> _bulkDiscounts = new List<BulkDiscount>();
        private Coupon _coupon;

        #region IDiscounts Members

        public decimal GetDiscount(IEnumerable<Item> items)
        {
            List<Item> allItems = items.ToList();

            decimal freeItemTotal = GetFreeItemTotal(allItems);

            decimal discountTotal = freeItemTotal;

            if (_coupon != null)
            {
                discountTotal += _coupon.GetDiscount(allItems);
            }

            return discountTotal;
        }

        #endregion

        public void Add(BulkDiscount bulkDiscount)
        {
            _bulkDiscounts.Add(bulkDiscount);
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

        private decimal GetFreeItemTotal(IEnumerable<Item> items)
        {
            IEnumerable<Item> freeItems = GetFreeItems(items);

            decimal freeItemTotal = freeItems.Sum(item => item.GetPrice());

            return freeItemTotal;
        }

        private IEnumerable<Item> GetFreeItems(IEnumerable<Item> items)
        {
            Dictionary<ItemId, BulkDiscount> discounts = CloneDiscountsToSupportMultiThreadedCashRegister();

            IEnumerable<Item> freeItems = items.Where(item => IsItemFree(item, discounts));

            return freeItems;
        }

        private Dictionary<ItemId, BulkDiscount> CloneDiscountsToSupportMultiThreadedCashRegister()
        {
            return _bulkDiscounts.Select(x => x.Clone()).ToDictionary(discount => discount.ItemId);
        }

        private static bool IsItemFree(Item item, Dictionary<ItemId, BulkDiscount> discounts)
        {
            BulkDiscount discount;
            return discounts.TryGetValue(item.Id, out discount) && discount.IsNextItemFree(item);
        }
    }
}