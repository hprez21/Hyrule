using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyrule.Data;
using Hyrule.DemosData.HyruleDataDemo;

namespace Hyrule.DemosData
{
    public class DemosSQLHelper
    {
        public void StartDemo()
        {
            //Utilizando SqlHelper para llenar un DataSet
            var sqlQuery = "SELECT * FROM Customers";                     
            var newDataSet = SqlHelper.ExecuteDataset(
                GlobalData.ConnectionString,
                CommandType.Text, sqlQuery);
            var customers = newDataSet.Tables[0];
            var query = from customer in customers.AsEnumerable()
                        select new
                        {
                            CompanyName = customer.Field<string>("CompanyName"),
                            ContactName = customer.Field<string>("ContactName")
                        };
            foreach (var customer in query)
            {
                Debug.WriteLine(customer.CompanyName);
            }
        }
    }
}
