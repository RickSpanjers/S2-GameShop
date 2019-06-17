using System.Collections.Generic;
using RicksWebWorld.Models;

namespace RicksWebWorld.Interfaces
{
	public interface IUser
	{
		List<User> RetrieveAllUsers();
		bool UpdateUserById(User u);
		bool CreateNewUser(User u);
		bool DeleteUserById(int userId);
		bool FindLoginData(string username, string password);
		User RetrieveUserById(int id);
		User RetrieveUserByUsername(string name);
		User RetrieveUserByEmail(string email);
		bool ConfirmEmail(int id);
		bool ResetPassword(User u);
	}
}