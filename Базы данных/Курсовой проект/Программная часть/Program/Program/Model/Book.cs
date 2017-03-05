using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Program
{
	public class Book : Binder
	{
		private string _title;
		private string _publisher;
		private string _language;
		private Int16 _year;
		private Int16 _pages;
		private Int64 _isbn;

		public int ID { get; private set; }
		public string Title
		{
			get { return _title; }
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException("Название указывать обязательно.");
				}

				if (value.Length > 200)
				{
					throw new ArgumentException("Длина названия не может быть больше 200 символов.");
				}

				_title = value;
			}
		}

		public string Publisher
		{
			get { return _publisher; }
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException("Название издательства указывать обязательно.");
				}

				if (value.Length > 50)
				{
					throw new ArgumentException("Длина названия издательства на может быть больше 50 символов.");
				}

				_publisher = value;
			}
		}

		public Int16 Year
		{
			get { return _year; }
			set
			{
				var year = DateTime.Now.Year;

				if (value < 0)
				{
					throw new ArgumentException("Год не может быть отрицательным!");
				}

				if (value == 0)
				{
					throw new ArgumentException("Дневник Марии что ли?");
				}

				if (value > year)
				{
					throw new ArgumentException("Книга из будущего?");
				}

				_year = value;
			}
		}

		public byte Amount { get; set; }
		public Int16 Pages
		{
			get { return _pages; }
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Количество страниц не может быть отрицательным или равным нулю!");
				}

				_pages = value;
			}
		}

		public Int64 ISBN
		{
			get { return _isbn; }
			set
			{
				if (value < 100000000 || value >= 10000000000000)
				{
					throw new ArgumentException("Длина ISBN должна быть от 9 до 13 цифр!");
				}

				_isbn = value;
			}
		}

		public string Language
		{
			get { return _language; }
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException("Код языка указывать обязательно.");
				}

				if ((value.Length == 5 && value[2] != '_') || (value.Length != 5 && value.Length != 2))
				{
					throw new ArgumentException("Формат кода языка: язык[_территория]. " +
												"<язык> и <территория> по 2 символа.");
				}

				if (value.Length == 2)
				{
					_language = value.ToLower();
				}

				if (value.Length == 5)
				{
					string[] parts = value.Split('_');
					_language = parts[0].ToLower() + '_' + parts[1].ToUpper();
				}
			}
		}

		public Subject _subject;
		public Subject Subject
		{
			get { return _subject; }
			set
			{
				if (value == null)
				{
					throw new ArgumentException("Жанр указывать обязательно.");
				}

				_subject = value;
			}
		}

		public List<Author> Authors { get; set; }

		private List<string> Objects
		{
			get
			{
				return new List<string>() { "Title", "Publisher", "Year", "Amount", "Pages", "ISBN", "Language",
											"SubjectID" };
			}
		}

		private List<string> Values
		{
			get
			{
				return new List<string>() { "'" + Title + "'", "'" + Publisher + "'", Year.ToString(),
											Amount.ToString(), Pages.ToString(), ISBN.ToString(),
											"'" + Language + "'", Globals.ToString(Subject.ID)};
			}
		}

		public Book() { }

		public Book(string title, string publisher, Int16 year, byte amount, Int16 pages, Int64 isbn,
					string language)
		{
			Title = title;
			Publisher = publisher;
			Year = year;
			Amount = amount;
			Pages = pages;
			ISBN = isbn;
			Language = language;
		}

		public static Book GetCopy(Book b)
		{
			Book newbook = new Book(b.Title, b.Publisher, b.Year, b.Amount, b.Pages, b.ISBN, b.Language);
			newbook.Authors = b.Authors;
			newbook.ID = b.ID;
			newbook.Subject = b.Subject;
			
			return newbook;
		}

		public void Copy(Book b)
		{
			Authors = b.Authors;
			ID = b.ID;
			Title = b.Title;
			Publisher = b.Publisher;
			Year = b.Year;
			Amount = b.Amount;
			Pages = b.Pages;
			ISBN = b.ISBN;
			Language = b.Language;
			Subject = b.Subject;
		}

		public static List<Book> Get(int? id = null)
		{
			string where = (id != null) ? "ID = " + id.ToString() : null;
			var result = new List<Book>();

			SqlDataReader reader = Globals.db.Select("Book", null, where);

			while (reader.Read())
			{
				var b = new Book(Convert.ToString(reader["Title"]),				Convert.ToString(reader["Publisher"]),
								 Convert.ToInt16(reader["Year"]),				Convert.ToByte(reader["Amount"]),
								 Convert.ToInt16(reader["Pages"]),				Convert.ToInt64(reader["ISBN"]),   
								 Convert.ToString(reader["Language"]));
				b.ID = Convert.ToInt32(reader["ID"]);
				b.Subject = Globals.Subjects.First(x => x.ID == Globals.ToNullableInt16(reader["SubjectID"]));
				result.Add(b);
			}

			reader.Close();

			for (int i = 0; i < result.Count; i++)
			{
				result[i].Authors = Author.Get(result[i]);
			}

			return result;
		}

		public static Book Get(int id)
		{
			 return Get((int?)id)[0];
		}

		public bool Insert()
		{
			try
			{
				int res;

				foreach (Author author in Authors)
				{
					Globals.db.Insert("AuthorBook", new List<string>() { "AuthorID", "BookID" },
									  new List<string>() { author.ID.ToString(), ID.ToString() }, false);
				}
				
				res = Globals.db.Insert("Book", Objects, Values);
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
			catch
			{
				return false;
			}
		}

		public bool Update()
		{
			try
			{
				Globals.db.Delete("AuthorBook", new List<string>() { "BookID" }, new List<string>() { ID.ToString() });
				
				for (int i = 0; i < Authors.Count; i++)
				{
					Globals.db.Insert("AuthorBook", new List<string>() { "AuthorID", "BookID" },
									  new List<string>() { Authors[i].ID.ToString(), ID.ToString() }, false);
				}

				Globals.db.Update("Book", Objects, Values, new List<string>() { "ID = " + ID.ToString() });
				
				return true;
			}
			catch
			{
				return false;
			}
			
		}

		public bool Delete()
		{
			// TODO: Delete all instances of book
			try
			{
				Globals.db.Delete("AuthorBook", new List<string>() { "BookID" }, new List<string>() { ID.ToString() });
				Globals.db.Delete("Book", new List<string>() { "ID" }, new List<string>() { ID.ToString() });

				return true;
			}
			catch
			{
				return false;
			}
		}

		public override string ToString()
		{
			return Title;
		}
	}
}
