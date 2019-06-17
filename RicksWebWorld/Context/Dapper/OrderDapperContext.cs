using Dapper;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Context.Dapper
{
	public class OrderDapperContext : ConnectionHelper, IOrder
	{
		public bool CreateNewOrder(Order o)
		{
			string sqlUserInsert = "INSERT INTO dbo.[WebWorldOrder] (UserID, Details, SendAddress, SendZipcode, SendPlace) VALUES (@UserId, @Ordernotes, @SendAddress, @SendZipcode, @SendPlace ) SELECT SCOPE_IDENTITY()";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { UserID = o.RetrieveCustomer().ReturnUserId(), Details = o.OrderDetails, SendAddress = o.SendAddress, SendZipcode = o.SendZipcode, SendPlace = o.SendPlace });
				return true;
			}
		}

		public bool DeleteOrderById(int orderId)
		{
			string sqlDelete = "DELETE FROM dbo.[WebWorldOrder] WHERE ID = @orderId";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					int affectedRows = connection.Execute(sqlDelete, new { orderId = orderId });
					return true;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return false;
				}
			}
		}

		public List<Order> RetrieveAllOrders()
		{
			throw new NotImplementedException();
		}

		public Order RetrieveOrderById(int id)
		{
			throw new NotImplementedException();
		}

		public bool UpdateOrderById(Order o)
		{
		
			string sqlUserInsert = "UPDATE dbo.[WebWorldOrder] SET Details=@Details, SendAddress=@SendAddress, SendZipcode=@SendZipcode, SendPlace=@SendPlace WHERE id = @id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { UserID = o.RetrieveCustomer().ReturnUserId(), Details = o.OrderDetails, SendAddress = o.SendAddress, SendZipcode = o.SendZipcode, SendPlace = o.SendPlace });
				return true;
			}
		}
	}
}
