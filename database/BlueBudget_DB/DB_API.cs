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
        private static String dbServer = "tcp:mednat.ieeta.pt\SQLSERVER,8101";
        private static String dbName = "p1g2";
        private static String dbUserName = "p1g2";
        private static String dbUserPass = "bananas@Bd";

        private static SqlConnection DBconnect()
        {

           SqlConnection cnx = new SqlConnection("Data Source = " + dbServer + " ;" + "Initial Catalog = " + dbName +
                                                       "; uid = " + dbUserName + ";" + "password = " + dbUserPass);

            try {
                cnx.Open();
                if (cnx.State == ConnectionState.Open) {
                }
            }
            catch (Exception ex) {
                Console.WriteLine("An error has ocurred connecting to the DB");
            }

            return cnx;
        }


        private static Boolean DBdisconnect(SqlConnection cnx)
        {
            if (cnx.State == ConnectionState.Open)
                cnx.Close();
            return true;
        }


        public static Boolean DBselect(List<String> collumns, String tableName, List<String> filters)
        {

            // create SQL statement

            String sql = "SELECT ";
            while (!collumns.Any())
            {
                sql += " " + collumns[0];
                collumns.RemoveAt(0);
                if (!collumns.Any())
                {
                    sql += ", ";
                }
            }

            sql += " FROM " + tableName;

            while (!filters.Any())
            {
                sql += " " + filters[0];
                filters.RemoveAt(0);
                if (!filters.Any())
                {
                    sql += ", ";
                }
            }

            sql += ";";

            // execute SQL

            SqlConnection cnx = DBconnect();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cnx);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //var myString = reader.GetString(0); //The 0 stands for "the 0'th column", so the first column of the result.
                    //Debug.WriteLine(Convert.ToString(myString));

                    var row = reader.ToString();
                    Debug.WriteLine(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has ocurred executing the command");
            }
            finally
            {
                Boolean success = DBdisconnect(cnx);
            }

            return true;
        }
    }
}
