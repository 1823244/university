using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication
{
    public partial class _Default : System.Web.UI.Page
    {
        protected List<Dictionary<string, string>> posts;

        protected void Page_Load(object sender, EventArgs e)
        { 
            var records = Global.DB.Select("posts", null, "post_id DESC", "10");
            var author_ids = new List<string>();

            if (records.Rows.Count > 0)
            {
                foreach (DataRow row in records.Rows)
                {
                    if (! author_ids.Contains(row["author_id"].ToString()))
                    {
                        author_ids.Add(row["author_id"].ToString());
                    }
                }
            }

            var authors = new Dictionary<string, string>();

            foreach (var author_id in author_ids)
            {
                var rec = Global.DB.Select("authors", new Dictionary<string, string>()
                {
                    { "author_id", author_id }
                });

                authors.Add(author_id, rec.Rows[0]["username"].ToString());
            }

            posts = new List<Dictionary<string, string>>();

            foreach (DataRow rec in records.Rows)
            {
                posts.Add(GetPost(rec, authors));
            }
        }

        private Dictionary<string, string> GetPost(DataRow rec, Dictionary<string, string> authors)
        {
            var post = rec["post"].ToString();
            if (post.Length > 200)
            {
                post.Substring(0, 200);
            }
            post += "..";
            post = post.Replace("\r\n", "<br>\r\n");

            return new Dictionary<string, string> {
                { "link", "/Post.aspx?id=" + rec["post_id"].ToString() },
                { "title", rec["title"].ToString() },
                { "author", authors[rec["author_id"].ToString()] },
                { "excerpt", post },
                { "date", (DateTime.Parse(rec["date"].ToString())).ToString("dd.MM.yy HH:mm") }
            };
        }
    }
}
