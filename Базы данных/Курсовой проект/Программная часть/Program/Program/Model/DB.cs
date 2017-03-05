using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration;
using System.Data;

namespace Program
{
	class DB
	{
		SqlConnection connector;
		SqlCommand commander;

		public DB()
		{
			string connString = ConfigurationManager
				.ConnectionStrings["Program.Properties.Settings.libraryConnectionString"]
				//.ConnectionStrings["Program.Properties.Settings.universityConnectionString"]
				.ConnectionString;
			connector = new SqlConnection(connString);

			commander = new SqlCommand();
			commander.Connection = connector;
			commander.CommandType = CommandType.Text;
		}

		public bool Open()
		{
			try
			{
				connector.Open();
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message + " " + connector.State.ToString());
				return false;
			}
		}

		public bool Close()
		{
			if (connector != null && connector.State == ConnectionState.Open)
			{
				connector.Close();
				return true;
			}
			return false;
		}

		public SqlDataReader Select(string table, List<string> objects = null, List<string> where = null)
		{
			string selection = objects == null ? "*" : String.Join(", ", objects);
			string conditions = (where == null || where.Count == 0 || (where.Count == 1 && where[0] == null))
								 ? ""
								 : " WHERE " + String.Join(" AND ", where);

			commander.CommandText = "SELECT " + selection + " FROM " + table + conditions;

			return commander.ExecuteReader();
		}

		public SqlDataReader Select(string table, List<string> objects = null, string where = null)
		{
			return Select(table, objects, new List<string>() { where });
		}

		public int Insert(string table, List<string> objects, List<string> values, bool output = true)
		{
			commander.CommandText = (output ? "DECLARE @T TABLE ( ID INT ) " : "")
									 + "INSERT INTO " + table + " ([" + String.Join("], [", objects) + "]) "
									 + (output ? "OUTPUT INSERTED.ID INTO @T " : "")
									 + "VALUES " + "(" + String.Join(", ", values) + ")"
									 + (output ? "SELECT * FROM @T " : "");
			try
			{
				if (output)
				{
					int? id = Globals.ToNullableInt32(commander.ExecuteScalar());
					return id == null ? -1 : Convert.ToInt32(id);
				}

				Globals.ToNullableInt32(commander.ExecuteNonQuery());
				return 0;
			}
			catch
			{
				return -1;
			}
		}

		public bool Update(string table, List<string> objects, List<string> values, List<string> where)
		{
			commander.CommandText = "UPDATE " + table + " SET ";
			var eqs = new List<string>();
			for (int i = 0; i < objects.Count; i++)
			{
				eqs.Add('[' + objects[i] + ']' + " = " + values[i]);
			}

			commander.CommandText += String.Join(", ", eqs) + " WHERE " + String.Join(" AND ", where);
			try
			{
				commander.ExecuteNonQuery();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Delete(string table, List<string> whereObjects = null, List<string> whereValues = null)
		{
			commander.CommandText = "DELETE FROM " + table;

			if (whereObjects != null && whereValues != null)
			{
				var eqs = new List<string>();
				for (int i = 0; i < whereObjects.Count; i++)
				{
					eqs.Add(whereObjects[i] + " = " + whereValues[i]);
				}

				commander.CommandText += " WHERE " + String.Join(" AND ", eqs);
			}

			try
			{
				commander.ExecuteNonQuery();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Execute(string title, List<string> objects = null, List<string> values = null)
		{
			var cmd = new SqlCommand(title, connector);
			cmd.CommandType = CommandType.StoredProcedure;

			if (objects != null && values != null)
			{
				for (int i = 0; i < objects.Count; i++)
				{
					cmd.Parameters.Add(new SqlParameter(objects[i], values[i]));
				}
			}

			try
			{
				commander.ExecuteNonQuery();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
