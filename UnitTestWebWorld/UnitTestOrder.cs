using Microsoft.VisualStudio.TestTools.UnitTesting;
using RicksWebWorld.Context.Debug;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestWebWorld
{
	[TestClass]
	public class UnitTestOrder
    {
		OrderRepository OrderRepoTest = new OrderRepository(new OrderTestContext());
		UserRepository UserRepoTest = new UserRepository(new UserTestContext());

		[TestMethod]
		public void CreateNewOrder01()
		{
			var admin = UserRepoTest.RetrieveUserById(1);
			Order o = new Order(admin, "None", 5, null);
			o.SendAddress = "Testlane";
			o.SendPlace = "Testplace";
			o.SendZipcode = "1234AB";

			OrderRepoTest.CreateNewOrder(o);
			var AllOrders = OrderRepoTest.RetrieveAllOrders();
			Assert.IsTrue(AllOrders.Count > 4);	
		}

		public void CreateNewOrder02()
		{
			var admin = UserRepoTest.RetrieveUserById(1);
			Order o = new Order(admin, "None", 5, null);
			o.SendAddress = "Testlane";
			o.SendPlace = "Testplace";
			o.SendZipcode = "1234AB";

			OrderRepoTest.CreateNewOrder(o);
			var AddedOrder = OrderRepoTest.RetrieveOrderById(5);
			Assert.IsTrue(AddedOrder.RetrieveOrderId() == 5);
		}

		public void CreateNewOrder03()
		{
			var admin = UserRepoTest.RetrieveUserById(1);
			Order o = new Order(admin, "None", 5, null);
			o.SendAddress = "Testlane";
			o.SendPlace = "Testplace";
			o.SendZipcode = "1234AB";

			OrderRepoTest.CreateNewOrder(o);
			Assert.IsTrue(OrderRepoTest.RetrieveAllOrders() != null);
		}

		[TestMethod]
		public void RetrieveAllOrders()
		{
			var allorders = OrderRepoTest.RetrieveAllOrders();
			Assert.IsTrue(allorders.Count == 4);
			Assert.IsTrue(allorders.Count < 5);
			Assert.IsTrue(allorders.Count > 3);
			Assert.IsTrue(OrderRepoTest.RetrieveAllOrders() != null);
		}

		[TestMethod]
		public void DeleteOrderById()
		{
			OrderRepoTest.DeleteOrder(2);

			var AllOrders = OrderRepoTest.RetrieveAllOrders();
			foreach (var single in AllOrders)
			{
				Assert.IsFalse(single.RetrieveOrderId() == 2);
				Assert.IsTrue(OrderRepoTest.RetrieveAllOrders() != null);
			}

		}

		[TestMethod]
		public void UpdateOrderById()
		{
			var admin = UserRepoTest.RetrieveUserById(1);
			Order o = new Order(admin, "Something", 5, null);
			o.SendAddress = "Testlane";
			o.SendPlace = "Testplace";
			o.SendZipcode = "1234AB";

			var result = OrderRepoTest.UpdateOrder(o);
			var TotalOrders = OrderRepoTest.RetrieveAllOrders();

			foreach (var single in TotalOrders)
			{
				Assert.IsFalse(single.OrderDetails == "Something");
				Assert.IsTrue(OrderRepoTest.RetrieveAllOrders() != null);
			}

		}

	}
}
