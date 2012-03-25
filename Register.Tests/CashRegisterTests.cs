using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Register.Tests
{
    [TestFixture]
    public class CashRegisterTests
    {
        #region Setup/Teardown

        [SetUp]
        public void CreateCashRegister()
        {
            _registerAllItemsOneDollar = new CashRegister(new AllItemsAreOneDollar());
        }

        #endregion

        private CashRegister _registerAllItemsOneDollar;

        private static IDiscounts WithNoDiscount
        {
            get { return new NoDiscount(); }
        }

        private class NoDiscount : IDiscounts
        {
            #region IDiscounts Members

            public decimal GetDiscount(IEnumerable<Item> items)
            {
                return 0.0M;
            }

            #endregion
        }

        private class AllItemsAreOneDollar : IItemPrices
        {
            public static decimal OneDollar
            {
                get { return 1.00M; }
            }

            #region IItemPrices Members

            public decimal GetPrice(ItemId id)
            {
                return OneDollar;
            }

            public decimal GetWeighedPrice(ItemId id)
            {
                return OneDollar;
            }

            #endregion
        }

        [Test]
        public void AddItem_ValidItemWithWeight_ReturnsWeighedItem()
        {
            Item item = _registerAllItemsOneDollar.AddItem(ItemId.Apples, 2.0M);

            Assert.IsNotNull(item);
            Assert.AreEqual(typeof (WeighedItem), item.GetType());
        }

        [Test]
        public void AddItem_ValidItem_ReturnsItem()
        {
            Item item = _registerAllItemsOneDollar.AddItem(ItemId.BoxOfCherrios);

            Assert.IsNotNull(item);
        }

        [Test]
        public void CalculateTotalBill_NoItems_ReturnsZero()
        {
            decimal total = _registerAllItemsOneDollar.CalculateTotalBill(WithNoDiscount);

            Assert.AreEqual(0.00M, total);
        }

        [Test]
        public void CalculateTotalBill_TwoItemsNoDiscount_ReturnsTwoItemCost()
        {
            decimal twoItemCost = AllItemsAreOneDollar.OneDollar*2;

            _registerAllItemsOneDollar.AddItem(ItemId.KraftDinner);
            _registerAllItemsOneDollar.AddItem(ItemId.BoxOfCherrios);

            decimal total = _registerAllItemsOneDollar.CalculateTotalBill(WithNoDiscount);

            Assert.AreEqual(twoItemCost, total);
        }

        [Test]
        public void Ctor_IntegationTestWithItems_Succeeds()
        {
            new CashRegister(new ItemPrices());
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Ctor_NullItems_ThrowsException()
        {
            new CashRegister(null);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void RemoveItem_ItemDoesntExist_ThrowsException()
        {
            _registerAllItemsOneDollar.RemoveItem(new Item(ItemId.Apples, 0.50M));
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void RemoveItem_NullItem_ThrowsException()
        {
            _registerAllItemsOneDollar.RemoveItem(null);
        }

        [Test]
        public void RemoveItem_RemoveValidItem_ItemRemoved()
        {
            Item addedItem = _registerAllItemsOneDollar.AddItem(ItemId.Apples);

            bool removed = _registerAllItemsOneDollar.RemoveItem(addedItem);

            Assert.IsTrue(removed);
        }
    }
}