using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Context.Dapper
{
    public class PostDapperContext : ConnectionHelper, IPost
    {
		public bool CreateNewPost(Post p)
		{
			throw new NotImplementedException();
		}

		public bool DeletePostById(int Id)
		{
			throw new NotImplementedException();
		}

		public List<Post> RetrieveAllPosts()
		{
			throw new NotImplementedException();
		}

		public Post RetrievePostById(int id)
		{
			throw new NotImplementedException();
		}

		public bool UpdatePost(Post p)
		{
			throw new NotImplementedException();
		}
	}
}
