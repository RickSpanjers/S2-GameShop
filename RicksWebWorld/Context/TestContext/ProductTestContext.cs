using System;
using System.Collections.Generic;
using System.Linq;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Context.Debug
{
	public class ProductTestContext : IProduct
	{
		private readonly List<Product> ListOfProducts = new List<Product>();

		public ProductTestContext()
		{
			ListOfProducts.Add(new Product(1, "Product01"));
			ListOfProducts.Add(new Product(2, "Product02"));
			ListOfProducts.Add(new Product(3, "Product03"));
			ListOfProducts.Add(new Product(4, "Product04"));
		}

		public List<Product> RetrieveAllProducts()
		{
			return ListOfProducts;
		}


		public bool DeleteProductById(int productId)
		{
			foreach (var single in ListOfProducts)
				if (single.RetrieveProductId() == productId)
				{
					ListOfProducts.Remove(single);
					return true;
				}

			return false;
		}

		public bool CreateNewProduct(Product p)
		{
			try
			{
				ListOfProducts.Add(new Product(ListOfProducts.Count() + 1, p.ProductName));
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}


		public bool UpdateProductById(Product p)
		{
			foreach (var single in ListOfProducts)
				if (single.RetrieveProductId() == p.RetrieveProductId())
				{
					single.ProductName = p.ProductName;
					single.ProductDesc = p.ProductDesc;

					return true;
				}

			return false;
		}

		public Product RetrieveProductById(int id)
		{
			foreach (var singleitem in ListOfProducts)
				if (singleitem.RetrieveProductId() == id)
					return singleitem;

			return null;
		}

		public Product RetrieveProductByName(string name)
		{
			foreach (var singleitem in ListOfProducts)
				if (singleitem.ProductName == name)
					return singleitem;

			return null;
		}

		public int RetrieveProductSold()
		{
			return 0;
		}
	}
}