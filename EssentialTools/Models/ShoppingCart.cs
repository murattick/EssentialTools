using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class ShoppingCart
    {
        private IValueCalculator calc;

        public ShoppingCart(IValueCalculator calcParam)
        {
            calc = calcParam;
        }

        public IEnumerable<Product> Products { get; set; }

        //добавляем коллекцию продуктов для подсчёта их суммы
        public decimal CalculateProductTotal()
        {
            return calc.ValueProduct(Products);
        }
    }
}