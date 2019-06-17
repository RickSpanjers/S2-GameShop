using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class PermissionViewModel
    {
		public string Description { get; set; }
		public int Id { get; set; }

		[Required(ErrorMessage = "You must enter a name")]
		public string Name { get; set; }
	}
}
