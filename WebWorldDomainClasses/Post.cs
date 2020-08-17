using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Models
{
    public class Post
    {
		private int id;
		private string title;
		private string content;
		private int status;
		private DateTime postdate;
		private string postimg;
		private string postexcerpt;

		public Post(int Id, string Title, string Content, int Status, DateTime Postdate, string PostImg, string PostExcerpt)
		{
			id = Id;
			title = Title;
			content = Content;
			status = Status;
			postdate = Postdate;
			postimg = PostImg;
			postexcerpt = PostExcerpt;
		}

		public int RetrieveId()
		{
			return id;
		}

		public string RetrieveTitle()
		{
			return title;
		}

		public string RetrieveContent()
		{
			return content;
		}

		public int RetrieveStatus()
		{
			return status;
		}

		public DateTime RetrieveDatePosted()
		{
			return postdate;
		}

		public string RetrieveImage()
		{
			return postimg;
		}

		public string RetrieveExcerpt()
		{
			return postexcerpt;
		}
    }
}
