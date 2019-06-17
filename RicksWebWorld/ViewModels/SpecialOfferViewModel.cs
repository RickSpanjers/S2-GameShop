using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class SpecialOfferViewModel
    {
		public int Id { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public decimal OfferPrice { get; set; }
	}
}
