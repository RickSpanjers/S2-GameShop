using Microsoft.VisualStudio.TestTools.UnitTesting;
using RicksWebWorld.Context.Debug;
using RicksWebWorld.Controllers;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestWebWorld
{
    [TestClass]
    public class UnitTestSpecialOffer
    {
		SpecialOfferRepository SPTestRepo = new SpecialOfferRepository(new SpecialOfferTestContext());
		ProductRepository PTestRepo = new ProductRepository(new ProductTestContext());

        [TestMethod]
		public void CreateNewSpecialOffer()
		{
			var p = PTestRepo.RetrieveProductById(1);
			SpecialOffer s = new SpecialOffer(5, 12, p.RetrieveProductId(), p.ProductName);
			s.StartTime = DateTime.Now;
			s.EndTime = DateTime.Now;

			SPTestRepo.CreateNewSpecialOffer(s);
			var Total = SPTestRepo.RetrieveAllOffers();
			Assert.IsTrue(Total.Count > 4);
		}

		[TestMethod]
		public void DeleteSpecialOfferByProductId()
		{
			var p = PTestRepo.RetrieveProductById(2);
			SPTestRepo.DeleteSpecialOfferByProductId(p);
			var All = SPTestRepo.RetrieveAllOffers();
			foreach (var single in All)
			{
				Assert.IsFalse(single.RetrieveProductId() == 2);
			}
		}

		[TestMethod]
		public void RetrieveAllOffers()
		{
			var All = SPTestRepo.RetrieveAllOffers();
			Assert.IsTrue(SPTestRepo.RetrieveAllOffers() != null);	
		}
	}
}
