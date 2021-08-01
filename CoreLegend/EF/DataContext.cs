using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreLegend.EF
{
    public class EmployeeDataContext:DbContext
    {
        public EmployeeDataContext(DbContextOptions<EmployeeDataContext> options) : base(options) { }

        public DbSet<Models.Employee> Employee { get; set; }

            //protected override void OnConfiguring(DbContextOptionsBuilder options)
            //=> options.UseSqlite(@"DESKTOP-3OM620D\SQLEXPRESS; Database = HumanResource; Trusted_Connection = True;");
    }
}
