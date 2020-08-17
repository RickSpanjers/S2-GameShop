using RicksWebWorld.Interfaces;
using RicksWebWorld.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Context.MssqlContext
{
	public class PostMSSQLContext : ConnectionHelper, IPost
	{
		public bool CreateNewPost(Post p)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "INSERT INTO dbo.[Blog] (Title, PageContent, PostDate, Status, PostImage, PostExcerpt) VALUES (@title, @content, @date, @status, @img, @excerpt)";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);

				newCmd.Parameters.AddWithValue("@title", p.RetrieveTitle());
				newCmd.Parameters.AddWithValue("@content", p.RetrieveContent());
				newCmd.Parameters.AddWithValue("@date", p.RetrieveDatePosted());
				newCmd.Parameters.AddWithValue("@status", p.RetrieveStatus());
				newCmd.Parameters.AddWithValue("@img", p.RetrieveImage());
				newCmd.Parameters.AddWithValue("@excerpt", p.RetrieveExcerpt());

				newCmd.ExecuteNonQuery();
				cnn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return false;
		}

		public bool DeletePostById(int Id)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "DELETE FROM dbo.[Blog] WHERE ID = @Id";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@Id", Id);
				cmd.ExecuteNonQuery();
				cnn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return false;
		}

		public List<Post> RetrieveAllPosts()
		{
			List<Post> listOfPosts = new List<Post>();
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * FROM dbo.[Blog]";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int Id = Convert.ToInt32(dr["ID"]);
					string title = dr["Title"].ToString();
					string content = dr["PageContent"].ToString();
					int status = Convert.ToInt32(dr["Status"]);
					DateTime postdate = Convert.ToDateTime(dr["PostDate"]);
					string postimage = dr["PostImage"].ToString();
					string postExcerpt = dr["PostExcerpt"].ToString();

					Post p = new Post(Id, title, content, status, postdate, postimage, postExcerpt);
					listOfPosts.Add(p);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return listOfPosts;
		}

		public Post RetrievePostById(int id)
		{
			var post = new Post(-1, null, null, 1, DateTime.Now, null, null);
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "SELECT * from dbo.[Blog] WHERE ID=@id";
				SqlCommand cmd = CreateSQLCommandText(query, cnn);	
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					int Id = Convert.ToInt32(dr["Id"]);
					string title = dr["Title"].ToString();
					string content = dr["PageContent"].ToString();
					int status = Convert.ToInt32(dr["Status"]);
					DateTime postdate = Convert.ToDateTime(dr["PostDate"]);
					string postimage = dr["PostImage"].ToString();
					string postExcerpt = dr["PostExcerpt"].ToString();

					post = new Post(Id, title, content, status, postdate, postimage, postExcerpt);
				}

				cnn.Close();
			}
			catch (NullReferenceException e)
			{
				Console.WriteLine(e.Message);
			}
			return post;
		}

		public bool UpdatePost(Post p)
		{
			try
			{
				SqlConnection cnn = ReturnSQLConnection();
				string query = "UPDATE dbo.[Blog] SET Title=@title, PageContent=@content, Status=@status, PostImage=@Image, PostExcerpt=@excerpt WHERE ID = @Id";
				SqlCommand newCmd = CreateSQLCommandText(query, cnn);

				newCmd.Parameters.AddWithValue("@Id", p.RetrieveId());
				newCmd.Parameters.AddWithValue("@title", p.RetrieveTitle());
				newCmd.Parameters.AddWithValue("@content", p.RetrieveContent());
				newCmd.Parameters.AddWithValue("@status", p.RetrieveStatus());
				newCmd.Parameters.AddWithValue("@Image", p.RetrieveImage());
				newCmd.Parameters.AddWithValue("@excerpt", p.RetrieveExcerpt());

				newCmd.ExecuteNonQuery();
				cnn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return false;
		}
	}
}
