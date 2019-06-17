using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;

namespace RicksWebWorld.Context.Release
{
	public class RevisionMssqlContext : ConnectionHelper, IRevision
	{
		public List<Revision> RetrieveAllRevisions()
		{
			List<Revision> allRevisions = new List<Revision>();
			ProductRepository ProductRepo = new ProductRepository(new ProductMssqlContext());
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[Revision]";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int id = Convert.ToInt32(dr["ID"]);
					int productId = Convert.ToInt32(dr["ProductID"]);
					DateTime revisionTime = Convert.ToDateTime(dr["RevisionTime"]);
					Product p = ProductRepo.RetrieveProductById(productId);
					Revision revision = new Revision(id, p);
					revision.RevisionDateTime = revisionTime;
					allRevisions.Add(revision);
				}
				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return allRevisions;
		}


		public bool DeleteRevisionsByProductId(int productId)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "DELETE FROM dbo.[Revision] WHERE ProductID = @ProductId";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@ProductID", productId);

				cmd.ExecuteNonQuery();
				cnn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return false;
		}


		public bool CreateNewRevision(Revision r)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "INSERT INTO dbo.[Revision] (ProductID, RevisionTime) VALUES (@ProductId, @RevisionTime)";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@ProductId", r.RetrieveProduct().RetrieveProductId());
				newCmd.Parameters.AddWithValue("@RevisionTime", r.RevisionDateTime);
				newCmd.ExecuteNonQuery();
				cnn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return false;
		}

		public Revision RetrieveRevisionById(int id)
		{
			ProductRepository ProductRepo = new ProductRepository(new ProductMssqlContext());
			Revision revision = new Revision(1, null);
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				SqlCommand newCmd = CreateSQLCommandStoredProcedure("GetOfferByID", cnn);
				newCmd.Parameters.AddWithValue("@ID", id);
				SqlDataReader dr = newCmd.ExecuteReader();
				while (dr.Read())
				{
					int revisionId = Convert.ToInt32(dr["ID"]);
					int productId = Convert.ToInt32(dr["ProductID"]);
					DateTime revisionTime = Convert.ToDateTime(dr["RevisionTime"]);
					Product p = ProductRepo.RetrieveProductById(productId);
					revision = new Revision(revisionId, p);
					revision.RevisionDateTime = revisionTime;
				}
				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return revision;
		}
	}
}