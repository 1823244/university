using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication
{
    public partial class Post : System.Web.UI.Page
    {
        protected List<string> errors = new List<string>();
        protected Dictionary<string, string> post;
        protected List<Dictionary<string, string>> comments = new List<Dictionary<string,string>>();

        protected void Page_Load(object sender, EventArgs e)
        {
            var _post = Request.Form;
            var _get = Request.QueryString;

            int value;
            if (_get["id"] != null && Int32.TryParse(_get["id"], out value))
            {
                if (_post["submit"] != null)
                {
                    if (_post["name"].Length == 0)
                    {
                        errors.Add("Вы не ввели имя.");
                    }

                    if (_post["email"].Length == 0)
                    {
                        errors.Add("Вы не ввели почту.");
                    }
                    else if (!IsValidEmail(_post["email"]))
                    {
                        errors.Add("Введённая электронная почта неверна.");
                    }

                    if (_post["comment"].Length == 0)
                    {
                        errors.Add("Вы не ввели текст комментария.");
                    }

                    if (errors.Count == 0)
                    {
                        Global.DB.Insert("comments", new Dictionary<string, string>()
                        {
                            { "name", _post["name"] },
                            { "post_id", _get["id"] },
                            { "comment", _post["comment"] },
                            { "email", _post["email"] },
                            { "date", (DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss") }
                        });
                    }
                }
                
                var record = Global.DB.Select("posts", new Dictionary<string, string>()
                {
                    { "post_id", _get["id"] }
                });

                if (record.Rows.Count > 0)
                {
                    var rec = record.Rows[0];

                    var authordb = Global.DB.Select("authors", new Dictionary<string, string>()
                    {
                        { "author_id", record.Rows[0]["author_id"].ToString() }
                    });

                    var author = authordb.Rows[0];

                    post = new Dictionary<string, string>()
                    {
                       { "title", rec["title"].ToString() },
                       { "author", author["username"].ToString() },
                       { "text", "<p>" + rec["post"].ToString().Replace("\r\n", "</p><p>\r\n") + "</p>" },
                       { "date", (DateTime.Parse(rec["date"].ToString())).ToString("dd.MM.yy HH:mm") },
                       { "edit-link", "/Edit.aspx?id=" + _get["id"] },
                       { "del-link", "/Delete.aspx?id=" + _get["id"] }
                    };

                    this.Title = post["title"];

                    var commentsdb = Global.DB.Select("comments", new Dictionary<string, string>()
                    {
                        { "post_id", _get["id"] }
                    });

                    foreach (DataRow commentdb in commentsdb.Rows)
                    {
                        comments.Add(GetComment(commentdb));
                    }

                    var tag_ids = Global.DB.Select("post_tags", new Dictionary<string, string>()
                    {
                        { "post_id", _get["id"] }
                    }, null, null, "tag_id");

                    var tags = new List<string>();

                    if (tag_ids.Rows.Count > 0)
                    {
                        foreach (DataRow tag_id in tag_ids.Rows)
                        {
                            var tag = Global.DB.Select("tags", new Dictionary<string, string>()
                        {
                            { "tag_id", tag_id["tag_id"].ToString() }
                        });

                            tags.Add(tag.Rows[0]["name"].ToString());
                        }
                    }

                    post["tags"] = String.Join(", ", tags);
                }
                else
                {
                    Server.Transfer("~/Default.aspx");
                }
            }
            else
            {
                Server.Transfer("~/Default.aspx");
            }
        }

        private Dictionary<string, string> GetComment(DataRow rec)
        {
            return new Dictionary<string, string> {
                { "name", rec["name"].ToString() },
                { "text", "<p>" + rec["comment"].ToString().Replace("\r\n", "</p><p>\r\n") + "</p>" },
                { "email", rec["email"].ToString() },
                { "date", (DateTime.Parse(rec["date"].ToString())).ToString("dd.MM.yy HH:mm") }
            };
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}