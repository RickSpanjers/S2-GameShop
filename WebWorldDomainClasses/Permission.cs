namespace RicksWebWorld.Models
{
	public class Permission
	{
		private readonly string description;
		private readonly int id;
		private readonly string name;

		/// <summary>
		///     Constructor for permission
		/// </summary>
		/// <param name="id">id from permission</param>
		/// <param name="name">name from permission</param>
		/// <param name="description">description for permission</param>
		public Permission(int id, string name, string description)
		{
			this.id = id;
			this.name = name;
			this.description = description;
		}

		/// <summary>
		///     Retrieve the name
		/// </summary>
		/// <returns>sting name</returns>
		public string RetrievePermissionName()
		{
			return name;
		}

		/// <summary>
		///     Retrieve the id
		/// </summary>
		/// <returns>int id</returns>
		public int RetrievePermissionId()
		{
			return id;
		}

		/// <summary>
		///     Retrieve the description
		/// </summary>
		/// <returns>string description</returns>
		public string RetrievePermissionDescription()
		{
			return description;
		}
	}
}