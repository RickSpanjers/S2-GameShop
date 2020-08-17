using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Interfaces
{
    public interface IPost
	{
		bool CreateNewPost(Post p);
		List<Post> RetrieveAllPosts();
		bool DeletePostById(int Id);
		bool UpdatePost(Post p);
		Post RetrievePostById(int id);
	}
}
