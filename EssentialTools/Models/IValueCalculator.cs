using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    //добавляем интерфейс, поможет сломать сильную связь с ShoppingCart & LinqValueCalculator
    public interface IValueCalculator
    {
        decimal ValueProduct(IEnumerable<Product> products);
    }
}