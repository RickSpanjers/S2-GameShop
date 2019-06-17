using System.Collections.Generic;
using RicksWebWorld.Models;

namespace RicksWebWorld.Interfaces
{
	public interface ICategory
	{
		bool CreateNewCategory(Category c);
		List<Category> RetrieveAllCategories();
		bool DeleteCategoryById(int categoryId);
		bool UpdateCategoryById(Category c);
		bool InsertCategoriesForProducts(int productId, int categoryId);
		List<Category> RetrieveCategoriesFromProduct(Product p);
		Category RetrieveCategoryById(int id);
	}
}