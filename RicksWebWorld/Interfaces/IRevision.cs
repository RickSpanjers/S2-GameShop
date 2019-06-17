using System.Collections.Generic;
using RicksWebWorld.Models;

namespace RicksWebWorld.Interfaces
{
	public interface IRevision
	{
		List<Revision> RetrieveAllRevisions();
		bool DeleteRevisionsByProductId(int productId);
		bool CreateNewRevision(Revision r);
		Revision RetrieveRevisionById(int id);
	}
}