using System.Collections.Generic;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;

namespace RicksWebWorld.Repositories
{
	public class OrderRepository
	{
		private readonly IOrder orderInterface;

		public OrderRepository(IOrder context)
		{
			orderInterface = context;
		}

		public void CreateNewOrder(Order o)
		{
			orderInterface.CreateNewOrder(o);
		}

		public List<Order> RetrieveAllOrders()
		{
			return orderInterface.RetrieveAllOrders() as List<Order>;
		}

		public bool DeleteOrder(int orderId)
		{
			return orderInterface.DeleteOrderById(orderId);
		}

		public bool UpdateOrder(Order o)
		{
			return orderInterface.UpdateOrderById(o);
		}

		public Order RetrieveOrderById(int id)
		{
			return orderInterface.RetrieveOrderById(id);
		}
	}
}