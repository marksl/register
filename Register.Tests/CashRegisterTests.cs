using System;
using NUnit.Framework;

namespace Register.Tests
{
    [TestFixture]   
    public class CashRegisterTests
    {
        private CashRegister _register;

        [TestFixtureSetUp]
        public void CreateCashRegister()
        {
            _register = new CashRegister();
        }


        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddItem_NullItem_ThrowsException()
        {
            _register.AddItem(null);
        }
         
    }
}