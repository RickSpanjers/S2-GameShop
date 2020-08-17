using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class UserOverviewViewModel
    {
		public string Username { get; set; }
		public string Email { get; set; }
		public int Id { get; set; }
		public List<RoleOverviewViewModel> RolesInUser { get; set; } = new List<RoleOverviewViewModel>();
    }
}
