using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RicksWebWorld.Context.Debug;
using RicksWebWorld.Controllers;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;


namespace UnitTestWebWorld
{
    [TestClass]
    public class UnitTestRole
    {
		RoleController Controller = new RoleController();
		RoleRepository RoleTestRepo = new RoleRepository(new RoleTestContext());
		UserRepository UserTestRepo = new UserRepository(new UserTestContext());


        [TestMethod]
        public void AddRole()
        {
			Role R = new Role(5, "R5", "Test");
			var result = RoleTestRepo.CreateNewRole(R);
			var TotalRoles = RoleTestRepo.RetrieveAllRoles();
			Assert.IsTrue(TotalRoles.Count > 4);
		}

        [TestMethod]
        public void EditRole()
        {
			Role R = new Role(1, "NewlyEditedRole", "Test");
			var result = RoleTestRepo.UpdateRoleById(R);
			var RetrievedRole = RoleTestRepo.RetrieveRoleById(1);
			Assert.IsTrue(RetrievedRole.RetrieveRoleName() == "NewlyEditedRole");
		}

        [TestMethod]
        public void DeleteRole()
        {
			var result = RoleTestRepo.DeleteRoleById(2);
			var Role = RoleTestRepo.RetrieveAllRoles();
			Assert.IsFalse(Role.Count > 3);
			Assert.IsTrue(RoleTestRepo.RetrieveAllRoles() != null);
		}

		[TestMethod]
		public void InsertRolesForUserTest()
		{
			int userid = 1;
			int roleid = 1;

			RoleTestRepo.InsertRolesForUser(userid, roleid);
			User u = UserTestRepo.RetrieveUserById(userid);

			foreach (var single in RoleTestRepo.RetrieveRolesFromUser(u))
			{
				Assert.IsTrue(single.RetrieveRoleId() == 1);
			}
		}

		[TestMethod]
		public void RetrieveAllRoles()
		{
			Assert.IsTrue(RoleTestRepo.RetrieveAllRoles() != null);	
		}
	}
}
