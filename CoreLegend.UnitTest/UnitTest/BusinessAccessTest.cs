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
    public class BusinessAccessTest
    {
        static DbContextOptions<OnlineDataContext> options = new DbContextOptions<OnlineDataContext>();
        OnlineDataContext context = new OnlineDataContext(options);
        BusinessAccess access = new BusinessAccess();        


        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            List<CartItem> list = this.GetMockCartList();
            //Act 
            var result=access.CalculatePayableAmount(list, context);
            //Assert
            Assert.IsNull(result);
        }
        private List<CartItem> GetMockCartList()
        {
            List<CartItem> list = new List<CartItem>();
            list.Add(new CartItem() { Id = 1, ImageName = "test.pnj", Price = 12, ProductId = 1000, ProductName = "test", Promotion = null, SalePrice = 098, SelectedQty = 1 });
            list.Add(new CartItem() { Id = 2, ImageName = "test.pnj", Price = 12, ProductId = 1001, ProductName = "test", Promotion = null, SalePrice = 098, SelectedQty = 1 });
            list.Add(new CartItem() { Id = 3, ImageName = "test.pnj", Price = 12, ProductId = 1002, ProductName = "test", Promotion = null, SalePrice = 098, SelectedQty = 1 });
            return list;
        }
    }
}
