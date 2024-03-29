﻿using System;
using NUnit.Framework;

namespace Register.Tests
{
    [TestFixture]
    public class BulkDiscountTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Ctor_InvalidNumberReceivedFree_ThrowsException()
        {
            new BulkDiscount(ItemId.Apples, 2, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Ctor_InvalidNumberRequired_ThrowsException()
        {
            new BulkDiscount(ItemId.Apples, 0, 1);
        }

        [Test]
        public void Ctor_ValidBulkDiscount_ThrowsException()
        {
            new BulkDiscount(ItemId.Apples, 2, 1);
        }
    }
}