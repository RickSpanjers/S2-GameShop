using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Context.Release
{
	public class PermissionMssqlContext : ConnectionHelper, IPermission
	{
		public List<Permission> RetrieveAllPermissions()
		{
			List<Permission> listOfPermissions = new List<Permission>();
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[Permission]";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int permissionId = Convert.ToInt32(dr["Id"]);
					string name = dr["name"].ToString();
					string description = dr["Description"].ToString();

					Permission permission = new Permission(permissionId, name, description);
					listOfPermissions.Add(permission);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return listOfPermissions;
		}

		public bool UpdatePermissionById(Permission p)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "UPDATE dbo.[Permission] SET Name=@name, Description=@Desc WHERE ID = @Id";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@Id", p.RetrievePermissionId());
				newCmd.Parameters.AddWithValue("@name", p.RetrievePermissionName());
				newCmd.Parameters.AddWithValue("@Desc", p.RetrievePermissionDescription());
				newCmd.ExecuteNonQuery();
				cnn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return false;
		}

		public bool CreateNewPermission(Permission p)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "INSERT INTO dbo.[Permission] (Name, Description) VALUES (@name, @Description)";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@name", p.RetrievePermissionName());
				newCmd.Parameters.AddWithValue("@Description", p.RetrievePermissionDescription());
				newCmd.ExecuteNonQuery();
				cnn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			return false;
		}

		public bool DeletePermissionById(int permissionId)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "DELETE FROM dbo.[Permission] WHERE ID = @Id";
				SqlCommand cmd = new SqlCommand(query, cnn);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@Id", permissionId);

				cmd.ExecuteNonQuery();
				cnn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			return false;
		}

		public bool InsertPermissionIntoRole(int roleId, int permissionId)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "INSERT INTO dbo.[rolepermission](RoleID, PermissionID) VALUES (@RoleId, @permissionId)";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@RoleId", roleId);
				newCmd.Parameters.AddWithValue("@permissionId", permissionId);

				newCmd.ExecuteNonQuery();
				cnn.Close();
				return true;

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			return false;
		}


		public List<Permission> RetrievePermissionsFromRole(Role r)
		{
			List<Permission> listOfPermissions = new List<Permission>();

			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.Permission AS P LEFT OUTER JOIN dbo.RolePermission as RP on P.ID = rp.PermissionID WHERE RP.RoleID = @RoleId";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@RoleId", r.RetrieveRoleId());
				SqlDataReader dr = newCmd.ExecuteReader();
				while (dr.Read())
				{
					int permissionId = Convert.ToInt32(dr["Id"]);
					string name = dr["name"].ToString();
					string description = dr["Description"].ToString();

					Permission permission = new Permission(permissionId, name, description);
					listOfPermissions.Add(permission);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return listOfPermissions;
		}

		public Permission RetrievePermissionById(int id)
		{
			Permission permission = new Permission(1, null, null);
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				SqlCommand cmd = CreateSQLCommandStoredProcedure("GetPermissionByID", cnn);
				cmd.Parameters.AddWithValue("@ID", id);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int permissionId = Convert.ToInt32(dr["Id"]);
					string name = dr["name"].ToString();
					string description = dr["Description"].ToString();
					permission = new Permission(permissionId, name, description);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}

			return permission;
		}

		public Permission RetrievePermissionByName(string name)
		{
			Permission permission = new Permission(1, null, null);

			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[Permission] WHERE name=@name";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@name", name);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int permissionId = Convert.ToInt32(dr["Id"]);
					string pName = dr["name"].ToString();
					string description = dr["Description"].ToString();
					permission = new Permission(permissionId, pName, description);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}

			return permission;
		}
	}
}