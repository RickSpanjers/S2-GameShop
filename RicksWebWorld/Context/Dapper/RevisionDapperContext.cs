using Dapper;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Context.Dapper
{
	public class RevisionDapperContext : ConnectionHelper, IRevision
	{
		public bool CreateNewRevision(Revision r)
		{
			string sqlUserInsert = "INSERT INTO dbo.[Revision] (ProductID, RevisionTime) VALUES (@ProductId, @RevisionTime)";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { ProductId = r.RetrieveId(), RevisionTime = r.RevisionDateTime });
				return true;
			}
		}

		public bool DeleteRevisionsByProductId(int productId)
		{
			string sqlDelete = "DELETE FROM dbo.[Revision] WHERE ProductID = @ProductId";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					int affectedRows = connection.Execute(sqlDelete, new { ProductId = productId });
					return true;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return false;
				}
			}
		}

		public List<Revision> RetrieveAllRevisions()
		{
			string sqlDetails = "SELECT * FROM dbo.[Revision]";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					List<Revision> listOfMethods = connection.Query<Revision>(sqlDetails).ToList();
					return listOfMethods;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public Revision RetrieveRevisionById(int id)
		{
			string StoredProcedure = "GetOfferByID";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					var Detail = connection.QueryFirstOrDefault<Revision>(StoredProcedure, new { ID = id }, commandType: CommandType.StoredProcedure);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}
	}
}
