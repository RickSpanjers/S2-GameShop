namespace RicksWebWorld.Models
{
	public class Category
	{
		private readonly int categoryId;
		private readonly string categoryName;

		/// <summary>
		///     Constructor for category
		/// </summary>
		/// <param name="name">name of the category</param>
		/// <param name="id">id of the category</param>
		public Category(string name, int id)
		{
			categoryName = name;
			categoryId = id;
		}

		/// <summary>
		/// Constructor for category
		/// </summary>
		/// <param name="Name">Name of the category</param>
		/// <param name="ID">Identificaton of the category</param>
		/// <param name="Description">Description of the category</param>
		/// <param name="Image">Image URL of the category</param>
		public Category(string Name, int ID, string Description, string Image)
		{
			categoryName = Name;
			categoryId = ID;
			this.CategoryDesc = Description;
			this.CategoryImg = Image;
		}

		/// <summary>
		///     Retrieve id from category
		/// </summary>
		/// <returns>id from category</returns>
		public int RetrieveCategoryId()
		{
			return categoryId;
		}

		/// <summary>
		///     Retrieve name from category
		/// </summary>
		/// <returns>Returns string name</returns>
		public string RetrieveCategoryName()
		{
			return categoryName;
		}

		/// <summary>
		///     Get or set description
		/// </summary>
		public string CategoryDesc { get; set; }

		/// <summary>
		///     Get or set image
		/// </summary>
		public string CategoryImg { get; set; }

	}
}