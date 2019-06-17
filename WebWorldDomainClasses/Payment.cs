using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Models
{
    public class Payment
    {
		private string name;
		private string description;
		private int id;

		/// <summary>
		/// Create new payment method
		/// </summary>
		/// <param name="ID">Identification</param>
		/// <param name="Name">String name</param>
		/// <param name="Description">string description</param>
		public Payment(int ID, string Name, string Description)
		{
			id = ID;
			description = Description;
			name = Name;
		}

		/// <summary>
		/// Return the Identification
		/// </summary>
		/// <returns>int ID</returns>
		public int RetrieveID()
		{
			return id;
		}

		/// <summary>
		/// Return the name of the method
		/// </summary>
		/// <returns>string name</returns>
		public string RetrieveName()
		{
			return name;
		}
		
		/// <summary>
		/// Retrieve the description
		/// </summary>
		/// <returns>string description</returns>
		public string RetrieveDesc()
		{
			return description;
		}
    }
}
