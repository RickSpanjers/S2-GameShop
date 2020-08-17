using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using RicksWebWorld.Helpers;

namespace RicksWebWorld.Controllers
{
	public class UserController : Controller
	{
		private readonly UserRepository userRep = new UserRepository(new UserMssqlContext());
		private readonly PermissionRepository permissionRepo = new PermissionRepository(new PermissionMssqlContext());
		private readonly RoleRepository roleRep = new RoleRepository(new RoleMssqlContext());
		private readonly AutoMapperExtension mapextension = new AutoMapperExtension();
		private readonly SessionHelper sessionHelper = new SessionHelper();

		/// <summary>
		///     Login
		/// </summary>
		/// <returns>Return actionresult loginpage</returns>
		public IActionResult Login()
		{
			return View("Login");
		}


		/// <summary>
		///     Return singleuser page
		/// </summary>
		/// <returns>Actionresult singleuser</returns>
		public IActionResult Single()
		{
			var model = new UserRoleViewModel();
			model.AllUserRoles = new List<RoleOverviewViewModel>();
			foreach (Role r in roleRep.RetrieveAllRoles())
			{
				var mapper = mapextension.RoleToRoleOverviewViewModel();
				RoleOverviewViewModel rmodel = mapper.Map<RoleOverviewViewModel>(r);
				model.AllUserRoles.Add(rmodel);
			}
			return View("SingleUser", model);
		}


		/// <summary>
		///     Return singleuser page with info to edit
		/// </summary>
		/// <param name="selectedUser">Id from User</param>
		/// <returns>Actionresult SingleUser</returns>
		public IActionResult SingleEdit(int selectedUser)
		{
			var selected = userRep.RetrieveUserById(selectedUser);
			if (selected.ReturnUserId() != 0)
			{
				UserRoleViewModel model = AutoMappingUserRoleViewModel(selected);
				return View("SingleUser", model);
			}
			return View("SingleUser");
		}

		/// <summary>
		///     Return create account page
		/// </summary>
		/// <returns>Actionresult creatnewuser</returns>
		public IActionResult NewUser()
		{
			return View("CreateNewUser");
		}


		/// <summary>
		///     Return useroverview
		/// </summary>
		/// <returns>Actionresult useroverview</returns>
		public IActionResult Overview()
		{
			var model = new UserRoleViewModel();
			var rc = new RoleController();

			model.AllUserRoles = new List<RoleOverviewViewModel>();
			foreach (Role r in roleRep.RetrieveAllRoles())
			{
				var mapperOne = mapextension.RoleToRoleOverviewViewModel();
				RoleOverviewViewModel rmodel = mapperOne.Map<RoleOverviewViewModel>(r);
				model.AllUserRoles.Add(rmodel);
			}
			foreach (User u in userRep.RetrieveAllUsers())
			{
				var mapperTwo = mapextension.UserToUserOverviewViewModel();
				UserOverviewViewModel umodel = mapperTwo.Map<UserOverviewViewModel>(u);

				foreach (Role r in roleRep.RetrieveRolesFromUser(u))
				{
					RoleOverviewViewModel rmodel = new RoleOverviewViewModel();
					rmodel.RoleId = r.RetrieveRoleId();
					rmodel.RoleName = r.RetrieveRoleName();
					umodel.RolesInUser.Add(rmodel);
				}

				model.UsersInSystem.Add(umodel);
			}

			return View("Useroverview", model);
		}



		/// <summary>
		///     Save user into system
		/// </summary>
		/// <param name="model">UserViewModel</param>
		/// <param name="selectedRole">List of IDs from roles</param>
		/// <returns>Return actionresult useroverview</returns>
		[HttpPost]
		public IActionResult Add(UserRoleViewModel model, List<int> selectedRole)
		{
			var user = new User(model.Username, GetStringSha256Hash(model.Password), 1);
			user.Email = model.Email;
			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.Address = model.Address;
			user.Zipcode = model.Zipcode;
			user.Place = model.Place;

			//Check if username or email already exists in system
			foreach (User u in userRep.RetrieveAllUsers())
			{
				if (u.Email == user.Email || u.ReturnUsername() == user.ReturnUsername())
				{
					return RedirectToAction("NewUser", "User");
				}
			}
			if (userRep.CreateNewUser(user))
			{
				InsertingRolesIntoUser(selectedRole, user);
				if (selectedRole.Count == 0){return RedirectToAction("Login", "User");}
				return RedirectToAction("Overview", "User");
			}
			return RedirectToAction("Index", "Home");
		}

		/// <summary>
		///     Delete user from system
		/// </summary>
		/// <param name="userToDelete">Id from User</param>
		/// <returns>Return useroverview</returns>
		[HttpPost]
		public ActionResult Delete(int userToDelete)
		{
			var rc = new RoleController();
			var selectedUser = userRep.RetrieveUserById(userToDelete);

			if (userRep.DeleteUserById(selectedUser.ReturnUserId()))
			{
				return RedirectToAction("Overview", "User");
			}

			return RedirectToAction("Overview", "User");
		}


		/// <summary>
		///     Edit user in system
		/// </summary>
		/// <param name="u">UserViewModel</param>
		/// <param name="userToEdit">userToEdit string</param>
		/// <param name="selectedRole">List of ids with roles</param>
		/// <returns>Return actionresult useroverview</returns>
		[HttpPost]
		public ActionResult Update(UserViewModel u, string userToEdit, List<int> selectedRole)
		{
			var rc = new RoleController();
			var users = userRep.RetrieveAllUsers();
			foreach (var singleuser in users)
			{
				if (singleuser.ReturnUsername() == userToEdit)
				{
					var newUser = new User(u.Username, GetStringSha256Hash(u.Password), singleuser.ReturnUserId());
					newUser.FirstName = u.FirstName;
					newUser.LastName = u.LastName;
					newUser.Email = u.Email;
					newUser.Address = u.Address;
					newUser.Zipcode = u.Zipcode;
					newUser.Place = u.Place;

					userRep.UpdateUserById(newUser);
					InsertingRolesIntoUser(selectedRole, newUser);
					return RedirectToAction("Overview", "User", singleuser);
				}
			}
			return RedirectToAction("Single", "User");
		}


		/// <summary>
		///     Insert roles into user
		/// </summary>
		/// <param name="allRoles">List of IDs from roles</param>
		/// <param name="u">Selected User</param>
		public void InsertingRolesIntoUser(List<int> allRoles, User u)
		{
			var rolesToBeInserted = new List<Role>();
			var rc = new RoleController();
			var dbRoles = roleRep.RetrieveAllRoles();

			if (allRoles.Count == 0)
			{
				Role UserRole = roleRep.RetrieveRoleById(2);
				rolesToBeInserted.Add(UserRole);
			}
			else
			{
				foreach (var singleRole in allRoles)
				{
					var selectedRole = roleRep.RetrieveRoleById(singleRole);
					if (selectedRole.RetrieveRoleId() != 0)
					{
						rolesToBeInserted.Add(selectedRole);
					}
				}
			}

			rc.InsertRolesForUser(rolesToBeInserted, u);
		}


		/// <summary>
		///     Login the user
		/// </summary>
		/// <param name="model">LoginViewModel</param>
		/// <returns>Return actionresult dashboard/loginpage</returns>
		[HttpPost]
		public ActionResult Login(LoginViewModel model)
		{
			var loggedIn = userRep.FindLoginData(model.Username, GetStringSha256Hash(model.Password));
			if (loggedIn)
			{
				if (CheckPermissionForLogin(model))
				{ 
					return RedirectToAction("Dashboard", "Home");
				}
				else
				{
					var selectedUser = userRep.RetrieveUserByUsername(model.Username);
					if (selectedUser.ReturnUserId() != 0)
					{
						sessionHelper.UpdateSessionsUser(selectedUser);
						return RedirectToAction("Index", "Home");
					}
				}
			}
			return View("Login");
		}


		/// <summary>
		///     Logout user
		/// </summary>
		/// <returns>Return actionresult index</returns>
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}


		/// <summary>
		///     Check if loginpermission is set
		/// </summary>
		/// <param name="model">LoginViewModel</param>
		/// <returns>True/False</returns>
		public bool CheckPermissionForLogin(LoginViewModel model)
		{
			var rc = new RoleController();

			var selectedUser = userRep.RetrieveUserByUsername(model.Username);
			if (selectedUser.ReturnUserId() != 0)
			{
				foreach (var singleRole in rc.RetrieveRolesFromUser(selectedUser))
				{
					var allPermissions = permissionRepo.RetrievePermissionsFromRole(singleRole);
					foreach (var singlePermission in allPermissions)
					{
						if (singlePermission.RetrievePermissionName() == "Login")
						{
							return true;
						}
					}
				}
			}
			return false;
		}


		/// <summary>
		///     Hash the password
		/// </summary>
		/// <param name="text">Password to hash</param>
		/// <returns>Hashed password</returns>
		public string GetStringSha256Hash(string text)
		{
			if (string.IsNullOrEmpty(text))
				return string.Empty;

			using (var sha = new SHA256Managed())
			{
				var textData = Encoding.UTF8.GetBytes(text);
				var hash = sha.ComputeHash(textData);
				return BitConverter.ToString(hash).Replace("-", string.Empty);
			}
		}

		[HttpGet]
		public IActionResult PasswordReset(int userId)
		{
			var user = userRep.RetrieveUserById(userId);
			PasswordResetViewModel model = new PasswordResetViewModel();
			model.Email = user.Email;
			model.Username = user.ReturnUsername();
			return View("ResetPassword", model);
		}

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			PasswordResetViewModel model = new PasswordResetViewModel();
			return View("ForgotPassword", model);
		}

		[HttpGet]
		public IActionResult ConfirmEmail(int userId)
		{
			if (userRep.ConfirmEmail(userId) == true)
			{
				return View("ConfirmEmail", "User");
			}
			else
			{
				return View("Index", "Home");
			}
		}

		public IActionResult ResetThePassword(PasswordResetViewModel model, string UserToEdit)
		{
			if (ModelState.IsValid)
			{
				if (model.Password == model.PasswordRepeat)
				{
					User SelectedUser = userRep.RetrieveUserByEmail(UserToEdit);
					User u = new User(SelectedUser.ReturnUsername(), GetStringSha256Hash(model.Password), SelectedUser.ReturnUserId());
					u.Email = SelectedUser.Email;
					if (userRep.ResetPassword(u) == true)
					{
						return RedirectToAction("Login", "User");
					}
				}
			}
			return View("ResetPassword", "User");
		}


		public UserRoleViewModel AutoMappingUserRoleViewModel(User selected)
		{
			RoleController rc = new RoleController();
			var mapper = mapextension.UserToUserRoleViewModel();
			UserRoleViewModel model = mapper.Map<UserRoleViewModel>(selected);

			model.RolesInUser = new List<RoleOverviewViewModel>();
			foreach (Role r in rc.RetrieveRolesFromUser(selected))
			{
				var mapperTwo = mapextension.RoleToRoleOverviewViewModel();
				RoleOverviewViewModel rmodel = mapperTwo.Map<RoleOverviewViewModel>(r);
				model.RolesInUser.Add(rmodel);
			}
			model.AllUserRoles = new List<RoleOverviewViewModel>();
			foreach (Role r in roleRep.RetrieveAllRoles())
			{
				var mapperThree = mapextension.RoleToRoleOverviewViewModel();
				RoleOverviewViewModel rmodel = mapperThree.Map<RoleOverviewViewModel>(r);
				model.AllUserRoles.Add(rmodel);
			}
			return model;
		}
	}
}