using System;
using NUnit.Framework;

namespace Register.Tests
{
    [TestFixture]
    public class ItemPricesTests
    {
        private ItemPrices _itemPrices;

        [SetUp]
        public void CreateItemPrices()
        {
            _itemPrices = new ItemPrices();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetPrice_ItemNotFound_ThrowsException()
        {
            _itemPrices.GetPrice(ItemId.Oranges);
        }

        [Test]
        public void GetPrice_ItemFound_ReturnsItemPrice()
        {
            const decimal price = 5.0M;
            
            _itemPrices.AddItem(ItemId.KraftDinner, price);
            decimal priceFromItems = _itemPrices.GetPrice(ItemId.KraftDinner);

            Assert.AreEqual(price, priceFromItems);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetWeighedPrice_ItemNotFound_ThrowsException()
        {
            _itemPrices.GetWeighedPrice(ItemId.Oranges);
        }

        [Test]
        public void GetWeighedPrice_ItemFound_ReturnsItemPrice()
        {
            const decimal price = 1.99M;

            _itemPrices.AddWeighedItem(ItemId.Apples, price);
            decimal priceFromItems = _itemPrices.GetWeighedPrice(ItemId.Apples);

            Assert.AreEqual(price, priceFromItems);
        }
    }
}