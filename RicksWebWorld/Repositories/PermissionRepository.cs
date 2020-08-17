using System.Collections.Generic;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Repositories
{
	public class PermissionRepository
	{
		private readonly IPermission permissionInterface;

		public PermissionRepository(IPermission context)
		{
			permissionInterface = context;
		}

		public List<Permission> RetrieveAllPermissions()
		{
			return permissionInterface.RetrieveAllPermissions() as List<Permission>;
		}

		public bool UpdatePermissionById(Permission p)
		{
			return permissionInterface.UpdatePermissionById(p);
		}

		public bool CreateNewPermission(Permission p)
		{
			return permissionInterface.CreateNewPermission(p);
		}

		public bool DeletePermissionById(int permissionId)
		{
			return permissionInterface.DeletePermissionById(permissionId);
		}

		public bool InsertPermissionIntoRole(int roleId, int permissionId)
		{
			return permissionInterface.InsertPermissionIntoRole(roleId, permissionId);
		}

		public List<Permission> RetrievePermissionsFromRole(Role r)
		{
			return permissionInterface.RetrievePermissionsFromRole(r) as List<Permission>;
		}

		public Permission RetrievePermissionById(int id)
		{
			return permissionInterface.RetrievePermissionById(id);
		}

		public Permission RetrievePermissionByName(string name)
		{
			return permissionInterface.RetrievePermissionByName(name);
		}
	}
}