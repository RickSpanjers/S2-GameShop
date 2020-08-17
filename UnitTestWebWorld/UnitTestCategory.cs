using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RicksWebWorld.Context;
using RicksWebWorld.Context.Debug;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Controllers;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestWebWorld
{
    [TestClass]
    public class UnitTestCategory
    {
		CategoryController Controller = new CategoryController();
		CategoryRepository CatRepoTest = new CategoryRepository(new CategoryTestContext());
		ProductRepository ProductRepoTest = new ProductRepository(new ProductTestContext());


		[TestMethod]
		public void RetrieveAllCategoriesNotNull()
		{
			var totalCategories = CatRepoTest.RetrieveAllCategories();
			Assert.IsTrue(totalCategories != null);
		}

		[TestMethod]
		public void SaveCategoryTestContext()
		{
			Category C = new Category("Testcategory", 1);
			C.CategoryDesc = "Testcategorydesc";
			C.CategoryImg = "NoImage.jpg";

			CatRepoTest.CreateNewCategory(C);
			var TotalCategories = CatRepoTest.RetrieveAllCategories();
			Assert.IsTrue(TotalCategories.Count > 4);
		}


		[TestMethod]
		public void EditCategoryTestContext()
		{
			Category C = new Category("Testcategory", 1);
			C.CategoryDesc = "Testcategorydesc";
			C.CategoryImg = "NoImage.jpg";

			var result = CatRepoTest.UpdateCategoryById(C);
			var TotalCategories = CatRepoTest.RetrieveAllCategories();

			foreach (var singlecat in TotalCategories)
			{
				Assert.IsFalse(singlecat.RetrieveCategoryName() == "Testcategory");
			}
		}


		[TestMethod]
		public void DeleteCategoryTestContext()
		{
			CatRepoTest.DeleteCategoryById(2);

			var AllCategories = CatRepoTest.RetrieveAllCategories();
			foreach (var singlecat in AllCategories)
			{
				Assert.IsFalse(singlecat.RetrieveCategoryId() == 2);
			}
		}

		[TestMethod]
		public void InsertCategoriesForProductsTestContext()
		{
			int productid = 1;
			int categoryid = 1;

			CatRepoTest.InsertCategoriesForProducts(productid, categoryid);
			Product P = ProductRepoTest.RetrieveProductById(productid);

			foreach(var single in CatRepoTest.RetrieveCategoriesFromProduct(P))
			{
				Assert.IsTrue(single.RetrieveCategoryId() == categoryid);
			}
		}	
	}
}
