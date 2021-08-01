using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreLegend.BusinessAccessLayer;
using CoreLegend.EF;
using CoreLegend.Helper;
using CoreLegend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreLegend.Controllers
{
    [Produces("application/json")]
    [Route("api/Trolley")]
    public class TrolleyController : Controller
    {
        private readonly OnlineDataContext _context;
        private readonly BusinessAccess _businessEngine;
        private int custId = 1;
        public TrolleyController(OnlineDataContext context)
        {
            _context = context;
            _businessEngine = new BusinessAccess();
        }
                       
        /// <summary>
        /// Delete the particular item from the cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteItem/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            ApiResponse apiResponse = null;
            
            try
            {
                //Check the id, if blank send the invalid error response to the consumer
                if (id <= 0)
                {
                    apiResponse = new ApiResponse(ErrorMappingCode.InvalidRequestInput,
                        new ApiError(ErrorMappingCode.InvalidRequestInput, CommonStatusHelper.ServiceStatusDesctiption("1001")));

                    return StatusCode((int)HttpStatusCode.BadRequest, apiResponse);
                }
                IEnumerable<Cart> cartList = _context.Cart.Where(obj => obj.CustomerId == custId && obj.ProductId == id);
                _context.Cart.RemoveRange(cartList);

                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.OK, new ApiResponse(ErrorMappingCode.Success, cartList));
            }
            catch (Exception ex)
            {
                string trace = ex.Message;
                apiResponse = new ApiResponse(ErrorMappingCode.InvalidRequestInput,
                      new ApiError(ErrorMappingCode.InvalidRequestInput, CommonStatusHelper.ServiceStatusDesctiption(ErrorMappingCode.UnknownError), "Unhandled exception occured while processing the request"));

                return StatusCode((int)HttpStatusCode.InternalServerError, apiResponse);
            }
        }

        /// <summary>
        /// This is api to add the item to the cart
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] Quantity quantity)
        {
            ApiResponse apiResponse = null;            
            try
            {
               //Validate the input object
                if(!ModelState.IsValid || (quantity !=null && quantity.SelectedQty <= 0))
                {                    
                    apiResponse = new ApiResponse(ErrorMappingCode.InvalidRequestInput, 
                        new ApiError(ErrorMappingCode.InvalidRequestInput,CommonStatusHelper.ServiceStatusDesctiption("1001"), "Check the input field/s"));

                    return StatusCode((int)HttpStatusCode.BadRequest, apiResponse);
                }
                //Check if the item already in the cart just increase the quantity
                List<Cart> cartList = _context.Cart.Where(obj => obj.CustomerId == custId && obj.ProductId==quantity.ProductId).ToList();
                if (cartList != null && cartList.Count() > 0)
                {
                    //update the existing quantity
                    Cart car=cartList[0];
                    car.SelectedQty = quantity.SelectedQty;                    
                    _context.Entry(car).State = EntityState.Modified;
                }
                else
                {
                    Cart item = new Cart() { CustomerId = custId, ProductId = quantity.ProductId, SelectedQty = quantity.SelectedQty };                  
                    _context.Cart.Add(item);
                }
                await _context.SaveChangesAsync();

                //Return the success response to the consumer                      
                return StatusCode((int)HttpStatusCode.OK, new ApiResponse(ErrorMappingCode.Success, quantity));
            }
            catch(Exception ex)
            {
                //Log the trace in the logger for debugging purpose
                string trace = ex.Message;                             
                apiResponse = new ApiResponse(ErrorMappingCode.InvalidRequestInput,
                      new ApiError(ErrorMappingCode.InvalidRequestInput, CommonStatusHelper.ServiceStatusDesctiption(ErrorMappingCode.UnknownError), "Unhandled exception occured while processing the request"));
                return StatusCode((int)HttpStatusCode.InternalServerError, apiResponse);
            }
        }
        /// <summary>
        /// Get all the products
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            ApiResponse apiResponse = null;
            PromotionOffer offer = null;
            try
            {
                List<Product> prodList = _context.Product.Select(obj => obj).ToList();

                if (prodList == null)
                {
                    //resource not found
                    apiResponse = new ApiResponse(ErrorMappingCode.CartIsEmpty,
                       new ApiError(ErrorMappingCode.ProductNotAvailable, CommonStatusHelper.ServiceStatusDesctiption("1003"), "No product found"));

                    return StatusCode((int)HttpStatusCode.NotFound, apiResponse);

                }

                //check if any promotion available for the products
                offer = new PromotionOffer();
                foreach(Product prod in prodList)
                {
                    prod.Promotion = offer.CalculatePromotionOnProduct(prod.ProductId, _context);
                }
                
                return StatusCode((int)HttpStatusCode.OK, new ApiResponse(ErrorMappingCode.Success, prodList));
            }
            catch (Exception ex)
            {
                string trace = ex.Message;
                apiResponse = new ApiResponse(ErrorMappingCode.InvalidRequestInput,
                      new ApiError(ErrorMappingCode.InvalidRequestInput, CommonStatusHelper.ServiceStatusDesctiption(ErrorMappingCode.UnknownError), "Unhandled exception occured while processing the request"));

                return StatusCode((int)HttpStatusCode.InternalServerError, apiResponse);
            }
        }

        private void MapPromotionOnProduct(List<Product> prodList)
        {
            if (prodList != null)
            {
                foreach(Product prod in prodList)
                {

                }
            }
        }

        [HttpGet("GetProductDetail/{prodId}")]
        public IActionResult GetProductDetail(int prodId)
        {
            ApiResponse apiResponse = null;
            PromotionOffer offer = null;
            try
            {
                //Query the product
                Product searchProduct = _context.Product.FirstOrDefault(obj => obj.ProductId == prodId);

                if (searchProduct == null)
                {
                    //resource not found
                    apiResponse = new ApiResponse(ErrorMappingCode.CartIsEmpty,
                       new ApiError(ErrorMappingCode.ProductNotAvailable, CommonStatusHelper.ServiceStatusDesctiption("1003"), "No product found"));

                    return StatusCode((int)HttpStatusCode.NotFound, apiResponse);

                }                
                //check if any promotion available for the products
                offer = new PromotionOffer();                
                searchProduct.Promotion = offer.CalculatePromotionOnProduct(searchProduct.ProductId, _context);
                //Calculate sale price of the product by applying the discount
                if (searchProduct.Promotion != null)
                {
                    searchProduct.SalePrice = offer.CalculateSalePriceOfProduct(searchProduct);
                }
                
                return StatusCode((int)HttpStatusCode.OK, new ApiResponse(ErrorMappingCode.Success, searchProduct));
            }
            catch (Exception ex)
            {
                string trace = ex.Message;
                apiResponse = new ApiResponse(ErrorMappingCode.InvalidRequestInput,
                      new ApiError(ErrorMappingCode.InvalidRequestInput, CommonStatusHelper.ServiceStatusDesctiption(ErrorMappingCode.UnknownError), "Unhandled exception occured while processing the request"));

                return StatusCode((int)HttpStatusCode.InternalServerError, apiResponse);
            }
        }
        /// <summary>
        /// This is the api to get the list of items added into the cart
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCart")]
        public IActionResult GetCart()
        {
            ApiResponse apiResponse = null;
            try
            {                
                List<Cart> cartList = _context.Cart.Where(obj => obj.CustomerId == custId).ToList(); 

                if (cartList == null)
                {
                    //resource not found
                    apiResponse= new ApiResponse(ErrorMappingCode.CartIsEmpty,
                       new ApiError(ErrorMappingCode.CartIsEmpty, CommonStatusHelper.ServiceStatusDesctiption("1003"), "You have not added any item to the cart"));

                    return StatusCode((int)HttpStatusCode.NotFound, apiResponse);
                    
                }               
                //Get the list of the items added into cart after the mapping done for productname, sale price, price etc.
                List<CartItem> items = this.GetProductsAddedInCart(cartList,custId);
                //Calcualte the payment summary for the items added into the cart
                PaymentSummary paymentSummary= _businessEngine.CalculatePayableAmount(items,_context);
                //prepare the trolley DTO to send the response back to consumer
                TrolleyDto trolleyDto = new TrolleyDto(items, paymentSummary);
                return StatusCode((int)HttpStatusCode.OK, new ApiResponse(ErrorMappingCode.Success, trolleyDto));              
            }
            catch (Exception ex)
            {
                string trace = ex.Message;
                apiResponse = new ApiResponse(ErrorMappingCode.InvalidRequestInput,
                      new ApiError(ErrorMappingCode.InvalidRequestInput, CommonStatusHelper.ServiceStatusDesctiption(ErrorMappingCode.UnknownError), "Unhandled exception occured while processing the request"));

                return StatusCode((int)HttpStatusCode.InternalServerError, apiResponse);
            }
        }
        /// <summary>
        /// This will give the list of products added into the cart
        /// </summary>
        /// <param name="cartItems"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private List<CartItem> GetProductsAddedInCart(List<Cart> cartItems,int customerId)
        {
            List<CartItem> items = null;
            CartItem itm = null;
            PromotionOffer offer = null;
            if (cartItems != null)
            {
                items = new List<CartItem>();
                //create the object for promo offer
                offer = new PromotionOffer();
                foreach (Cart crt in cartItems)
                {   
                    //Get the prodcut entity to map the product price and productname
                    Product prod = _context.Product.First(obj => obj.ProductId == crt.ProductId && crt.CustomerId==customerId);
                    //
                    if (prod != null)
                    {                                                
                        itm = new CartItem
                        {
                            ProductId = prod.ProductId,
                            ProductName = prod.ProductName,
                            Price = prod.Price,                            
                            SelectedQty = crt.SelectedQty,
                            ImageName=prod.ImageName
                        };
                        //Get the product promotion
                        itm.Promotion=prod.Promotion = offer.CalculatePromotionOnProduct(crt.ProductId, _context);
                        if (itm.Promotion != null)
                        {
                            //update the product quantity to the quantity added in the cart
                            prod.AvailableQty = crt.SelectedQty;
                            itm.SalePrice = (crt.SelectedQty) * (offer.CalculateSalePriceOfProduct(prod));                           
                        }
                        else
                            itm.SalePrice = crt.SelectedQty * itm.Price;
                    }
                    items.Add(itm);
                }
            }
            return items;
        }
    }
}
