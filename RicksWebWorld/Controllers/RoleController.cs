using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;

namespace RicksWebWorld.Controllers
{
	public class RoleController : Controller
	{
		private readonly RoleRepository roleRep = new RoleRepository(new RoleMssqlContext());
		private readonly UserRepository userRep = new UserRepository(new UserMssqlContext());
		private readonly AutoMapperExtension mapextension = new AutoMapperExtension();

		/// <summary>
		///     Return roleoverview
		/// </summary>
		/// <returns>Return roleoverview</returns>
		public IActionResult Overview()
		{
			var model = new RoleViewModel();
			model.AllRolesInSystem = new List<RoleOverviewViewModel>();
			foreach(Role r in roleRep.RetrieveAllRoles())
			{
				var mapper = mapextension.RoleToRoleOverviewViewModel();
				RoleOverviewViewModel rmodel = mapper.Map<RoleOverviewViewModel>(r);
				model.AllRolesInSystem.Add(rmodel);
			}
			return View("RoleOverview", model);
		}

		/// <summary>
		///     Return roleoverview with info
		/// </summary>
		/// <param name="selectedRole">Id of the role to edit</param>
		/// <returns>Return roleoverview</returns>
		public IActionResult OverviewEdit(int selectedRole)
		{
			var allRoles = roleRep.RetrieveAllRoles();
			var selected = roleRep.RetrieveRoleById(selectedRole);
			if (selected.RetrieveRoleId() != 0)
			{
				var mapperOne = mapextension.RoleToRoleViewModel();
				RoleViewModel model = mapperOne.Map<RoleViewModel>(selected);

				model.AllRolesInSystem = new List<RoleOverviewViewModel>();
				foreach (Role r in roleRep.RetrieveAllRoles())
				{
					var mapperTwo = mapextension.RoleToRoleOverviewViewModel();
					RoleOverviewViewModel rmodel = mapperTwo.Map<RoleOverviewViewModel>(r);
					model.AllRolesInSystem.Add(rmodel);
				}

				return View("Roleoverview", model);
			}
			return View("Roleoverview");
		}



		/// <summary>
		///     Add role to system
		/// </summary>
		/// <param name="model">RoleViewModel</param>
		/// <returns>Actionresult roleoverview</returns>
		[HttpPost]
		public IActionResult AddForms(RoleViewModel model)
		{
			if (model.RoleDescription == null) model.RoleDescription = "No description";
			var role = new Role(1, model.RoleName, model.RoleDescription);
			if (roleRep.CreateNewRole(role))
			{
				return RedirectToAction("Overview", "Role");
			}	
			return RedirectToAction("Overview", "Role");
		}

		[HttpPost]
		public IActionResult Add(string roleName, string roleDesc, RoleViewModel Model)
		{
			if (roleDesc == null) roleDesc = "No description";
			var role = new Role(1, roleName, roleDesc);

			if (roleRep.CreateNewRole(role))
			{
				return RedirectToAction("RetrieveRoles", "Role");
			}
			return RedirectToAction("RetrieveRoles", "Role");
		}

		/// <summary>
		///     Delete role from system
		/// </summary>
		/// <param name="roleToDelete">Id from Role</param>
		/// <returns>Actionresult roleoverview</returns>
		[HttpPost]
		public IActionResult Delete(int roleToDelete)
		{
			var selectedRole = roleRep.RetrieveRoleById(roleToDelete);
			if (selectedRole.RetrieveRoleId() != 0)
			{
				if (roleRep.DeleteRoleById(selectedRole.RetrieveRoleId()))
				{
					return RedirectToAction("Overview", "Role");
				}
			}
			return RedirectToAction("Overview", "Role");
		}


		/// <summary>
		///     Edit role in system
		/// </summary>
		/// <param name="model">RoleViewModel</param>
		/// <param name="roleToEdit">Id from Role</param>
		/// <returns>Return actionresult</returns>
		[HttpPost]
		public IActionResult Update(RoleViewModel model, int roleToEdit)
		{
			var selectedRole = roleRep.RetrieveRoleById(roleToEdit);

			if (selectedRole.RetrieveRoleId() != 0)
			{
				var newRole = new Role(selectedRole.RetrieveRoleId(), model.RoleName, model.RoleDescription);
				roleRep.UpdateRoleById(newRole);
				return RedirectToAction("Overview", "Role", selectedRole);
			}

			return RedirectToAction("Overview", "Role");
		}


		/// <summary>
		///     Insert roles into user
		/// </summary>
		/// <param name="listOfRoles">List of IDs from roles</param>
		/// <param name="u">Selected User</param>
		[HttpPost]
		public void InsertRolesForUser(List<Role> listOfRoles, User u)
		{
			foreach (var singleRole in listOfRoles)
			{
				var selectedUser = userRep.RetrieveUserByUsername(u.ReturnUsername());

				if (selectedUser.ReturnUserId() != 0)
					roleRep.InsertRolesForUser(singleRole.RetrieveRoleId(), selectedUser.ReturnUserId());
			}
		}

		/// <summary>
		/// Retrieves the roles from a specific user
		/// </summary>
		/// <param name="u">User object</param>
		/// <returns>Returns a list of roles</returns>
		public List<Role> RetrieveRolesFromUser(User u)
		{
			var allRoles = roleRep.RetrieveRolesFromUser(u);
			return allRoles;
		}


		/// <summary>
		/// Retrieves list of roles from certain permission
		/// </summary>
		/// <param name="p">Permission object</param>
		/// <returns>List of roles</returns>
		public List<Role> RetrieveRolesFromPermission(Permission p)
		{
			var listOfRoles = roleRep.RetrieveRolesWithPermission(p);
			return listOfRoles;
		}


		/// <summary>
		/// Retrieves a list of roles
		/// </summary>
		/// <returns>Returns a partialview with roles</returns>
		public IActionResult RetrieveRoles()
		{
			var model = new RoleViewModel();
			model.AllRolesInSystem = new List<RoleOverviewViewModel>();
			foreach (Role r in roleRep.RetrieveAllRoles())
			{
				var mapper = mapextension.RoleToRoleOverviewViewModel();
				RoleOverviewViewModel rmodel = mapper.Map<RoleOverviewViewModel>(r);
				model.AllRolesInSystem.Add(rmodel);
			}
			return PartialView("ListOfRoles", model);
		}
	}
}