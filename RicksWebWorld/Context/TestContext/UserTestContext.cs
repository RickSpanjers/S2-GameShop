using System;
using System.Collections.Generic;
using System.Linq;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Context.Debug
{
	public class UserTestContext : IUser
	{
		private readonly List<User> ListOfUsers = new List<User>();

		public UserTestContext()
		{
			ListOfUsers.Add(new User("Admin", "Welkom01!", 1));
			ListOfUsers.Add(new User("Jeroen", "Welkom02!", 2));
			ListOfUsers.Add(new User("Janine", "Welkom03!", 3));
			ListOfUsers.Add(new User("Dennis", "Welkom04!", 4));
		}

		public List<User> RetrieveAllUsers()
		{
			return ListOfUsers;
		}

		public bool UpdateUserById(User u)
		{
			foreach (var single in ListOfUsers)
				if (single.ReturnUserId() == u.ReturnUserId())
				{
					ListOfUsers.Remove(single);
					ListOfUsers.Add(u);
					return true;
				}

			return false;
		}

		public bool CreateNewUser(User u)
		{
			try
			{
				ListOfUsers.Add(u);
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}

		public bool DeleteUserById(int userId)
		{
			var selected = RetrieveUserById(userId);
			ListOfUsers.Remove(selected);
			return false;
		}

		public bool FindLoginData(string username, string password)
		{
			foreach (var single in ListOfUsers)
				if (single.ReturnUsername() == username && single.ReturnPassword() == password)
					return true;

			return false;
		}

		public User RetrieveUserById(int id)
		{
			foreach (var singleitem in ListOfUsers)
			{
				if (singleitem.ReturnUserId() == id)
				{
					return singleitem;
				}
			}
				

			return null;
		}


		public User RetrieveUserByUsername(string name)
		{
			foreach (var singleitem in ListOfUsers)
				if (singleitem.ReturnUsername() == name)
					return singleitem;

			return null;
		}

		public User RetrieveUserByEmail(string email)
		{
			foreach (var singleitem in ListOfUsers)
				if (singleitem.Email == email)
					return singleitem;

			return null;
		}

		public bool ConfirmEmail(int id)
		{
			return true;
		}

		public bool ResetPassword(User u)
		{
			return true;
		}
	}
}