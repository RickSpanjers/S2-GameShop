using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RicksWebWorld.Models;

namespace RicksWebWorld.ViewModels
{
	public class RolePermissionViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "You must enter a name")]
		public string Name { get; set; }

		public string Description { get; set; }

		public List<PermissionViewModel> AllPermissionsInSystem { get; set; } = new List<PermissionViewModel>();

		public List<RoleOverviewViewModel> AllRolesInSystem { get; set; } = new List<RoleOverviewViewModel>();

		public List<RoleOverviewViewModel> AllRolesWithPermission { get; set; } = new List<RoleOverviewViewModel>();

		public User UserLoggedIn { get; set; }
	}
}