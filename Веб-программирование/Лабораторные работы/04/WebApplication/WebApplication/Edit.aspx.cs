using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication
{
    public partial class Edit : System.Web.UI.Page
    {
        protected List<string> errors = new List<string>();
        protected Dictionary<string, string> post = new Dictionary<string,string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            var _post = Request.Form;
            var _get = Request.QueryString;

            post["back-link"] = "/Default.aspx";

            int value;
            if (_get["id"] != null && Int32.TryParse(_get["id"], out value))
            {
                post["back-link"] = "/Post.aspx?id=" + _get["id"];

                if (_post["publish"] != null)
                {
                    if (_post["title"].Length == 0)
                    {
                        errors.Add("Придумайте заголовок для поста.");
                    }

                    if (_post["post"].Length == 0)
                    {
                        errors.Add("Напишите что-нибудь.");
                    }

                    if (_post["login"].Length == 0 || _post["password"].Length == 0)
                    {
                        errors.Add("Введите авторизационные данные.");
                    }

                    post["title"] = _post["title"];
                    post["post"] = _post["post"];
                    post["tags"] = _post["tags"];
                    post["login"] = _post["login"];

                    if (errors.Count == 0)
                    {
                        Global.DB.lastError = null;

                        var user = Global.DB.Select("authors", new Dictionary<string, string>()
                        {
                            { "username", _post["login"] },
                            { "password", _post["password"] }
                        });

                        if (user.Rows.Count > 0)
                        {
                            Global.DB.Update("posts", new Dictionary<string, string>()
                            {
                                { "title", _post["title"] },
                                { "post", _post["post"] },
                                { "author_id", user.Rows[0]["author_id"].ToString() },
                                { "date", (DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss") }
                            }, new Dictionary<string, string>()
                            {
                                { "post_id", _get["id"] }
                            });

                            var postid = _get["id"];

                            if (_post["tags"].Length > 0)
                            {
                                var tags = _post["tags"].Split(new string[] { ", " }, StringSplitOptions.None)
                                                    .Select(n => n.Trim())
                                                    .Distinct()
                                                    .Where(x => !String.IsNullOrEmpty(x)).ToArray();

                                Global.DB.Delete("post_tags", new Dictionary<string, string>()
                                {
                                    { "post_id", postid.ToString() }
                                });

                                foreach (var tag in tags)
                                {
                                    var found = Global.DB.Select("tags", new Dictionary<string, string>()
                                    {
                                        { "name", tag }
                                    });

                                    if (found.Rows.Count > 0)
                                    {
                                        Global.DB.Insert("post_tags", new Dictionary<string, string>()
                                        {
                                            { "post_id", postid.ToString() },
                                            { "tag_id", found.Rows[0]["tag_id"].ToString() }
                                        });

                                        if (Global.DB.lastError != null && Global.DB.lastError.IndexOf("Duplicate entry") == 0)
                                        {
                                            Global.DB.lastError = null;
                                        }
                                    }
                                    else
                                    {
                                        Global.DB.Insert("tags", new Dictionary<string, string>()
                                        {
                                            { "name", tag }
                                        });

                                        var tagid = Global.DB.lastInsertedID;

                                        Global.DB.Insert("post_tags", new Dictionary<string, string>()
                                        {
                                            { "post_id", postid.ToString() },
                                            { "tag_id", tagid.ToString() }
                                        });
                                    }
                                }
                            }

                            if (Global.DB.lastError == null)
                            {
                                Server.Transfer("~/Post.aspx?id=" + _get["id"]);
                            }
                            else
                            {
                                errors.Add("Ошибка при изменении поста.");
                            }
                        }
                        else
                        {
                            errors.Add("Авторизационные данные неверны.");
                        }
                    }
                }
                else
                {
                    var record = Global.DB.Select("posts", new Dictionary<string, string>()
                    {
                        { "post_id", _get["id"] }
                    });

                    if (record.Rows.Count > 0)
                    {
                        post["title"] = record.Rows[0]["title"].ToString();
                        post["post"] = record.Rows[0]["post"].ToString();

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
                        post["login"] = "";
                    }
                    else
                    {
                        Server.Transfer("~/Default.aspx");
                    }
                }
            }
            else
            {
                Server.Transfer("~/Default.aspx");
            }
        }
    }
}