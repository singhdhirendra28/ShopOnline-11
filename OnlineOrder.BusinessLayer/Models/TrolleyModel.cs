using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineOrder.BusinessLayer
{    

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int AvailableQty { get; set; }
    }

    public class Promotion
    {
        public int PromoId { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
        public int PromoTypeId { get; set; }
    }
    public class PromoType
    {
        public int PromoTypeId { get; set; }
        public string Type { get; set; }
        public decimal PromoDiscount { get; set; }
    }

    public class PromotionProduct
    {
        public int ProductId { get; set; }
        public int PromoId { get; set; }
        public bool IsOverride { get; set; }
        public string OverrideValue { get; set; }
    }

}

}
