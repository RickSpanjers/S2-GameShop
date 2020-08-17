using System.Collections.Generic;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Repositories
{
	public class RoleRepository
	{
		private readonly IRole roleInterface;

		public RoleRepository(IRole context)
		{
			roleInterface = context;
		}

		public List<Role> RetrieveAllRoles()
		{
			return roleInterface.RetrieveAllRoles() as List<Role>;
		}

		public bool UpdateRoleById(Role r)
		{
			return roleInterface.UpdateRoleById(r);
		}

		public bool CreateNewRole(Role r)
		{
			return roleInterface.CreateNewRole(r);
		}

		public bool DeleteRoleById(int roleId)
		{
			return roleInterface.DeleteRoleById(roleId);
		}

		public bool InsertRolesForUser(int roleId, int userId)
		{
			return roleInterface.InsertRolesForUser(roleId, userId);
		}

		public List<Role> RetrieveRolesFromUser(User u)
		{
			return roleInterface.RetrieveRolesFromUser(u) as List<Role>;
		}

		public List<Role> RetrieveRolesWithPermission(Permission p)
		{
			return roleInterface.RetrieveRolesWithPermission(p) as List<Role>;
		}

		public Role RetrieveRoleById(int id)
		{
			return roleInterface.RetrieveRoleById(id);
		}
	}
}