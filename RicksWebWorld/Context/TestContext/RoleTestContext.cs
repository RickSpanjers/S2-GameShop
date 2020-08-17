using System;
using System.Collections.Generic;
using System.Linq;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Context.Debug
{
	public class RoleTestContext : IRole
	{
		private readonly List<Role> ListOfRoles = new List<Role>();
		private readonly Dictionary<int, int> UserRole = new Dictionary<int, int>();

		public RoleTestContext()
		{
			ListOfRoles.Add(new Role(1, "Role01", "No Description"));
			ListOfRoles.Add(new Role(2, "Role02", "No Description"));
			ListOfRoles.Add(new Role(3, "Role03", "No Description"));
			ListOfRoles.Add(new Role(4, "Role04", "No Description"));
		}

		public List<Role> RetrieveAllRoles()
		{
			return ListOfRoles;
		}

		public bool UpdateRoleById(Role r)
		{
			foreach(var role in ListOfRoles)
			{
				if(r.RetrieveRoleId() == role.RetrieveRoleId())
				{
					ListOfRoles.Remove(role);
					ListOfRoles.Add(r);
					return true;
				}
			}

			return false;
		}

		public bool CreateNewRole(Role r)
		{
			try
			{
				ListOfRoles.Add(new Role(ListOfRoles.Count() + 1, r.RetrieveRoleName(), r.RetrieveRoleDesc()));
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}

		public bool DeleteRoleById(int roleId)
		{
			foreach (var single in ListOfRoles)
				if (single.RetrieveRoleId() == roleId)
				{
					ListOfRoles.Remove(single);
					return true;
				}

			return false;
		}

		public bool InsertRolesForUser(int userId, int roleId)
		{
			UserRole.Add(userId, roleId);
			return true;
		}

		public List<Role> RetrieveRolesFromUser(User u)
		{
			List<Role> Roles = new List<Role>();

			foreach (var single in UserRole)
			{
				if (single.Key == u.ReturnUserId())
				{
					Role ToAdd = RetrieveRoleById(single.Value);
					Roles.Add(ToAdd);
				}
			}

			return Roles;
		}

		public List<Role> RetrieveRolesWithPermission(Permission p)
		{
			var ListOfRoles = new List<Role>();
			return ListOfRoles;
		}

		public Role RetrieveRoleById(int id)
		{
			foreach (var singleitem in ListOfRoles)
				if (singleitem.RetrieveRoleId() == id)
					return singleitem;

			return null;
		}
	}
}