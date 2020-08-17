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
	public class ProductDapperContext : ConnectionHelper, IProduct
	{
		public bool CreateNewProduct(Product p)
		{
			string sqlUserInsert = "INSERT INTO dbo.[Product] (name, Description, Instock, Price, Discount_Price, Status, Amount, ImageURL, BTWPercentage) VALUES (@name, @Description, @Instock, @Price, @Discount, @Status, @Amount, @ImageUrl, @BTW)";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { name = p.ProductName, Description = p.ProductDesc, Instock = p.ProductInStock, Price = p.ProductPrice, Discount = p.ProductDiscount, Status = p.ProductStatus, Amount = p.ProductAmount, ImageUrl = p.ImageUrl, BTW = p.BtwPercentage });
				return true;
			}
		}

		public bool DeleteProductById(int productId)
		{
			string sqlDelete = "DELETE FROM dbo.[Product] WHERE ID = @Id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					int affectedRows = connection.Execute(sqlDelete, new { Id = productId });
					return true;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return false;
				}
			}
		}

		public List<Product> RetrieveAllProducts()
		{
			string sqlDetails = "SELECT * FROM dbo.[Product]";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					List<Product> listOfMethods = connection.Query<Product>(sqlDetails).ToList();
					return listOfMethods;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public Product RetrieveProductById(int id)
		{
			string StoredProcedure = "GetProductByID";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					Product Detail = connection.QueryFirstOrDefault<Product>(StoredProcedure, new { ID = id }, commandType: CommandType.StoredProcedure);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public Product RetrieveProductByName(string name)
		{
			string Query = "SELECT * FROM dbo.[Product] WHERE name=@name";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					Product Detail = connection.QueryFirstOrDefault<Product>(Query, new { name = name }, commandType: CommandType.Text);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}
			}
		}

		public int RetrieveProductSold()
		{
			string Query = "SELECT COUNT(*), Amount FROM dbo.ProductOrder GROUP BY Amount";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					int Detail = connection.QueryFirstOrDefault<int>(Query, commandType: CommandType.Text);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return 0;
				}
			}
		}

		public bool UpdateProductById(Product p)
		{
			string sql = "UPDATE dbo.[Product] SET name=@name, Amount=@Amount, Description=@Description, Instock=@Instock, Price=@Price, Discount_Price=@Discount, Status=@Status, ImageURL=@ImageUrl, BTWPercentage=@BTW WHERE ID = @Id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sql, new { name = p.ProductName, Description = p.ProductDesc, Instock = p.ProductInStock, Price = p.ProductPrice, Discount = p.ProductDiscount, Status = p.ProductStatus, Amount = p.ProductAmount, ImageUrl = p.ImageUrl, BTW = p.BtwPercentage });
				return true;
			}
		}
	}
}
