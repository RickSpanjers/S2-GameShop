using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RicksWebWorld.Models;

namespace RicksWebWorld.ViewModels
{
	public class UserViewModel
	{
		[Required(ErrorMessage = "You must enter an e-mail")] [StringLength(100)]
		private string userEmail;

		[Required(ErrorMessage = "You must enter a firstname")] [StringLength(100)]
		private string userFirstname;

		[Required(ErrorMessage = "You must enter a lastname")] [StringLength(100)]
		private string userLastname;

		[Required(ErrorMessage = "You must enter a username")] [StringLength(100)]
		private string username;

		[Required(ErrorMessage = "You must enter a password")] [StringLength(100)]
		private string userPassword;

		public UserViewModel()
		{
			if (ShoppingCart == null) ShoppingCart = new ShoppingCartViewModel();
			ShoppingCart.cartId = 1;
		}

		public string Username
		{
			get => username;
			set => username = value;
		}

		public string Password
		{
			get => userPassword;
			set => userPassword = value;
		}

		public string FirstName
		{
			get => userFirstname;
			set => userFirstname = value;
		}


		public string LastName
		{
			get => userLastname;
			set => userLastname = value;
		}

		public string Email
		{
			get => userEmail;
			set => userEmail = value;
		}

		public string Address { get; set; }

		public string Zipcode { get; set; }

		public string Place { get; set; }

		public ShoppingCartViewModel ShoppingCart { get; set; }

		public int UserId { get; set; }

		public List<ProductViewModel> Wishlist { get; set; } = new List<ProductViewModel>();
	}
}