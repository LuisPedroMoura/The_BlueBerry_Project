using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BlueBudget_DB
{
    class DB_API
    {

        //Instance Fields
        private static String dbServer = "tcp:mednat.ieeta.pt\\SQLSERVER,8101";
        private static String dbName = "p1g2";
        private static String dbUserName = "p1g2";
        private static String dbUserPass = "bananas@Bd";


        private static SqlConnection DBconnect()
        {

            SqlConnection cnx = new SqlConnection("Data Source = " + dbServer + " ;" + "Initial Catalog = " + dbName +
                                                        "; uid = " + dbUserName + ";" + "password = " + dbUserPass);

            try
            {
                cnx.Open();
                if (cnx.State == ConnectionState.Open)
                {
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has ocurred connecting to the DB: " + ex.ToString());
            }

            return cnx;
        }

        private static Boolean DBdisconnect(SqlConnection cnx)
        {
            if (cnx.State == ConnectionState.Open)
                cnx.Close();
            return true;
        }

        // ----------------------------------------------------------------------------------------------
        // VERY COMMON QUERIES ENCAPSULATED BY METHODS --------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static bool Exists(String tableName, String columnName, String attrName)
        {
            var where = new List<IDictionary<string, string>>();
            where.Add(new Dictionary<String, String> { { columnName, attrName } });
            var rdr = DB_API.DBselect(tableName, new string[] { "*" }, where);
            if (!rdr.HasRows)
            {
                return false;
            }
            return true;
        }

        // ----------------------------------------------------------------------------------------------
        // SQL QUERY GENERIC METHODS --------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static int DBinsert(String tableName, IDictionary<String, String> attrValue)
        {
            // step 1: create sql query
            String sql = "INSERT INTO "+tableName+" ";
            sql += AttrParser(attrValue, ",", "()");
            sql += " VALUES ";
            sql += ValuesParser(attrValue, ",", "()");

            Console.WriteLine("################################################");
            Console.WriteLine(sql);
            Console.WriteLine("################################################");

            // step 2: execute
            return ExecuteNonQuery(sql);
        }

        public static int DBinsertGetID(String tableName, IDictionary<String, String> attrValue)
        {
            // step 1: create sql query
            String sql = "INSERT INTO " + tableName + " ";
            sql += AttrParser(attrValue, ",", "()");
            sql += " OUTPUT INSERTED.id";
            sql += " VALUES ";
            sql += ValuesParser(attrValue, ",", "()");

            Console.WriteLine("################################################");
            Console.WriteLine(sql);
            Console.WriteLine("################################################");

            // step 2: execute
            return (int) ExecuteScalar(sql);
        }

        public static int DBinsert(String sql)
        {
            Console.WriteLine("################################################");
            Console.WriteLine(sql);
            Console.WriteLine("################################################");

            return ExecuteNonQuery(sql);
        }

        public static int DBupdate(String tableName, IDictionary<String, String> set,
            List<IDictionary<String, String>> where)
        {
            // step 1: create sql query
            String sql = "UPDATE " + tableName + " SET ";
            List<IDictionary<String, String>> set_list = new List<IDictionary<string, string>>();
            set_list.Add(set);
            sql += AttrValueParser(set_list, ",", "", "=", "  ");
            sql += " WHERE ";
            sql += AttrValueParser(where, "AND", "OR", "is", "()");

            Console.WriteLine("################################################");
            Console.WriteLine(sql);
            Console.WriteLine("################################################");

            // step 2: execute
            return ExecuteNonQuery(sql);
        }

        public static int DBupdate(String sql)
        {
            Console.WriteLine("################################################");
            Console.WriteLine(sql);
            Console.WriteLine("################################################");

            return ExecuteNonQuery(sql);
        }

        public static int DBdelete(String tableName, List<IDictionary<String, String>> where)
        {
            // step 1: create sql query
            String sql = "DELETE FROM " + tableName + " ";
            sql += " WHERE ";
            sql += AttrValueParser(where, "AND", "OR", "is", "()");

            Console.WriteLine("################################################");
            Console.WriteLine(sql);
            Console.WriteLine("################################################");

            // step 2: execute
            return ExecuteNonQuery(sql);
        }

        public static int DBdelete(String sql)
        {
            Console.WriteLine("################################################");
            Console.WriteLine(sql);
            Console.WriteLine("################################################");

            return ExecuteNonQuery(sql);
        }

        public static DataTableReader DBselect(String tableName, String[] collumns,
            List<IDictionary<String,String>> where)
        {

            // create SQL statement
            string sql = "SELECT";
            foreach (String str in collumns)
            {
                sql += " " + str + ",";
            }
            sql = sql.Substring(0, sql.Length - 1); // removes last comma
            sql += " FROM " + tableName;

            bool emptyWhere = true;
            foreach (Dictionary<String, String> dict in where)
            {
                if (dict.Count > 0)
                {
                    emptyWhere = false;
                    break;
                }
            }
            if (!emptyWhere)
            {
                sql += " WHERE ";
                sql += AttrValueParser(where, "AND", "OR", "is", "()");
            }
            Console.WriteLine("################################################");
            Console.WriteLine(sql);
            Console.WriteLine("################################################");

            // execute SQL and return
            return ExecuteReader(sql);
        }

        public static DataTableReader DBselect(String sql)
        {
            Console.WriteLine("################################################");
            Console.WriteLine(sql);
            Console.WriteLine("################################################");

            return ExecuteReader(sql);
        }

        // ----------------------------------------------------------------------------------------------
        // GETTERS FOR DATA TYPES THAT ARE BORING TO INSTANTIATE ----------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static List<IDictionary<String, String>> where()
        {
            return new List<IDictionary<String, String>> { new Dictionary<String, String>() };
        }

        public static IDictionary<String, String> set()
        {
            return new Dictionary<String, String>();
        }

        public static IDictionary<String, String> attrValue()
        {
            return set();
        }

        // ----------------------------------------------------------------------------------------------
        // QUERY EXECUTION METHODS ----------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        private static int ExecuteNonQuery(String query)
        {
            SqlConnection cnx = DBconnect();
            SqlCommand cmd = new SqlCommand(query, cnx);
            int rows = 0;
            try
            {
                rows = cmd.ExecuteNonQuery();
                Console.WriteLine("Query executed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing query: " + ex.ToString());
            }
            finally
            {
                DBdisconnect(cnx);
            }
            return rows;
        }

        private static DataTableReader ExecuteReader(String query)
        {
            SqlConnection cnx = DBconnect();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(query, cnx);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Close();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing query: " + ex.ToString());
            }
            finally
            {
                DBdisconnect(cnx);
            }
            return dt.CreateDataReader();
        }

        private static object ExecuteScalar(String query)
        {
            SqlConnection cnx = DBconnect();
            try
            {
                SqlCommand cmd = new SqlCommand(query, cnx);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing query: " + ex.ToString());
            }
            finally
            {
                DBdisconnect(cnx);
            }
            return null;
        }

        // ----------------------------------------------------------------------------------------------
        // AUXILAR PARSING METHODS ----------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        private static String AttrValueParser(List<IDictionary<String, String>> attrValue, String separator1,
            String separator2, String nullSeparator, String parenthesis)
        {
            if (attrValue.Count == 0)
            {
                return "";
            }

            string sql = "";
            bool hasLastSep = false;
            foreach (IDictionary<String, String> dict in attrValue)
            {
                if (dict.Count > 0)
                {
                    sql += parenthesis[0];

                    foreach (KeyValuePair<string, string> entry in dict)
                    {

                        if (entry.Value.Equals("null"))
                        {
                            sql += entry.Key + " " + nullSeparator + entry.Value;
                        }
                        else
                        {
                            sql += entry.Key + "=" + entry.Value;
                        }
                        sql += " " + separator1 + " ";
                    }
                    sql = sql.Substring(0, sql.Length - 1 - (separator1.Length + 1)); // removes last separator1
                    sql += parenthesis[1] + " " + separator2 + " ";
                    hasLastSep = true;
                }
                
            }
            if (hasLastSep) {
                sql = sql.Substring(0, sql.Length - 1 - (separator2.Length + 1)); // removes last separator2
            }

            return sql;
        }

        public static String AttrParser(IDictionary<String, String> attrValue, String separator1,
            String parenthesis)
        {

            string sql = "";
            sql += parenthesis[0];
            foreach (KeyValuePair<string, string> entry in attrValue)
            {
                sql += entry.Key + separator1 + " ";
            }
            sql = sql.Substring(0, sql.Length - (separator1.Length + 1)); // removes last separator1
            sql += parenthesis[1];

            return sql;
        }

        private static String ValuesParser(IDictionary<String, String> attrValue, String separator1,
            String parenthesis)
        {

            string sql = "";
            sql += parenthesis[0];

            foreach (KeyValuePair<string, string> entry in attrValue)
            {
                sql += entry.Value + separator1 + " ";
            }
            sql = sql.Substring(0, sql.Length - (separator1.Length + 1)); // removes last separator1
            sql += parenthesis[1];

            return sql;
        }

        public static IDictionary<String,String> CleanDict(IDictionary<String,String> dict)
        {
            foreach (KeyValuePair<String,String> entry in dict)
            {
                if (entry.Value.Equals("")){
                    dict.Remove(entry.Key);

                }
            }
            return dict;
        }

        public static IDictionary<String, String> nullifyDict(IDictionary<String, String> dict)
        {
            foreach (KeyValuePair<String, String> entry in dict)
            {
                if (entry.Value.Equals(""))
                {
                    dict[entry.Key] = "null";
                }
            }
            return dict;
        }

        public static String Str(String str)
        {
            return "'" + str + "'";
        }
    }
}