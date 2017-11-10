using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using System.Linq;
using Moq;

namespace EssentialTools.Tests
{
    /// <summary>
    /// Сводное описание для UnitTest2
    /// </summary>
    [TestClass]
    public class UnitTest2
    {
        /*
Преимущество использования Moq таким образом заключается в том, что наш юнит тест проверяет
только поведение объекта LinqValueCalculator и не зависит от какой-либо реальной реализации
интерфейса IDiscountHelper в папке Models. Это означает, что если наши тесты не сработают, мы
будем знать, что проблема заключается либо в реализации LinqValueCalculator или в том, как мы
создали mock-объект.
        */
        private Product[] products = {
                new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
                };

        [TestMethod]
        public void Sum_Products_Correctly()
        {
            // arrange
            //var discounter = new MinimumDiscountHelper();
            //var target = new LinqValueCalculator(discounter);
            //var goalTotal = products.Sum(e => e.Price);

            //сообщаяем mock с каким объектом будем рабтать,
            //Мы создаем строго типизированный объект Mock< IDiscountHelper >, который говорит библиотеке
            //какой тип он будет обрабатывать - это интерфейс IDiscountHelper
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            //метод Setup, Moq передает нам интерфейс, который мы попросили реализовать
            //также должны сказать Moq, в каких значениях параметров мы заинтересованы, используя класс It
            //метод IsAny, используя decimal как универсальный тип. Это говорит
            //Moq, что поведение, которое мы определяем, следует применять всякий раз, когда вызывается
            //ApplyDiscount с любым десятичным значением.
            //Метод Returns позволяет определить результат - decimal
            mock.Setup(m => m.ApplyDiscont(It.IsAny<decimal>())).Returns<decimal>(total => total);
            //использовании mock-объекта в юнит тесте
            var target = new LinqValueCalculator(mock.Object);
            // act
            var result = target.ValueProduct(products);
            // assert
            // Assert.AreEqual(goalTotal, result);
            Assert.AreEqual(products.Sum(e => e.Price), result);
        }

        private Product[] createProduct(decimal value)
        {
            return new[] { new Product { Price = value } };
        }
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discounts()
        {
            // arrange
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscont(It.IsAny<decimal>())).Returns<decimal>(total => total);
            //Is, возвращает true, если значение, переданное методу
            //ApplyDiscount равно 0.Вместо того чтобы вернуть результат, мы использовали метод Throws,
            //который заставляет Moq выбросить новый экземпляр исключения, которое мы указываем с параметром типа.
            mock.Setup(m => m.ApplyDiscont(It.Is<decimal>(v => v == 0))).Throws<System.ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscont(It.Is<decimal>(v => v > 100))).Returns<decimal>(total => (total * 0.9M));
            //использование объекта It связано с методом IsInRange, который позволяет нам
            //охватить определенный диапазон значений параметров
            mock.Setup(m => m.ApplyDiscont(It.IsInRange<decimal>(10, 100, Range.Inclusive)))
                .Returns<decimal>(total => total - 5);
            var target = new LinqValueCalculator(mock.Object);
            // act
            decimal FiveDollarDiscount = target.ValueProduct(createProduct(5));
            decimal TenDollarDiscount = target.ValueProduct(createProduct(10));
            decimal FiftyDollarDiscount = target.ValueProduct(createProduct(50));
            decimal HundredDollarDiscount = target.ValueProduct(createProduct(100));
            decimal FiveHundredDollarDiscount = target.ValueProduct(createProduct(500));
            // assert
            Assert.AreEqual(5, FiveDollarDiscount, "$5 Fail");
            Assert.AreEqual(5, TenDollarDiscount, "$10 Fail");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 Fail");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 Fail");
            Assert.AreEqual(450, FiveHundredDollarDiscount, "$500 Fail");
            target.ValueProduct(createProduct(0));
        }
    }
}
