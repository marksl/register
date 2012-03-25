using System;
using NUnit.Framework;

namespace Register.Tests
{
    [TestFixture]
    public class WeighedItemTests
    {
        [Test]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void Ctr_ZeroWeight_ThrowsArgumentOutOfRangeException()
        {
            new WeighedItem(ItemId.Apples, 5.0M, 0.0M);
        }

        [Test]
        public void GetPrice_ReturnsWeightTimesPrice()
        {
            var weighedItem = new WeighedItem(ItemId.Apples, 5.0M, 2.0M);

            decimal price = weighedItem.GetPrice();

            Assert.AreEqual(10.0M, price);
        }
    }
}