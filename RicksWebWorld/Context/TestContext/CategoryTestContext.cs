using System;
using System.Collections.Generic;
using System.Linq;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Context.Debug
{
	public class CategoryTestContext : ICategory
	{
		private readonly List<Category> ListOfCategories = new List<Category>();
		private readonly Dictionary<int, int> ProductCategories = new Dictionary<int, int>();

		public CategoryTestContext()
		{
			ListOfCategories.Add(new Category("Category01", 1));
			ListOfCategories.Add(new Category("Category02", 2));
			ListOfCategories.Add(new Category("Category03", 3));
			ListOfCategories.Add(new Category("Category04", 4));
		}

		public bool CreateNewCategory(Category c)
		{
			try
			{
				ListOfCategories.Add(new Category(c.RetrieveCategoryName(), ListOfCategories.Count() + 1));
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}

		public List<Category> RetrieveAllCategories()
		{
			return ListOfCategories;
		}

		public bool DeleteCategoryById(int categoryId)
		{
			foreach (var singlecat in ListOfCategories)
				if (singlecat.RetrieveCategoryId() == categoryId)
				{
					ListOfCategories.Remove(singlecat);
					return true;
				}

			return false;
		}

		public bool UpdateCategoryById(Category c)
		{
			foreach (var singlecat in ListOfCategories)
				if (singlecat.RetrieveCategoryId() == c.RetrieveCategoryId())
				{
					singlecat.CategoryDesc = c.CategoryDesc;
					singlecat.CategoryImg = c.CategoryImg;
					return true;
				}

			return false;
		}

		public bool InsertCategoriesForProducts(int productId, int categoryId)
		{
			ProductCategories.Add(productId, categoryId);
			return true;
		}


		public List<Category> RetrieveCategoriesFromProduct(Product p)
		{
			List<Category> Categories = new List<Category>();

			foreach(var single in ProductCategories)
			{
				if(single.Key == p.RetrieveProductId())
				{
					Category ToAdd = RetrieveCategoryById(single.Value);
					Categories.Add(ToAdd);
				}
			}

			return Categories;
		}

		public Category RetrieveCategoryById(int id)
		{
			foreach (var singleitem in ListOfCategories)
				if (singleitem.RetrieveCategoryId() == id)
					return singleitem;

			return null;
		}
	}
}