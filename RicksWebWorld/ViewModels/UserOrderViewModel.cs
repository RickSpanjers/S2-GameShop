using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RicksWebWorld.Models;

namespace RicksWebWorld.ViewModels
{
	public class UserOrderViewModel
	{
		public List<OrderViewModel> OrdersInSystem { get; set; } = new List<OrderViewModel>();

		public int OrderId { get; set; }

		public string OrderDescription { get; set; }

		public ShoppingCartViewModel ShoppingCart { get; set; }

		public List<ProductOverviewViewModel> ProductsInSystem { get; set; } = new List<ProductOverviewViewModel>();

		[Required(ErrorMessage = "Please fill the field")]
		public string SendAddress { get; set; }

		[Required(ErrorMessage = "Please fill the field")]
		[DataType(DataType.PostalCode)]
		public string SendZipcode { get; set; }

		[Required(ErrorMessage = "Please fill the field")]
		public string SendPlace { get; set; }

		[Required(ErrorMessage = "Please fill the field")]
		[DataType(DataType.Text)]
		public string Username { get; set; }

		[Required(ErrorMessage = "Please fill the field")]
		[DataType(DataType.Text)]
		public string Firstname { get; set; }

		[Required(ErrorMessage = "Please fill the field")]
		[DataType(DataType.Text)]
		public string Lastname { get; set; }

		[Required(ErrorMessage = "Please fill the field")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		public List<PaymentMethodViewModel> PaymentMethods { get; set; }
	}
}