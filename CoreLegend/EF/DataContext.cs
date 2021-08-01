using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLegend.Models;
using Microsoft.EntityFrameworkCore;


namespace CoreLegend.EF
{
    public class OnlineDataContext : DbContext
    {
        public OnlineDataContext(DbContextOptions<OnlineDataContext> options) : base(options) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<PromotionProduct> PromotionProduct { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
    }

}
