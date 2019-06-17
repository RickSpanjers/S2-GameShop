using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class BlogOverviewViewModel
    {
		public List<PostViewModel> PostsInSystem { get; set; } = new List<PostViewModel>();

		[Required(ErrorMessage = "You must enter a name")]
		public string FullName { get; set; }

		[Required(ErrorMessage = "You must enter an email")]
		public string Email { get; set; }
		public string Details { get; set; }
	}
}
