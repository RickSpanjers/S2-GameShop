using System.Collections.Generic;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Repositories
{
	public class ProductRepository
	{
		private readonly IProduct productInterface;

		public ProductRepository(IProduct context)
		{
			productInterface = context;
		}

		public List<Product> RetrieveAllProducts()
		{
			return productInterface.RetrieveAllProducts() as List<Product>;
		}

		public bool DeleteProductById(int productId)
		{
			return productInterface.DeleteProductById(productId);
		}

		public bool CreateNewProduct(Product p)
		{
			return productInterface.CreateNewProduct(p);
		}

		public bool UpdateProductById(Product p)
		{
			return productInterface.UpdateProductById(p);
		}

		public Product RetrieveProductById(int id)
		{
			return productInterface.RetrieveProductById(id);
		}

		public Product RetrieveProductByName(string name)
		{
			return productInterface.RetrieveProductByName(name);
		}
		public int RetrieveProductSold()
		{
			return productInterface.RetrieveProductSold();
		}
	}
}