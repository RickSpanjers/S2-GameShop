using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;

namespace RicksWebWorld.Context.Release
{
	public class ProductMssqlContext : ConnectionHelper, IProduct
	{
		public List<Product> RetrieveAllProducts()
		{
			List<Product> listOfProducts = new List<Product>();
			SpecialOfferRepository offerRepo = new SpecialOfferRepository(new SpecialOfferMssqlContext());
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[Product]";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				SqlDataReader dr = newCmd.ExecuteReader();
				while (dr.Read())
				{
					int productId = Convert.ToInt32(dr["ID"]);
					string productName = dr["name"].ToString();
					SpecialOffer offer = offerRepo.RetrieveOfferByProductID(productId);

					if (offer.RetrieveOfferId() != -1)
					{
						SpecialOffer product = new SpecialOffer(offer.RetrieveOfferId(), offer.RetrieveOfferPrice(), productId, productName);
						product.ProductAmount = Convert.ToInt32(dr["Amount"]);
						product.ProductDesc = dr["Description"].ToString();
						product.ProductDiscount = Convert.ToDecimal(dr["Discount_Price"]);
						product.ProductPrice = Convert.ToDecimal(dr["Price"]);
						product.ProductStatus = dr["Status"].ToString();
						product.ImageUrl = dr["ImageURL"].ToString();
						product.BtwPercentage = Convert.ToInt32(dr["BTWPercentage"]);
						product.ProductInStock = Convert.ToInt32(dr["Instock"]);
						product.StartTime = offer.StartTime;
						product.EndTime = offer.EndTime;

						listOfProducts.Add(product);
					}
					else
					{
						Product product = new Product(productId, productName);
						product.ProductAmount = Convert.ToInt32(dr["Amount"]);
						product.ProductDesc = dr["Description"].ToString();
						product.ProductDiscount = Convert.ToDecimal(dr["Discount_Price"]);
						product.ProductPrice = Convert.ToDecimal(dr["Price"]);
						product.ProductStatus = dr["Status"].ToString();
						product.ImageUrl = dr["ImageURL"].ToString();
						product.BtwPercentage = Convert.ToInt32(dr["BTWPercentage"]);
						product.ProductInStock = Convert.ToInt32(dr["Instock"]);

						listOfProducts.Add(product);
					}
				}
				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return listOfProducts;
		}


		public bool DeleteProductById(int productId)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "DELETE FROM dbo.[Product] WHERE ID = @Id";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@Id", productId);
				cmd.ExecuteNonQuery();
				cnn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return false;
		}

		public bool CreateNewProduct(Product p)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "INSERT INTO dbo.[Product] (name, Description, Instock, Price, Discount_Price, Status, Amount, ImageURL, BTWPercentage) VALUES (@name, @Description, @Instock, @Price, @Discount, @Status, @Amount, @ImageUrl, @BTW)";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@name", p.ProductName);
				newCmd.Parameters.AddWithValue("@Amount", p.ProductAmount);
				newCmd.Parameters.AddWithValue("@Description", p.ProductDesc);
				newCmd.Parameters.AddWithValue("@Instock", p.ProductInStock);
				newCmd.Parameters.AddWithValue("@Price", p.ProductPrice);
				newCmd.Parameters.AddWithValue("@Discount", p.ProductDiscount);
				newCmd.Parameters.AddWithValue("@Status", p.ProductStatus);
				newCmd.Parameters.AddWithValue("@ImageUrl", p.ImageUrl);
				newCmd.Parameters.AddWithValue("@BTW", p.BtwPercentage);

				newCmd.ExecuteNonQuery();
				cnn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return false;
		}


		public bool UpdateProductById(Product p)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "UPDATE dbo.[Product] SET name=@name, Amount=@Amount, Description=@Description, Instock=@Instock, Price=@Price, Discount_Price=@Discount, Status=@Status, ImageURL=@ImageUrl, BTWPercentage=@BTW WHERE ID = @Id";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);

				newCmd.Parameters.AddWithValue("@Id", p.RetrieveProductId());
				newCmd.Parameters.AddWithValue("@name", p.ProductName);
				newCmd.Parameters.AddWithValue("@Amount", p.ProductAmount);
				newCmd.Parameters.AddWithValue("@Description", p.ProductDesc);
				newCmd.Parameters.AddWithValue("@Instock", p.ProductInStock);
				newCmd.Parameters.AddWithValue("@Price", p.ProductPrice);
				newCmd.Parameters.AddWithValue("@Discount", p.ProductDiscount);
				newCmd.Parameters.AddWithValue("@Status", p.ProductStatus);
				newCmd.Parameters.AddWithValue("@ImageUrl", p.ImageUrl);
				newCmd.Parameters.AddWithValue("@BTW", p.BtwPercentage);

				newCmd.ExecuteNonQuery();
				cnn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return false;
		}


		public Product RetrieveProductById(int id)
		{
			Product product = new Product(1, null);

			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				SqlCommand cmd = CreateSQLCommandStoredProcedure("GetProductByID", cnn);
				cmd.Parameters.AddWithValue("@ID", id);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int productId = Convert.ToInt32(dr["ID"]);
					string productName = dr["name"].ToString();

					product = new Product(productId, productName);
					product.ProductAmount = Convert.ToInt32(dr["Amount"]);
					product.ProductDesc = dr["Description"].ToString();
					product.ProductDiscount = Convert.ToDecimal(dr["Discount_Price"]);
					product.ProductPrice = Convert.ToDecimal(dr["Price"]);
					product.ProductStatus = dr["Status"].ToString();
					product.ImageUrl = dr["ImageURL"].ToString();
					product.BtwPercentage = Convert.ToInt32(dr["BTWPercentage"]);
					product.ProductInStock = Convert.ToInt32(dr["Instock"]);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return product;
		}

		public Product RetrieveProductByName(string name)
		{
			Product product = new Product(1, null);
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[Product] WHERE name=@name";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);	
				cmd.Parameters.AddWithValue("@name", name);
	
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int productId = Convert.ToInt32(dr["ID"]);
					string productName = dr["name"].ToString();

					product = new Product(productId, productName);
					product.ProductAmount = Convert.ToInt32(dr["Amount"]);
					product.ProductDesc = dr["Description"].ToString();
					product.ProductDiscount = Convert.ToDecimal(dr["Discount_Price"]);
					product.ProductPrice = Convert.ToDecimal(dr["Price"]);
					product.ProductStatus = dr["Status"].ToString();
					product.ImageUrl = dr["ImageUrl"].ToString();
					product.BtwPercentage = Convert.ToInt32(dr["BTWPercentage"]);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}

			return product;
		}


		public int RetrieveProductSold()
		{
			int result = 0;
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT COUNT(*), Amount FROM dbo.ProductOrder GROUP BY Amount";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				result = Convert.ToInt32(cmd.ExecuteScalar());
				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return result;
		}
	}
}