using System.Collections.Generic;

namespace RicksWebWorld.Models
{
	public class Product
	{
		private readonly int productId;


		/// <summary>
		///     Constructor for product
		/// </summary>
		/// <param name="id">id for product</param>
		/// <param name="pName">Name for product</param>
		public Product(int id, string pName)
		{
			productId = id;
			ProductName = pName;
		}

		/// <summary>
		/// Constructor for product with just the id
		/// </summary>
		/// <param name="id">Identification of product</param>
		public Product(int id)
		{
			productId = id;
		}

		/// <summary>
		/// Constructor product for Dapper
		/// </summary>
		/// <param name="ProductName">Name</param>
		/// <param name="Description">Desc</param>
		/// <param name="Instock">Instock</param>
		/// <param name="Price">Decimal price</param>
		/// <param name="Discount">Decimal discount</param>
		/// <param name="Status">string status</param>
		/// <param name="Amount">int amount</param>
		/// <param name="ImageUrl">string image url</param>
		/// <param name="BTW">decimal btw</param>
		public Product(string ProductName, string Description, int Instock, decimal Price, decimal Discount, string Status, int Amount, string ImageUrl, int BTW)
		{
			this.ProductName = ProductName;
			this.ProductDesc = Description;
			this.ProductInStock = Instock;
			this.ProductPrice = Price;
			this.ProductDiscount = Discount;
			this.ProductStatus = Status;
			this.ProductAmount = Amount;
			this.ImageUrl = ImageUrl;
			this.BtwPercentage = BTW;
		}

		/// <summary>
		///     get or set the name
		/// </summary>
		public string ProductName { get; set; }

		/// <summary>
		///     Get or set productamount
		/// </summary>
		public int ProductAmount { get; set; }

		/// <summary>
		///		Description for product
		/// </summary>
		public string ProductDesc { get; set; }

		/// <summary>
		///     get or set productprice
		/// </summary>
		public decimal ProductPrice { get; set; }

		/// <summary>
		///     Get or set productdiscount
		/// </summary>
		public decimal ProductDiscount { get; set; }

		/// <summary>
		///     Get or set productstatus
		/// </summary>
		public string ProductStatus { get; set; }

		/// <summary>
		///     get or set BTW
		/// </summary>
		public int BtwPercentage { get; set; }

		/// <summary>
		///     get or set productcategories
		/// </summary>
		public List<Category> ProductCategories { get; set; } = new List<Category>();

		/// <summary>
		///     Get or set the productinstock
		/// </summary>
		public int ProductInStock { get; set; }

		/// <summary>
		///     get or set image
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
		///     Retrieve id from product
		/// </summary>
		/// <returns>id from product</returns>
		public int RetrieveProductId()
		{
			return productId;
		}

		/// <summary>
		/// Virtual which returns a true or false
		/// </summary>
		/// <returns>returns true or false</returns>
		public virtual bool RetrieveSpecialOffer()
		{
			return false;
		}
	}
}