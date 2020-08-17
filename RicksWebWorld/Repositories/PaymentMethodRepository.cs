using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Repositories
{
    public class PaymentMethodRepository
    {
		private readonly IPaymentMethod paymentInterface;

		public PaymentMethodRepository(IPaymentMethod context)
		{
			paymentInterface = context;
		}

		public bool CreateNewPaymentMethod(Payment p)
		{
			return paymentInterface.CreateNewPaymentMethod(p);
		}

		public List<Payment> RetrieveAllPayments()
		{
			return paymentInterface.RetrieveAllPayments() as List<Payment>;
		}

		public bool DeletePaymentMethodById(int Id)
		{
			return paymentInterface.DeletePaymentMethodById(Id);
		}

		public bool UpdatePaymentMethodById(Payment p)
		{
			return paymentInterface.UpdatePaymentMethodById(p);
		}
		
		public Payment RetrievePaymentMethodsById(int Id)
		{
			return paymentInterface.RetrievePaymentMethodsById(Id);
		}
		
	}
}
