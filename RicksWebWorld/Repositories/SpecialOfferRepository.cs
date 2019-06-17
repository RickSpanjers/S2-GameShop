using System.Collections.Generic;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Repositories
{
	public class SpecialOfferRepository
	{
		private readonly ISpecialOffer offerInterface;

		public SpecialOfferRepository(ISpecialOffer context)
		{
			offerInterface = context;
		}

		public bool CreateNewSpecialOffer(SpecialOffer s)
		{
			return offerInterface.CreateNewSpecialOffer(s);
		}

		public bool DeleteSpecialOfferByProductId(Product p)
		{
			return offerInterface.DeleteSpecialOfferByProductId(p);
		}

		public List<SpecialOffer> RetrieveAllOffers()
		{
			return offerInterface.RetrieveAllOffers() as List<SpecialOffer>;
		}

		public bool UpdateOfferById(SpecialOffer o)
		{
			return offerInterface.UpdateOfferById(o);
		}

		public SpecialOffer RetrieveOfferById(int id)
		{
			return offerInterface.RetrieveOfferById(id);
		}

		public SpecialOffer RetrieveOfferByProductID(int productId)
		{
			return offerInterface.RetrieveOfferByProductID(productId);
		}
	}
}