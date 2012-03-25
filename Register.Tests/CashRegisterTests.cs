using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Register.Tests
{
    [TestFixture]
    public class CashRegisterTests
    {
        private CashRegister _registerAllItemsOneDollar;

        [SetUp]
        public void CreateCashRegister()
        {
            _registerAllItemsOneDollar = new CashRegister(new AllItemsAreOneDollar());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_NullItems_ThrowsException()
        {
            new CashRegister(null);
        }

        [Test]
        public void AddItem_ValidItem_ReturnsItem()
        {
            Item item = _registerAllItemsOneDollar.AddItem(ItemId.BoxOfCherrios);

            Assert.IsNotNull(item);
        }

        [Test]
        public void AddItem_ValidItemWithWeight_ReturnsWeighedItem()
        {
            Item item = _registerAllItemsOneDollar.AddItem(ItemId.Apples, 2.0M);

            Assert.IsNotNull(item);
            Assert.AreEqual(typeof (WeighedItem), item.GetType());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveItem_NullItem_ThrowsException()
        {
            _registerAllItemsOneDollar.RemoveItem(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveItem_ItemDoesntExist_ThrowsException()
        {
            _registerAllItemsOneDollar.RemoveItem(new Item(ItemId.Apples, 0.50M));
        }

        [Test]
        public void RemoveItem_RemoveValidItem_ItemRemoved()
        {
            Item addedItem = _registerAllItemsOneDollar.AddItem(ItemId.Apples);

            bool removed = _registerAllItemsOneDollar.RemoveItem(addedItem);

            Assert.IsTrue(removed);
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
            const decimal twoItemCost = 2.00M;

            _registerAllItemsOneDollar.AddItem(ItemId.KraftDinner);
            _registerAllItemsOneDollar.AddItem(ItemId.BoxOfCherrios);

            decimal total = _registerAllItemsOneDollar.CalculateTotalBill(WithNoDiscount);

            Assert.AreEqual(twoItemCost, total);
        }

        private static IDiscounts WithNoDiscount
        {
            get { return new NoDiscount(); }
        }

        private class NoDiscount : IDiscounts
        {
            public decimal GetTotalAfterDiscounts(IEnumerable<Item> items, decimal total)
            {
                return total;
            }
        }

        private class AllItemsAreOneDollar : IItemPrices
        {
            public decimal GetPrice(ItemId itemId)
            {
                return OneDollar;
            }

            public decimal GetWeighedPrice(ItemId itemId)
            {
                return OneDollar;
            }

            private static decimal OneDollar
            {
                get { return 1.00M; }
            }
        }
    }
}