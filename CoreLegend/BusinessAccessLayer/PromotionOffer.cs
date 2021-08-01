using CoreLegend.EF;
using CoreLegend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLegend.BusinessAccessLayer
{
    public class PromotionOffer
    {
        public Promotion CalculatePromotionOnProduct(int productId, OnlineDataContext context)
        {
            PromotionProduct findPromoProd = context.PromotionProduct.FirstOrDefault(obj => obj.ProductId == productId);
            if (findPromoProd != null)
            {
                //Check if the promo price is overriden by other price. If not then apply the statand promotion
                if (!findPromoProd.IsOverride)
                {
                    Promotion findPromotion = context.Promotion.FirstOrDefault(obj => obj.PromoId == findPromoProd.PromoId);
                    if (findPromotion != null)
                    {
                        return findPromotion;
                    }
                }
            }            
            return null;
        }
        public decimal CalculateSalePriceOfProduct(Product prod)
        {
            decimal price = 0;
            if (prod != null && prod.Promotion != null) {
                price = prod.Price;
                switch (prod.Promotion.Level)
                {
                    case "Product":
                        if (prod.Promotion.Type == PromoType.PriceDiscount)
                        {
                            if(prod.Promotion.CalculateIn== PromoCalculateUnit.DollarAmt)
                                return prod.Price - Convert.ToDecimal(prod.Promotion.PromoDiscount);
                            else if(prod.Promotion.CalculateIn == PromoCalculateUnit.Percent)
                                 return prod.Price- (Convert.ToDecimal(prod.Promotion.PromoDiscount)/100)*10;
                        }
                        if (prod.Promotion.Type == PromoType.PercentDiscount)
                        {
                            if (prod.Promotion.CalculateIn == PromoCalculateUnit.DollarAmt)
                                return prod.Price - Convert.ToDecimal(prod.Promotion.PromoDiscount);
                            else if (prod.Promotion.CalculateIn == PromoCalculateUnit.Percent)
                                return prod.Price - (Convert.ToDecimal(prod.Promotion.PromoDiscount) / 100 * Convert.ToDecimal(prod.Promotion.PromoDiscount));
                        }
                        break;
                        //Default is at Trolley level
                    default:
                        if (prod.Promotion.Type == PromoType.MultiBuyDiscount)
                        {
                            return prod.Price ;
                        }
                        break;
                }                
            }
            return price;
        }
        /// <summary>
        /// This method will check the multibuy discoun at the trolley and provide the discount
        /// </summary>
        /// <param name="crt"></param>
        public int CalculateMultiBuyDiscount(int productId,int SelectedQty, OnlineDataContext context)
        {
            //Get the promotion applied on the product first
            Promotion promotion = this.CalculatePromotionOnProduct(productId, context);
            //If prmotion found, check if promotion type is MultiBuy
            if (promotion != null)
            {
                if (promotion.Type == PromoType.MultiBuyDiscount )
                {
                    //total item after adding the promotional quantity
                    return SelectedQty + SelectedQty * Convert.ToInt16(promotion.PromoDiscount);
                }
            }
            return SelectedQty;
        }
    }
}
