using Microsoft.AspNetCore.Http;
using RicksWebWorld.Models;
using RicksWebWorld.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Helpers
{
    public class SessionHelper
    {
		private IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
		private readonly AutoMapperExtension mapextension = new AutoMapperExtension();

		public void UpdateCurrentSessionUVM(UserViewModel loggedInUser)
		{
			//Fill a new session
			var uvw = new UserViewModel();
			uvw.FirstName = loggedInUser.FirstName;
			uvw.LastName = loggedInUser.LastName;
			uvw.Password = loggedInUser.Password;
			uvw.Place = loggedInUser.Place;
			uvw.Username = loggedInUser.Username;
			uvw.Zipcode = loggedInUser.Zipcode;

			if (loggedInUser.ShoppingCart.ItemsInCart != null) { uvw.ShoppingCart.ItemsInCart = loggedInUser.ShoppingCart.ItemsInCart; }
			else { uvw.ShoppingCart.ItemsInCart = new List<CartItemViewModel>(); }

			if (loggedInUser.Wishlist != null){ uvw.Wishlist = loggedInUser.Wishlist;}
			else{ uvw.Wishlist = new List<ProductViewModel>(); }

			//Create new updated session
			var userId = httpContextAccessor.HttpContext.Session.GetString("userId");

			httpContextAccessor.HttpContext.Session.Clear();
			httpContextAccessor.HttpContext.Session.SetObjectAsJson("ComplexObject", uvw);
			httpContextAccessor.HttpContext.Session.SetString("userId", userId);
			httpContextAccessor.HttpContext.Session.SetString("cartItems", uvw.ShoppingCart.ItemsInCart.Count.ToString());
		}

		public void UpdateSessionsUser(User selectedUser)
		{
			var mapper = mapextension.UserToUserViewModel();
			UserViewModel uvw = mapper.Map<UserViewModel>(selectedUser);
			uvw.ShoppingCart = new ShoppingCartViewModel();
			uvw.ShoppingCart.ItemsInCart = new List<CartItemViewModel>();

			httpContextAccessor.HttpContext.Session.SetObjectAsJson("ComplexObject", uvw);
			httpContextAccessor.HttpContext.Session.SetString("userId", selectedUser.ReturnUserId().ToString());
			httpContextAccessor.HttpContext.Session.SetString("cartItems", 0.ToString());
		}
	}
}
