using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;

namespace RicksWebWorld.Controllers
{
	public class SpecialOfferController : Controller
	{
		private readonly SpecialOfferRepository offerRepo = new SpecialOfferRepository(new SpecialOfferMssqlContext());
		private readonly ProductRepository productRep = new ProductRepository(new ProductMssqlContext());
		private readonly AutoMapperExtension mapextension = new AutoMapperExtension();


		/// <summary>
		///     Retrieve all offers
		/// </summary>
		/// <returns>List of special offers</returns>
		public List<SpecialOffer> RetrieveAllOffers()
		{
			var allOffers = offerRepo.RetrieveAllOffers();
			return allOffers;
		}


		/// <summary>
		///     Add offer to system
		/// </summary>
		/// <param name="model">ProductViewModel</param>
		/// <param name="p">Product</param>
		public void Add(ProductViewModel model, Product p)
		{
			if (model.OfferPrice != 0)
			{
				var selectedProduct = productRep.RetrieveProductByName(p.ProductName);

				if (selectedProduct.RetrieveProductId() != 0)
				{
					var specialOffer = new SpecialOffer(1, model.OfferPrice, selectedProduct.RetrieveProductId(), selectedProduct.ProductName);
					specialOffer.StartTime = model.OfferStart;
					specialOffer.EndTime = model.OfferEnd;
					offerRepo.CreateNewSpecialOffer(specialOffer);
				}
			}
		}

		/// <summary>
		///     Delete offer from system
		/// </summary>
		/// <param name="p">Product</param>
		public void Delete(Product p)
		{
			var selectedProduct = productRep.RetrieveProductByName(p.ProductName);

			if (selectedProduct.RetrieveProductId() != 0) offerRepo.DeleteSpecialOfferByProductId(selectedProduct);
		}

		/// <summary>
		///     Edit offer in system
		/// </summary>
		/// <param name="model">ProductViewModel</param>
		/// <param name="p">Product</param>
		public void Update(ProductViewModel model, Product p)
		{
			if(model.OfferPrice == 0)
			{
				Delete(p);
			}
			else
			{
				List<SpecialOffer> allOffers = offerRepo.RetrieveAllOffers();
				if (allOffers.Where(x => x.RetrieveProductId() == p.RetrieveProductId()).FirstOrDefault() != null)
				{
					foreach (var singleOffer in allOffers)
					{
						if (singleOffer.RetrieveProductId() == p.RetrieveProductId())
						{
							var specialOffer = new SpecialOffer(singleOffer.RetrieveOfferId(), model.OfferPrice, p.RetrieveProductId(), p.ProductName);
							specialOffer.StartTime = model.OfferStart;
							specialOffer.EndTime = model.OfferEnd;

							offerRepo.UpdateOfferById(specialOffer);
						}
					}
				}
				else
				{
					Add(model, p);
				}
			}			
		}
	}
}