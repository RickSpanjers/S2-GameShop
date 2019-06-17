using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;
using AutoMapper;
using RicksWebWorld.Helpers;

namespace RicksWebWorld.Controllers
{
	public class CartController : Controller
	{
		//Make instances of repositories
		private readonly ProductRepository productRepo = new ProductRepository(new ProductMssqlContext());
		private readonly SpecialOfferRepository offerRepo = new SpecialOfferRepository(new SpecialOfferMssqlContext());
		private readonly PaymentMethodRepository paymentRepo = new PaymentMethodRepository(new PaymentMssqlContext());
		private readonly AutoMapperExtension mapextension = new AutoMapperExtension();
		private readonly SessionHelper sessionHelper = new SessionHelper();

		/// <summary>
		///  Call for the shopping cart page
		/// </summary>
		/// <returns>IActionresult Cart</returns>
		public IActionResult Cart()
		{
			UserViewModel model = HttpContext.Session.GetObjectFromJson<UserViewModel>("ComplexObject");
			return View("Cart", model);
		}


		/// <summary>
		///     Calls for the checkout page
		/// </summary>
		/// <returns>IActionresult Checkout</returns>
		public IActionResult Checkout()
		{
			UserOrderViewModel model = CheckForLoggedInUserForOrder();
			return View("Checkout", model);
		}


		/// <summary>
		///     Adds a product to the cart of the currently logged in user
		/// </summary>
		/// <param name="product"></param>
		/// <returns>IActionresult </returns>
		public IActionResult Add(int product, bool order)
		{
			//Turn product into cartitem
			CartItemViewModel c = ProductsToCartItems(product);

			//Get logged in user
			var loggedInUser = HttpContext.Session.GetObjectFromJson<UserViewModel>("ComplexObject");
			if (loggedInUser == null) return RedirectToAction("Login", "User");

			//Check if itemsincart is null
			if(loggedInUser.ShoppingCart.ItemsInCart == null){loggedInUser.ShoppingCart.ItemsInCart = new List<CartItemViewModel>();}

			//Add item to  cart
			if (loggedInUser.ShoppingCart.ItemsInCart.Count > 0 )
			{
				var ProductToAdd = loggedInUser.ShoppingCart.ItemsInCart.Where(p => p.Product.ProductName == c.Product.ProductName).FirstOrDefault();
				if(ProductToAdd!=null){ProductToAdd.Quantity++;}
				else{loggedInUser.ShoppingCart.ItemsInCart.Add(c);}				
			}
			else
			{
				loggedInUser.ShoppingCart.ItemsInCart.Add(c);
			}

			//Update the session
			sessionHelper.UpdateCurrentSessionUVM(loggedInUser);

			//Return to page depending on user choice
			if(order == true)
			{
				return RedirectToAction("Cart");
			}
			else
			{
				return RedirectToAction("Products", "Home");
			}
			
		}


		public CartItemViewModel ProductsToCartItems(int product)
		{
			var selectedProduct = productRepo.RetrieveProductById(product);
			var selectedProductSO = offerRepo.RetrieveOfferByProductID(product);
			//Set up automapping
			var mapper = mapextension.ProductToProductviewmodel();
			ProductViewModel pmodel = mapper.Map<ProductViewModel>(selectedProduct);

			if (selectedProductSO.RetrieveOfferId() != -1)
			{
				if (DateTime.Compare(selectedProductSO.EndTime, DateTime.Now) > 0 && DateTime.Compare(selectedProductSO.StartTime, DateTime.Now) < 0 && selectedProductSO.RetrieveOfferPrice() < selectedProduct.ProductPrice && selectedProductSO.RetrieveOfferPrice() < selectedProduct.ProductDiscount)
				{
					pmodel.ProductPrice = selectedProductSO.RetrieveOfferPrice();
				}
			}

			CartItemViewModel c = new CartItemViewModel();
			c.Product = pmodel;
			c.Quantity = 1;
			return c;
		}


		/// <summary>
		/// Remove product from the cart of the current logged in user
		/// </summary>
		/// <param name="item">Name of the item to remove</param>
		/// <returns>IActionresult</returns>
		public IActionResult Remove(int item)
		{
			var loggedInUser = HttpContext.Session.GetObjectFromJson<UserViewModel>("ComplexObject");
			foreach (var singleitem in loggedInUser.ShoppingCart.ItemsInCart)
			{
				if (singleitem.Product.ProductId == item)
				{
					loggedInUser.ShoppingCart.ItemsInCart.Remove(singleitem);
					sessionHelper.UpdateCurrentSessionUVM(loggedInUser);
					return RedirectToAction("Cart", "Cart");
				}
			}
			return RedirectToAction("Products", "Home");
		}


	
		/// <summary>
		///     Check which user is logged in
		/// </summary>
		/// <returns>UserViewModel</returns>
		[HttpPost]
		public UserOrderViewModel CheckForLoggedInUserForOrder()
		{
			try
			{
				var objComplex = HttpContext.Session.GetObjectFromJson<UserViewModel>("ComplexObject");

				//Set up automapping
				var mapper = mapextension.UserViewModelToUserOrderViewModel();
				UserOrderViewModel model = mapper.Map<UserOrderViewModel>(objComplex);
				model.PaymentMethods = new List<PaymentMethodViewModel>();

				foreach(Payment p in paymentRepo.RetrieveAllPayments())
				{
					PaymentMethodViewModel pmodel = new PaymentMethodViewModel();
					pmodel.PaymentMethodId = p.RetrieveID();
					pmodel.PaymentMethodName = p.RetrieveName();
					model.PaymentMethods.Add(pmodel);
				}

				return model;
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine("No session set");
			}

			return null;
		}
	}
}