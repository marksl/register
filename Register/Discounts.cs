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

        public decimal GetTotalAfterDiscounts(IEnumerable<Item> items, decimal total)
        {
            decimal freeItemTotal = GetFreeItemTotal(items);

            total = total - freeItemTotal;

            if (_coupon != null)
            {
                total = _coupon.GetTotal(total);
            }

            return total;
        }

        #endregion

        public bool Add(BulkDiscount bulkDiscount)
        {
            _bulkDiscounts.Add(bulkDiscount);

            return true;
        }

        public bool AddCoupon(Coupon coupon)
        {
            if (coupon == null) throw new ArgumentNullException("coupon");

            if (_coupon != null)
            {
                throw new InvalidOperationException(
                    "Only one coupon is allowed. Threshold coupons are always specified as 'this coupon can not be applied with any other coupon'.");
            }

            _coupon = coupon;

            return true;
        }

        public bool RemoveCoupon()
        {
            if (_coupon == null)
                throw new InvalidOperationException("Failed to remove coupon. No coupon exists.");

            _coupon = null;

            return true;
        }

        private decimal GetFreeItemTotal(IEnumerable<Item> items)
        {
            IEnumerable<Item> freeItems = GetFreeItems(items);

            decimal freeItemTotal = freeItems.Sum(item => item.GetPrice());

            return freeItemTotal;
        }

        private IEnumerable<Item> GetFreeItems(IEnumerable<Item> items)
        {
            Dictionary<ItemId, BulkDiscount> discounts = CloneDiscountsToSupportMultieThreadedCashRegister();

            IEnumerable<Item> freeItems = items.Where(item => IsItemFree(item, discounts));

            return freeItems;
        }

        private Dictionary<ItemId, BulkDiscount> CloneDiscountsToSupportMultieThreadedCashRegister()
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