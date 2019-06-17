using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Repositories
{
    public class PostRepository
    {
		private readonly IPost postInterface;

		public PostRepository(IPost context)
		{
			postInterface = context;
		}

		public bool CreateNewPost(Post p)
		{
			return postInterface.CreateNewPost(p);
		}

		public bool DeletePostById(int Id)
		{
			return postInterface.DeletePostById(Id);
		}

		public List<Post> RetrieveAllPosts()
		{
			return postInterface.RetrieveAllPosts() as List<Post>;
		}

		public Post RetrievePostById(int id)
		{
			return postInterface.RetrievePostById(id);
		}

		public bool UpdatePostById(Post p)
		{
			return postInterface.UpdatePost(p);
		}
	}
}
