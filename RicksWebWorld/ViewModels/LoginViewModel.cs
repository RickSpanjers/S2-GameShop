using System;
using System.ComponentModel.DataAnnotations;

namespace RicksWebWorld.ViewModels
{
	[Serializable]
	public class LoginViewModel
	{
		[Required(ErrorMessage = "You must enter a username")]
		[StringLength(100)]
		public string Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}
}