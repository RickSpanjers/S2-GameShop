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
	public class CategoryDapperContext : ConnectionHelper, ICategory
	{
		public bool CreateNewCategory(Category c)
		{
			string sqlUserInsert = "INSERT INTO dbo.[Category] (Name, Description, Image) VALUES (@Name, @Description, @Image)";
			using (SqlConnection cnn = ReturnSQLConnection())
			{
				int affectedRows = cnn.Execute(sqlUserInsert, new { Name = c.RetrieveCategoryName(), Description = c.CategoryDesc, Image = c.CategoryImg });
				return true;
			}
		}

		public bool DeleteCategoryById(int categoryId)
		{
			string sqlUserDelete = "DELETE FROM dbo.[Category] WHERE ID = @Id";
			using (SqlConnection cnn = ReturnSQLConnection())
			{
				try
				{
					int affectedRows = cnn.Execute(sqlUserDelete, new { ID = categoryId });
					return true;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return false;
				}
			}
		}

		public bool InsertCategoriesForProducts(int productId, int categoryId)
		{
			string sqlUserInsert = "INSERT INTO dbo.[productcategory](Product_ID, Category_ID) VALUES (@productId, @categoryId)";
			using (SqlConnection cnn = ReturnSQLConnection())
			{
				int affectedRows = cnn.Execute(sqlUserInsert, new { Product_ID = productId, categoryId = categoryId });
				return true;
			}
		}

		public List<Category> RetrieveAllCategories()
		{
			string sqlCategoryDetails = "SELECT * FROM dbo.[Category]";
			using (SqlConnection cnn = ReturnSQLConnection())
			{
				try
				{
					List<Category> listOfCategories = cnn.Query<Category>(sqlCategoryDetails).ToList();
					return listOfCategories;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}
			}
		}

		public List<Category> RetrieveCategoriesFromProduct(Product p)
		{	
			string sqlCategoryDetails = "SELECT * FROM dbo.Category AS C INNER JOIN dbo.ProductCategory as PC on C.ID = PC.Category_ID WHERE PC.Product_ID = @productId";
			using (SqlConnection cnn = ReturnSQLConnection())
			{
				try
				{
					List<Category> listOfCategories = cnn.Query<Category>(sqlCategoryDetails, new {productId = p.RetrieveProductId()}).ToList();
					return listOfCategories;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public Category RetrieveCategoryById(int id)
		{
			string StoredProcedure = "GetCategoryByID";
			using (SqlConnection cnn = ReturnSQLConnection())
			{
				try
				{
					Category catDetail = cnn.QueryFirstOrDefault<Category>(StoredProcedure, new { ID = id }, commandType: CommandType.StoredProcedure);
					return catDetail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public bool UpdateCategoryById(Category c)
		{
			
			string sqlUserInsert = "UPDATE dbo.[Category] SET Name=@Name, Description=@Description, Image=@Image WHERE ID = @Id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new {Id=c.RetrieveCategoryId(), Name = c.RetrieveCategoryName(), Description = c.CategoryDesc, Image = c.CategoryImg });
				return true;
			}
		}
	}
}
