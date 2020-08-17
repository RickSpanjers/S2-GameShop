using System.Collections.Generic;

namespace RicksWebWorld.Models
{
	public class User
	{
		private readonly int userId;
		private readonly string username;
		private readonly string userPassword;


		/// <summary>
		///     Constructor User
		/// </summary>
		/// <param name="username">string username</param>
		/// <param name="password">string password</param>
		/// <param name="userId">int userId</param>
		public User(string username, string password, int userId)
		{
			this.username = username;
			userPassword = password;
			this.userId = userId;
		}

		/// <summary>
		/// Constructor User for Dapper
		/// </summary>
		/// <param name="ID">Identification of the user</param>
		/// <param name="Firstname">Firstname of the user</param>
		/// <param name="Lastname">Lastname of the user</param>
		/// <param name="Email">Email of the user</param>
		/// <param name="Username">Username of the user</param>
		/// <param name="Password">Password of the user</param>
		/// <param name="Address">Address of the user</param>
		/// <param name="Zipcode">Zipcode of the user</param>
		/// <param name="Place">Place of the user</param>
		public User(int ID, string Username, string Password, string Firstname, string Lastname, string Email, string Address, string Zipcode, string Place, bool EmailConfirmed)
		{
			userId = ID;
			this.FirstName = Firstname;
			this.LastName = LastName;
			this.Email = Email;
			this.username = Username;
			this.userPassword = Password;
			this.Address = Address;
			this.Zipcode = Zipcode;
			this.Place = Place;
			this.EmailConfirmed = EmailConfirmed;
		}

		/// <summary>
		///     get or set firstname
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		///     get or set lastname
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		///     get or set email
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Get or set if the email has been confirmed
		/// </summary>
		public bool EmailConfirmed { get; set; }

		/// <summary>
		///     get or set address
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		///     get or set zipcode
		/// </summary>
		public string Zipcode { get; set; }

		/// <summary>
		///     get or set place
		/// </summary>
		public string Place { get; set; }

		/// <summary>
		///     get or set roles for user
		/// </summary>
		public List<Role> RolesForUser { get; set; } = new List<Role>();


		/// <summary>
		///     Retrieve Id
		/// </summary>
		/// <returns>int Id</returns>
		public int ReturnUserId()
		{
			return userId;
		}


		/// <summary>
		///     Return username
		/// </summary>
		/// <returns>string username</returns>
		public string ReturnUsername()
		{
			return username;
		}


		/// <summary>
		///     Return password
		/// </summary>
		/// <returns>string password</returns>
		public string ReturnPassword()
		{
			return userPassword;
		}
	}
}