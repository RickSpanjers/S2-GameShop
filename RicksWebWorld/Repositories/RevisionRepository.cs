using System.Collections.Generic;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Repositories
{
	public class RevisionRepository
	{
		private readonly IRevision revisionInterface;

		public RevisionRepository(IRevision context)
		{
			revisionInterface = context;
		}

		public List<Revision> RetrieveAllRevisions()
		{
			return revisionInterface.RetrieveAllRevisions() as List<Revision>;
		}

		public bool DeleteRevisionsByProductId(int productId)
		{
			return revisionInterface.DeleteRevisionsByProductId(productId);
		}

		public bool CreateNewRevision(Revision r)
		{
			return revisionInterface.CreateNewRevision(r);
		}

		public Revision RetrieveRevisionById(int id)
		{
			return revisionInterface.RetrieveRevisionById(id);
		}
	}
}