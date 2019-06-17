using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RicksWebWorld.Models;

namespace RicksWebWorld.ViewModels
{
	public class ProductViewModel
	{
		/*All categories and all offers*/
		public List<CategoryOverviewViewModel> ListOfAllCategories = new List<CategoryOverviewViewModel>();
		public List<SpecialOfferViewModel> ListOfAllOffers = new List<SpecialOfferViewModel>();

		[Required(ErrorMessage = "You must enter an amount")]
		private int productamount;

		[Required(ErrorMessage = "You must enter a productname")]
		private string productname;

		[Required(ErrorMessage = "You must enter a price")]
		private decimal productPrice;

		public int ProductId { get; set; }

		[Required(ErrorMessage = "You must enter a productname")]
		[DataType(DataType.Text)]
		public string ProductName
		{
			get => productname;
			set => productname = value;
		}

		[Required(ErrorMessage = "You must enter an amount")]
		public int ProductAmount
		{
			get => productamount;
			set => productamount = value;
		}

		public int AmountToOrder { get; set; }

		[DataType(DataType.MultilineText)]
		public string ProductDesc { get; set; }

		[Required(ErrorMessage = "You must enter a price")]
		public decimal ProductPrice
		{
			get => productPrice;
			set => productPrice = value;
		}

		public bool IsSpecialOffer { get; set; }

		[Required(ErrorMessage = "You must enter a discount price. It can also be the same as the regular price")]
		public decimal ProductDiscount { get; set; }

		[Required(ErrorMessage = "You must enter a status")]
		public string ProductStatus { get; set; }

		[Required(ErrorMessage = "You must at least choose one category")]
		public List<CategoryOverviewViewModel> ProductCategories { get; set; } = new List<CategoryOverviewViewModel>();

		[Required(ErrorMessage = "You must choose if the product is in stock")]
		public string ProductInStock { get; set; }

		public DateTime OfferStart { get; set; }

		public DateTime OfferEnd { get; set; }

		public decimal OfferPrice { get; set; }

		[Required(ErrorMessage = "You must upload an image")]
		public string ImageUrl { get; set; }

		[Required(ErrorMessage = "You must enter a BTW percentage")]
		public int BtwPercentage { get; set; }

		public List<RevisionViewModel> RevisionsInProduct { get; set; } = new List<RevisionViewModel>();

		public User UserLoggedIn { get; set; }

	}
}