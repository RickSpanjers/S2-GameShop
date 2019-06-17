using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.Context.MssqlContext;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Helpers;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;

namespace RicksWebWorld.Controllers
{
	public class HomeController : Controller
	{

		//Instances of repositories
		private readonly ProductRepository productRepo = new ProductRepository(new ProductMssqlContext());
		private readonly CategoryRepository categoryRepo = new CategoryRepository(new CategoryMssqlContext());
		private readonly RoleRepository roleRepo = new RoleRepository(new RoleMssqlContext());
		private readonly PermissionRepository permissionRepo = new PermissionRepository(new PermissionMssqlContext());
		private readonly UserRepository userRepo = new UserRepository(new UserMssqlContext());
		private readonly OrderRepository orderRepo = new OrderRepository(new OrderMssqlContext());
		private readonly PostRepository postRepo = new PostRepository(new PostMSSQLContext());
		private readonly UserController UC = new UserController();
		private readonly AutoMapperExtension mapextension = new AutoMapperExtension();
		private readonly SessionHelper sessionHelper = new SessionHelper();


		/// <summary>
		///     Returns the index page
		/// </summary>
		/// <returns>IActionresult</returns>
		public IActionResult Index()
		{
			HomeViewModel model = AutoMappingHomeViewModel();
			return View("Index", model);
		}


		/// <summary>
		///     Returns the dashboard page
		/// </summary>
		/// <returns>IActionresult</returns>
		public IActionResult Dashboard()
		{
			var model = new DashboardViewModel();
			model.CategoryList = categoryRepo.RetrieveAllCategories().Count;
			model.ProductList = productRepo.RetrieveAllProducts().Count;
			model.RoleList = roleRepo.RetrieveAllRoles().Count;
			model.UserList = userRepo.RetrieveAllUsers().Count;
			model.PermissionList = permissionRepo.RetrieveAllPermissions().Count;
			model.OrderList = orderRepo.RetrieveAllOrders().Count;
			model.ProductsSold = productRepo.RetrieveProductSold();

			return View("dashboard", model);
		}


		/// <summary>
		///     Returns about us page
		/// </summary>
		/// <returns>IActionresult</returns>
		public IActionResult About()
		{
			return View("AboutUs");
		}


		/// <summary>
		///     Returns contactpage
		/// </summary>
		/// <returns>IActionresult</returns>
		public IActionResult Contact()
		{
			return View("Contact");
		}


		/// <summary>
		///     Returns productoverview
		/// </summary>
		/// <returns>IActionresult</returns>
		public IActionResult Products()
		{
			HomeViewModel model = AutoMappingHomeViewModel();
			return View("Productoverview", model);
		}

		public IActionResult Blog()
		{
			BlogOverviewViewModel model = new BlogOverviewViewModel();
			model.PostsInSystem = new List<PostViewModel>();
			var mapper = mapextension.PostToPostViewModel();

			foreach(Post p in postRepo.RetrieveAllPosts())
			{
				PostViewModel pmodel = mapper.Map<PostViewModel>(p);
				model.PostsInSystem.Add(pmodel);
			}

			return View("Blog", model);
		}

		public IActionResult Post(int Post)
		{
			Post p = postRepo.RetrievePostById(Post);
			var mapper = mapextension.PostToPostMailViewModel();
			PostMailViewModel model = mapper.Map<PostMailViewModel>(p);
			return View("SinglePost", model);
		}

		/// <summary>
		///     Returns singleproduct page
		/// </summary>
		/// <param name="selectedProduct">Product that was selected</param>
		/// <returns>IActionResult</returns>
		public IActionResult Single(int selectedProduct)
		{
			var newModel = new ProductViewModel();
			var selected = productRepo.RetrieveProductById(selectedProduct);

			if (selected != null)
			{
				var mapperOne = mapextension.ProductToProductviewmodel();
				newModel = mapperOne.Map<ProductViewModel>(selected);
			}

			return View("SingleProduct", newModel);
		}

		/// <summary>
		///     Returns an error page
		/// </summary>
		/// <returns>IActionresult</returns>
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		/// <summary>
		/// Adds or removes an item from the user's wishlist
		/// </summary>
		/// <param name="product">The ID of the product to be added</param>
		/// <returns>Returns the productoverview page</returns>
		public IActionResult AddRemoveItemWishlist(int product)
		{
			var selectedProduct = productRepo.RetrieveProductById(product);

			//Set up automapping
			var mapper = mapextension.ProductToProductviewmodel();
			ProductViewModel pmodel = mapper.Map<ProductViewModel>(selectedProduct);

			//Get logged in user
			var loggedInUser = HttpContext.Session.GetObjectFromJson<UserViewModel>("ComplexObject");
			if (loggedInUser == null) return RedirectToAction("Login", "User");

			//Check if itemsincart is null
			if (loggedInUser.Wishlist == null){loggedInUser.Wishlist = new List<ProductViewModel>();}
			if (loggedInUser.Wishlist.Count == 0){loggedInUser.Wishlist.Add(pmodel);}
			else
			{
				var item = loggedInUser.Wishlist.SingleOrDefault(x => x.ProductName == pmodel.ProductName);
				if (item != null)
				{
					loggedInUser.Wishlist.Remove(item);
				}
				else
				{
					loggedInUser.Wishlist.Add(pmodel);
				}
			}

			sessionHelper.UpdateCurrentSessionUVM(loggedInUser);
			return RedirectToAction("Products", "Home");
		}


		public HomeViewModel AutoMappingHomeViewModel()
		{
			var model = new HomeViewModel();
			model.ListOfProducts = new List<ProductViewModel>();
			model.ListOfCategories = new List<CategoryOverviewViewModel>();

			//Setup automapping
			var mapperOne = mapextension.ProductToProductviewmodel();
			var mapperSO = mapextension.SpecialOfferToProductviewmodel();

			//For every product in system
			foreach (var singleProduct in productRepo.RetrieveAllProducts())
			{
				singleProduct.ProductCategories = categoryRepo.RetrieveCategoriesFromProduct(singleProduct);
				ProductViewModel pmodel = new ProductViewModel();
				pmodel.IsSpecialOffer = singleProduct.RetrieveSpecialOffer();

				//Check if product is specialoffer
				if (singleProduct is SpecialOffer){pmodel = mapperSO.Map<ProductViewModel>(singleProduct);}
				else{pmodel = mapperOne.Map<ProductViewModel>(singleProduct);}			

				//Filling categories for specific product
				foreach (Category c in categoryRepo.RetrieveCategoriesFromProduct(singleProduct))
				{
					//Set up automapping
					var mapperTwo = mapextension.CategoryToCategoryOverviewViewModel();
					CategoryOverviewViewModel m = mapperTwo.Map<CategoryOverviewViewModel>(c);
					pmodel.ProductCategories.Add(m);
				}

				model.ListOfProducts.Add(pmodel);
			}

			foreach (Category c in categoryRepo.RetrieveAllCategories())
			{
				//Set up automapping
				var mapperTwo = mapextension.CategoryToCategoryOverviewViewModel();
				CategoryOverviewViewModel m = mapperTwo.Map<CategoryOverviewViewModel>(c);
				model.ListOfCategories.Add(m);
			}

			foreach(Post p in postRepo.RetrieveAllPosts())
			{
				//Set up automapping
				var mapperThree = mapextension.PostToPostViewModel();
				PostViewModel pmodel = mapperThree.Map<PostViewModel>(p);
				model.PostsInSystem.Add(pmodel);
			}

			//Get currently logged in user
			var loggedInUser = HttpContext.Session.GetObjectFromJson<UserViewModel>("ComplexObject");
			model.UserLoggedIn = loggedInUser;

			return model;
		}
	}
}