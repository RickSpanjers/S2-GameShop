using System;

namespace RicksWebWorld.Models
{
	public class SpecialOffer : Product
	{
		private readonly int offerId;
		private readonly decimal offerPrice;

		/// <summary>
		///     Constructor for special offer
		/// </summary>
		/// <param name="offerId">int Id</param>
		/// <param name="p">Proudct p</param>
		/// <param name="price">int price</param>
		public SpecialOffer(int offerId, decimal price, int productId, string productName) : base(productId, productName)
		{
			this.offerId = offerId;
			offerPrice = price;
		}

		/// <summary>
		/// Constructor special offer for Dapper
		/// </summary>
		/// <param name="ID">Identification for specialoffer</param>
		/// <param name="Startdate">Starttime</param>
		/// <param name="Enddate">Endtime</param>
		/// <param name="OfferPrice">Price of the offer</param>
		/// <param name="ProductID">ID of the product</param>
		public SpecialOffer(int ID, DateTime Startdate, DateTime Enddate, int OfferPrice, int ProductID): base(ProductID)
		{
			offerId = ID;
			StartTime = Startdate;
			EndTime = Enddate;
			offerPrice = OfferPrice;
		}

		/// <summary>
		///     Retrieve decimal offerprice
		/// </summary>
		/// <returns>decimal offerprice</returns>
		public decimal RetrieveOfferPrice()
		{
			return offerPrice;
		}

		/// <summary>
		///     Retrieve Id
		/// </summary>
		/// <returns>int Id</returns>
		public int RetrieveOfferId()
		{
			return offerId;
		}

		/// <summary>
		/// Overrides the virtual in product base class with true
		/// </summary>
		/// <returns>Returns true</returns>
		public override bool RetrieveSpecialOffer()
		{
			return true;
		}

		/// <summary>
		///     Get or set the starttime
		/// </summary>
		public DateTime StartTime { get; set; }

		/// <summary>
		///     Get or set the endtime
		/// </summary>
		public DateTime EndTime { get; set; }
	}
}