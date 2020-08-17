using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Interfaces
{
    public interface IPaymentMethod
    {
		bool CreateNewPaymentMethod(Payment p);
		List<Payment> RetrieveAllPayments();
		bool DeletePaymentMethodById(int Id);
		bool UpdatePaymentMethodById(Payment p);
		Payment RetrievePaymentMethodsById(int id);
		
	}
}
