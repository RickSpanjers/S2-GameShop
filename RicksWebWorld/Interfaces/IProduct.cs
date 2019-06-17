using System.Collections.Generic;
using RicksWebWorld.Models;

namespace RicksWebWorld.Interfaces
{
	public interface IProduct
	{
		List<Product> RetrieveAllProducts();
		bool DeleteProductById(int productId);
		bool CreateNewProduct(Product p);
		bool UpdateProductById(Product p);
		Product RetrieveProductById(int id);
		Product RetrieveProductByName(string name);
		int RetrieveProductSold();
	}
}