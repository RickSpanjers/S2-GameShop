using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;

namespace RicksWebWorld.Context.Release
{
	public class OrderMssqlContext : ConnectionHelper, IOrder
	{
		public bool CreateNewOrder(Order o)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "INSERT INTO dbo.[WebWorldOrder] (UserID, Details, SendAddress, SendZipcode, SendPlace) VALUES (@UserId, @Ordernotes, @SendAddress, @SendZipcode, @SendPlace ) SELECT SCOPE_IDENTITY()";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@UserId", o.RetrieveCustomer().ReturnUserId());
				cmd.Parameters.AddWithValue("@Ordernotes", o.OrderDetails);
				cmd.Parameters.AddWithValue("@SendAddress", o.SendAddress);
				cmd.Parameters.AddWithValue("@SendZipcode", o.SendZipcode);
				cmd.Parameters.AddWithValue("@SendPlace", o.SendPlace);

				//Return the id of the last inserted Order
				decimal orderId = (decimal)cmd.ExecuteScalar();
				InsertProductsIntoOrder(o.RetrieveItemsInOrder(), orderId);
				cnn.Close();
				return true;

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			return false;
		}

		public List<Order> RetrieveAllOrders()
		{
			List<Order> listOfOrders = new List<Order>();
			UserRepository userRepo = new UserRepository(new UserMssqlContext());

			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[WebWorldOrder]";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int orderId = Convert.ToInt32(dr["id"]);
					int userId = Convert.ToInt32(dr["UserID"]);
					string ordernotes = dr["Details"].ToString();
					string sendAddress = dr["SendAddress"].ToString();
					string sendZipcode = dr["SendZipcode"].ToString();
					string sendPlace = dr["SendPlace"].ToString();

					User user = userRepo.RetrieveUserById(userId);
					List<CartItem> cartItems = RetrieveProductsFromOrderByOrderId(orderId);
					Order order = new Order(user, ordernotes, orderId, cartItems);
					order.SendPlace = sendPlace;
					order.SendAddress = sendAddress;
					order.SendZipcode = sendZipcode;

					listOfOrders.Add(order);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}

			return listOfOrders;
		}

		public bool DeleteOrderById(int orderId)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "DELETE FROM dbo.[WebWorldOrder] WHERE ID = @orderId";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@orderId", orderId);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return false;
		}

		public bool UpdateOrderById(Order o)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "UPDATE dbo.[WebWorldOrder] SET Details=@Details, SendAddress=@SendAddress, SendZipcode=@SendZipcode, SendPlace=@SendPlace WHERE id = @id";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);

				newCmd.Parameters.AddWithValue("@id", o.RetrieveOrderId());
				newCmd.Parameters.AddWithValue("@SendAddress", o.SendAddress);
				newCmd.Parameters.AddWithValue("@SendZipcode", o.SendZipcode);
				newCmd.Parameters.AddWithValue("@SendPlace", o.SendPlace);
				newCmd.Parameters.AddWithValue("@Details", o.OrderDetails);
				newCmd.ExecuteNonQuery();

				InsertProductsIntoOrder(o.RetrieveItemsInOrder(), o.RetrieveOrderId());
				cnn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return false;
		}

		public Order RetrieveOrderById(int id)
		{
			Order order = new Order(null, null, 1, null);
			UserRepository userRepo = new UserRepository(new UserMssqlContext());

			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				SqlCommand cmd = CreateSQLCommandStoredProcedure("GetOrderByID", cnn);	
				cmd.Parameters.AddWithValue("@ID", id);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int orderId = Convert.ToInt32(dr["id"]);
					int userId = Convert.ToInt32(dr["UserId"]);
					string ordernotes = dr["Details"].ToString();
					string sendAddress = dr["SendAddress"].ToString();
					string sendZipcode = dr["SendZipcode"].ToString();
					string sendPlace = dr["SendPlace"].ToString();

					User user = userRepo.RetrieveUserById(userId);
					List<CartItem> cartItems = RetrieveProductsFromOrderByOrderId(orderId);
					order = new Order(user, ordernotes, orderId, cartItems);
					order.SendPlace = sendPlace;
					order.SendAddress = sendAddress;
					order.SendZipcode = sendZipcode;
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}

			return order;
		}


		public bool InsertProductsIntoOrder(List<CartItem> items, decimal orderId)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				foreach (CartItem cartItem in items)
				{
					ProductRepository productRepo = new ProductRepository(new ProductMssqlContext());
					Product product = productRepo.RetrieveProductByName(cartItem.Product.ProductName);
					string query = "INSERT INTO dbo.[ProductOrder] (Order_ID, Product_ID, Amount, Price) VALUES (@orderId, @ProductId, @Amount, @Price)";
					SqlCommand cmd = CreateSQLCommandText(query, cnn);
					cmd.Parameters.AddWithValue("@orderId", orderId);
					cmd.Parameters.AddWithValue("@ProductId", product.RetrieveProductId());
					cmd.Parameters.AddWithValue("@Amount", cartItem.Quantity);

					if (cartItem.Product.ProductDiscount < cartItem.Product.ProductPrice)
					{
						cmd.Parameters.AddWithValue("@Price", cartItem.Product.ProductDiscount);
					}
					else
					{
						cmd.Parameters.AddWithValue("@Price", cartItem.Product.ProductPrice);
					}

					cmd.ExecuteNonQuery();
				}

				cnn.Close();
				return true;

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}

		public List<CartItem> RetrieveProductsFromOrderByOrderId(int orderId)
		{
			List<CartItem> listOfCartItems = new List<CartItem>();
			ProductRepository productRepo = new ProductRepository(new ProductMssqlContext());

			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[ProductOrder] WHERE Order_ID = @orderId";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@orderId", orderId);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int productId = Convert.ToInt32(dr["Product_ID"]);
					int amount = Convert.ToInt32(dr["Amount"]);
					Product product = productRepo.RetrieveProductById(productId);
					product.ProductPrice = Convert.ToDecimal(dr["Price"]);

					CartItem cartItem = new CartItem(1);
					cartItem.Product = product;
					cartItem.Quantity = amount;

					listOfCartItems.Add(cartItem);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}

			return listOfCartItems;
		}
	}
}