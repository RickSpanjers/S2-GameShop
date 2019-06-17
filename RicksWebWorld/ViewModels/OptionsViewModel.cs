using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class OptionsViewModel
    {
		public List<PaymentMethodViewModel> AllMethodsInSystem = new List<PaymentMethodViewModel>();

		public int PaymentMethodId { get; set; }

		[Required(ErrorMessage = "You enter a name")]
		public string PaymentMethodName { get; set; }

		public string PaymentMethodDescription { get; set; }

	}
}
