using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
	public class Author : Binder
	{
		public int ID { get; private set; }

		private string _firstname;
		public string FirstName
		{
			get { return _firstname; }
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException("Имя указывать обязательно.");
				}

				if (value.Length > 50)
				{
					throw new ArgumentException("Длина имени не может быть больше 50 символов.");
				}

				_firstname = value;
			}
		}

		private string _middlename;
		public string MiddleName
		{
			get { return _middlename; }
			set
			{
				if (value.Length > 100)
				{
					throw new ArgumentException("Длина отчества не может быть больше 100 символов.");
				}

				_middlename = value;
			}
		}

		private string _lastname;
		public string LastName
		{
			get { return _lastname; }
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException("Фамилию указывать обязательно.");
				}

				if (value.Length > 50)
				{
					throw new ArgumentException("Длина фамилии не может быть больше 50 символов.");
				}

				_lastname = value;
			}
		}

		public string Birth { get; set; }

		public List<string> Objects
		{
			get
			{
				return new List<string>() { "FirstName", "MiddleName", "LastName", "Birth" };
			}
		}

		public List<string> Values
		{
			get
			{
				return new List<string>() { "'" + FirstName + "'", 
											MiddleName != null ?  "'" + MiddleName + "'" : "NULL",
											"'" + LastName + "'",
											Birth != null ? "'" + Birth + "'" : "NULL" };
			}
		}

		public Author() { }

		public Author(string firstname, string middlename, string lastname, string birth)
		{
			FirstName = firstname;
			MiddleName = middlename;
			LastName = lastname;
			Birth = birth;
		}

		public static Author GetCopy(Author a)
		{
			var newauthor = new Author(a.FirstName, a.MiddleName, a.LastName, a.Birth);
			newauthor.ID = a.ID;

			return newauthor;
		}

		public static List<Author> Get(int? id = null)
		{
			string where = (id != null) ? "ID = " + id.ToString() : null;
			var result = new List<Author>();

			SqlDataReader reader = Globals.db.Select("Author", null, where);

			while (reader.Read())
			{
				var a = new Author(Convert.ToString(reader["FirstName"]),	Convert.ToString(reader["MiddleName"]),
									  Convert.ToString(reader["LastName"]),	Convert.ToString(reader["Birth"]).Split(' ')[0]);

				a.ID = Convert.ToInt32(reader["ID"]);
				result.Add(a);
			}

			reader.Close();

			return result;
		}

		public static Author Get(int id)
		{
			return Get((int?)id)[0];
		}

		public static List<Author> Get(Book book)
		{
			var result = new List<Author>();
			var preresult = new List<int>();

			string where = "BookID = " + book.ID.ToString();
			SqlDataReader reader = Globals.db.Select("AuthorBook", new List<string>() { "AuthorID" }, where);

			while (reader.Read())
			{
				preresult.Add(Convert.ToInt32(reader["AuthorID"]));
			}

			reader.Close();

			foreach (var item in preresult)
			{
				result.Add(Get(item));
			}

			return result;
		}
		
		public bool Insert()
		{
			var res = Globals.db.Insert("Author", Objects, Values);
			if (res != -1)
			{
				ID = res;
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Update()
		{
			return Globals.db.Update("Author", Objects, Values, new List<string>() { "ID = " + ID.ToString() });
		}

		public bool Delete()
		{
			// TODO: Delete all books of author and authorbooks
			try
			{
				Globals.db.Delete("Author", new List<string>() { "ID" }, new List<string>() { ID.ToString() });

				return true;
			}
			catch
			{
				return false;
			}
		}

		public override string ToString()
		{
			return FirstName + ' ' + LastName;
		}
	}
}
