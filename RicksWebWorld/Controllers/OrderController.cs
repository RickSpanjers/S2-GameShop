using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.Context;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Helpers;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;

namespace RicksWebWorld.Controllers
{
	public class OrderController : Controller
	{
		private readonly OrderRepository orderRepo = new OrderRepository(new OrderMssqlContext());
		private readonly ProductRepository productRepo = new ProductRepository(new ProductMssqlContext());
		private readonly UserRepository userRepo = new UserRepository(new UserMssqlContext());
		private readonly AutoMapperExtension mapextension = new AutoMapperExtension();
		private readonly SessionHelper sessionHelper = new SessionHelper();

		/// <summary>
		///     Returns the OrderOverview page
		/// </summary>
		/// <returns>Actionresult OrderOverview</returns>
		public IActionResult Overview()
		{
			UserOrderViewModel model = new UserOrderViewModel();
			model.OrdersInSystem = new List<OrderViewModel>();
			foreach (Order o in orderRepo.RetrieveAllOrders())
			{
				IMapper mapper = mapextension.OrdertoOrderViewModel();
				OrderViewModel omodel = mapper.Map<OrderViewModel>(o);
				omodel.ItemsInOrder = new List<CartItemViewModel>();
				omodel.User = new UserOverviewViewModel();
				omodel.User.Email = o.RetrieveCustomer().Email;

				foreach (CartItem c in o.RetrieveItemsInOrder())
				{
					CartItemViewModel cmodel = new CartItemViewModel();
					cmodel.Quantity = c.Quantity;
					omodel.ItemsInOrder.Add(cmodel);
				}
				model.OrdersInSystem.Add(omodel);
			}
			return View("OrderOverview", model);
		}

		/// <summary>
		///     Returns the single order page
		/// </summary>
		/// <returns>Actionresult SingleOrder</returns>
		public IActionResult Single()
		{
			var model = new UserOrderViewModel();
			model.ProductsInSystem = new List<ProductOverviewViewModel>();
			foreach (Product p in productRepo.RetrieveAllProducts())
			{
				ProductOverviewViewModel pmodel = new ProductOverviewViewModel();
				pmodel.ProductId = p.RetrieveProductId();
				pmodel.ProductName = p.ProductName;
				model.ProductsInSystem.Add(pmodel);
			}
			return View("SingleOrder", model);
		}


		/// <summary>
		///     Opens the single page for an order with data to edit
		/// </summary>
		/// <param name="orderToEdit"></param>
		/// <returns></returns>
		public IActionResult SingleEdit(int orderToEdit)
		{
			var order = orderRepo.RetrieveOrderById(orderToEdit);
			var mapperOne = mapextension.OrdertoUserOrderViewModel();

			UserOrderViewModel model = mapperOne.Map<UserOrderViewModel>(order);
			model.ShoppingCart = new ShoppingCartViewModel();
			model.ShoppingCart.cartId = 1;
			model.ShoppingCart.ItemsInCart = new List<CartItemViewModel>();
			model.ProductsInSystem = new List<ProductOverviewViewModel>();

			foreach (var c in order.RetrieveItemsInOrder())
			{
				CartItemViewModel cmodel = new CartItemViewModel();
				cmodel.Quantity = c.Quantity;
				var mapper = mapextension.CartItemToProductViewModel();
				ProductViewModel p = mapper.Map<ProductViewModel>(c);
				cmodel.Product = p;
				model.ShoppingCart.ItemsInCart.Add(cmodel);
			}	
			foreach (Product p in productRepo.RetrieveAllProducts())
			{
				ProductOverviewViewModel pmodel = new ProductOverviewViewModel();
				pmodel.ProductId = p.RetrieveProductId();
				pmodel.ProductName = p.ProductName;
				model.ProductsInSystem.Add(pmodel);
			}
			return View("SingleOrder", model);
		}


		/// <summary>
		///     Create order
		/// </summary>
		/// <param name="model">UserOrderViewModel</param>
		/// <param name="orderingUser">User that orders</param>
		/// <returns>IAction</returns>
		[HttpPost]
		public async Task<IActionResult> CreateOrder(UserOrderViewModel model, string orderingUser)
		{
			var loggedInUser = HttpContext.Session.GetObjectFromJson<UserViewModel>("ComplexObject");
			var selectedUser = userRepo.RetrieveUserByUsername(orderingUser);
			List<CartItem> ListOfCartItems = CartItemViewModelToCartItem(loggedInUser.ShoppingCart.ItemsInCart);

			if(model.OrderDescription == null)
			{model.OrderDescription = "No description";}

			Order order = new Order(selectedUser, model.OrderDescription, 1, ListOfCartItems);
			order.SendAddress = model.SendAddress;
			order.SendZipcode = model.SendZipcode;
			order.SendPlace = model.SendPlace;
			orderRepo.CreateNewOrder(order);

			sessionHelper.UpdateCurrentSessionUVM(loggedInUser);
			await new MailController().OrderConfirmationMail(model);
			return RedirectToAction("Index", "Home");
		}


		/// <summary>
		///     Create order
		/// </summary>
		/// <param name="model">UserViewModel</param>
		/// <param name="selectedProduct">Product</param>
		/// <returns>IAction</returns>
		public IActionResult CreateOrderAdmin(UserOrderViewModel model, List<string> selectedProduct)
		{
			var selectedUser = userRepo.RetrieveUserByUsername(model.Username);
			if (selectedUser == null) RedirectToAction("Dashboard", "Home");
			var itemsToOrder = new List<CartItem>();

			//Create cartitems
			foreach (var item in selectedProduct)
			{
				var product = productRepo.RetrieveProductByName(item);
				var cartItem = new CartItem(1);
				cartItem.Product = product;
				cartItem.Quantity = 1;
				itemsToOrder.Add(cartItem);
			}
		
			if(model.OrderDescription == null)
			{model.OrderDescription = "No description";}

			var order = new Order(selectedUser, model.OrderDescription, 1, itemsToOrder);
			order.SendAddress = model.SendAddress;
			order.SendZipcode = model.SendZipcode;
			order.SendPlace = model.SendPlace;
			orderRepo.CreateNewOrder(order);

			return RedirectToAction("Overview", "Order");
		}


		/// <summary>
		///     Update an order from the backend
		/// </summary>
		/// <param name="model">UserOrderViewModel</param>
		/// <param name="selectedProduct">List of selected products as strings</param>
		/// <param name="orderToEdit">Identification of the order to edit</param>
		/// <returns>Actionresult that should go back to OrderOverview</returns>
		public IActionResult Update(UserOrderViewModel model, List<string> selectedProduct, int orderToEdit)
		{
			var orderToUpdate = orderRepo.RetrieveOrderById(orderToEdit);
			var itemsToOrder = new List<CartItem>();

			//Create cartitems
			foreach (var item in selectedProduct)
			{
				var product = productRepo.RetrieveProductByName(item);
				var cartItem = new CartItem(1);
				cartItem.Product = product;
				cartItem.Quantity = 1;
				itemsToOrder.Add(cartItem);
			}

			//New order to update the old one
			var newOrder = new Order(orderToUpdate.RetrieveCustomer(), orderToUpdate.OrderDetails, orderToUpdate.RetrieveOrderId(), itemsToOrder);
			newOrder.SendAddress = model.SendAddress;
			newOrder.SendPlace = model.SendPlace;
			newOrder.SendZipcode = model.SendZipcode;

			if (orderRepo.UpdateOrder(newOrder) == true)
			{return RedirectToAction("Overview", "Order");}
			return RedirectToAction("Single", "Order");
		}


		/// <summary>
		///     Delete order from system
		/// </summary>
		/// <param name="orderToDelete">Identification of order in system</param>
		/// <returns>Actionresult OrderOverview on success</returns>
		public IActionResult Delete(int orderToDelete)
		{
			if (orderRepo.DeleteOrder(orderToDelete) == true)
			{
				return RedirectToAction("Overview", "Order");
			}
			return RedirectToAction("Overview", "Order");
		}


		public List<CartItem> CartItemViewModelToCartItem(List<CartItemViewModel> CartItems )
		{
			//Set cartitems to cartitemviewmodels
			List<CartItem> ItemsInCart = new List<CartItem>();
			foreach (CartItemViewModel c in CartItems)
			{
				CartItem newcartitem = new CartItem(1);
				newcartitem.Quantity = c.Quantity;

				Product p = new Product(c.Product.ProductId, c.Product.ProductName);
				p.BtwPercentage = c.Product.BtwPercentage;
				p.ImageUrl = c.Product.ImageUrl;
				p.ProductAmount = c.Product.ProductAmount;
				p.ProductDesc = c.Product.ProductDesc;
				p.ProductPrice = c.Product.ProductPrice;
				p.ProductDiscount = c.Product.ProductDiscount;
				p.ProductStatus = c.Product.ProductStatus;

				newcartitem.Product = p;
				ItemsInCart.Add(newcartitem);
			}
			return ItemsInCart;
		}


	}
}
