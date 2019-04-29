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
    class DB_IO
    {

        //Instance Fields
        private static readonly String dbServer = "tcp:mednat.ieeta.pt\\SQLSERVER,8101";
        private static readonly String dbName = "p1g2";
        private static readonly String dbUserName = "p1g2";
        private static readonly String dbUserPass = "bananas@Bd";

        public enum ProcType
        {
            Reader,
            NonQuery,
            Scalar
        };

        public enum Entity
        {
            user,
            subscription,
            recurrence,
            money_account,
            purchased_stock,
            stock,
            stock_type,
            loan,
            wallet,
            categorie,
            budget,
            goal,
            transaction,
            transfer,
            transaction_type
        };


        // ----------------------------------------------------------------------------------------------
        // SQL SERVER AND DATABASE CONNECTION -----------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

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
        // SQL QUERY GENERIC METHODS --------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static int Insert(System.Enum entity, IDictionary<System.Enum, String> attrValue)
        {
            return ExecProcNonQuery("pr_insert_"+entity, attrValue);
        }

        public static int Delete(System.Enum entity, IDictionary<System.Enum, String> attrValue)
        {
            return ExecProcNonQuery("pr_delete_" + entity, attrValue);
        }

        public static int Update(System.Enum entity, IDictionary<System.Enum, String> attrValue)
        {
            return ExecProcNonQuery("pr_update_" + entity, attrValue);
        }

        public static DataTableReader SelectReader(System.Enum entity, IDictionary<System.Enum, String> attrValue)
        {
            return ExecProcReader("pr_select_" + entity, attrValue);
        }

        public static Object SelectScalar(System.Enum entity, IDictionary<System.Enum, String> attrValue)
        {
            return ExecProcScalar("pr_select_" + entity, attrValue);
        }

        public static bool Exists(System.Enum entity, System.Enum column, String value)
        {
            var attrValue = AttrValue();
            attrValue[column] = value;
            int res = ExecProcExists("pr_exists_"+entity, attrValue);
            if (res == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // ----------------------------------------------------------------------------------------------
        // EXECUTION OF STORED PROCEDURES ---------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        private static int ExecProcNonQuery(String procName, IDictionary<System.Enum, String> attrValue)
        {
            SqlConnection cnx = DBconnect();

            // step 1: create stored procedure command (cmd)
            SqlCommand cmd = new SqlCommand(procName, cnx)
            {
                CommandType = CommandType.StoredProcedure
            };

            // step 2: parameterize cmd
            CmdParameterizer(cmd, attrValue);

            Console.WriteLine("################################################");
            Console.WriteLine(cmd.CommandText);
            Console.WriteLine("################################################");

            // step 3: execute
            return ExecuteNonQuery(cnx, cmd);
        }

        private static DataTableReader ExecProcReader(String procName, IDictionary<System.Enum, String> attrValue)
        {
            SqlConnection cnx = DBconnect();

            // step 1: create stored procedure command (cmd)
            SqlCommand cmd = new SqlCommand(procName, cnx)
            {
                CommandType = CommandType.StoredProcedure
            };

            // step 2: parameterize cmd
            CmdParameterizer(cmd, attrValue);

            Console.WriteLine("################################################");
            Console.WriteLine(cmd.CommandText);
            Console.WriteLine("################################################");

            // step 3: execute
            return ExecuteReader(cnx, cmd);
        }

        private static Object ExecProcScalar(String procName, IDictionary<System.Enum, String> attrValue)
        {
            SqlConnection cnx = DBconnect();

            // step 1: create stored procedure command (cmd)
            SqlCommand cmd = new SqlCommand(procName, cnx)
            {
                CommandType = CommandType.StoredProcedure
            };

            // step 2: parameterize cmd
            CmdParameterizer(cmd, attrValue);

            Console.WriteLine("################################################");
            Console.WriteLine(cmd.CommandText);
            Console.WriteLine("################################################");

            // step 3: execute
            return ExecuteScalar(cnx, cmd);
        }

        private static int ExecProcExists(String procName, IDictionary<System.Enum, String> attrValue)
        {
            SqlConnection cnx = DBconnect();

            // step 1: create stored procedure command (cmd)
            SqlCommand cmd = new SqlCommand(procName, cnx)
            {
                CommandType = CommandType.StoredProcedure
            };

            // step 2: parameterize cmd
            CmdParameterizer(cmd, attrValue);

            Console.WriteLine("################################################");
            Console.WriteLine(cmd.CommandText);
            Console.WriteLine("################################################");

            // step 3: execute
            return (int)ExecuteScalar(cnx, cmd);
        }


        // ----------------------------------------------------------------------------------------------
        // QUERY EXECUTION METHODS ----------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        private static int ExecuteNonQuery(SqlConnection cnx, SqlCommand cmd)
        {
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

        private static DataTableReader ExecuteReader(SqlConnection cnx, SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Close();
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
            return dt.CreateDataReader();
        }

        private static object ExecuteScalar(SqlConnection cnx, SqlCommand cmd)
        {
            object res = null;
            try
            {
                res = cmd.ExecuteScalar();
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
            return res;
        }


        // ----------------------------------------------------------------------------------------------
        // AUXILAR PARSING METHODS ----------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        private static void CmdParameterizer(SqlCommand cmd, IDictionary<System.Enum, String> attrValue)
        {
            foreach (KeyValuePair<System.Enum, String> entry in attrValue)
            {
                cmd.Parameters.AddWithValue("@" + entry.Key, entry.Value);
            }
        }

        public static IDictionary<System.Enum, String> CleanDict(IDictionary<System.Enum, String> dict)
        {
            foreach (KeyValuePair<System.Enum, String> entry in dict)
            {
                if (entry.Value.Equals("")){
                    dict.Remove(entry.Key);

                }
            }
            return dict;
        }

        public static IDictionary<System.Enum, String> NullifyDict(IDictionary<System.Enum, String> dict)
        {
            foreach (KeyValuePair<System.Enum, String> entry in dict)
            {
                if (entry.Value.Equals(""))
                {
                    dict[entry.Key] = "null";
                }
            }
            return dict;
        }


        // ----------------------------------------------------------------------------------------------
        // GETTERS FOR DATA TYPES THAT ARE BORING TO INSTANTIATE ----------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static IDictionary<System.Enum, String> AttrValue()
        {
            return new Dictionary<System.Enum, String>();
        }


    }
}