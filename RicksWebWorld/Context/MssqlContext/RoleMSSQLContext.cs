using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Identity;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Context.Release
{
	public class RoleMssqlContext : ConnectionHelper, IRole
	{
		public List<Role> RetrieveAllRoles()
		{
			List<Role> listOfRoles = new List<Role>();
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[Role]";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int roleId = Convert.ToInt32(dr["ID"]);
					string name = dr["Name"].ToString();
					string description = dr["Description"].ToString();
					Role role = new Role(roleId, name, description);
					listOfRoles.Add(role);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return listOfRoles;
		}

		public bool UpdateRoleById(Role r)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "UPDATE dbo.[Role] SET Name=@Name, Description=@Desc WHERE ID = @Id";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@Id", r.RetrieveRoleId());
				newCmd.Parameters.AddWithValue("@Name", r.RetrieveRoleName());
				newCmd.Parameters.AddWithValue("@Desc", r.RetrieveRoleDesc());
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

		public bool CreateNewRole(Role r)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "INSERT INTO dbo.[Role] (Name, Description) VALUES (@Name, @Description)";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@Name", r.RetrieveRoleName());
				newCmd.Parameters.AddWithValue("@Description", r.RetrieveRoleDesc());
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

		public bool DeleteRoleById(int roleId)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "DELETE FROM dbo.[Role] WHERE ID = @Id";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.Parameters.AddWithValue("@ID", roleId);
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

		public bool InsertRolesForUser(int roleId, int userId)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "INSERT INTO dbo.[userrole](User_ID, Role_ID) VALUES (@userId, @RoleId)";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@userId", userId);
				newCmd.Parameters.AddWithValue("@RoleId", roleId);
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

		public List<Role> RetrieveRolesFromUser(User u)
		{
			List<Role> listOfRoles = new List<Role>();
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[Role] AS R INNER JOIN dbo.[Userrole] as UR on R.ID = UR.Role_ID WHERE UR.User_ID = @userId";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@userId", u.ReturnUserId());
				SqlDataReader dr = newCmd.ExecuteReader();
				while (dr.Read())
				{
					int roleId = Convert.ToInt32(dr["ID"]);
					string name = dr["Name"].ToString();
					string description = dr["Description"].ToString();
					Role role = new Role(roleId, name, description);
					listOfRoles.Add(role);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return listOfRoles;
		}

		public List<Role> RetrieveRolesWithPermission(Permission p)
		{
			List<Role> listOfRoles = new List<Role>();
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[Role] AS R INNER JOIN dbo.[RolePermission] as RP on R.ID = RP.RoleID WHERE RP.PermissionID = @PermissionID";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);
				newCmd.Parameters.AddWithValue("@PermissionID", p.RetrievePermissionId());
				SqlDataReader dr = newCmd.ExecuteReader();
				while (dr.Read())
				{
					int roleId = Convert.ToInt32(dr["ID"]);
					string name = dr["Name"].ToString();
					string description = dr["Description"].ToString();
					Role role = new Role(roleId, name, description);
					listOfRoles.Add(role);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return listOfRoles;
		}

		public Role RetrieveRoleById(int id)
		{
			Role role = new Role(1, null, null);
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				SqlCommand newCmd = CreateSQLCommandStoredProcedure("GetRoleByID", cnn);
				newCmd.Parameters.AddWithValue("@ID", id);
				SqlDataReader dr = newCmd.ExecuteReader();
				while (dr.Read())
				{
					int roleId = Convert.ToInt32(dr["ID"]);
					string name = dr["Name"].ToString();
					string description = dr["Description"].ToString();
					role = new Role(roleId, name, description);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return role;
		}
	}
}