using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hyrule.Data;
using Hyrule.DemosData.HyruleDataDemo;
using Microsoft.SqlServer.Server;

namespace Hyrule.DemosData
{
    public class DemosSQLHelper
    {
        public void StartDemo()
        {
            StartScalarDemo();
        }

        private void StartDataSetDemo()
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

        private void StartDataReaderDemo()
        {
            //Utilizando SqlHelper para llenar un DataReader
            var sqlQuery = "SELECT * FROM Customers";
            using (var newDataReader = SqlHelper.ExecuteReader(
                GlobalData.ConnectionString,
                CommandType.Text, sqlQuery))
            {
                while (newDataReader.Read())
                {
                    var customerInfo = string.Format(
                        "Company Name: {0}, ContactName: {1}",
                        newDataReader.GetString(1),
                        newDataReader.GetString(2));
                    Debug.WriteLine(customerInfo);
                }
            }
        }

        private void StartDataReaderDemo2()
        {
            //Utilizando SqlHelper para llenar un DataReader a través de un stored procedure
            var sqlQuery = "Sales by Year";
            var param1 = new SqlParameter {ParameterName = "@Beginning_Date", Value = new DateTime(1995, 1, 1)};
            var param2 = new SqlParameter {ParameterName = "@Ending_Date", Value = new DateTime(1997, 1, 1)};
            SqlParameter[] parameters = new[] {param1, param2};
            using (var newDataReader = SqlHelper.ExecuteReader(
                GlobalData.ConnectionString,
                CommandType.StoredProcedure, sqlQuery, parameters))
            {
                while (newDataReader.Read())
                {
                    var customerInfo = string.Format(
                        "Order ID: {0}, Subtotal{1}",
                        newDataReader.GetInt32(1),
                        newDataReader.GetDecimal(2));
                    Debug.WriteLine(customerInfo);
                }
            }
        }

        private void StartScalarDemo()
        {
            var query = "SELECT CompanyName FROM Customers WHERE CustomerID = 'BLAUS'";
            var result = SqlHelper.ExecuteScalar(GlobalData.ConnectionString, CommandType.Text, query);
            Debug.WriteLine(result);                               
        }
        private void StartNonQueryDemo1()
        {
            String query = "";
            query = query + "INSERT INTO [dbo].[Customers] " + "\n";
            query = query + "           ([CustomerID] " + "\n";
            query = query + "           ,[CompanyName] " + "\n";
            query = query + "           ,[ContactName] " + "\n";
            query = query + "           ,[ContactTitle] " + "\n";
            query = query + "           ,[Address] " + "\n";
            query = query + "           ,[City] " + "\n";
            query = query + "           ,[Region] " + "\n";
            query = query + "           ,[PostalCode] " + "\n";
            query = query + "           ,[Country] " + "\n";
            query = query + "           ,[Phone] " + "\n";
            query = query + "           ,[Fax]) " + "\n";
            query = query + "     VALUES " + "\n";
            query = query + "           ('XYZ' " + "\n";
            query = query + "           ,'HECTORS COMPANY' " + "\n";
            query = query + "           ,'HÉCTOR PÉREZ' " + "\n";
            query = query + "           ,'M.C.C' " + "\n";
            query = query + "           ,'AV. HOGWARTS' " + "\n";
            query = query + "           ,'MAGICLAND' " + "\n";
            query = query + "           ,'REGIONLAND' " + "\n";
            query = query + "           ,'99999' " + "\n";
            query = query + "           ,'UK' " + "\n";
            query = query + "           ,'2222' " + "\n";
            query = query + "           ,'2222')";

            var result = SqlHelper.ExecuteNonQuery(GlobalData.ConnectionString, CommandType.Text, query);
            Debug.WriteLine("Registros agregados: {0}", result);
        }

        private void StartNonQueryDemo2()
        {
            String query = "DELETE FROM [Customers] WHERE [CustomerID] = 'XYZ'";            

            var result = SqlHelper.ExecuteNonQuery(GlobalData.ConnectionString, CommandType.Text, query);
            Debug.WriteLine("Filas eliminadas: {0}", result);
        }
    }
}