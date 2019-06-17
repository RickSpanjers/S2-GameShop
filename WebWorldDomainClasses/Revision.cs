using System;

namespace RicksWebWorld.Models
{
	public class Revision
	{
		private readonly int id;
		private readonly Product product;

		/// <summary>
		///     Constructor for revision
		/// </summary>
		/// <param name="id"></param>
		/// <param name="productId"></param>
		public Revision(int id, Product p)
		{
			this.id = id;
			this.product = p;
		}

		/// <summary>
		/// Constructor for revision
		/// </summary>
		/// <param name="ID">Identification of revision</param>
		/// <param name="Product">Product object</param>
		/// <param name="RevisionTime">Revision</param>
		public Revision(int ID, Product Product, DateTime RevisionTime)
		{
			id = ID;
			product = Product;
			RevisionDateTime = RevisionTime;
		}

		/// <summary>
		///     Retrieve the id
		/// </summary>
		/// <returns>int id</returns>
		public int RetrieveId()
		{
			return id;
		}

		/// <summary>
		///     Retrieve the product
		/// </summary>
		/// <returns>int productid</returns>
		public Product RetrieveProduct()
		{
			return product;
		}

		/// <summary>
		///     get or set revision date time
		/// </summary>
		public DateTime RevisionDateTime { get; set; }

	}
}