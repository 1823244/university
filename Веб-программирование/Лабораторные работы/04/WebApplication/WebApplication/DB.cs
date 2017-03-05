using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Data;

namespace WebApplication
{
    public class DB
    {
        #region public fields
        public string lastError;
        public string lastQuery;
        public long lastInsertedID;
        public int affected;
        #endregion

        #region private fields
        private string hostname;
        private string username;
        private string password;
        private string database;
        private MySqlConnection databaseLink = null;
        #endregion

        public DB(string database, string username, string password, string hostname = "localhost")
        {
            this.database = database;
            this.username = username;
            this.password = password;
            this.hostname = hostname;

            Connect();
        }

        ~DB()
        {
            CloseConnection();
        }

        private bool Connect()
        {
            CloseConnection();
            string conString =  "SERVER=" + this.hostname + ";" +
                                "DATABASE=" + this.database + ";" +
                                "UID=" + this.username + ";" +
                                "PASSWORD=" + this.password + ";";
            conString = SetCharset(conString);

            this.databaseLink = new MySqlConnection(conString);
            this.databaseLink.Open();

            if (this.databaseLink == null)
            {
                this.lastError = "Can't connect to server";

                return false;
            }

		    return true;
        }

        private void CloseConnection()
        {
            if (this.databaseLink != null)
            {
                Commit();

                this.databaseLink.Close();
            }
        }

        private bool Commit()
        {
            return (new MySqlCommand("COMMIT", this.databaseLink)).ExecuteNonQuery() > 0;
        }

        private bool Rollback()
        {
            return (new MySqlCommand("ROLLBACK", this.databaseLink)).ExecuteNonQuery() > 0;
        }

        private string SetCharset(string conString, string charset = "utf8")
        {
            return conString + "CharSet=" + charset + ";";
        }

        private string MySQLRealEscapeString(string str)
        {
            return System.Text.RegularExpressions.Regex.Replace(str, @"[\000\010\011\012\015\032\042\047\134\140]", "\\$0");
        }

        private string SecureData(string data)
        {
            return MySQLRealEscapeString(data);
        }

        private Dictionary<string, string> SecureData(Dictionary<string, string> data)
        {
            return data.ToDictionary(x => x.Key, x => MySQLRealEscapeString(x.Value));
        }

        private bool ExecuteNonQuery(string query)
        {
            this.lastQuery = query;
            this.affected = 0;

            try
            {
                this.affected = (new MySqlCommand(query, this.databaseLink)).ExecuteNonQuery();
            }
            catch (Exception e)
            {
                this.lastError = e.Message;
            }

            return this.affected > 0;
        }

        private bool ExecuteScalar(string query)
        {
            this.lastQuery = query;

            long id = 0;

            try {
                var cmd = (new MySqlCommand(query, this.databaseLink));
                this.affected = cmd.ExecuteNonQuery();

                id = cmd.LastInsertedId;
            } catch (MySqlException e)
            {
                this.lastError = e.Message;
            }

            this.lastInsertedID = id;

            return id > 0;
        }

        private DataTable ExecuteQuery(string query)
        {
            this.lastQuery = query;
            try
            {
                var reader = (new MySqlCommand(query, this.databaseLink)).ExecuteReader();

                var dt = new DataTable();
                dt.Load(reader);
                reader.Close();

                return dt;
            }
            catch (Exception e)
            {
                this.lastError = e.Message;

                return new DataTable();
            }
        }

        public bool Insert(string table, Dictionary<string, string> vars)
        {
            vars = SecureData(vars);

            var query = String.Format("INSERT INTO `{0}` SET ", table);
            foreach (var item in vars)
            {
                query += String.Format("`{0}` = '{1}', ", item.Key, item.Value);
            }

            return ExecuteScalar(query.Substring(0, query.Length - 2));
        }

        public bool Delete(string table, Dictionary<string, string> where = null, string limit = null)
        {
            var query = String.Format("DELETE FROM `{0}` WHERE", table);

            if (where != null)
            {
                where = SecureData(where);

                foreach (var item in where)
                {
                    query += String.Format("`{0}` = '{1}' AND ", item.Key, item.Value);
                }

                query = query.Substring(0, query.Length - 5);
            }

            if (limit != null)
            {
                query += " LIMIT " + limit;
            }

            return ExecuteNonQuery(query);
        }

        public DataTable Select(string from, Dictionary<string, string> where = null,
                                                 string orderBy = null, string limit = null, string cols = null,
                                                 string operand = "AND")
        {
            if (from.Trim().Length == 0)
            {
                return null;
            }

            var numCols = (cols == null) ? "*" : cols;
            var query = String.Format("SELECT {0} FROM `{1}` WHERE ", numCols, from);

            if (where != null)
            {
                where = SecureData(where);

                foreach (var item in where)
                {
                    query += String.Format("`{0}` = '{1}' {2} ", item.Key, item.Value, operand);
                }

                query = query.Substring(0, query.Length - (operand.Length + 2));
            }
            else
            {
                query = query.Substring(0, query.Length - 7);
            }

            if (orderBy != null)
            {
                query += String.Format(" ORDER BY {0}", orderBy);
            }

            if (limit != null)
            {
                query += String.Format(" LIMIT {0}", limit);
            }

            return ExecuteQuery(query);
        }

        public bool Update(string table, Dictionary<string, string> set, Dictionary<string, string> where)
        {
            if (table.Length == 0 || set == null || where == null)
            {
                return false;
            }

            set = SecureData(set);
            where = SecureData(where);

            var query = String.Format("UPDATE `{0}` SET ", table);

            foreach (var item in set)
            {
                query += String.Format("`{0}` = '{1}', ", item.Key, item.Value);
            }

            query = String.Format("{0} WHERE ", query.Substring(0, query.Length - 2));

            foreach (var item in where)
            {
                query += String.Format("`{0}` = '{1}' AND ", item.Key, item.Value);
            }

            query = query.Substring(0, query.Length - 5);

            return ExecuteNonQuery(query);
        }

        public DataTable CountRows(string from, Dictionary<string, string> where = null)
        {
            if (from.Trim().Length == 0)
            {
                return null;
            }

            var result = Select(from, where, null, null, "count(*)");

            return result;
        }
    }
}