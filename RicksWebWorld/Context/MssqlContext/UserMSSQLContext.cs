using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Context.Release
{
	public class UserMssqlContext : ConnectionHelper, IUser
	{
		public List<User> RetrieveAllUsers()
		{
			List<User> listOfUsers = new List<User>();
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[User]";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int userId = Convert.ToInt32(dr["id"]);
					string username = dr["Username"].ToString();
					string password = dr["Password"].ToString();

					User user = new User(username, password, userId);
					user.FirstName = dr["Firstname"].ToString();
					user.LastName = dr["Lastname"].ToString();
					user.Email = dr["Email"].ToString();
					user.Address = dr["Address"].ToString();
					user.Zipcode = dr["Zipcode"].ToString();
					user.Place = dr["Place"].ToString();

					listOfUsers.Add(user);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return listOfUsers;
		}

		public bool UpdateUserById(User u)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "UPDATE dbo.[User] SET Username=@Username, Password=@Password, Firstname=@Firstname, Lastname=@Lastname, Email=@Email, Address=@Address, Zipcode=@Zipcode, Place=@Place WHERE ID = @Id";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@Id", u.ReturnUserId());
				newCmd.Parameters.AddWithValue("@Username", u.ReturnUsername());
				newCmd.Parameters.AddWithValue("@Password", u.ReturnPassword());
				newCmd.Parameters.AddWithValue("@Firstname", u.FirstName);
				newCmd.Parameters.AddWithValue("@Lastname", u.LastName);
				newCmd.Parameters.AddWithValue("@Email", u.Email);
				newCmd.Parameters.AddWithValue("@Address", u.Address);
				newCmd.Parameters.AddWithValue("@Zipcode", u.Zipcode);
				newCmd.Parameters.AddWithValue("@Place", u.Place);

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

		public bool CreateNewUser(User u)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "INSERT INTO dbo.[User] (Username, Password, Firstname, Lastname, Email, Address, Zipcode, Place, EmailConfirmed) VALUES (@Username, @Password, @Firstname, @Lastname, @Email, @Address, @Zipcode, @Place, @EmailConfirmed)";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@Username", u.ReturnUsername());
				newCmd.Parameters.AddWithValue("@Password", u.ReturnPassword());
				newCmd.Parameters.AddWithValue("@Firstname", u.FirstName);
				newCmd.Parameters.AddWithValue("@Lastname", u.LastName);
				newCmd.Parameters.AddWithValue("@Email", u.Email);
				newCmd.Parameters.AddWithValue("@Address", u.Address);
				newCmd.Parameters.AddWithValue("@Zipcode", u.Zipcode);
				newCmd.Parameters.AddWithValue("@Place", u.Place);
				newCmd.Parameters.AddWithValue("@EmailConfirmed", 0);

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

		public bool DeleteUserById(int userId)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "DELETE FROM dbo.[User] WHERE ID = @Id";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@Id", userId);
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

		public bool FindLoginData(string username, string password)
		{
			bool result = false;
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[User] WHERE Username = @username AND Password = @password";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@username", username);
				cmd.Parameters.AddWithValue("@password", password);
				cmd.CommandType = CommandType.Text;
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					if (Convert.ToInt32(dr["EmailConfirmed"]) != 0)
					{
						result = true;
					}
					else
					{
						result = false;
					}
				}

				cnn.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return result;
		}


		public User RetrieveUserById(int id)
		{
			User user = new User(null, null, 1);
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				SqlCommand cmd = CreateSQLCommandStoredProcedure("GetUserByID", cnn);	
				cmd.Parameters.AddWithValue("@ID", id);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int userId = Convert.ToInt32(dr["Id"]);
					string username = dr["Username"].ToString();
					string password = dr["Password"].ToString();

					user = new User(username, password, userId);
					user.FirstName = dr["Firstname"].ToString();
					user.LastName = dr["Lastname"].ToString();
					user.Email = dr["Email"].ToString();
					user.Address = dr["Address"].ToString();
					user.Zipcode = dr["Zipcode"].ToString();
					user.Place = dr["Place"].ToString();
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return user;
		}

		public User RetrieveUserByUsername(string name)
		{
			User user = new User(null, null, 1);
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[User] WHERE Username=@name";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@name", name);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int userId = Convert.ToInt32(dr["Id"]);
					string username = dr["Username"].ToString();
					string password = dr["Password"].ToString();

					user = new User(username, password, userId);
					user.FirstName = dr["Firstname"].ToString();
					user.LastName = dr["Lastname"].ToString();
					user.Email = dr["Email"].ToString();
					user.Address = dr["Address"].ToString();
					user.Zipcode = dr["Zipcode"].ToString();
					user.Place = dr["Place"].ToString();
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return user;
		}

		public User RetrieveUserByEmail(string email)
		{
			User user = new User(null, null, 1);
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[User] WHERE Email=@email";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@email", email);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int userId = Convert.ToInt32(dr["Id"]);
					string username = dr["Username"].ToString();
					string password = dr["Password"].ToString();

					user = new User(username, password, userId);
					user.FirstName = dr["Firstname"].ToString();
					user.LastName = dr["Lastname"].ToString();
					user.Email = dr["Email"].ToString();
					user.Address = dr["Address"].ToString();
					user.Zipcode = dr["Zipcode"].ToString();
					user.Place = dr["Place"].ToString();
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return user;
		}

		public bool ConfirmEmail(int id)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "UPDATE dbo.[User] SET EmailConfirmed=1 WHERE ID = @Id";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@Id", id);
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

		public bool ResetPassword(User u)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "UPDATE dbo.[User] SET Password=@Password WHERE ID = @Id";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@Id", u.ReturnUserId());
				newCmd.Parameters.AddWithValue("@Password", u.ReturnPassword());
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
	}
}