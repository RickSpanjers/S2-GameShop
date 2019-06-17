using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;

namespace RicksWebWorld.Controllers
{
	public class RevisionController : Controller
	{
		private readonly ProductRepository productRep = new ProductRepository(new ProductMssqlContext());
		private readonly RevisionRepository revisionRep = new RevisionRepository(new RevisionMssqlContext());
		private readonly AutoMapperExtension mapextension = new AutoMapperExtension();

		/// <summary>
		///     Add revision to system
		/// </summary>
		/// <param name="p">Product</param>
		/// <returns>True/false</returns>
		public bool Add(Product p)
		{
			var selectedProduct = productRep.RetrieveProductByName(p.ProductName);
			if (selectedProduct.RetrieveProductId() != 0)
			{
				var revision = new Revision(1, selectedProduct);
				revision.RevisionDateTime = DateTime.Now;
				if (revisionRep.CreateNewRevision(revision))
					return true;
				return false;
			}
			return false;
		}

		/// <summary>
		///     Delete revision from system
		/// </summary>
		/// <param name="productToDelete">Id of product</param>
		/// <returns></returns>
		public bool Delete(int productToDelete)
		{
			var selectedProduct = productRep.RetrieveProductById(productToDelete);
			if (selectedProduct.RetrieveProductId() != 0)
				if (revisionRep.DeleteRevisionsByProductId(selectedProduct.RetrieveProductId()))
					return true;
				else
					return false;
			return false;
		}

		/// <summary>
		///     Retrieve all revisions from product
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		public List<Revision> RetrieveAllRevisionsByProductId(int productId)
		{
			var listOfRevision = new List<Revision>();
			var allRevisions = revisionRep.RetrieveAllRevisions();

			foreach (var singleRevision in allRevisions)
			{
				if (singleRevision.RetrieveProduct().RetrieveProductId() == productId)
				{
					listOfRevision.Add(singleRevision);
				}		
			}
			
			return listOfRevision;
		}
	}
}