using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
	public class Subject : Binder
	{
		public Int16 ID { get; private set; }

		private string _title;
		public string Title
		{
			get { return _title; }
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException("Название указывать обязательно.");
				}

				if (value.Length > 50)
				{
					throw new ArgumentException("Длина названия не может быть больше 50 символов.");
				}

				_title = value;
			}
		}

		public Section _section;
		public Section Section
		{
			get { return _section; }
			set
			{
				if (value == null)
				{
					throw new ArgumentException("Зал указывать обязательно.");
				}

				_section = value;
			}
		}

		public List<string> Objects
		{
			get
			{
				return new List<string>() { "Title", "SectionID" };
			}
		}

		public List<string> Values
		{
			get
			{
				return new List<string>() { "'" + Title + "'", Section.ID.ToString() };
			}
		}

		public Subject() { }

		public Subject(string title)
		{
			Title = title;
		}

		public Subject(string title, Section section)
		{
			Title = title;
			Section = section;
		}

		public static Subject GetCopy(Subject s)
		{
			var newsubject = new Subject(s.Title, s.Section);
			newsubject.ID = s.ID;

			return newsubject;
		}

		public static List<Subject> Get(Int16? id = null)
		{
			string where = (id != null) ? "ID = " + id.ToString() : null;
			var result = new List<Subject>();

			SqlDataReader reader = Globals.db.Select("Subject", null, where);

			while (reader.Read())
			{
				var s = new Subject(Convert.ToString(reader["Title"]));
				s.Section = Globals.Sections.First(x => x.ID == Convert.ToByte(reader["SectionID"]));
				s.ID = Convert.ToInt16(reader["ID"]);
				result.Add(s);
			}

			reader.Close();

			return result;
		}

		public static Subject Get(Int16 id)
		{
			return Get((Int16?)id)[0];
		}

		public bool Insert()
		{
			var res = Globals.db.Insert("Subject", Objects, Values);

			if (res != -1 && res <= Int16.MaxValue)
			{
				ID = Convert.ToInt16(res);
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Update()
		{
			return Globals.db.Update("Subject", Objects, Values, new List<string>() { "ID = " + ID.ToString() });
		}

		public bool Delete()
		{
			// TODO: Delete all books of subject
			try
			{
				Globals.db.Delete("Subject", new List<string>() { "ID" }, new List<string>() { ID.ToString() });

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
