using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class CartItemViewModel
    {
		private int CartId { get; set; }
		public int Quantity { get; set; }
		public ProductViewModel Product { get; set; }
		public double CurrentPrice() {
			if (Product.ProductDiscount < Product.ProductPrice)
			{
				return Convert.ToDouble(Product.ProductDiscount);
			}
			else
			{
				return Convert.ToDouble(Product.ProductPrice);
			}
		}
		public double FullPriceQuantity()
		{
			double result = CurrentPrice() * Quantity;
			return result;
		}
	}
}
