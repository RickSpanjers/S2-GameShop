using System.Collections.Generic;
using RicksWebWorld.Models;

namespace RicksWebWorld.ViewModels
{
	public class DashboardViewModel
	{
		public int UserList { get; set; }

		public int ProductList { get; set; }

		public int RoleList { get; set; }

		public int CategoryList { get; set; } 

		public int OrderList { get; set; } 

		public int PermissionList { get; set; }

		public int ProductsSold { get; set; }
	}
}