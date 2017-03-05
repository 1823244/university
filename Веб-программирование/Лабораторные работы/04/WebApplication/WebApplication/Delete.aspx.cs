using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class Delete : System.Web.UI.Page
    {
        protected List<string> errors = new List<string>();
        protected Dictionary<string, string> post = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            var _post = Request.Form;
            var _get = Request.QueryString;

            post["back-link"] = "/Default.aspx";

            int value;
            if (_get["id"] != null && Int32.TryParse(_get["id"], out value))
            {
                post["back-link"] = "/Post.aspx?id=" + _get["id"];

                if (_post["del"] != null)
                {
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
                            Global.DB.Delete("posts", new Dictionary<string, string>()
                            {
                                { "post_id", _get["id"] }
                            });

                            if (Global.DB.lastError == null)
                            {
                                Server.Transfer("~/Default.aspx");
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

                var record = Global.DB.Select("posts", new Dictionary<string, string>()
                {
                    { "post_id", _get["id"] }
                }, null, null, "title");

                if (record.Rows.Count > 0)
                {
                    post["title"] = record.Rows[0]["title"].ToString();
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
    }
}