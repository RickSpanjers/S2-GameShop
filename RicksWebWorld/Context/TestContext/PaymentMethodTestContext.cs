using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Context.Debug
{
    public class PaymentMethodTestContext : IPaymentMethod
    {
		private readonly List<Payment> ListOfMethods = new List<Payment>();

		public PaymentMethodTestContext()
		{
			ListOfMethods.Add(new Payment(1, "P1", "No Description"));
			ListOfMethods.Add(new Payment(2, "P2", "No Description"));
			ListOfMethods.Add(new Payment(3, "P3", "No Description"));
			ListOfMethods.Add(new Payment(4, "P4", "No Description"));
		}

		public bool CreateNewPaymentMethod(Payment p)
		{
			try
			{
				ListOfMethods.Add(p);
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}

		public List<Payment> RetrieveAllPayments()
		{
			return ListOfMethods;
		}

		public bool DeletePaymentMethodById(int Id)
		{
			foreach (var single in ListOfMethods)
				if (single.RetrieveID() == Id)
				{
					ListOfMethods.Remove(single);
					return true;
				}

			return false;
		}

		public bool UpdatePaymentMethodById(Payment p)
		{
			foreach (var single in ListOfMethods)
			{
				if (single.RetrieveID() == p.RetrieveID())
				{
					ListOfMethods.Remove(single);
					ListOfMethods.Add(p);
					return true;
				}
			}

			return false;
		}

		public Payment RetrievePaymentMethodsById(int id)
		{
			foreach (var singleitem in ListOfMethods)
				if (singleitem.RetrieveID() == id)
					return singleitem;

			return null;
		}
	}
}
