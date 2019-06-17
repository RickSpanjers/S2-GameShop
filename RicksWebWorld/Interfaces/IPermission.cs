using System.Collections.Generic;
using RicksWebWorld.Models;

namespace RicksWebWorld.Interfaces
{
	public interface IPermission
	{
		List<Permission> RetrieveAllPermissions();
		bool UpdatePermissionById(Permission p);
		bool CreateNewPermission(Permission p);
		bool DeletePermissionById(int permissionId);
		bool InsertPermissionIntoRole(int roleId, int permissionId);
		List<Permission> RetrievePermissionsFromRole(Role r);
		Permission RetrievePermissionById(int id);
		Permission RetrievePermissionByName(string name);
	}
}