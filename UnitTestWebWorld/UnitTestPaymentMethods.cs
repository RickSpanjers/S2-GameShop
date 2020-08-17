using Microsoft.VisualStudio.TestTools.UnitTesting;
using RicksWebWorld.Context.Debug;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestWebWorld
{
	[TestClass]
	public class UnitTestPaymentMethods
    {
		PaymentMethodRepository PaymentTestRepo = new PaymentMethodRepository(new PaymentMethodTestContext());
		
		[TestMethod]
		public void AddMethod()
		{
			Payment p = new Payment(5, "P5", "None");
			var result = PaymentTestRepo.CreateNewPaymentMethod(p);
			var Total = PaymentTestRepo.RetrieveAllPayments();
			Assert.IsTrue(Total.Count > 4);

		}

		[TestMethod]
		public void RetrieveAllMethods()
		{
			var Total = PaymentTestRepo.RetrieveAllPayments();
			Assert.IsTrue(PaymentTestRepo.RetrieveAllPayments() != null);
		}

		[TestMethod]
		public void EditRole()
		{
			Payment p = new Payment(1, "P5", "None");
			var result = PaymentTestRepo.UpdatePaymentMethodById(p);
			var Retrieved = PaymentTestRepo.RetrievePaymentMethodsById(1);
			Assert.IsTrue(Retrieved.RetrieveName() == "P5");
		}

		[TestMethod]
		public void DeleteRole()
		{
			var result = PaymentTestRepo.DeletePaymentMethodById(2);
			var Methods = PaymentTestRepo.RetrieveAllPayments();
			Assert.IsFalse(Methods.Count > 4);
		}
	}
}
