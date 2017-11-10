using EssentialTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        private Product[] products = {
            new Product {Name = "Ball", Category = "Football", Price = 275M},
            new Product {Name = "Shorts", Category = "Football", Price = 48.95M},
            new Product {Name = "Ball", Category = "Basketball", Price = 19.50M},
            new Product {Name = "Shorts", Category = "Basketball", Price = 34.95M}
        };
        //определяем интерфейс
        private IValueCalculator calc;
        //добавляем конструктор, который принимает реализацию интерфейса
        public HomeController(IValueCalculator calcParam)
        {
            calc = calcParam;
        }
                // GET: Home
        public ActionResult Index()
        {
           // мы разбили сильную связь между HomeController и классом LinqValueCalculator, 
           //с помощью внедрени конструктора
           //по этому код ниже можно убрать

               ////создаем ядро ninject
               //IKernel ninjectKarnel = new StandardKernel();

               ////добавление связи между интерфейсом и классом реализации
               //ninjectKarnel.Bind<IValueCalculator>().To<LinqValueCalculator>();

               ////IValueCalculator calc = new LinqValueCalculator();
               ////указываем ninjec в каком интерфейсе мы заинтересованны
               //IValueCalculator calc = ninjectKarnel.Get<IValueCalculator>();

            ShoppingCart cart = new ShoppingCart(calc) { Products = products };
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
        /*
        Вот то, что произошло, когда вы запустили пример приложения и browser
        сделал запрос на корневой URL приложения:
            1. MVC фреймворк получил запрос и выяснил, что запрос предназначен для контроллера Home
            2. MVC фреймворк попросил наш пользовательский DR класс создать новый экземпляр класса
               HomeController, указав класс, используя параметр Type метода GetService.
            3. Наш DR попросил Ninject создать новый класс HomeController при помощи передачи объекта
               Type методу TryGet.
            4. Ninject изучил конструктор HomeController и обнаружил, что он требует реализацию
               IValueCalculator.
            5. Ninject создает экземпляр класса LinqValueCalculator и использует его для создания нового
               экземпляра класса HomeController.
            6. Ninject передает вновь созданный экземпляр HomeController пользовательскому DR, который
               возвращает его MVC фреймоворку.MVC фреймворк использует экземпляр контроллера для
               обработки запроса.
        */
    }
}