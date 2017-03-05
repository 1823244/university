using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication
{
    public partial class Users : System.Web.UI.Page
    {
        protected List<string> errors = new List<string>();
        protected bool del = false;
        protected Dictionary<string, string> edit = null;
        protected List<Dictionary<string, string>> users = new List<Dictionary<string, string>>();
        protected void Page_Load(object sender, EventArgs e)
        {
            var _post = Request.Form;
            var _get = Request.QueryString;

            int value;

            if (_get.GetValues(null) != null && _get.GetValues(null).Contains("add"))
            {
                edit = new Dictionary<string, string>();

                if (_post["save"] != null)
                {
                    if (_post["login"].Length == 0)
                    {
                        errors.Add("Введите логин.");
                    }

                    if (_post["password"].Length == 0)
                    {
                        errors.Add("Введите пароль.");
                    }

                    if (errors.Count == 0)
                    {
                        Global.DB.lastError = null;

                        var userdb = Global.DB.Select("authors", new Dictionary<string, string>()
                        {
                            { "username", _post["login"] }
                        });

                        if (userdb.Rows.Count > 0)
                        {
                            errors.Add("Такой пользователь уже существует.");
                        }

                        if (errors.Count == 0)
                        {
                            Global.DB.Insert("authors", new Dictionary<string, string>()
                            {
                                { "username", _post["login"] },
                                { "password", _post["password"] }
                            });

                            if (Global.DB.lastError == null)
                            {
                                Response.Redirect("/Users.aspx");
                            }
                            else
                            {
                                errors.Add("Ошибка при добавлении пользователя.");
                            }
                        }
                    }
                }
            }

            if (_get["edit"] != null && Int32.TryParse(_get["edit"], out value))
            {
                var record = Global.DB.Select("authors", new Dictionary<string, string>()
                {
                    { "author_id", _get["edit"] }
                });

                edit = new Dictionary<string, string>()
                {
                    { "login", record.Rows[0]["username"].ToString() },
                    { "password", record.Rows[0]["password"].ToString() }
                };

                if (_post["save"] != null)
                {
                    if (_post["login"].Length == 0)
                    {
                        errors.Add("Введите логин.");
                    }

                    if (_post["password"].Length == 0)
                    {
                        errors.Add("Введите пароль.");
                    }

                    if (errors.Count == 0)
                    {
                        Global.DB.lastError = null;

                        Global.DB.Update("authors", new Dictionary<string, string>()
                        {
                            { "username", _post["login"] },
                            { "password", _post["password"] }
                        }, new Dictionary<string, string>()
                        {
                            { "author_id", _get["edit"] }
                        });

                        if (Global.DB.lastError == null)
                        {
                            Response.Redirect("/Users.aspx");
                        }
                        else
                        {
                            errors.Add("Ошибка при изменении пользователя.");
                        }
                    }
                }
            }

            if (_get["del"] != null && Int32.TryParse(_get["del"], out value))
            {
                del = true;

                var record = Global.DB.Select("authors", new Dictionary<string, string>()
                {
                    { "author_id", _get["del"] }
                });

                if (record.Rows.Count == 0)
                {
                    del = false;
                } else if (_post["del"] != null)
                {
                    if (_post["password"].Length == 0)
                    {
                        errors.Add("Введите пароль.");
                    }
                    else if (_post["password"] != record.Rows[0]["password"].ToString())
                    {
                        errors.Add("Пароль неверен.");
                    }

                    if (errors.Count == 0)
                    {
                        Global.DB.lastError = null;

                        Global.DB.Delete("authors", new Dictionary<string, string>()
                        {
                            { "author_id", _get["del"] }
                        });

                        if (Global.DB.lastError == null)
                        {
                            Response.Redirect("/Users.aspx");
                        }
                        else
                        {
                            errors.Add("Ошибка при удалении пользователя.");
                        }
                    }
                }
            }

            var usersdb = Global.DB.Select("authors");

            if (usersdb.Rows.Count > 0)
            {
                foreach (DataRow userdb in usersdb.Rows)
                {
                    users.Add(new Dictionary<string, string>()
                    {
                        { "id", userdb["author_id"].ToString() },
                        { "name", userdb["username"].ToString() }
                    });
                }
            }
        }
    }
}