using System;
using NUnit.Framework;

namespace Register.Tests
{
    [TestFixture]
    public class ItemTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Ctr_InvalidItemId_ArgumentOutOfRangeException()
        {
            new Item(ItemId.Invalid, 2.0M);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Ctr_InvalidPrice_ArgumentOutOfRangeException()
        {
            new Item(ItemId.Apples, 0.0M);
        }
         
    }
}