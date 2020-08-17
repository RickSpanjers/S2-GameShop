using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RicksWebWorld.Context.Debug;
using RicksWebWorld.Controllers;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;
using System.Collections.Generic;

namespace UnitTestWebWorld
{
	[TestClass]
	public class UnitTestUser
	{
		UserRepository userRepo = new UserRepository(new UserTestContext());


		[TestMethod]
		public void SaveUser()
		{
			User u = new User("NewUser", "Password", 5);
			u.Address = "Testaddress";
			u.Email = "Testemail@email.nl";
			u.FirstName = "Firstname";
			u.LastName = "Lastname";
			u.Place = "Testplace";
			u.Zipcode = "5091TT";

			var result = userRepo.CreateNewUser(u);
			var TotalUsers = userRepo.RetrieveAllUsers();
			Assert.IsTrue(TotalUsers.Count > 4);
		}

		[TestMethod]
		public void EditUser()
		{
			User u = new User("NewUser", "Password", 1);
			u.Address = "Testaddress";
			u.Email = "Testemail@email.nl";
			u.FirstName = "Firstname";
			u.LastName = "Lastname";
			u.Place = "Testplace";
			u.Zipcode = "5091TT";


			var result = userRepo.UpdateUserById(u);
			var TotalUsers = userRepo.RetrieveAllUsers();

			foreach (var singleuser in TotalUsers)
			{
				Assert.IsFalse(singleuser.ReturnUsername() == "Admin");
			}
		}

		[TestMethod]
		public void DeleteUser()
		{
			var result = userRepo.DeleteUserById(1);
			var TotalUsers = userRepo.RetrieveAllUsers();

			foreach (var singleuser in TotalUsers)
			{
				Assert.IsFalse(singleuser.ReturnUsername() == "Admin");
			}
		}

		[TestMethod]
		public void RetrieveAllUsers()
		{
			var TotalUsers = userRepo.RetrieveAllUsers();
			Assert.IsTrue(userRepo.RetrieveAllUsers() != null);
		}
	}
}
