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
	public class PermissionDapperContext : ConnectionHelper, IPermission
	{
		public bool CreateNewPermission(Permission p)
		{
			string sqlUserInsert = "INSERT INTO dbo.[Permission] (Name, Description) VALUES (@name, @Description)";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { Name = p.RetrievePermissionName(), Description = p.RetrievePermissionDescription() });
				return true;
			}
		}

		public bool DeletePermissionById(int permissionId)
		{
			string sqlDelete = "DELETE FROM dbo.[Permission] WHERE ID = @Id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					int affectedRows = connection.Execute(sqlDelete, new { Id = permissionId });
					return true;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return false;
				}
			}
		}

		public bool InsertPermissionIntoRole(int roleId, int permissionId)
		{
			string sqlUserInsert = "INSERT INTO dbo.[rolepermission](RoleID, PermissionID) VALUES (@RoleId, @permissionId)";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { RoleID = roleId, PermissionID = permissionId });
				return true;
			}
		}

		public List<Permission> RetrieveAllPermissions()
		{
			string sqlDetails = "SELECT * FROM dbo.[Permission]";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					List<Permission> listOfMethods = connection.Query<Permission>(sqlDetails).ToList();
					return listOfMethods;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public Permission RetrievePermissionById(int id)
		{
			string StoredProcedure = "GetPermissionByID";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					Permission Detail = connection.QueryFirstOrDefault<Permission>(StoredProcedure, new { ID = id }, commandType: CommandType.StoredProcedure);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public Permission RetrievePermissionByName(string name)
		{
			string Query = "SELECT * FROM dbo.[Permission] WHERE name=@name";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					Permission Detail = connection.QueryFirstOrDefault<Permission>(Query, new { name = name }, commandType: CommandType.Text);
					return Detail;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}
			}
		}

		public List<Permission> RetrievePermissionsFromRole(Role r)
		{
			string sqlDetails = "SELECT * FROM dbo.Permission AS P LEFT OUTER JOIN dbo.RolePermission as RP on P.ID = rp.PermissionID WHERE RP.RoleID = @RoleId";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				try
				{
					List<Permission> listOfMethods = connection.Query<Permission>(sqlDetails, new { RoleID = r.RetrieveRoleId() }).ToList();
					return listOfMethods;
				}
				catch (NullReferenceException e)
				{
					Console.WriteLine(e.Message);
					return null;
				}

			}
		}

		public bool UpdatePermissionById(Permission p)
		{
			string sqlUserInsert = "UPDATE dbo.[Permission] SET Name=@name, Description=@Desc WHERE ID = @Id";
			using (SqlConnection connection = ReturnSQLConnection())
			{
				int affectedRows = connection.Execute(sqlUserInsert, new { Name = p.RetrievePermissionName(), Description = p.RetrievePermissionDescription(), Id = p.RetrievePermissionId() });
				return true;
			}
		}
	}
}
