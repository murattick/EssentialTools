using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IDiscountHelper getTestObject()
        {
            return new MinimumDiscountHelper();
        }
        [TestMethod]
        public void Discount_Above_100()
        {
            //arrage
            //создает экземпляр объекта, который мы собираемся
            //тестировать: в данном случае это класса MinimumDiscountHelper
            IDiscountHelper target = getTestObject();
            decimal total = 200;
            //act
            //присваиваем результат переменной discountedTotal 
            var discountedTotal = target.ApplyDiscont(total);
            //assert
            //чтобы проверить, что значение, которые мы получили от метода
            //ApplyDiscount, составляет 90 % от первоначальной общей стоимости.
            Assert.AreEqual(total * 0.9m, discountedTotal);
        }

        [TestMethod]
        public void Discount_Between_10_And_100()
        {
            //arrage
            IDiscountHelper target = getTestObject();
            //act
            decimal tenDollar = target.ApplyDiscont(10);
            decimal hundredDollar = target.ApplyDiscont(100);
            decimal fiftyDollar = target.ApplyDiscont(50);
            //assert
            Assert.AreEqual(5, tenDollar, "$10 dollar is wrong");
            Assert.AreEqual(95, hundredDollar, "$100 dollar is wrong");
            Assert.AreEqual(45, fiftyDollar, "$50 dollar is wrong" );

        }

        [TestMethod]
        public void Discount_Less_Than_10()
        {
            //arrage
            IDiscountHelper target = getTestObject();
            //act
            decimal discount5 = target.ApplyDiscont(5);
            decimal discount0 = target.ApplyDiscont(0);
            //assert
            Assert.AreEqual(5,discount5);
            Assert.AreEqual(0, discount0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Discount_Negative_Total()
        {
            //arrage
            IDiscountHelper target = getTestObject();
            //act
            target.ApplyDiscont(-1);
        }
    }
}
