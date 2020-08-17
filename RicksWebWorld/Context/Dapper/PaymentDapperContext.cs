using Dapper;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Context.Dapper
{
	public class PaymentDapperContext : ConnectionHelper, IPaymentMethod
	{
		public bool CreateNewPaymentMethod(Payment p)
		{
			string sqlUserInsert = "INSERT INTO dbo.[PaymentMethod](Name, Description) VALUES(@Name, @Description)";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { Name = p.RetrieveName(), Description = p.RetrieveDesc() });
				return true;
			}
		}

		public bool DeletePaymentMethodById(int Id)
		{
			string sqlDelete = "DELETE FROM dbo.[PaymentMethod] WHERE ID = @Id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					int affectedRows = connection.Execute(sqlDelete, new { Id = Id });
					return true;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return false;
				}
			}
		}

		public List<Payment> RetrieveAllPayments()
		{
			string sqlDetails = "SELECT * FROM dbo.[PaymentMethod]";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					List<Payment> listOfMethods= connection.Query<Payment>(sqlDetails).ToList();
					return listOfMethods;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public Payment RetrievePaymentMethodsById(int id)
		{
			string StoredProcedure = "GetPaymentMethodByID";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					Payment Detail = connection.QueryFirstOrDefault<Payment>(StoredProcedure, new { ID = id }, commandType: CommandType.StoredProcedure);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public bool UpdatePaymentMethodById(Payment p)
		{
			string sqlUserInsert = "UPDATE dbo.[PaymentMethod] SET Name=@Name, Description=@Description WHERE id = @id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new {id=p.RetrieveID(), Name = p.RetrieveName(), Description = p.RetrieveDesc() });
				return true;
			}
		}
	}
}
