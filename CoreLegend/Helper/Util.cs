using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLegend.Helper
{
    public class Util : IUtil
    {
        //public IConfiguration Configuration { get; }

        //public Util(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        //public bool IsCacheEnabled
        //{
        //    get
        //    {
        //        //return true;
        //        return (bool)Configuration.Get("IsCacheEnabled");
        //        //return true;
        //        //IConfiguration
        //    }
        //}
    }
    public interface IUtil
    {
        //bool IsCacheEnabled { get; }
    }
}
