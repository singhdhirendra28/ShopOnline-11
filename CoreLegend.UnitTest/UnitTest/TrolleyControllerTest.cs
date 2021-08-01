using CoreLegend.BusinessAccessLayer;
using CoreLegend.Controllers;
using CoreLegend.EF;
using CoreLegend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CoreLegend.UnitTest
{
    [TestClass]
    public class TrolleyControllerTest
    {
        static DbContextOptions<OnlineDataContext> options = new DbContextOptions<OnlineDataContext>();
        static OnlineDataContext context = new OnlineDataContext(options);
        BusinessAccess access = new BusinessAccess();
        TrolleyController trolley = new TrolleyController(context);

        /// <summary>
        /// Create the mock object
        /// </summary>
        /// <returns></returns>
        private List<CartItem> GetMockCartList()
        {
            List<CartItem> list = new List<CartItem>();
            list.Add(new CartItem() { Id = 1, ImageName = "test.pnj", Price = 12, ProductId = 1000, ProductName = "test", Promotion = null, SalePrice = 098, SelectedQty = 1 });
            list.Add(new CartItem() { Id = 2, ImageName = "test.pnj", Price = 12, ProductId = 1001, ProductName = "test", Promotion = null, SalePrice = 098, SelectedQty = 1 });
            list.Add(new CartItem() { Id = 3, ImageName = "test.pnj", Price = 12, ProductId = 1002, ProductName = "test", Promotion = null, SalePrice = 098, SelectedQty = 1 });
            return list;
        }
        [TestMethod]
        public void AddToCart()
        {
            //Arracnge
            Quantity qty = new Quantity() { ProductId = 1000, SelectedQty = 2 };
            //Act
            var result =trolley.AddToCart(qty);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetProducts()
        {
            //Act
            var result = trolley.GetProducts();
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetProductDetail()
        {
            //Arrange
            int prodId = 1000;
            //Act
            var result = trolley.GetProductDetail(prodId);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetCart()
        {
            //Act
            var result = trolley.GetCart();
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void DeleteCart()
        {
            //Arrange
            int prodId = 1000;
            //Act
            var result = trolley.DeleteItem(prodId);
            //Assert
            Assert.IsNull(result);
        }
    }
}
