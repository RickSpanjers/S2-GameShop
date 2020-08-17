using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class RevisionViewModel
    {
		public int Id { get; set; }
		public int ProductId { get; set; }
		public DateTime RevisionDateTime { get; set; }
	}
}
