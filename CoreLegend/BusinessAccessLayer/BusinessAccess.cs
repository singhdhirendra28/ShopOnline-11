using CoreLegend.EF;
using CoreLegend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLegend.BusinessAccessLayer
{
    public class BusinessAccess : IBusinessAccess
    {
        /// <summary>
        /// Get the payment summary
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public PaymentSummary CalculatePayableAmount(List<CartItem> items, OnlineDataContext context)
        {
            PaymentSummary summary = null;
            if (items?.Count > 0)
            {
                summary = new PaymentSummary();
                decimal subtotal = 0;
                decimal totolDiscount = 0;
                foreach (CartItem crt in items)
                {
                    subtotal += crt.SalePrice;
                    if(crt.Promotion!=null && crt.Promotion.Level== PromoLevel.OnTrolley && crt.Promotion.Type == PromoType.MultiBuyDiscount)
                    {
                        totolDiscount += (crt.Price * crt.SelectedQty);
                    }
                    else 
                        totolDiscount += (crt.Price * crt.SelectedQty) - (crt.SalePrice);
                }

                Promotion promoSpendAndSave = context.Promotion.FirstOrDefault(obj => obj.Level == PromoLevel.OnTrolley && obj.Type == PromoType.SpentAndSave && Convert.ToDecimal(obj.PromoDiscount) > 0);
                if (promoSpendAndSave != null && subtotal>=Convert.ToDecimal(promoSpendAndSave.Limit))
                {
                    summary.TotalDiscount = totolDiscount + Convert.ToDecimal(promoSpendAndSave.PromoDiscount);
                    summary.SubTotal = subtotal - Convert.ToDecimal(promoSpendAndSave.PromoDiscount);
                }
                else {
                    summary.SubTotal = subtotal;
                    summary.TotalDiscount = totolDiscount;
                }
            }            
            return summary;
        }

       

    }
    interface IBusinessAccess
    {
        PaymentSummary CalculatePayableAmount(List<CartItem> items, OnlineDataContext context);   
        
    }
}
