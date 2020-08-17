using System.Collections.Generic;
using RicksWebWorld.Models;

namespace RicksWebWorld.Interfaces
{
	public interface IOrder
	{
		bool CreateNewOrder(Order o);
		List<Order> RetrieveAllOrders();
		bool DeleteOrderById(int orderId);
		bool UpdateOrderById(Order o);
		Order RetrieveOrderById(int id);
	}
}