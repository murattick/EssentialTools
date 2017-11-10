using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    // класс расчитывающий общую стоимость продуктов (коллекции)
    public class LinqValueCalculator : IValueCalculator
    {
        private IDiscountHelper discounter;
        public LinqValueCalculator(IDiscountHelper discountParam)
        {
            discounter = discountParam;
        }
        public decimal ValueProduct(IEnumerable<Product> products)
        {
            //return products.Sum(p => p.Price);
            return discounter.ApplyDiscont(products.Sum(p => p.Price));
        }
        /*
        Мы создали цепочку зависимостей, которую легко обрабатывает Ninject, используя связи, которые
        мы определили в пользовательском DR. 
        Для того чтобы удовлетворить запрос для класса
        HomeController, Ninject считает, что необходимо создать реализацию для класса IValueCalculator,
        и он делает это, рассматривая свои связи и понимая, что наша политика для этого интерфейса
        заключается в использовании класса LinqValueCalculator. 
        Но для того, чтобы создать объект LinqValueCalculator, Ninject понимает, 
        что он должен использовать реализацию IDiscountHelper,
        и поэтому он рассматривает связи и создает объект DefaultDiscountHelper. 
        Он создает DefaultDiscountHelper и передает его конструктору объекта LinqValueCalculator, 
        который в свою очередь передает его конструктору класса HomeController. 
        Затем он используется для обслуживания запроса пользователя. 
        Ninject проверяет каждый класс, для которого он создал экземпляр для
        внедрения зависимостей, таким образом неважно, насколько длинными и сложными являются
        цепочки зависимостей.
        */
    }
}