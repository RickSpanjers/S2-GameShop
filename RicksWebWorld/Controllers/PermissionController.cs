using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;

namespace RicksWebWorld.Controllers
{
	public class PermissionController : Controller
	{
		private readonly PermissionRepository permissionRep = new PermissionRepository(new PermissionMssqlContext());
		private readonly RoleRepository roleRep = new RoleRepository(new RoleMssqlContext());
		private readonly AutoMapperExtension mapextension = new AutoMapperExtension();


		/// <summary>
		/// Opens the overview of permissions in the backend
		/// </summary>
		/// <returns>Returns actionresult permissionoverview</returns>
		public IActionResult Overview()
		{
			var model = new RolePermissionViewModel();
			model.AllPermissionsInSystem = AutoMappingRolePermissionViewModel().AllPermissionsInSystem;
			model.AllRolesInSystem = AutoMappingRolePermissionViewModel().AllRolesInSystem;
			return View("PermissionOverview", model);
		}


		/// <summary>
		///     Return permisionoverview with edit info
		/// </summary>
		/// <param name="selectedPermission">Identification of permission</param>
		/// <returns>Returns actionresult permissionoverview</returns>
		public IActionResult OverviewEdit(int selectedPermission)
		{
			var model = new RolePermissionViewModel();
			RoleController rc = new RoleController();

			var selected = permissionRep.RetrievePermissionById(selectedPermission);
			var allRoles = roleRep.RetrieveAllRoles();
			var allPermissions = permissionRep.RetrieveAllPermissions();

			if (selected != null)
			{
				model.Name = selected.RetrievePermissionName();
				model.Id = selected.RetrievePermissionId();
				model.Description = selected.RetrievePermissionDescription();
				model.AllPermissionsInSystem = AutoMappingRolePermissionViewModel().AllPermissionsInSystem;
				model.AllRolesInSystem = AutoMappingRolePermissionViewModel().AllRolesInSystem;
				foreach (Role r in rc.RetrieveRolesFromPermission(selected))
				{
					var mapperThree = mapextension.RoleToRoleOverviewViewModel();
					RoleOverviewViewModel rmodel = mapperThree.Map<RoleOverviewViewModel>(r);
					model.AllRolesWithPermission.Add(rmodel);
				}

				return View("Permissionoverview", model);
			}

			return View("Permissionoverview", model);
		}


		/// <summary>
		///     Adding a new permission to system
		/// </summary>
		/// <param name="model">RolePermissionOverview</param>
		/// <param name="selectedRole">List of IDs from selectedroles</param>
		/// <returns>Actionresult permissionoverview</returns>
		[HttpPost]
		public ActionResult Add(RolePermissionViewModel model, List<int> selectedRole)
		{
			if (model.Description == null) model.Description = "No description";

			var permission = new Permission(1, model.Name, model.Description);

			if (permissionRep.CreateNewPermission(permission))
			{
				foreach (var singleRole in selectedRole)
				{
					var selectedPermission = permissionRep.RetrievePermissionByName(permission.RetrievePermissionName());
					InsertPermissionsForRole(selectedPermission, singleRole);
				}

				return RedirectToAction("Overview");
			}

			return RedirectToAction("Overview");
		}


		/// <summary>
		///     Delete permission from system
		/// </summary>
		/// <param name="permissionToDelete">Identification of permission</param>
		/// <returns>Actionresult permissionoverview</returns>
		[HttpPost]
		public ActionResult Delete(int permissionToDelete)
		{
			var selectedPermission = permissionRep.RetrievePermissionById(permissionToDelete);

			if (selectedPermission.RetrievePermissionId() != 0)
			{
				if (permissionRep.DeletePermissionById(selectedPermission.RetrievePermissionId()))
				{
					return RedirectToAction("Overview");
				}
					
				return RedirectToAction("Overview");
			}

			return RedirectToAction("Overview");
		}


		/// <summary>
		///     Edit permissiom from system
		/// </summary>
		/// <param name="model">RolePermissionViewModel</param>
		/// <param name="permissionToEdit">Identification of permission</param>
		/// <param name="selectedRole">List of IDs from roles</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Update(RolePermissionViewModel model, int permissionToEdit, List<int> selectedRole)
		{
			var selectedPermission = permissionRep.RetrievePermissionById(permissionToEdit);
			if (selectedPermission.RetrievePermissionId() != 0)
			{
				if (model.Description == null) { model.Description = "No Description"; }
				var newPermission = new Permission(selectedPermission.RetrievePermissionId(), model.Name, model.Description);
				permissionRep.UpdatePermissionById(newPermission);
				foreach (var singleRole in selectedRole) { InsertPermissionsForRole(selectedPermission, singleRole); }
				return RedirectToAction("Overview", selectedPermission);
			}
			return RedirectToAction("Overview");
		}


		/// <summary>
		///     Inserting permissions into roles
		/// </summary>
		/// <param name="p">The necessary permission to insert</param>
		/// <param name="roleToInsert">identification of the role</param>
		[HttpPost]
		public void InsertPermissionsForRole(Permission p, int roleToInsert)
		{
			var selectedPermission = permissionRep.RetrievePermissionById(p.RetrievePermissionId());
			var allRoles = roleRep.RetrieveAllRoles();
			foreach (var singleRole in allRoles)
			{
				if (singleRole.RetrieveRoleId() == roleToInsert)
				{
					permissionRep.InsertPermissionIntoRole(singleRole.RetrieveRoleId(), selectedPermission.RetrievePermissionId());
				}		
			}		
		}


		public RolePermissionViewModel AutoMappingRolePermissionViewModel()
		{
			RolePermissionViewModel model = new RolePermissionViewModel();
			model.AllPermissionsInSystem = new List<PermissionViewModel>();
			model.AllRolesInSystem = new List<RoleOverviewViewModel>();
			model.AllRolesWithPermission = new List<RoleOverviewViewModel>();

			foreach (Permission p in permissionRep.RetrieveAllPermissions())
			{
				var mapperOne = mapextension.PermissionToPermissionViewModel();
				PermissionViewModel pmodel = mapperOne.Map<PermissionViewModel>(p);
				model.AllPermissionsInSystem.Add(pmodel);
			}
			foreach (Role r in roleRep.RetrieveAllRoles())
			{
				var mapperTwo = mapextension.RoleToRoleOverviewViewModel();
				RoleOverviewViewModel rmodel = mapperTwo.Map<RoleOverviewViewModel>(r);
				model.AllRolesInSystem.Add(rmodel);
			}
			return model;
		}
	}
}