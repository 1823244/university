using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Program
{
	public class Reader : Binder
	{
		public int ID { get; private set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Birth { get; set; }

		private long _phone;
		public long Phone
		{
			get { return _phone; }
			set
			{
				_phone = value;
				FormatedPhone = String.Format("{0:+# ### ### ## ##}", value);
			}
		}

		private string _formatedphone;
		public string FormatedPhone
		{
			get { return _formatedphone; }
			set
			{
				var _p = Globals.ToNullableInt64((new Regex(@"[\D]").Replace(value, "")));
				
				if (_p != null) _phone = Convert.ToInt64(_p);
				_formatedphone = value;
			}
		}

		private long? _passport;
		public long? Passport 
		{
			get { return _passport; }
			set
			{
				_passport = value;
				FormatedPassport = String.Format("{0:## ## ######}", value);
			}
		}

		private string _formatedpassport;
		public string FormatedPassport
		{
			get { return _formatedpassport; }
			set
			{
				if (value == "" || value == null)
				{
					_passport = null;
				}
				else
				{
					_passport = Globals.ToNullableInt64((new Regex(@"[^\d]").Replace(value, "")));
				}

				_formatedpassport = value;
			}
		}

		public string Group { get; set; }

		public List<Section> Sections { get; set; }

		public List<string> Objects
		{
			get
			{
				return new List<string>() { "FirstName", "MiddleName", "LastName", "Birth",
											"Phone", "Passport", "Group" };
			}
		}

		public List<string> Values
		{
			get
			{
				return new List<string>() { "'" + FirstName + "'",
											MiddleName != null ?  "'" + MiddleName + "'" : "NULL",
											"'" + LastName + "'",  Birth != null ? "'" + Birth + "'" : "NULL",
											Phone.ToString(),
											Passport == null ? "NULL" : Passport.ToString(), "'" + Group + "'"};
			}
		}

		public Reader() { }

		public Reader(string firstname, string middlename, string lastname, string birth, long phone,
					  long? passport, string group)
		{
			FirstName = firstname;
			MiddleName = middlename;
			LastName = lastname;
			Birth = birth;
			Phone = phone;
			Passport = passport;
			Group = group;
		}

		public static Reader GetCopy(Reader r)
		{
			var newreader = new Reader(r.FirstName, r.MiddleName, r.LastName, r.Birth, r.Phone, r.Passport, r.Group);
			newreader.Sections = r.Sections;
			newreader.ID = r.ID;

			return newreader;
		}

		public static List<Reader> Get(int? id = null)
		{
			string where = (id != null) ? "ID = " + id.ToString() : null;
			var result = new List<Reader>();

			SqlDataReader reader = Globals.db.Select("Reader", null, where);

			while (reader.Read())
			{
				var r = new Reader(Convert.ToString(reader["FirstName"]),	Convert.ToString(reader["MiddleName"]),
								   Convert.ToString(reader["LastName"]),	Convert.ToString(reader["Birth"]).Split(' ')[0],
								   Convert.ToInt64(reader["Phone"]),		Globals.ToNullableInt64(reader["Passport"]),
								   Convert.ToString(reader["Group"]));
				r.ID = Convert.ToInt32(reader["ID"]);
				result.Add(r);
			}

			reader.Close();

			for (int i = 0; i < result.Count; i++)
			{
				result[i].Sections = Section.Get(result[i]);
			}

			return result;
		}

		public static Reader Get(int id)
		{
			return Get((int?)id)[0];
		}

		public bool Insert()
		{
			try
			{
				int res;

				foreach (Section section in Sections)
				{
					Globals.db.Insert("SectionReader", new List<string>() { "ReaderID", "SectionID" },
									  new List<string>() { ID.ToString(), section.ID.ToString() }, false);
				}

				res = Globals.db.Insert("Reader", Objects, Values);

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
				Globals.db.Delete("SectionReader", new List<string>() { "ReaderID" }, new List<string>() { ID.ToString() });

				for (int i = 0; i < Sections.Count; i++)
				{
					Globals.db.Insert("SectionReader", new List<string>() { "SectionID", "ReaderID" },
									  new List<string>() { Sections[i].ID.ToString(), ID.ToString() }, false);
				}
				Globals.db.Update("Reader", Objects, Values, new List<string>() { "ID = " + ID.ToString() });

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Delete()
		{
			// TODO: ???
			try
			{
				Globals.db.Delete("SectionReader", new List<string>() { "ReaderID" }, new List<string>() { ID.ToString() });
				Globals.db.Delete("Reader", new List<string>() { "ID" }, new List<string>() { ID.ToString() });

				return true;
			}
			catch
			{
				return false;
			}
		}

		public override string ToString()
		{
			return (LastName == null && FirstName == null) ? null : LastName + ' ' + FirstName +", " + Group;
		}
	}
}
