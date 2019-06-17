using System.Collections.Generic;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Repositories
{
	public class CategoryRepository
	{
		private readonly ICategory categoryInterface;

		public CategoryRepository(ICategory context)
		{
			categoryInterface = context;
		}

		public bool CreateNewCategory(Category c)
		{
			return categoryInterface.CreateNewCategory(c);
		}

		public List<Category> RetrieveAllCategories()
		{
			return categoryInterface.RetrieveAllCategories() as List<Category>;
		}

		public bool DeleteCategoryById(int categoryId)
		{
			return categoryInterface.DeleteCategoryById(categoryId);
		}

		public bool UpdateCategoryById(Category c)
		{
			return categoryInterface.UpdateCategoryById(c);
		}

		public bool InsertCategoriesForProducts(int productId, int categoryId)
		{
			return categoryInterface.InsertCategoriesForProducts(productId, categoryId);
		}

		public List<Category> RetrieveCategoriesFromProduct(Product p)
		{
			return categoryInterface.RetrieveCategoriesFromProduct(p) as List<Category>;
		}

		public Category RetrieveCategoryById(int id)
		{
			return categoryInterface.RetrieveCategoryById(id);
		}
	}
}