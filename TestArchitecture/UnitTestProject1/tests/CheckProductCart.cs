using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestArchitecture.tests
{
    [TestFixture]
    class CheckProductCart : TestBase
    {
        [Test]
        public void CheckProductCartTest()
        {
            int numberOfProduct = 3;
            app.AddProductsToCart(numberOfProduct);
            app.DeleteProductsFromCart();
            Assert.IsTrue(app.IsCartEmpty());
        }

    }
}
