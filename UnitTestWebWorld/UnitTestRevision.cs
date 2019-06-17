using Microsoft.VisualStudio.TestTools.UnitTesting;
using RicksWebWorld.Context.Debug;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestWebWorld
{
	[TestClass]
	public class UnitTestRevision
	{
		RevisionRepository RevRepoTest = new RevisionRepository(new RevisionTestContext());

		[TestMethod]
		public void DeleteRevisionsByProductIdTest()
		{
			var result = RevRepoTest.DeleteRevisionsByProductId(2);
			var TotalRevs = RevRepoTest.RetrieveAllRevisions();

			foreach (var single in TotalRevs)
			{
				Assert.IsFalse(single.RetrieveId() == 2);
			}
		}

		[TestMethod]
		public void CreateNewRevision01()
		{
			Revision R = new Revision(5, new Product(5), DateTime.Now);
			RevRepoTest.CreateNewRevision(R);

			var AllRevisions = RevRepoTest.RetrieveAllRevisions();
			Assert.IsTrue(AllRevisions.Count > 4);
		}

		[TestMethod]
		public void CreateNewRevision02()
		{
			Revision R = new Revision(5, new Product(5), DateTime.Now);
			RevRepoTest.CreateNewRevision(R);

			var AddedRevision = RevRepoTest.RetrieveRevisionById(5);
			Assert.IsTrue(AddedRevision.RetrieveId() == 5);
		}

		[TestMethod]
		public void RetrieveAllMethods01()
		{
			var AllRevisions = RevRepoTest.RetrieveAllRevisions();
			Assert.IsTrue(RevRepoTest.RetrieveAllRevisions() != null);
		}

		[TestMethod]
		public void RetrieveAllMethods02()
		{
			var AllRevisions = RevRepoTest.RetrieveAllRevisions();
			Assert.IsTrue(AllRevisions.Count > 4);
		}



	}
}
