using System.Collections.Generic;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Repositories
{
	public class UserRepository
	{
		private readonly IUser userInterace;

		public UserRepository(IUser context)
		{
			userInterace = context;
		}

		public IList<User> RetrieveAllUsers()
		{
			return userInterace.RetrieveAllUsers() as List<User>;
		}

		public bool UpdateUserById(User u)
		{
			return userInterace.UpdateUserById(u);
		}

		public bool CreateNewUser(User u)
		{
			return userInterace.CreateNewUser(u);
		}

		public bool DeleteUserById(int userId)
		{
			return userInterace.DeleteUserById(userId);
		}

		public bool FindLoginData(string username, string password)
		{
			return userInterace.FindLoginData(username, password);
		}

		public User RetrieveUserById(int id)
		{
			return userInterace.RetrieveUserById(id);
		}

		public User RetrieveUserByUsername(string name)
		{
			return userInterace.RetrieveUserByUsername(name);
		}

		public User RetrieveUserByEmail(string email)
		{
			return userInterace.RetrieveUserByEmail(email);
		}

		public bool ConfirmEmail(int id)
		{
			return userInterace.ConfirmEmail(id);
		}

		public bool ResetPassword(User u)
		{
			return userInterace.ResetPassword(u);
		}
	}
}