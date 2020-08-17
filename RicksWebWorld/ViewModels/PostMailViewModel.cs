using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class PostMailViewModel
    {
		public int Id { get; set; }
		[Required(ErrorMessage = "You must enter a name")]
		public string Title { get; set; }
		[Required(ErrorMessage = "You must enter content")]
		public string Content { get; set; }
		[Required(ErrorMessage = "You must enter an excerpt")]
		public string Excerpt { get; set; }
		public DateTime Postdate { get; set; }
		public string PostImage { get; set; }
		public int Status { get; set; }
		[Required(ErrorMessage = "You must enter a name")]
		public string FullName { get; set; }
		[Required(ErrorMessage = "You must enter an email")]
		public string Email { get; set; }
		public string Details { get; set; }
	}
}
