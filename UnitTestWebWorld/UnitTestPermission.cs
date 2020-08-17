using Microsoft.VisualStudio.TestTools.UnitTesting;
using RicksWebWorld.Context.Debug;
using RicksWebWorld.Controllers;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;


namespace UnitTestWebWorld
{
	[TestClass]
	class UnitTestPermission
    {
		PermissionController Controller = new PermissionController();
		PermissionRepository PerRepoTest = new PermissionRepository(new PermissionTestContext());
		RoleRepository RoleRepoTest = new RoleRepository(new RoleTestContext());

		[TestMethod]
		public void SavePermissionTestContext()
		{
			Permission P = new Permission(5, "Test", "Test");
			PerRepoTest.CreateNewPermission(P);

			var TotalPermissions = PerRepoTest.RetrieveAllPermissions();

			Assert.IsTrue(TotalPermissions.Count > 4);
			Assert.IsTrue(PerRepoTest.RetrieveAllPermissions() != null);
		}

		[TestMethod]
		public void EditPermissionTestContext()
		{
			Permission P = new Permission(1, "Test2", "Test");
			var result = PerRepoTest.UpdatePermissionById(P);
			var Permissions = PerRepoTest.RetrieveAllPermissions();

			foreach (var single in Permissions)
			{
				Assert.IsFalse(single.RetrievePermissionName() == "Permission01");
				Assert.IsTrue(PerRepoTest.RetrieveAllPermissions() != null);
			}

		}

		[TestMethod]
		public void DeletePermissionTestContext()
		{
			PerRepoTest.DeletePermissionById(1);

			var allpermissions = PerRepoTest.RetrieveAllPermissions();
			foreach (var single in allpermissions)
			{
				Assert.IsFalse(single.RetrievePermissionId() == 1);
				Assert.IsTrue(PerRepoTest.RetrieveAllPermissions() != null);
			}
		}

		[TestMethod]
		public void InsertPermissionIntoRole()
		{
			int permissionid = 1;
			int roleid = 1;

			PerRepoTest.InsertPermissionIntoRole(roleid, permissionid);
			Role R = RoleRepoTest.RetrieveRoleById(1);

			foreach (var single in PerRepoTest.RetrievePermissionsFromRole(R))
			{
				Assert.IsTrue(single.RetrievePermissionId() == permissionid);
				Assert.IsTrue(PerRepoTest.RetrieveAllPermissions() != null);
			}
		}

		[TestMethod]
		public void RetrieveAllPermissions()
		{
			var allpermissions = PerRepoTest.RetrieveAllPermissions();
			Assert.IsTrue(PerRepoTest.RetrieveAllPermissions() != null);		
		}
	}
}
