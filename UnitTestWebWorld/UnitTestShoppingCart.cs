using Microsoft.VisualStudio.TestTools.UnitTesting;
using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestWebWorld
{
	[TestClass]
	public class UnitTestShoppingCart
    {
		ShoppingCart cart = new ShoppingCart(1);

		public UnitTestShoppingCart()
		{
			CartItem c1 = new CartItem(1);
			c1.Product = new Product(1, "P1");
			c1.Product.ProductPrice = 25;
			c1.Product.ProductDiscount = 20;
			c1.Quantity = 1;

			CartItem c2 = new CartItem(1);
			c2.Product = new Product(2, "P2");
			c2.Product.ProductPrice = 25;
			c2.Product.ProductDiscount = 24;
			c2.Quantity = 2;

			CartItem c3 = new CartItem(1);
			c3.Product = new Product(3, "P2");
			c3.Product.ProductPrice = 25;
			c3.Product.ProductDiscount = 30;
			c3.Quantity = 1;

			cart.AddItemToCart(c1);
			cart.AddItemToCart(c2);
			cart.AddItemToCart(c3);
		}


		[TestMethod]
		public void GetSubtotalCart01()
		{
			Assert.IsNotNull(cart.GetSubtotalCart());
		}

		[TestMethod]
		public void GetSubtotalCart02()
		{
			Assert.AreEqual(93, cart.GetSubtotalCart());
		}

		[TestMethod]
		public void GetSubtotalCart03()
		{
			Assert.AreNotEqual(94.00, cart.GetSubtotalCart());
		}

		[TestMethod]
		public void GetTotalCart01()
		{
			Assert.IsNotNull(cart.GetTotalCart());
		}

		[TestMethod]
		public void GetTotalCart02()
		{
			Assert.AreEqual(97.50, cart.GetTotalCart());
		}

		[TestMethod]
		public void GetTotalCart03()
		{
			Assert.AreNotEqual(97.00, cart.GetTotalCart());
		}

		[TestMethod]
		public void GetBTWAddition01()
		{
			Assert.IsNotNull(cart.GetBTWAddition());
		}

		[TestMethod]
		public void GetBTWAddition02()
		{
			Assert.AreEqual(20.47, cart.GetBTWAddition());
		}

		[TestMethod]
		public void GetBTWAddition03()
		{
			Assert.AreNotEqual(20.48, cart.GetBTWAddition());
		}

		[TestMethod]
		public void GetGrandTotal01()
		{
			Assert.IsNotNull(cart.GetGrandTotal());
		}

		[TestMethod]
		public void GetGrandTotal02()
		{
			Assert.AreEqual(117.97, cart.GetGrandTotal());
		}

		[TestMethod]
		public void GetGrandTotal03()
		{
			Assert.AreNotEqual(118.00, cart.GetGrandTotal());
		}
	}
}
