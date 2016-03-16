using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyrule.DemosData.HyruleDataDemo
{
    public static class GlobalData
    {
        public static string ConnectionString { get; set; }
        static GlobalData()
        {
            ConnectionString = @"Data Source=DESKTOP-LKL3FGR\SQLSERVER;Initial Catalog=Northwind;Integrated Security=True";
        }
    }
}
