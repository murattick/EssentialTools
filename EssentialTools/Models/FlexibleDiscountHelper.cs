using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class FlexibleDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscont(decimal totalParam)
        {
            //применяет различные скидки, исходя из величины общей суммы, на
            //которую распространяется скидка
            decimal discount = totalParam > 100 ? 70 : 25;
            return (totalParam - (discount / 100 * totalParam));
        }
    }
}