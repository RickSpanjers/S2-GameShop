using System.Collections.Generic;

namespace RicksWebWorld.Models
{
	public class Order
	{
		private readonly User customer;
		private readonly List<CartItem> itemsInOrder = new List<CartItem>();
		private readonly int orderId;


		/// <summary>
		///     Create new order (constructor)
		/// </summary>
		/// <param name="c">User</param>
		/// <param name="details">details string</param>
		/// <param name="orderId">Id from the order</param>
		/// <param name="listOfItems">List of cart items</param>
		public Order(User c, string details, int orderId, List<CartItem> listOfItems)
		{
			customer = c;
			OrderDetails = details;
			this.orderId = orderId;
			itemsInOrder = listOfItems;
		}


		/// <summary>
		///     get or set details from order
		/// </summary>
		public string OrderDetails { get; set; }

		/// <summary>
		///     Address to send product to
		/// </summary>
		public string SendAddress { get; set; }

		/// <summary>
		///     Zipcode to send product to
		/// </summary>
		public string SendZipcode { get; set; }

		/// <summary>
		///     Place to send product to
		/// </summary>
		public string SendPlace { get; set; }

		/// <summary>
		///     Retrieve id from order
		/// </summary>
		/// <returns>int Id</returns>
		public int RetrieveOrderId()
		{
			return orderId;
		}

		/// <summary>
		///     Retrieve items in order
		/// </summary>
		/// <returns>List of cart items</returns>
		public List<CartItem> RetrieveItemsInOrder()
		{
			return itemsInOrder;
		}

		/// <summary>
		///     Retrieve the user
		/// </summary>
		/// <returns>Return user that orders</returns>
		public User RetrieveCustomer()
		{
			return customer;
		}
	}
}