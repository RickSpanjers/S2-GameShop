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
	public class UserDapperContext : ConnectionHelper, IUser
	{
		public bool ConfirmEmail(int id)
		{
			string sqlUserInsert = "UPDATE dbo.[User] SET EmailConfirmed=1 WHERE ID = @Id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { Id = id });
				return true;
			}
		}

		public bool CreateNewUser(User u)
		{
			string sqlUserInsert = "INSERT INTO dbo.[User] (Username, Password, Firstname, Lastname, Email, Address, Zipcode, Place, EmailConfirmed) VALUES (@Username, @Password, @Firstname, @Lastname, @Email, @Address, @Zipcode, @Place, @EmailConfirmed)";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { Username = u.ReturnUsername(), Password = u.ReturnPassword(), Firstname = u.FirstName, Lastname = u.LastName, Email = u.Email, Address = u.Address, Zipcode = u.Zipcode, Place = u.Place, EmailConfirmed = 0 });
				return true;
			}
		}

		public bool DeleteUserById(int userId)
		{
			string sqlDelete = "DELETE FROM dbo.[User] WHERE ID = @Id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					int affectedRows = connection.Execute(sqlDelete, new { Id = userId });
					return true;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return false;
				}
			}
		}

		public bool FindLoginData(string username, string password)
		{
			string sqlUserSelect = "SELECT * FROM dbo.[User] WHERE Username = @username AND Password = @password";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					int selectedUser = connection.Execute(sqlUserSelect, new { username = username, password = password });
					if (selectedUser != 0)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return false;
				}
			}
		}

		public bool ResetPassword(User u)
		{
			string sqlUserInsert = "UPDATE dbo.[User] SET Password=@Password WHERE ID = @Id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { Password = u.ReturnPassword(), Id = u.ReturnUserId() });
				return true;
			}
		}

		public List<User> RetrieveAllUsers()
		{
			string sqlDetails = "SELECT * FROM dbo.[User]";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					List<User> List = connection.Query<User>(sqlDetails).ToList();
					return List;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public User RetrieveUserById(int id)
		{
			string StoredProcedure = "GetUserByID";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					User Detail = connection.QueryFirstOrDefault<User>(StoredProcedure, new { ID = id }, commandType: CommandType.StoredProcedure);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}
			}
		}

		public User RetrieveUserByUsername(string name)
		{
			string Query = "SELECT* FROM dbo.[User] WHERE Username = @name";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					User Detail = connection.QueryFirstOrDefault<User>(Query, new { name = name }, commandType: CommandType.Text);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}


		public User RetrieveUserByEmail(string email)
		{
			string Query = "SELECT* FROM dbo.[User] WHERE Email = @Email";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					User Detail = connection.QueryFirstOrDefault<User>(Query, new { Email = email }, commandType: CommandType.Text);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public bool UpdateUserById(User u)
		{
			string sqlUserInsert = "UPDATE dbo.[User] SET Username=@Username, Password=@Password, Firstname=@Firstname, Lastname=@Lastname, Email=@Email, Address=@Address, Zipcode=@Zipcode, Place=@Place WHERE ID = @Id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { Username = u.ReturnUsername(), Password = u.ReturnPassword(), Firstname = u.FirstName, Lastname = u.LastName, Email = u.Email, Address = u.Address, Zipcode = u.Zipcode, Place = u.Place, Id = u.ReturnUserId() });
				return true;
			}
		}
	}
}
