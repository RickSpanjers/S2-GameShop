using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class OrderViewModel
    {
		public UserOverviewViewModel User { get; set; }
		public List<CartItemViewModel> ItemsInOrder { get; set; } = new List<CartItemViewModel>();
		public int OrderId { get; set; }
		public string OrderDetails { get; set; }

		[Required(ErrorMessage = "You must an address")]
		public string SendAddress { get; set; }

		[Required(ErrorMessage = "You must a zipcode")]
		public string SendZipcode { get; set; }

		[Required(ErrorMessage = "You must a place")]
		public string SendPlace { get; set; }

	
	}
}
