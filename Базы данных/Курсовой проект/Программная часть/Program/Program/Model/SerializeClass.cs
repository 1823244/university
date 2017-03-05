using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.Serialization;
using System.Windows;

namespace Program
{
	class SerializeClass
	{
		[Serializable]
		[DataContract(IsReference = true)]
		private class DataBase
		{
			[DataMember]
			public List<Author> Authors { get; set; }
			[DataMember]
			public List<Book> Books { get; set; }
			[DataMember]
			public List<Instance> Instances { get; set; }
			[DataMember]
			public List<Reader> Readers { get; set; }
			[DataMember]
			public List<Section> Sections { get; set; }
			[DataMember]
			public List<Subject> Subjects { get; set; }

			public DataBase()
			{
				Authors = new List<Author>();
				Books = new List<Book>();
				Instances = new List<Instance>();
				Readers = new List<Reader>();
				Sections = new List<Section>();
				Subjects = new List<Subject>();
			}

			public DataBase Load()
			{
				Authors = Globals.Authors;
				Books = Globals.Books;
				Instances = Instance.Get();
				Sections = Globals.Sections;
				Subjects = Globals.Subjects;

				return this;
			}

			public void Insert()
			{
				Globals.Authors = Authors;
				Globals.Books = Books;
				foreach (var book in Books)
				{
					book.Insert();
				}

				foreach (var instance in Instances)
				{
					instance.Insert();
				}

				Globals.Sections = Sections;
				Globals.Subjects = Subjects;
				foreach (var subject in Subjects)
				{
					subject.Insert();
				}
			}
		}

		private static void Serialize(Object obj, String path)
		{
			using (XmlWriter writer = XmlWriter.Create(path))
			{
				try
				{
					DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
					serializer.WriteObject(writer, obj);
				}
				catch
				{
					throw new Exception("Ошибка сериализации!");
				}
			}
		}

		private static object Deserialize(string path, Type type)
		{
			using (XmlReader reader = XmlReader.Create(path))
			{
				try
				{
					DataContractSerializer serializer = new DataContractSerializer(type);
					return serializer.ReadObject(reader);
				}
				catch
				{
					throw new Exception("Ошибка в XML!");
				}
			}
		}

		public static void Export(String path)
		{
			DataBase db = new DataBase().Load();

			Serialize(db, path);
		}

		public static void Import(String path)
		{
			DataBase db = new DataBase();

			db = (DataBase)Deserialize(path, db.GetType());

			db.Insert();
		}
	}
}
