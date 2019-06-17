using Dapper;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Context.Dapper
{
	public class RoleDapperContext : ConnectionHelper, IRole
	{
		public bool CreateNewRole(Role r)
		{
			string sqlUserInsert = "INSERT INTO dbo.[Role] (Name, Description) VALUES (@Name, @Description)";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { Name = r.RetrieveRoleName(), Description = r.RetrieveRoleDesc() });
				return true;
			}
		}

		public bool DeleteRoleById(int roleId)
		{
			string sqlDelete = "DELETE FROM dbo.[Role] WHERE ID = @Id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					int affectedRows = connection.Execute(sqlDelete, new { Id = roleId });
					return true;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return false;
				}
			}
		}

		public bool InsertRolesForUser(int roleId, int userId)
		{
			string sqlUserInsert = "INSERT INTO dbo.[userrole](User_ID, Role_ID) VALUES (@userId, @RoleId)";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { userId = userId, RoleId = roleId});
				return true;
			}
		}

		public List<Role> RetrieveAllRoles()
		{
			string sqlDetails = "SELECT * FROM dbo.[Role]";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					List<Role> listOfMethods = connection.Query<Role>(sqlDetails).ToList();
					return listOfMethods;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public Role RetrieveRoleById(int id)
		{
			string StoredProcedure = "GetRoleByID";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					Role Detail = connection.QueryFirstOrDefault<Role>(StoredProcedure, new { ID = id }, commandType: CommandType.StoredProcedure);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public List<Role> RetrieveRolesFromUser(User u)
		{
			string sqlDetails = "SELECT * FROM dbo.[Role] AS R INNER JOIN dbo.[Userrole] as UR on R.ID = UR.Role_ID WHERE UR.User_ID = @userId";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					List<Role> listOfMethods = connection.Query<Role>(sqlDetails, new { userId = u.ReturnUserId() }).ToList();
					return listOfMethods;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public List<Role> RetrieveRolesWithPermission(Permission p)
		{
			string sqlDetails = "SELECT * FROM dbo.[Role] AS R INNER JOIN dbo.[RolePermission] as RP on R.ID = RP.RoleID WHERE RP.PermissionID = @PermissionID";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					List<Role> listOfMethods = connection.Query<Role>(sqlDetails, new { PermissionID = p.RetrievePermissionId()}).ToList();
					return listOfMethods;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public bool UpdateRoleById(Role r)
		{
			string sqlUserInsert = "UPDATE dbo.[Role] SET Name=@Name, Description=@Desc WHERE ID = @Id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { Name = r.RetrieveRoleName(), Desc = r.RetrieveRoleDesc(), Id = r.RetrieveRoleId() });
				return true;
			}
		}
	}
}
