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
	public class SpecialOfferDapperContext : ConnectionHelper, ISpecialOffer
	{
		public bool CreateNewSpecialOffer(SpecialOffer s)
		{
			string sqlUserInsert = "INSERT INTO dbo.[SpecialOffer] (Startdate, Enddate, OfferPrice, ProductID) VALUES (@Startdate, @Enddate, @OfferPrice, @ProductId)";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { Startdate = s.StartTime, Enddate = s.EndTime, OfferPrice =  s.RetrieveOfferPrice(), ProductId = s.RetrieveOfferId()});
				return true;
			}
		}

		public bool DeleteSpecialOfferByProductId(Product p)
		{
			string sqlDelete = "DELETE FROM dbo.[SpecialOffer] WHERE ProductID = @ProductId";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					int affectedRows = connection.Execute(sqlDelete, new { ProductId = p.RetrieveProductId() });
					return true;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return false;
				}
			}
		}

		public List<SpecialOffer> RetrieveAllOffers()
		{
			string sqlDetails = "SELECT * FROM dbo.[SpecialOffer]";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					List<SpecialOffer> listOfMethods = connection.Query<SpecialOffer>(sqlDetails).ToList();
					return listOfMethods;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}
			}
		}

		public SpecialOffer RetrieveOfferById(int id)
		{
			string StoredProcedure = "GetOfferByID";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					SpecialOffer Detail = connection.QueryFirstOrDefault<SpecialOffer>(StoredProcedure, new { ID = id }, commandType: CommandType.StoredProcedure);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public SpecialOffer RetrieveOfferByProductID(int productId)
		{
			string Query = "SELECT * FROM dbo.[SpecialOffer] where ProductID = @ProductID";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					SpecialOffer Detail = connection.QueryFirstOrDefault<SpecialOffer>(Query, new { ID = productId }, commandType: CommandType.Text);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public bool UpdateOfferById(SpecialOffer o)
		{
			string sqlUserInsert = "UPDATE dbo.[SpecialOffer] SET Startdate=@Start, Enddate=@End, OfferPrice=@OfferPrice WHERE ProductID = @ProductId";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { Start = o.StartTime, End = o.EndTime, OfferPrice = o.RetrieveOfferPrice(), ProductId = o.RetrieveProductId() });
				return true;
			}
		}
	}
}
