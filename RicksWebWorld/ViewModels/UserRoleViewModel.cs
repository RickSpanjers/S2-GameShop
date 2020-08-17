using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RicksWebWorld.Models;

namespace RicksWebWorld.ViewModels
{
	public class UserRoleViewModel
	{
		[Required(ErrorMessage = "You must enter a username")]
		[StringLength(100)]
		public string Username { get; set; }

		[Required(ErrorMessage = "You must enter a password")]
		[StringLength(100)]
		public string Password { get; set; }

		[Required(ErrorMessage = "You must enter a firstname")]
		[StringLength(100)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "You must enter a lastname")]
		[StringLength(100)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "You must enter an e-mail")]
		[StringLength(100)]
		public string Email { get; set; }

		[Required(ErrorMessage = "You must enter an address")]
		public string Address { get; set; }

		[Required(ErrorMessage = "You must enter a zipcode")]
		public string Zipcode { get; set; }

		[Required(ErrorMessage = "You must enter a place")]
		public string Place { get; set; }

		public List<RoleOverviewViewModel> AllUserRoles { get; set; } = new List<RoleOverviewViewModel>();

		public List<RoleOverviewViewModel> RolesInUser { get; set; } = new List<RoleOverviewViewModel>();

		public List<UserOverviewViewModel> UsersInSystem { get; set; } = new List<UserOverviewViewModel>();

		public User UserLoggedIn { get; set; }
	}
}