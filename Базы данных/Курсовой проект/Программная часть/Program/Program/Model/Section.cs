using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
	public class Section : Binder
	{
		public byte ID { get; private set; }

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

		private string _location;
		public string Location
		{
			get { return _location; }
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException("Местоположение указывать обязательно.");
				}

				if (value.Length > 100)
				{
					throw new ArgumentException("Длина местоположения не может быть больше 100 символов.");
				}

				_location = value;
			}
		}

		public List<string> Objects
		{
			get
			{
				return new List<string>() { "Title", "Location" };
			}
		}

		public List<string> Values
		{
			get
			{
				return new List<string>() { "'" + Title + "'", "'" + Location + "'" };
			}
		}

		public Section() { }

		public Section(string title, string location)
		{
			Title = title;
			Location = location;
		}

		public static List<Section> Get(byte? id = null)
		{
			string where = (id != null) ? "ID = " + id.ToString() : null;
			var result = new List<Section>();

			SqlDataReader reader = Globals.db.Select("Section", null, where);

			while (reader.Read())
			{
				var s = new Section(Convert.ToString(reader["Title"]), Convert.ToString(reader["Location"]));

				s.ID = Convert.ToByte(reader["ID"]);
				result.Add(s);
			}

			reader.Close();

			return result;
		}

		public static Section Get(byte id)
		{
			return Get((byte?)id)[0];
		}

		public static List<Section> Get(Reader reader)
		{
			var result = new List<Section>();
			var preresult = new List<byte>();

			string where = "ReaderID = " + reader.ID.ToString();
			SqlDataReader sdreader = Globals.db.Select("SectionReader", new List<string>() { "SectionID" }, where);

			while (sdreader.Read())
			{
				preresult.Add(Convert.ToByte(sdreader["SectionID"]));
			}

			sdreader.Close();

			foreach (var item in preresult)
			{
				result.Add(Get(item));
			}

			return result;
		}

		public bool Insert()
		{
			var res = Globals.db.Insert("Section", Objects, Values);

			if (res != -1 && res <= Byte.MaxValue)
			{
				ID = Convert.ToByte(res);
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Update()
		{
			return Globals.db.Update("Section", Objects, Values, new List<string>() { "ID = " + ID.ToString() });
		}

		public bool Delete()
		{
			// TODO: Delete all books of section or anything else
			try
			{
				Globals.db.Delete("Section", new List<string>() { "ID" }, new List<string>() { ID.ToString() });

				return true;
			}
			catch
			{
				return false;
			}
		}

		public static Section GetCopy(Section s)
		{
			var newsection = new Section(s.Title, s.Location);
			newsection.ID = s.ID;

			return newsection;
		}

		public override string ToString()
		{
			return Title;
		}
	}
}
