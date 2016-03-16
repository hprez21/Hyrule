using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyrule.Data;

namespace Hyrule.DemosData
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var demo1 = new DemosSQLHelper();
            demo1.StartDemo();
        }
    }
}
