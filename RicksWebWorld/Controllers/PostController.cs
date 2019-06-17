using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.Context.MssqlContext;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;

namespace RicksWebWorld.Controllers
{
    public class PostController : Controller
    {
		private readonly PostRepository postRep = new PostRepository(new PostMSSQLContext());
		private AutoMapperExtension mapextension = new AutoMapperExtension();


		public IActionResult Overview()
		{
			PostOverviewViewModel model = new PostOverviewViewModel();
			model.PostsInSystem = new List<PostViewModel>();
			var mapper = mapextension.PostToPostViewModel();
			foreach (Post item in postRep.RetrieveAllPosts())
			{
				PostViewModel pmodel = mapper.Map<PostViewModel>(item);
				model.PostsInSystem.Add(pmodel);
			}
			return View("PostOverview", model);
		}

		public IActionResult Single()
		{
			PostViewModel model = new PostViewModel();
			return View("SinglePost", model);
		}


		public IActionResult SingleEdit(int pageToEdit)
		{
			Post p = postRep.RetrievePostById(pageToEdit);
			var mapper = mapextension.PostToPostViewModel();
			PostViewModel model =  mapper.Map<PostViewModel>(p);
			return View("SinglePost", model);
		}

		[HttpPost]
		public IActionResult Add(PostViewModel model, string uploadedImage)
		{
			model.PostImage = uploadedImage;
			Post p = new Post(-1, model.Title, model.Content, model.Status, DateTime.Now, model.PostImage, model.Excerpt);
			if(postRep.CreateNewPost(p) == true)
			{
				return RedirectToAction("Overview", "Post");
			}
			else
			{
				return RedirectToAction("Dashboard", "Home");
			}
	
		}

		[HttpPost]
		public IActionResult Delete(int pageToDelete)
		{
			if(postRep.DeletePostById(pageToDelete) == true)
			{
				return RedirectToAction("Overview", "Post");
			}
			else
			{
				return RedirectToAction("Dashboard", "Home");
			}		
		}

		[HttpPost]
		public IActionResult Update(PostViewModel model, int pageToEdit, string uploadedImage)
		{
			model.Postdate = DateTime.Now;
			if(uploadedImage == null)
			{model.PostImage = postRep.RetrievePostById(pageToEdit).RetrieveImage();}

			Post p = new Post(pageToEdit, model.Title, model.Content, model.Status, model.Postdate, model.PostImage, model.Excerpt);

			if(postRep.UpdatePostById(p) == true){return RedirectToAction("Overview", "Post");}
			else{return RedirectToAction("Dashboard", "Home");}

		}
	}
}