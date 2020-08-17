using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class HomeViewModel
    {
		public List<CategoryOverviewViewModel> ListOfCategories = new List<CategoryOverviewViewModel>();
		public List<ProductViewModel> ListOfProducts = new List<ProductViewModel>();
		public UserViewModel UserLoggedIn { get; set; }

		public List<PostViewModel> PostsInSystem { get; set; } = new List<PostViewModel>();
	}
}
