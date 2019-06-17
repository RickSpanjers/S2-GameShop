using System;
using System.Collections.Generic;
using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;

namespace RicksWebWorld.Context.Debug
{
	public class OrderTestContext : IOrder
	{
		List<Order> ListOfOrders = new List<Order>();


		public OrderTestContext()
		{
			UserRepository UserRepo = new UserRepository(new UserTestContext());
			User Admin = UserRepo.RetrieveUserById(1);

			Order O1 = new Order(Admin, "Nothing", 1, null);
			Order O2 = new Order(Admin, "Nothing", 2, null);
			Order O3 = new Order(Admin, "Nothing", 3, null);
			Order O4 = new Order(Admin, "Nothing", 4, null);

			ListOfOrders.Add(O1);
			ListOfOrders.Add(O2);
			ListOfOrders.Add(O3);
			ListOfOrders.Add(O4);
		}
			 
		public bool CreateNewOrder(Order o)
		{
			try
			{
				ListOfOrders.Add(o);
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}

		public List<Order> RetrieveAllOrders()
		{
			return ListOfOrders;
		}

		public bool DeleteOrderById(int orderId)
		{
			foreach(var singleorder in ListOfOrders)
			{
				if(singleorder.RetrieveOrderId() == orderId)
				{
					ListOfOrders.Remove(singleorder);
					return true;
				}
			}

			return false;
		}

		public bool UpdateOrderById(Order o)
		{
			foreach (var single in ListOfOrders)
				if (single.RetrieveOrderId() == o.RetrieveOrderId())
				{
					ListOfOrders.Remove(single);
					ListOfOrders.Add(o);
					return true;
				}
					
			return false;
		}

		public Order RetrieveOrderById(int id)
		{
			foreach (var singleitem in RetrieveAllOrders())
				if (singleitem.RetrieveOrderId() == id)
					return singleitem;

			return null;
		}
	}
}