using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Context.Release
{
	public class CategoryMssqlContext : ConnectionHelper, ICategory
	{
		public bool CreateNewCategory(Category c)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "INSERT INTO dbo.[Category] (Name, Description, Image) VALUES (@Name, @Description, @Image)";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);

				newCmd.Parameters.AddWithValue("@Name", c.RetrieveCategoryName());
				newCmd.Parameters.AddWithValue("@Description", c.CategoryDesc);
				newCmd.Parameters.AddWithValue("Image", c.CategoryImg);
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

		public List<Category> RetrieveAllCategories()
		{
			var listOfCategories = new List<Category>();

			try
			{	
				SqlConnection cnn = ReturnSQLConnection();
				var query = "SELECT * FROM dbo.[Category]";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);

				var dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					var categoryId = Convert.ToInt32(dr["Id"]);
					var categoryName = dr["Name"].ToString();
					Category category = new Category(categoryName, categoryId);
					category.CategoryDesc = dr["Description"].ToString();
					category.CategoryImg = dr["Image"].ToString();

					listOfCategories.Add(category);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}

			return listOfCategories;
		}

		public bool DeleteCategoryById(int categoryId)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				var query = "DELETE FROM dbo.[Category] WHERE ID = @Id";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);		
				cmd.Parameters.AddWithValue("@Id", categoryId);
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

		public bool UpdateCategoryById(Category c)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				var query = "UPDATE dbo.[Category] SET Name=@Name, Description=@Description, Image=@Image WHERE ID = @Id";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);

				newCmd.Parameters.AddWithValue("@Id", c.RetrieveCategoryId());
				newCmd.Parameters.AddWithValue("@Name", c.RetrieveCategoryName());
				newCmd.Parameters.AddWithValue("@Description", c.CategoryDesc);
				newCmd.Parameters.AddWithValue("Image", c.CategoryImg);

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

		public bool InsertCategoriesForProducts(int productId, int categoryId)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				var query = "INSERT INTO dbo.[productcategory](Product_ID, Category_ID) VALUES (@productId, @categoryId)";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@productId", productId);
				newCmd.Parameters.AddWithValue("@categoryId", categoryId);
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


		public List<Category> RetrieveCategoriesFromProduct(Product p)
		{
			var listofCategories = new List<Category>();
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				var query = "SELECT * FROM dbo.Category AS C INNER JOIN dbo.ProductCategory as PC on C.ID = PC.Category_ID WHERE PC.Product_ID = @productId";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@productId", p.RetrieveProductId());

				var dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					var categoryId = Convert.ToInt32(dr["Id"]);
					var categoryName = dr["Name"].ToString();
					var category = new Category(categoryName, categoryId);
					category.CategoryDesc = dr["Description"].ToString();
					category.CategoryImg = dr["Image"].ToString();

					listofCategories.Add(category);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return listofCategories;
		}



		/// <summary>
		/// Get Category By id 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Category RetrieveCategoryById(int id)
		{
			var category = new Category(null, 1);
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				SqlCommand cmd = CreateSQLCommandStoredProcedure("GetCategoryByID", cnn);
				cmd.Parameters.AddWithValue("@ID", id);
				var dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					var categoryId = Convert.ToInt32(dr["Id"]);
					var categoryName = dr["Name"].ToString();
					category = new Category(categoryName, categoryId);
					category.CategoryDesc = dr["Description"].ToString();
					category.CategoryImg = dr["Image"].ToString();
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return category;
		}

	}
}