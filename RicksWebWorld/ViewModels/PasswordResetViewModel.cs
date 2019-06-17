using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
	public class PasswordResetViewModel
	{
		[Required(ErrorMessage = "You need to fill this field")]
		public string Username { get; set;
		}
		[Required(ErrorMessage = "You need to fill this field")]
		public string Email { get; set; }

		public int Id { get; set; }

		[Required(ErrorMessage = "You need to fill this field")]
		public string Password { get; set; }

		[Required(ErrorMessage = "You need to fill this field")]
		public string PasswordRepeat { get; set; }
	}
}
