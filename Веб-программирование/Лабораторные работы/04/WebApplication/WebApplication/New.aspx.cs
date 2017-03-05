using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class New : System.Web.UI.Page
    {
        protected List<string> errors = new List<string>();
        protected Dictionary<string, string> post = new Dictionary<string,string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            var _post = Request.Form;

            post["title"] = _post["title"];
            post["post"] = _post["post"];
            post["tags"] = _post["tags"];
            post["login"] = _post["login"];

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
                        Global.DB.Insert("posts", new Dictionary<string, string>()
                        {
                            { "title", _post["title"] },
                            { "post", _post["post"] },
                            { "author_id", user.Rows[0]["author_id"].ToString() },
                            { "date", (DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss") }
                        });

                        var postid = Global.DB.lastInsertedID;

                        if (_post["tags"].Length > 0)
                        {
                            var tags = _post["tags"].Split(new string[] { ", " }, StringSplitOptions.None)
                                                    .Select(n => n.Trim())
                                                    .Distinct()
                                                    .Where(x => !String.IsNullOrEmpty(x)).ToArray();

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
                            Server.Transfer("~/Default.aspx");
                        }
                        else
                        {
                            errors.Add("Ошибка при добавлении поста.");
                        }
                    }
                    else
                    {
                        errors.Add("Авторизационные данные неверны.");
                    }
                }
            }
        }
    }
}