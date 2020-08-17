namespace RicksWebWorld.Models
{
	public class Role
	{
		private readonly string roleDescription;
		private readonly int roleId;
		private readonly string roleName;

		/// <summary>
		///     Constructor for Role
		/// </summary>
		/// <param name="name">string name</param>
		/// <param name="id">int id</param>
		/// <param name="desc">string description</param>
		public Role(int ID, string Name, string Description)
		{
			roleName = Name;
			roleId = ID;
			roleDescription = Description;
		}

		/// <summary>
		///     Retrieve id
		/// </summary>
		/// <returns>int id</returns>
		public int RetrieveRoleId()
		{
			return roleId;
		}

		/// <summary>
		///     Retrieve Name
		/// </summary>
		/// <returns>string Name</returns>
		public string RetrieveRoleName()
		{
			return roleName;
		}

		/// <summary>
		///     Retrieve desc
		/// </summary>
		/// <returns>string desc</returns>
		public string RetrieveRoleDesc()
		{
			return roleDescription;
		}
	}
}