using System;
using System.Collections.Generic;
using System.Linq;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;

namespace RicksWebWorld.Context.Debug
{
	public class PermissionTestContext : IPermission
	{
		private readonly List<Permission> ListOfPermissions = new List<Permission>();
		private readonly Dictionary<int, int> RolePermission = new Dictionary<int, int>();

		public PermissionTestContext()
		{
			ListOfPermissions.Add(new Permission(1, "Permission01", "No Description"));
			ListOfPermissions.Add(new Permission(2, "Permission02", "No Description"));
			ListOfPermissions.Add(new Permission(3, "Permission03", "No Description"));
			ListOfPermissions.Add(new Permission(4, "Permission04", "No Description"));
		}

		public List<Permission> RetrieveAllPermissions()
		{
			return ListOfPermissions;
		}

		public bool UpdatePermissionById(Permission p)
		{
			foreach (var single in ListOfPermissions)
				if (single.RetrievePermissionId() == single.RetrievePermissionId())
					return true;
			return false;
		}

		public bool CreateNewPermission(Permission p)
		{
			try
			{
				ListOfPermissions.Add(new Permission(ListOfPermissions.Count() + 1, p.RetrievePermissionName(), "No Description"));
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}

		public bool DeletePermissionById(int permissionId)
		{
			foreach (var single in ListOfPermissions)
				if (single.RetrievePermissionId() == permissionId)
				{
					ListOfPermissions.Remove(single);
					return true;
				}

			return false;
		}

		public bool InsertPermissionIntoRole(int roleId, int permissionId)
		{
			RolePermission.Add(roleId, permissionId);
			return true;
		}

		public List<Permission> RetrievePermissionsFromRole(Role r)
		{
			List<Permission> Permissions = new List<Permission>();

			foreach (var single in RolePermission)
			{
				if (single.Key == r.RetrieveRoleId())
				{
					Permission ToAdd = RetrievePermissionById(single.Value);
					Permissions.Add(ToAdd);
				}
			}

			return Permissions;
		}

		public Permission RetrievePermissionById(int id)
		{
			foreach (var singleitem in ListOfPermissions)
				if (singleitem.RetrievePermissionId() == id)
					return singleitem;

			return null;
		}

		public Permission RetrievePermissionByName(string name)
		{
			foreach (var singleitem in ListOfPermissions)
				if (singleitem.RetrievePermissionName() == name)
					return singleitem;

			return null;
		}

		public List<Role> RetrieveRolesWithPermission(Permission P)
		{
			RoleRepository RoleRepo = new RoleRepository(new RoleTestContext());
			List<Role> Roles = new List<Role>();

			foreach (var single in RolePermission)
			{
				if (single.Value == P.RetrievePermissionId())
				{
					Role ToAdd = RoleRepo.RetrieveRoleById(single.Key);
					Roles.Add(ToAdd);
				}
			}

			return Roles;
		}
	}
}