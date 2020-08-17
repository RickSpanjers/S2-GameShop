using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class ShoppingCartViewModel
    {
		public int cartId { get; set; }
		public List<CartItemViewModel> ItemsInCart { get; set; }
		public double Subtotal()
		{
			double subtotal = 0;
			foreach (var Item in ItemsInCart)
			{
				if (Item.Product.ProductPrice > Item.Product.ProductDiscount)
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

		public double Total()
		{
			double shipping = 4.50;
			double total = Subtotal() + shipping;
			return total;
		}
		public double BTW() {

			double btwaddition = Total() / 100 * 21;
			return Math.Round(btwaddition, 2, MidpointRounding.AwayFromZero);
		}
		public double GrandTotal()
		{
			double grandtotal = BTW() + Total();
			return Math.Round(grandtotal, 2, MidpointRounding.AwayFromZero);
		}
	}
}

