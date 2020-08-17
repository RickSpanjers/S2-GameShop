using System;
using System.Collections.Generic;

namespace RicksWebWorld.Models
{
	public class ShoppingCart
	{
		private readonly int cartId;

		/// <summary>
		///     Constructor shoppingcart
		/// </summary>
		/// <param name="cartId">int Id</param>
		public ShoppingCart(int cartId)
		{
			this.cartId = cartId;
			ItemsInCart = new List<CartItem>();
		}

		/// <summary>
		///     get or set items in cart
		/// </summary>
		public List<CartItem> ItemsInCart { get; set; }

		/// <summary>
		///     Retrieve items from cart
		/// </summary>
		/// <returns>List cartitems</returns>
		public List<CartItem> RetrieveItems()
		{
			return ItemsInCart;
		}

		/// <summary>
		///     Add item to cart
		/// </summary>
		/// <param name="c">Cartitem</param>
		public void AddItemToCart(CartItem c)
		{
			ItemsInCart.Add(c);
			foreach (var singleitem in ItemsInCart) Console.WriteLine(singleitem.Product.ProductName);
		}

		/// <summary>
		/// Return ID of the cart
		/// </summary>
		/// <returns></returns>
		public int ReturnCartId()
		{
			return cartId;
		}

		/// <summary>
		/// Returns the calculated subtotal
		/// </summary>
		/// <returns>Double calculated subtotal</returns>
		public double GetSubtotalCart()
		{
			double subtotal = 0;
			foreach(var Item in ItemsInCart)
			{
				if(Item.Product.ProductPrice > Item.Product.ProductDiscount)
				{
					subtotal += Convert.ToDouble(Item.Product.ProductDiscount) * Item.Quantity;
				}
				else
				{
					subtotal += Convert.ToDouble(Item.Product.ProductPrice) * Item.Quantity;
				}				
			}
			return subtotal;
		}

		/// <summary>
		/// Returns calculated total in cart
		/// </summary>
		/// <returns>Double calculated total</returns>
		public double GetTotalCart()
		{
			double shipping = 4.50;
			double total = GetSubtotalCart() + shipping;
			return total;
		}

		/// <summary>
		/// Returns the additional btw amount
		/// </summary>
		/// <returns>Returns double with BTW</returns>
		public double GetBTWAddition()
		{
			double btwaddition = GetTotalCart() / 100 * 21;
			return Math.Round(btwaddition, 2, MidpointRounding.AwayFromZero);
		}

		/// <summary>
		/// Returns the grandtotal of the shoppingcart
		/// </summary>
		/// <returns>Double calculated grandtotal</returns>
		public double GetGrandTotal()
		{
			double grandtotal = GetBTWAddition() + GetTotalCart();
			return Math.Round(grandtotal, 2, MidpointRounding.AwayFromZero);
		}

	
	}
}