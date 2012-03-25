using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Register.Tests
{
    [TestFixture]
    public class DiscountsTests
    {
        #region Setup/Teardown

        [SetUp]
        public void CreateDiscounts()
        {
            _discounts = new Discounts();
        }

        #endregion

        private Discounts _discounts;

        private static IEnumerable<Item> FiveApplesAndOneCheerios
        {
            get
            {
                var itemsWithNoDiscounts = new List<Item> {new Item(ItemId.BoxOfCherrios, 0.5M)};
                for (int i = 0; i < 5; i++)
                {
                    itemsWithNoDiscounts.Add(new Item(ItemId.Apples, 1.0M));
                }
                return itemsWithNoDiscounts;
            }
        }

        private static IEnumerable<Item> TenApplesAndOneCherrios
        {
            get
            {
                var itemsWithNoDiscounts = new List<Item> {new Item(ItemId.BoxOfCherrios, 0.5M)};
                for (int i = 0; i < 10; i++)
                {
                    itemsWithNoDiscounts.Add(new Item(ItemId.Apples, 1.0M));
                }
                return itemsWithNoDiscounts;
            }
        }

        private static BulkDiscount ApplesBuy5Get2Free
        {
            get { return new BulkDiscount(ItemId.Apples, 5, 2); }
        }

        private static Coupon CreateValidCoupon()
        {
            return new Coupon(1.0M, 1.0M);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void AddCoupon_AddTwoCoupons_ThrowsException()
        {
            _discounts.AddCoupon(CreateValidCoupon());
            _discounts.AddCoupon(CreateValidCoupon());
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void AddCoupon_NullCoupon_ThrowsException()
        {
            _discounts.AddCoupon(null);
        }

        [Test]
        public void GetDiscount_CouponAddedAndItemsExceedThreshold_CouponAddedToDiscountTotal()
        {
            const decimal moneyOff = 5.0M;

            _discounts.AddCoupon(new Coupon(moneyOff, 50.0M));

            decimal discount = _discounts.GetDiscount(new Item[] {new Item(ItemId.BoxOfCherrios, 51.0M)});

            Assert.AreEqual(moneyOff, discount);
        }

        [Test]
        public void GetDiscount_CouponAddedAndItemsAreUnderThreshold_CouponAddedToDiscountTotal()
        {
            _discounts.AddCoupon(new Coupon(5.0M, 50.0M));

            decimal discount = _discounts.GetDiscount(new Item[] { new Item(ItemId.BoxOfCherrios, 40.0M) });

            Assert.AreEqual(0.0M, discount);
        }

        [Test]
        public void GetTotalAfterDiscounts_BulkDiscountBuy10With5Get2Free_Returns4Free()
        {
            _discounts.Add(ApplesBuy5Get2Free);

            decimal discount = _discounts.GetDiscount(TenApplesAndOneCherrios);

            Assert.AreEqual(4.0M, discount);
        }

        [Test]
        public void GetTotalAfterDiscounts_BulkDiscountBuy5Get2Free_Returns2Free()
        {
            _discounts.Add(ApplesBuy5Get2Free);

            decimal discount = _discounts.GetDiscount(FiveApplesAndOneCheerios);

            Assert.AreEqual(2.0M, discount);
        }

        [Test]
        public void GetTotalAfterDiscounts_NoDiscounts_ReturnsBillUndiscounted()
        {
            decimal discount = _discounts.GetDiscount(FiveApplesAndOneCheerios);

            Assert.AreEqual(0.0M, discount);
        }
    }
}