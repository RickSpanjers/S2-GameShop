using System;
using System.Collections.Generic;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Context.Debug
{
	public class SpecialOfferTestContext : ISpecialOffer
	{
		private readonly List<SpecialOffer> ListOfSpecialOffers = new List<SpecialOffer>();

		public SpecialOfferTestContext()
		{
			var p = new Product(1, "Test");

			var SP1 = new SpecialOffer(1, 15, p.RetrieveProductId(), p.ProductName);
			var SP2 = new SpecialOffer(2, 15, p.RetrieveProductId(), p.ProductName);
			var SP3 = new SpecialOffer(3, 15, p.RetrieveProductId(), p.ProductName);
			var SP4 = new SpecialOffer(4, 15, p.RetrieveProductId(), p.ProductName);

			SP1.StartTime = DateTime.Now;
			SP1.EndTime = DateTime.Now;
			SP2.StartTime = DateTime.Now;
			SP2.EndTime = DateTime.Now;
			SP3.StartTime = DateTime.Now;
			SP3.EndTime = DateTime.Now;
			SP4.StartTime = DateTime.Now;
			SP4.EndTime = DateTime.Now;

			ListOfSpecialOffers.Add(SP1);
			ListOfSpecialOffers.Add(SP2);
			ListOfSpecialOffers.Add(SP3);
			ListOfSpecialOffers.Add(SP4);
		}

		public bool CreateNewSpecialOffer(SpecialOffer S)
		{
			ListOfSpecialOffers.Add(S);
			return true;
		}

		public bool DeleteSpecialOfferByProductId(Product p)
		{
			foreach (var single in ListOfSpecialOffers)
			{
				if (single.RetrieveProductId() == p.RetrieveProductId())
				{
					ListOfSpecialOffers.Remove(single);
					return true;
				}
			}

			return false;
		}

		public List<SpecialOffer> RetrieveAllOffers()
		{
			return ListOfSpecialOffers;
		}

		public SpecialOffer RetrieveOfferById(int id)
		{
			foreach(var single in ListOfSpecialOffers)
			{
				if(single.RetrieveOfferId() == id)
				{
					return single;
				}
			}

			return null;
		}

		public SpecialOffer RetrieveOfferByProductID(int productId)
		{
			throw new NotImplementedException();
		}

		public bool UpdateOfferById(SpecialOffer o)
		{
			foreach (var single in ListOfSpecialOffers)
			{
				if (single.RetrieveOfferId() == o.RetrieveOfferId())
				{
					ListOfSpecialOffers.Remove(single);
					ListOfSpecialOffers.Add(o);
					return true;
				}

			}
			return false;
		}
	}
}