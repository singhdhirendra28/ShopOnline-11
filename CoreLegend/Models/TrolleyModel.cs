using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLegend.Models
{

    public class ProductCommonAttribute
    {
        /// <summary>
        /// productid
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// product name 
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Price of the product
        /// </summary>
        public decimal Price { get; set; }
  

    }
    public class Product:ProductCommonAttribute
    {
        /// <summary>
        /// Quantity of the selected product
        /// </summary>
        public int AvailableQty { get; set; }
        /// <summary>
        /// Image Name
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// Sale Price of the product
        /// </summary>
        [NotMapped]
        public decimal SalePrice { get; set; }
        /// <summary>
        /// Product promotion
        /// </summary>
        [NotMapped]
        public Promotion Promotion { get; set; }

    }
    public class CartItem: ProductCommonAttribute
    {
        /// <summary>
        /// This is the quantity added for a particular product
        /// </summary>
        public int SelectedQty { get; set; }
        /// <summary>
        /// Sale Price of the product
        /// </summary>
        public decimal SalePrice { get; set; }
        /// <summary>
        /// UniqueId for the item which can help in sorting of the list
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Image Name
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// Product promotion
        /// </summary>        
        public Promotion Promotion { get; set; }
    }

    public class Promotion
    {
        /// <summary>
        /// Id of the promo
        /// </summary>
        [Key]
        public int PromoId { get; set; }
        /// <summary>
        /// Name of the promotion
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Definition of the promo
        /// </summary>
        public string Definition { get; set; }
        /// <summary>
        /// Product,Trolley
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// PriceDiscount,PercentDiscount,MultiBuyDiscount,SplitDiscount(Buy one get second in half)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// DollarAmt,Percent,Quantity
        /// </summary>
        public string CalculateIn { get; set; }
        /// <summary>
        /// Discount amount/unit
        /// </summary>
        public string PromoDiscount { get; set; }
        /// <summary>
        /// This is the limit for discount
        /// </summary>
        public string Limit { get; set; }
    }

    public class PromotionProduct
    {
        /// <summary>
        /// Id of the promotion
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// ProductId
        /// </summary>        
        public int ProductId { get; set; }
        /// <summary>
        /// PromoId
        /// </summary>
        public int PromoId { get; set; }
        /// <summary>
        /// If price is overriden
        /// </summary>
        public bool IsOverride { get; set; }
        /// <summary>
        /// New promo value
        /// </summary>
        public string OverrideValue { get; set; }
    }
    
    public class Cart
    {
        /// <summary>
        /// Id of the cart item
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// customerid of the customer
        /// </summary>
        public long CustomerId { get; set; }
        /// <summary>
        /// prodcutId
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// selected quantity
        /// </summary>
        public int SelectedQty { get; set; }

    }
    
    public class Quantity
    {
        /// <summary>
        /// productId
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// selected quantity
        /// </summary>
        public int SelectedQty { get; set; }
    }

    public class PaymentSummary
    {
        /// <summary>
        /// Cart total
        /// </summary>
        public decimal SubTotal { get; set; }
        /// <summary>
        /// Payable amount
        /// </summary>
        public decimal Payable { get; set; }
        /// <summary>
        /// Total discount applied at the trolley level
        /// </summary>
        public decimal TotalDiscount { get; set; }
        public PaymentSummary() { }

        public PaymentSummary(decimal subtotal, decimal totalDiscount,decimal payable)
        {
            this.SubTotal = subtotal;
            this.TotalDiscount = totalDiscount;
            this.Payable = payable;
        }
    }
    public class TrolleyDto
    {
        /// <summary>
        /// List of items returned for the consumer
        /// </summary>
        public List<CartItem> Items { get; set; }
        /// <summary>
        /// payment summary
        /// </summary>
        public PaymentSummary PaymentSummary { get; set; }
        /// <summary>
        /// Trolley count
        /// </summary>
        public int TotalItems { get; set; }
        public TrolleyDto(List<CartItem> items, PaymentSummary summary)
        {
            this.Items = items;
            this.PaymentSummary = summary;
            this.TotalItems= this.GetTrolleyCount(items);
        }
        private int GetTrolleyCount(List<CartItem> items)
        {
            int count = 0;
            if (items != null)
            {
                foreach(CartItem crt in items)
                {
                    count += crt.SelectedQty;
                }
            }
            return count;
        }
    }
    /// <summary>
    /// Calculation unit
    /// </summary>
    public static class PromoCalculateUnit
    {
        /// <summary>
        /// <summary>DollarAmt
        /// 
        /// </summary>
        public static string DollarAmt = "DollarAmt";
        /// <summary>
        /// Percent
        /// </summary>
        public static string Percent = "Percent";
        /// <summary>
        /// Quantity
        /// </summary>
        public static string Quantity = "Quantity";
    }

    /// <summary>
    /// PromoType
    /// </summary>
    public static class PromoType
    {
        /// <summary>
        /// PriceDiscount
        /// </summary>
        public static string PriceDiscount = "PriceDiscount";
        /// <summary>
        /// PercentDiscount
        /// </summary>
        public static string PercentDiscount = "PercentDiscount";
        /// <summary>
        /// MultiBuyDiscount
        /// </summary>
        public static string MultiBuyDiscount = "MultiBuyDiscount";
        /// <summary>
        /// SpentAndSave
        /// </summary>
        public static string SpentAndSave = "SpentAndSave";        
    }
    /// <summary>
    /// PromoLevel
    /// </summary>
    public static class PromoLevel
    {
        /// <summary>
        /// OnProduct
        /// </summary>
        public static string OnProduct = "Product";
        /// <summary>
        /// Trolley
        /// </summary>
        public static string OnTrolley = "Trolley";
        
    }
}
