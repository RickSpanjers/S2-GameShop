using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RicksWebWorld.Context.Debug;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Controllers;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;
using System.Collections.Generic;

namespace UnitTestWebWorld
{
    [TestClass]
    public class UnitTestProduct
    {
		ProductController Controller = new ProductController();
		ProductRepository ProductTestRepo = new ProductRepository(new ProductTestContext());

		[TestMethod]
		public void SaveProductTestContext()
		{
			Product P = new Product(5, "Test");
			P.ImageUrl = "NoImage.jpg";
			P.ProductAmount = 25;
			P.ProductDesc = "Description";
			P.ProductInStock = 1;
			P.ProductName = "NewProduct";
			P.ProductPrice = 25;
			P.ProductStatus = "Nieuw";
			P.ProductDiscount = 25;

			var result = ProductTestRepo.CreateNewProduct(P);
			var TotalProducts = ProductTestRepo.RetrieveAllProducts();
			Assert.IsTrue(TotalProducts.Count > 4);
			Assert.IsTrue(ProductTestRepo.RetrieveAllProducts() != null);
		}

		[TestMethod]
		public void EditProductTestContext()
		{
			Product P = new Product(1, "Test");
			var result = ProductTestRepo.UpdateProductById(P);
			var TotalProducts = ProductTestRepo.RetrieveAllProducts();

			foreach (var singleproduct in TotalProducts)
			{
				Assert.IsFalse(singleproduct.ProductName == "Product01");
				Assert.IsTrue(ProductTestRepo.RetrieveAllProducts() != null);
			}
		}

		[TestMethod]
		public void DeleteProductTestContext()
		{
			var result = ProductTestRepo.DeleteProductById(1);
			var TotalProducts = ProductTestRepo.RetrieveAllProducts();

			foreach (var singleproduct in TotalProducts)
			{
				Assert.IsFalse(singleproduct.ProductName == "Test");
				Assert.IsTrue(ProductTestRepo.RetrieveAllProducts() != null);
			}
		}

		[TestMethod]
		public void RetrieveAllProducts()
		{
			var TotalProducts = ProductTestRepo.RetrieveAllProducts();
			Assert.IsTrue(ProductTestRepo.RetrieveAllProducts() != null);		
		}

	}
}
