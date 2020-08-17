using System;
using System.Collections.Generic;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Context.Debug
{
	public class RevisionTestContext : IRevision
	{
		private readonly List<Revision> ListOfRevisions = new List<Revision>();

		public RevisionTestContext()
		{
			var R1 = new Revision(1, new Product(1));
			var R2 = new Revision(2, new Product(2));
			var R3 = new Revision(3, new Product(3));
			var R4 = new Revision(4, new Product(4));

			R1.RevisionDateTime = DateTime.Now;
			R2.RevisionDateTime = DateTime.Now;
			R3.RevisionDateTime = DateTime.Now;
			R4.RevisionDateTime = DateTime.Now;

			ListOfRevisions.Add(R1);
			ListOfRevisions.Add(R2);
			ListOfRevisions.Add(R3);
			ListOfRevisions.Add(R4);
		}

		public List<Revision> RetrieveAllRevisions()
		{
			return ListOfRevisions;
		}


		public bool DeleteRevisionsByProductId(int productId)
		{
			foreach (var single in ListOfRevisions)
			{
				if (single.RetrieveProduct().RetrieveProductId() == productId)
				{
					ListOfRevisions.Remove(single);
					return true;
				}
			}
			return false;
		}


		public bool CreateNewRevision(Revision r)
		{
			ListOfRevisions.Add(r);
			return true;
		}


		public Revision RetrieveRevisionById(int id)
		{
			foreach (var singleitem in RetrieveAllRevisions())
			{
				if (singleitem.RetrieveId() == id)
				{
					return singleitem;
				}
					
			}
				
			return null;
		}
	}
}