using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using EssentialTools.Models;

namespace EssentialTools.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        //dependency resolver - функциональная возможность, которая отвечает за создание зависимостей
        IKernel kernel;
        public NinjectDependencyResolver()
        {
            //создаем ядро ninject
            kernel = new StandardKernel();
            AddBindings();
        }
        //вызывается, когда MVC будет нужен экземпляр класса для обработки входящих запросов
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        //Создание экземпляра DR
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        //устанавливаем связи
        private void AddBindings()
        {
            //связь между интерфейсом и классом
            kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            //kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithPropertyValue("DiscountSize", 50m);
            //Связывание с классом, который требует параметр конструктора
            //Эта техника позволяет внедрять значение в конструктор
            kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithConstructorArgument("discountParam", 50m);
            //Новая связка указывает, что если класс FlexibleDiscountHelper должен использоваться в качестве
            //реализации интерфейса IDiscountHelper, тогда Ninject будет внедрять реализацию в объект
            //LinqValueCalculator.
            kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>().WhenInjectedInto<LinqValueCalculator>();
        }
    }
}