using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Context.Release
{
	public class PaymentMssqlContext : ConnectionHelper, IPaymentMethod
	{
		public bool CreateNewPaymentMethod(Payment p)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "INSERT INTO dbo.[PaymentMethod] (Name, Description) VALUES (@Name, @Description)";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@ID", p.RetrieveID());
				cmd.Parameters.AddWithValue("@Name", p.RetrieveName());
				cmd.Parameters.AddWithValue("@Description", p.RetrieveDesc());
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

		public List<Payment> RetrieveAllPayments()
		{
			List<Payment> listOfPayments = new List<Payment>();
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[PaymentMethod]";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int Id = Convert.ToInt32(dr["ID"]);
					string name = (dr["Name"].ToString());
					string description = dr["Description"].ToString();
					Payment p = new Payment(Id, name, description);
					listOfPayments.Add(p);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return listOfPayments;
		}

		public bool DeletePaymentMethodById(int Id)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "DELETE FROM dbo.[PaymentMethod] WHERE ID = @Id";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@Id", Id);
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

		public bool UpdatePaymentMethodById(Payment p)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "UPDATE dbo.[PaymentMethod] SET Name=@Name, Description=@Description WHERE id = @id";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@id", p.RetrieveID());
				newCmd.Parameters.AddWithValue("@Name", p.RetrieveName());
				newCmd.Parameters.AddWithValue("@Description", p.RetrieveDesc());
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

		public Payment RetrievePaymentMethodsById(int id)
		{
			Payment paymentMethod = new Payment(1, null, null);
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				SqlCommand cmd = CreateSQLCommandStoredProcedure("GetPaymentMethodByID", cnn);
				cmd.Parameters.AddWithValue("@ID", id);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int Id = Convert.ToInt32(dr["ID"]);
					string name = (dr["Name"].ToString());
					string description = dr["Description"].ToString();
					paymentMethod = new Payment(Id, name, description);
				}
				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return paymentMethod;
		}



	}
}
