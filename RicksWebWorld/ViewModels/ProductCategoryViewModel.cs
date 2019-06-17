using System.Collections.Generic;
using RicksWebWorld.Models;

namespace RicksWebWorld.ViewModels
{
	public class ProductCategoryViewModel
	{
		public List<CategoryOverviewViewModel> ListOfCategories = new List<CategoryOverviewViewModel>();
		public List<ProductViewModel> ListOfProducts = new List<ProductViewModel>();
		public UserViewModel UserLoggedIn { get; set; }
	}
}