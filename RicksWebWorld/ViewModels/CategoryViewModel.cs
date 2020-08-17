using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RicksWebWorld.Models;

namespace RicksWebWorld.ViewModels
{
	public class CategoryViewModel
	{
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Please fill the field")]
		public string CategoryName { get; set; }

		public string CategoryDesc { get; set; }

		[Required(ErrorMessage = "You upload an image")]
		public string CategoryImg { get; set; }

		public List<CategoryOverviewViewModel> AllCategoriesInSystem { get; set; } = new List<CategoryOverviewViewModel>();
	}
}