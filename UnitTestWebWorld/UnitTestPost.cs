using Microsoft.VisualStudio.TestTools.UnitTesting;
using RicksWebWorld.Context.TestContext;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestWebWorld
{
	[TestClass]
	class UnitTestPost
    {
		PostRepository postRepo = new PostRepository(new PostTestContext());

		[TestMethod]
		public void SavePostTestContext()
		{
			Post p4 = new Post(4, "Post04", "test", 1, DateTime.Now, "None", "None");
			var result = postRepo.CreateNewPost(p4);
			var TotalPosts = postRepo.RetrieveAllPosts();
			Assert.IsTrue(TotalPosts.Count > 3);
		}

		[TestMethod]
		public void EditPostTestContext()
		{
			Post p3 = new Post(3, "Post04", "test", 1, DateTime.Now, "None", "None");

			var result = postRepo.UpdatePostById(p3);
			var TotalPosts = postRepo.RetrieveAllPosts();

			foreach (var single in TotalPosts)
			{
				Assert.IsFalse(single.RetrieveTitle() == "Post03");
			}
		}

		[TestMethod]
		public void DeletePostTestContext()
		{
			var result = postRepo.DeletePostById(1);
			var TotalPosts = postRepo.RetrieveAllPosts();

			foreach (var single in TotalPosts)
			{
				Assert.IsFalse(single.RetrieveTitle() == "Post01");
			}
		}

		[TestMethod]
		public void RetrieveAllPosts()
		{
			var TotalPosts = postRepo.RetrieveAllPosts();
			Assert.IsTrue(postRepo.RetrieveAllPosts() != null);	
		}
	}
}
