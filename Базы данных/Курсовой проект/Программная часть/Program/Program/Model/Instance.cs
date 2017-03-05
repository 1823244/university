using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
	public class Instance : Binder
	{
		public int ID { get; private set; }
		public Reader Reader { get; set; }
		public Book Book { get; set; }
		public string Issue { get; set; }
		public bool Available { get; set; }

		public List<string> Objects
		{
			get
			{
				return new List<string>() { "ReaderID", "BookID", "Issue", "Available" };
			}
		}

		public List<string> Values
		{
			get
			{
				return new List<string>() { Reader != null ? Reader.ID.ToString() : "NULL", Book.ID.ToString(),
											Issue != null && Issue.Length != 0 ? "'" + Issue + "'" : "NULL",
											Convert.ToByte(Available).ToString() };
			}
		}

		public Instance() { }

		public Instance(Reader reader, Book book, string issue, bool available)
		{
			Reader = reader;
			Book = book;
			Issue = issue;
			Available = available;
		}

		public static Instance GetCopy(Instance i)
		{
			var newinstance = new Instance(i.Reader, i.Book, i.Issue, i.Available);
			newinstance.ID = i.ID;

			return newinstance;
		}

		public static List<Instance> Get(int? id = null)
		{
			string where = (id != null) ? "ID = " + id.ToString() : null;
			var result = new List<Instance>();

			SqlDataReader reader = Globals.db.Select("Instance", null, where);

			while (reader.Read())
			{
				var i = new Instance(null, null, Convert.ToString(reader["Issue"]),
									 Convert.ToBoolean(reader["Available"]));
				i.ID = Convert.ToInt32(reader["ID"]);

				int? readerid = Globals.ToNullableInt32(reader["ReaderID"]);
				if (readerid != null) i.Reader = Globals.Readers.First(x => x.ID == readerid);
				int? bookid = Globals.ToNullableInt32(reader["BookID"]);
				if (bookid != null) i.Book = Globals.Books.First(x => x.ID == bookid);

				result.Add(i);
			}

			reader.Close();

			return result;
		}

		public static Instance Get(int id)
		{
			return Get((int?)id)[0];
		}

		public static List<Instance> Get(Book book)
		{
			var result = new List<Instance>();
			var preresult = new List<int>();

			string where = "BookID = " + book.ID.ToString();
			SqlDataReader reader = Globals.db.Select("Instance", null, where);

			while (reader.Read())
			{
				var i = new Instance(null, Globals.Books.First(x => x.ID == Convert.ToInt32(reader["BookID"])),
									 Convert.ToString(reader["Issue"]).Split(' ')[0],
									 Convert.ToBoolean(reader["Available"]));
				i.ID = Convert.ToInt32(reader["ID"]);
				int? readerid = Globals.ToNullableInt32(reader["ReaderID"]);
				if (readerid != null) i.Reader = Globals.Readers.First(x => x.ID == readerid);
				result.Add(i);
			}

			reader.Close();

			return result;
		}

		public static List<Instance> Get(Reader reader)
		{
			var result = new List<Instance>();
			var preresult = new List<int>();

			string where = "ReaderID = " + reader.ID.ToString();
			SqlDataReader sdreader = Globals.db.Select("Instance", null, where);

			while (sdreader.Read())
			{
				var i = new Instance(reader, Globals.Books.First(x => x.ID == Convert.ToInt32(sdreader["BookID"])),
									 Convert.ToString(sdreader["Issue"]).Split(' ')[0],
									 Convert.ToBoolean(sdreader["Available"]));
				i.ID = Convert.ToInt32(sdreader["ID"]);
				int? readerid = Globals.ToNullableInt32(sdreader["ReaderID"]);
				result.Add(i);
			}

			sdreader.Close();

			return result;
		}

		public bool Insert()
		{
			var res = Globals.db.Insert("Instance", Objects, Values);

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
			return Globals.db.Update("Instance", Objects, Values, new List<string>() { "ID = " + ID.ToString() });
		}

		public bool Delete()
		{
			// TODO: Delete all books of section or anything else
			try
			{
				Globals.db.Delete("Instance", new List<string>() { "ID" }, new List<string>() { ID.ToString() });

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Give()
		{
			try
			{
				Globals.db.Execute("GiveBook", new List<string>() { "ID", "Reader" }, new List<string>() { ID.ToString(), Reader.ID.ToString() });
				Available = false;
				DateTime date = DateTime.Today;
				Issue = date.Day.ToString().PadLeft(2, '0') + "." + date.Month.ToString().PadLeft(2, '0')
						+ "." + date.Year;
				Book.Amount--;

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Return()
		{
			try
			{
				Globals.db.Execute("ReturnBook", new List<string>() { "ID" }, new List<string>() { ID.ToString() });
				Available = true;
				Reader = null;
				Issue = null;
				Book.Amount++;

				return true;
			}
			catch
			{
				return false;
			}
		}

		public override string ToString()
		{
			return ID.ToString();
		}
	}
}
