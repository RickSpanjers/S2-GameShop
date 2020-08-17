using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;

namespace RicksWebWorld.Controllers
{
	public class CategoryController : Controller
	{
		private readonly CategoryRepository categoryRep = new CategoryRepository(new CategoryMssqlContext());
		private readonly ProductRepository productRep = new ProductRepository(new ProductMssqlContext());
		private readonly AutoMapperExtension mapextension = new AutoMapperExtension();

		/// <summary>
		/// Returns the categoryoverview
		/// </summary>
		/// <returns>IActionresult</returns>
		public IActionResult Overview()
		{
			var model = new CategoryViewModel();
			var categories = new List<CategoryOverviewViewModel>();
			foreach(Category c in categoryRep.RetrieveAllCategories())
			{
				//Set up automapping
				var mapper = mapextension.CategoryToCategoryOverviewViewModel();
				CategoryOverviewViewModel m = mapper.Map<CategoryOverviewViewModel>(c);
				categories.Add(m);
			}

			model.AllCategoriesInSystem = categories;
			return View("Categoryoverview", model);
		}


		/// <summary>
		///     Returns the categoryoverview page with selectedcategory
		/// </summary>
		/// <param name="selectedCategory">Category which was selected</param>
		/// <returns>IActionresult</returns>
		public IActionResult OverviewEdit(int selectedCategory)
		{
			var allCategories = categoryRep.RetrieveAllCategories();
			var selected = categoryRep.RetrieveCategoryById(selectedCategory);
			CategoryViewModel model = AutoMappingToCategoryViewModel(selected, allCategories);
			return View("Categoryoverview", model);
		}



		/// <summary>
		///     Add category to system
		/// </summary>
		/// <param name="model">CategoryViewModel</param>
		/// <param name="uploadedImage">Image URL</param>
		/// <returns>IActionresult</returns>
		[HttpPost]
		public ActionResult Add(CategoryViewModel model, string uploadedImage)
		{
			var c = new Category(model.CategoryName, 1);
			c.CategoryDesc = model.CategoryDesc;

			if (c.CategoryDesc == null){c.CategoryDesc = "No description";}
			c.CategoryImg = uploadedImage;
			if (categoryRep.CreateNewCategory(c)){return RedirectToAction("Overview", "Category");}
			
			return RedirectToAction("Overview", "Category");
		}

		/// <summary>
		///     Delete category from system
		/// </summary>
		/// <param name="categoryToDelete">Selected category string</param>
		/// <returns>IActionresult</returns>
		[HttpPost]
		public ActionResult Delete(int categoryToDelete)
		{
			var selectedCategory = categoryRep.RetrieveCategoryById(categoryToDelete);
			if (categoryRep.DeleteCategoryById(selectedCategory.RetrieveCategoryId()))
			{
				return RedirectToAction("Overview", "Category");
			}
				
			return RedirectToAction("Overview", "Category");
		}


		/// <summary>
		///     Edit category in system
		/// </summary>
		/// <param name="c">Categoryviewmodel</param>
		/// <param name="categoryToEdit">Category to edit</param>
		/// <param name="uploadedImage">Image URL</param>
		/// <returns>IActionresult</returns>
		[HttpPost]
		public ActionResult Edit(CategoryViewModel c, int categoryToEdit, string uploadedImage)
		{
			var selectedCategory = categoryRep.RetrieveCategoryById(categoryToEdit);

			var newCategory = new Category(c.CategoryName, selectedCategory.RetrieveCategoryId());
			newCategory.CategoryDesc = c.CategoryDesc;
			newCategory.CategoryImg = uploadedImage;

			if (newCategory.CategoryImg == null)
			{
				newCategory.CategoryImg = "NoImage.jpg";
			}
			if (categoryRep.UpdateCategoryById(newCategory))
			{
				return RedirectToAction("Overview", "Category", selectedCategory);
			}

			return RedirectToAction("Overview", "Category");
		}


		/// <summary>
		///     Insert selectedcategories in product
		/// </summary>
		/// <param name="listOfCategories">List of categories</param>
		/// <param name="p">Product</param>
		[HttpPost]
		public void InsertCategoriesForProduct(List<Category> listOfCategories, Product p)
		{
			var selectedProduct = productRep.RetrieveProductById(p.RetrieveProductId());

			foreach (var category in listOfCategories)
			{
				categoryRep.InsertCategoriesForProducts(selectedProduct.RetrieveProductId(), category.RetrieveCategoryId());
			}
				
		}


		/// <summary>
		///     Retrieve categories from product
		/// </summary>
		/// <param name="p">Product to select from</param>
		/// <returns>List of categories</returns>
		public List<Category> RetrieveCategoriesFromProduct(Product p)
		{
			var categories = categoryRep.RetrieveCategoriesFromProduct(p);
			return categories;
		}


		public CategoryViewModel AutoMappingToCategoryViewModel(Category selected, List<Category> listOfCategories)
		{
			//Set up automapping
			var mapperOne = mapextension.CategoryToCategoryViewModel();
			CategoryViewModel model = mapperOne.Map<CategoryViewModel>(selected);

			var categories = new List<CategoryOverviewViewModel>();
			foreach (Category c in categoryRep.RetrieveAllCategories())
			{
				//Set up automapping
				var mapper = mapextension.CategoryToCategoryOverviewViewModel();
				CategoryOverviewViewModel m = mapper.Map<CategoryOverviewViewModel>(c);
				categories.Add(m);

			}
			model.AllCategoriesInSystem = categories;
			return model;
		}
	}
}