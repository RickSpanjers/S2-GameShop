using System.Collections.Generic;
using RicksWebWorld.Models;

namespace RicksWebWorld.Interfaces
{
	public interface IRole
	{
		List<Role> RetrieveAllRoles();
		bool UpdateRoleById(Role r);
		bool CreateNewRole(Role r);
		bool DeleteRoleById(int roleId);
		bool InsertRolesForUser(int roleId, int userId);
		List<Role> RetrieveRolesFromUser(User u);
		List<Role> RetrieveRolesWithPermission(Permission p);
		Role RetrieveRoleById(int id);
	}
}