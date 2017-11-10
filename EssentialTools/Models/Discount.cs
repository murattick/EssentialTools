using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
  public interface IDiscountHelper
    {
        decimal ApplyDiscont(decimal totalParam);
    }
    public class DefaultDiscountHelper : IDiscountHelper
    {
        public decimal discountSize;
        public DefaultDiscountHelper(decimal discountParam)
        {
            discountSize = discountParam;
        }
        //public decimal DiscountSize { get; set; }
        public decimal ApplyDiscont(decimal totalParam)
        {
            return (totalParam - (discountSize / 100m * totalParam));
        }
    }
}