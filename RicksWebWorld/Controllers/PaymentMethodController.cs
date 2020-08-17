using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.Context.Dapper;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;

namespace RicksWebWorld.Controllers
{
    public class PaymentMethodController : Controller
    {
		private readonly PaymentMethodRepository payRep = new PaymentMethodRepository(new PaymentDapperContext());
		private AutoMapperExtension mapextension = new AutoMapperExtension();

		/// <summary>
		/// OptionsOverview
		/// </summary>
		/// <returns>Return IActionresult</returns>
		public IActionResult Overview()
		{
			var model = new OptionsViewModel();
			model.AllMethodsInSystem = new List<PaymentMethodViewModel>();

			foreach(Payment p in payRep.RetrieveAllPayments())
			{
				var mapper = mapextension.PaymentToPaymentViewModel();
				PaymentMethodViewModel pmodel = mapper.Map<PaymentMethodViewModel>(p);
				model.AllMethodsInSystem.Add(pmodel);
			}
			return View("OptionsOverview", model);
		}

	
		/// <summary>
		/// OptionsOverview Edit
		/// </summary>
		/// <param name="selectedMethod">ID of method to edit</param>
		/// <returns>IActionresult</returns>
		public IActionResult OverviewEdit(int selectedMethod)
		{
			var selected = payRep.RetrievePaymentMethodsById(selectedMethod);

			if (selected.RetrieveID()!= 0)
			{
				var mapper = mapextension.PaymentToOptionsViewModel();
				OptionsViewModel model = mapper.Map<OptionsViewModel>(selected);
				model.AllMethodsInSystem = new List<PaymentMethodViewModel>();

				foreach (Payment p in payRep.RetrieveAllPayments())
				{
					var mapperTwo = mapextension.PaymentToPaymentViewModel();
					PaymentMethodViewModel pmodel = mapperTwo.Map<PaymentMethodViewModel>(p);
					model.AllMethodsInSystem.Add(pmodel);
				}

				return View("OptionsOverview", model);
			}

			return View("OptionsOverview");
		}

	
		/// <summary>
		/// Add a new payment method
		/// </summary>
		/// <param name="model">OptionsViewModel</param>
		/// <returns>IActionresult</returns>
		[HttpPost]
		public IActionResult Add(OptionsViewModel model)
		{
			Payment p = new Payment(1, model.PaymentMethodName, model.PaymentMethodDescription);
			if (model.PaymentMethodDescription == null){model.PaymentMethodDescription = "No description";}
			if (payRep.CreateNewPaymentMethod(p)){return RedirectToAction("Overview", "PaymentMethod");}
			return RedirectToAction("Overview", "PaymentMethod");
		}


		/// <summary>
		/// Delete payment method
		/// </summary>
		/// <param name="paymentMethodToDelete">ID of method to delete</param>
		/// <returns>IActionresult</returns>
		[HttpPost]
		public IActionResult Delete(int paymentMethodToDelete)
		{
			var selected = payRep.RetrievePaymentMethodsById(paymentMethodToDelete);
			if (selected.RetrieveID() != 0)
			{
				if (payRep.DeletePaymentMethodById(selected.RetrieveID()))
				{
					return RedirectToAction("Overview", "PaymentMethod");
				}

			}

			return RedirectToAction("Overview", "PaymentMethod");
		}

		
		/// <summary>
		/// Edit payment method
		/// </summary>
		/// <param name="model">OptionsViewModel</param>
		/// <param name="paymentMethodToEdit">ID of the method to edit</param>
		/// <returns>IActionresult</returns>
		[HttpPost]
		public IActionResult Update(OptionsViewModel model, int paymentMethodToEdit)
		{
			var selected = payRep.RetrievePaymentMethodsById(paymentMethodToEdit);
			if (selected.RetrieveID() != 0)
			{
				var newPaymentMethod = new Payment(selected.RetrieveID(), model.PaymentMethodName, model.PaymentMethodDescription);
				payRep.UpdatePaymentMethodById(newPaymentMethod);
				return RedirectToAction("Overview", "PaymentMethod", selected);
			}
			return RedirectToAction("Overview", "PaymentMethod");
		}
	}
}