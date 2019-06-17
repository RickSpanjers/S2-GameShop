using System.Collections.Generic;
using RicksWebWorld.Models;

namespace RicksWebWorld.Interfaces
{
	public interface ISpecialOffer
	{
		bool CreateNewSpecialOffer(SpecialOffer s);
		bool DeleteSpecialOfferByProductId(Product p);
		List<SpecialOffer> RetrieveAllOffers();
		bool UpdateOfferById(SpecialOffer o);
		SpecialOffer RetrieveOfferById(int id);
		SpecialOffer RetrieveOfferByProductID(int productId);
	}
}