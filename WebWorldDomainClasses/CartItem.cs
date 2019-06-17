using System;

namespace RicksWebWorld.Models
{
	public class CartItem
	{
		private readonly int cartId;

		/// <summary>
		///     Constructor for cartitem
		/// </summary>
		/// <param name="cartId">Expects a cartid</param>
		public CartItem(int cartId)
		{
			this.cartId = cartId;
		}


		/// <summary>
		///     Quantity of the product
		/// </summary>
		public int Quantity { get; set; }


		/// <summary>
		///     Product for the item
		/// </summary>
		public Product Product { get; set; }


		/// <summary>
		/// Returns ID of cart
		/// </summary>
		/// <returns></returns>
		public int ReturnCartId()
		{
			return cartId;
		}


		/// <summary>
		/// Returns current price of the cartitem
		/// </summary>
		/// <returns>Double with calculated price</returns>
		public double GetCurrentPrice()
		{
			if(Product.ProductDiscount < Product.ProductPrice)
			{
				return Convert.ToDouble(Product.ProductDiscount);
			}
			else
			{
				return Convert.ToDouble(Product.ProductPrice);
			}
		}


		/// <summary>
		/// Return the price times the quantity
		/// </summary>
		/// <returns>Double calculated price</returns>
		public double GetFullPriceQuantity()
		{
			double result = GetCurrentPrice() * Quantity;
			return result;
		}
	}
}