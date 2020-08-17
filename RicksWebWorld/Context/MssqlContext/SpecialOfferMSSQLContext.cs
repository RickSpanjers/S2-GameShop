using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;

namespace RicksWebWorld.Context.Release
{
	public class SpecialOfferMssqlContext : ConnectionHelper, ISpecialOffer
	{
		public bool CreateNewSpecialOffer(SpecialOffer s)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "INSERT INTO dbo.[SpecialOffer] (Startdate, Enddate, OfferPrice, ProductID) VALUES (@Startdate, @Enddate, @OfferPrice, @ProductId)";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@Startdate", s.StartTime);
				newCmd.Parameters.AddWithValue("@Enddate", s.EndTime);
				newCmd.Parameters.AddWithValue("@OfferPrice", s.RetrieveOfferPrice());
				newCmd.Parameters.AddWithValue("@ProductId", s.RetrieveProductId());

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


		public bool DeleteSpecialOfferByProductId(Product p)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "DELETE FROM dbo.[SpecialOffer] WHERE ProductID = @ProductId";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@ProductId", p.RetrieveProductId());
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

		public List<SpecialOffer> RetrieveAllOffers()
		{
			var listOfOffers = new List<SpecialOffer>();

			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[SpecialOffer]";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					var id = Convert.ToInt32(dr["Id"]);
					var start = Convert.ToDateTime(dr["Startdate"]);
					var end = Convert.ToDateTime(dr["Enddate"]);
					var offerPrice = Convert.ToDecimal(dr["OfferPrice"]);
					var productId = Convert.ToInt32(dr["ProductID"]);
					var productRep = new ProductRepository(new ProductMssqlContext());
					var allProducts = productRep.RetrieveAllProducts();

					foreach (var singleproduct in allProducts)
					{
						if (productId == singleproduct.RetrieveProductId())
						{
							var product = singleproduct;
							var specialOffer = new SpecialOffer(id, offerPrice, product.RetrieveProductId(), product.ProductName);
							specialOffer.StartTime = start;
							specialOffer.EndTime = end;

							listOfOffers.Add(specialOffer);
						}
					}
				}
				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return listOfOffers;
		}


		public bool UpdateOfferById(SpecialOffer o)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "UPDATE dbo.[SpecialOffer] SET Startdate=@Start, Enddate=@End, OfferPrice=@OfferPrice WHERE ProductID = @ProductId";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@Start", o.StartTime);
				newCmd.Parameters.AddWithValue("@End", o.EndTime);
				newCmd.Parameters.AddWithValue("@OfferPrice", o.RetrieveOfferPrice());
				newCmd.Parameters.AddWithValue("@ProductId", o.RetrieveProductId());
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


		public SpecialOffer RetrieveOfferById(int id)
		{
			var offer = new SpecialOffer(1, 1, 1, null);
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				SqlCommand cmd = CreateSQLCommandStoredProcedure("GetOfferByID", cnn);	
				cmd.Parameters.AddWithValue("@ID", id);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					var offerId = Convert.ToInt32(dr["ID"]);
					var start = Convert.ToDateTime(dr["Startdate"]);
					var end = Convert.ToDateTime(dr["Enddate"]);
					var offerPrice = Convert.ToDecimal(dr["OfferPrice"]);
					var productId = Convert.ToInt32(dr["ProductID"]);
					var productRep = new ProductRepository(new ProductMssqlContext());
					var allProducts = productRep.RetrieveAllProducts();

					foreach (var singleproduct in allProducts)
					{
						if (productId == singleproduct.RetrieveProductId())
						{
							var product = singleproduct;
							offer = new SpecialOffer(offerId, offerPrice, product.RetrieveProductId(), product.ProductName);
							offer.StartTime = start;
							offer.EndTime = end;
						}
					}
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return offer;
		}

		public SpecialOffer RetrieveOfferByProductID(int productId)
		{
			var offer = new SpecialOffer(-1, 1, 1, null);

			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[SpecialOffer] where ProductID = @ProductID";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@ProductID", productId);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					var id = Convert.ToInt32(dr["Id"]);
					var start = Convert.ToDateTime(dr["Startdate"]);
					var end = Convert.ToDateTime(dr["Enddate"]);
					var offerPrice = Convert.ToDecimal(dr["OfferPrice"]);

					var productRep = new ProductRepository(new ProductMssqlContext());
					var product = productRep.RetrieveProductById(productId);

					offer = new SpecialOffer(id, offerPrice, product.RetrieveProductId(), product.ProductName);
					offer.StartTime = start;
					offer.EndTime = end;
				}
				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return offer;
		}
	}
}