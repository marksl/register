using System;
using NUnit.Framework;

namespace Register.Tests
{
    [TestFixture]
    public class CouponTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Ctor_InvalidMoneyOff_ThrowsException()
        {
            new Coupon(0.0M, 1.0M);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Ctor_InvalidThreshold_ThrowsException()
        {
            new Coupon(1.0M, 0.0M);
        }

        [Test]
        public void Ctor_ValidParameters_Succeeds()
        {
            new Coupon(1.0M, 1.0M);
        }
    }
}