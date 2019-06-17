using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class ProductOverviewViewModel
    {
		public int ProductId { get; set; }
		public string ProductName{ get; set; }
		public List<CategoryOverviewViewModel> CategoriesInProduct { get; set; }
	}
}
