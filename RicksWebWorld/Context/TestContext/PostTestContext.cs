using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Context.TestContext
{
	public class PostTestContext : IPost
	{

		private List<Post> listOfPost = new List<Post>();

		public PostTestContext()
		{
			Post p1 = new Post(1, "Post01", "test", 1, DateTime.Now, "None", "None");
			Post p2 = new Post(2, "Post02", "test", 1, DateTime.Now, "None", "None");
			Post p3 = new Post(3, "Post03", "test", 1, DateTime.Now, "None", "None");
			listOfPost.Add(p1);
			listOfPost.Add(p2);
			listOfPost.Add(p3);
		}

		public bool CreateNewPost(Post p)
		{
			listOfPost.Add(p);
			return true;
		}

		public bool DeletePostById(int Id)
		{
			foreach(Post p in listOfPost)
			{
				if(p.RetrieveId() == Id)
				{
					listOfPost.Remove(p);
					return true;
				}
			}

			return false;
		}

		public List<Post> RetrieveAllPosts()
		{
			return listOfPost;
		}

		public Post RetrievePostById(int id)
		{
			foreach (Post p in listOfPost)
			{
				if (p.RetrieveId() == id)
				{
					return p;
				}
			}

			return null;
		}

		public bool UpdatePost(Post p)
		{
			foreach (Post post in listOfPost)
			{
				if (post.RetrieveId() == p.RetrieveId())
				{
					listOfPost.Remove(post);
					listOfPost.Add(p);
					return true;
				}
			}

			return false;
		}
	}
}
