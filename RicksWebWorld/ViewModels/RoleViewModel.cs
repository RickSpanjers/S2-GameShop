using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RicksWebWorld.Models;

namespace RicksWebWorld.ViewModels
{
	public class RoleViewModel
	{
		public List<RoleOverviewViewModel> AllRolesInSystem = new List<RoleOverviewViewModel>();

		public int RoleId { get; set; }

		[Required(ErrorMessage = "You must enter a name")]
		public string RoleName { get; set; }

		public string RoleDescription { get; set; }

		public User UserLoggedIn { get; set; }
	}
}