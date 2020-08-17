using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.ViewModels
{
    public class PostOverviewViewModel
    {
		public int Id { get; set; }
		public string Title { get; set; }
		public List<PostViewModel> PostsInSystem { get; set; } = new List<PostViewModel>();
    }
}
