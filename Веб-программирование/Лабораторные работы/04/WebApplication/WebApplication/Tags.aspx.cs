using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication
{
    public partial class Tags : System.Web.UI.Page
    {
        protected List<string> errors = new List<string>();
        protected Dictionary<string, string> edit = null;
        protected List<Dictionary<string, string>> tags = new List<Dictionary<string, string>>();
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
                    if (_post["tag"].Length == 0)
                    {
                        errors.Add("Введите тег.");
                    }

                    if (errors.Count == 0)
                    {
                        Global.DB.lastError = null;

                        var tagdb = Global.DB.Select("tags", new Dictionary<string, string>()
                        {
                            { "name", _post["tag"] }
                        });

                        if (tagdb.Rows.Count > 0)
                        {
                            errors.Add("Такой тег уже существует.");
                        }

                        if (errors.Count == 0)
                        {
                            Global.DB.Insert("tags", new Dictionary<string, string>()
                            {
                                { "name", _post["tag"] }
                            });

                            if (Global.DB.lastError == null)
                            {
                                Response.Redirect("/Tags.aspx");
                            }
                            else
                            {
                                errors.Add("Ошибка при добавлении тега.");
                            }
                        }
                    }
                }
            }

            if (_get["edit"] != null && Int32.TryParse(_get["edit"], out value))
            {
                var record = Global.DB.Select("tags", new Dictionary<string, string>()
                {
                    { "tag_id", _get["edit"] }
                });

                edit = new Dictionary<string, string>()
                {
                    { "tag", record.Rows[0]["name"].ToString() }
                };

                if (_post["save"] != null)
                {
                    if (_post["tag"].Length == 0)
                    {
                        errors.Add("Введите тег.");
                    }

                    if (errors.Count == 0)
                    {
                        Global.DB.lastError = null;

                        Global.DB.Update("tags", new Dictionary<string, string>()
                        {
                            { "name", _post["tag"] }
                        }, new Dictionary<string, string>()
                        {
                            { "tag_id", _get["edit"] }
                        });

                        if (Global.DB.lastError == null)
                        {
                            Response.Redirect("/Tags.aspx");
                        }
                        else
                        {
                            errors.Add("Ошибка при изменении тега.");
                        }
                    }
                }
            }

            if (_get["del"] != null && Int32.TryParse(_get["del"], out value))
            {
                var record = Global.DB.Select("tags", new Dictionary<string, string>()
                {
                    { "tag_id", _get["del"] }
                });

                if (record.Rows.Count > 0)
                {
                    Global.DB.lastError = null;

                    Global.DB.Delete("tags", new Dictionary<string, string>()
                    {
                        { "tag_id", _get["del"] }
                    });

                    Global.DB.Delete("post_tags", new Dictionary<string, string>()
                    {
                        { "tag_id", _get["del"] }
                    });

                    if (Global.DB.lastError == null)
                    {
                        Response.Redirect("/Tags.aspx");
                    }
                }
            }

            var tagsdb = Global.DB.Select("tags");

            if (tagsdb.Rows.Count > 0)
            {
                foreach (DataRow tagdb in tagsdb.Rows)
                {
                    tags.Add(new Dictionary<string, string>()
                    {
                        { "id", tagdb["tag_id"].ToString() },
                        { "name", tagdb["name"].ToString() }
                    });
                }
            }
        }
    }
}