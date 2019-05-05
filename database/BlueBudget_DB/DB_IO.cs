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


        public enum DB_Interface
        {
            pr_insert_user,
            pr_update_user,
            pr_delete_subscription,
            pr_delete_user,
            pr_select_users,
            pr_exists_user,
            //---
            pr_select_recurrences,
            pr_select_recurrence_id,
            //---
            pr_insert_money_account,
            pr_delete_money_account,
            pr_select_money_accounts,
            pr_select_user_money_accounts,
            pr_exists_money_account,
            pr_money_account_add_user,
            pr_money_account_remove_user,
            //---
            pr_select_categories,
            pr_insert_category,
            pr_insert_subcategory,
            pr_delete_category,
            pr_select_category_types,
            //---
            pr_select_loans,
            pr_exists_loan,
            pr_insert_loan,
            //---
            pr_select_budgets,
            pr_insert_budget,
            pr_select_budgets_by_category_id,
            //---
            pr_insert_goal,
            pr_select_goals,
            //---
            pr_select_wallets,
            //---
            pr_select_transactions,
            pr_insert_transaction,
            pr_select_transaction_types,
            pr_delete_transaction,
            //---
            pr_select_purchased_stocks,
            pr_select_stocks,
            pr_insert_purchased_stock,
            pr_delete_purchased_stocks

        }

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

        public static int Insert(System.Enum proc, IDictionary<System.Enum, Object> attrValue)
        {
            return ExecProcNonQuery(proc.ToString(), attrValue);
        }

        public static int Delete(System.Enum proc, IDictionary<System.Enum, Object> attrValue)
        {
            return ExecProcNonQuery(proc.ToString(), attrValue);
        }

        public static int Update(System.Enum proc, IDictionary<System.Enum, Object> attrValue)
        {
            return ExecProcNonQuery(proc.ToString(), attrValue);
        }

        public static DataTableReader SelectReader(System.Enum proc, IDictionary<System.Enum, Object> attrValue)
        {
            return ExecProcReader(proc.ToString(), attrValue);
        }

        public static Object SelectScalar(System.Enum proc, IDictionary<System.Enum, Object> attrValue)
        {
            return ExecProcScalar(proc.ToString(), attrValue);
        }

        public static bool Exists(System.Enum proc, IDictionary<System.Enum, Object> attrValue)
        {
            return ExecProcExists(proc.ToString(), attrValue);
        }


        // ----------------------------------------------------------------------------------------------
        // EXECUTION OF STORED PROCEDURES ---------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        private static int ExecProcNonQuery(String procName, IDictionary<System.Enum, Object> attrValue)
        {
            SqlConnection cnx = DBconnect();

            // step 1: create stored procedure command (cmd)
            SqlCommand cmd = new SqlCommand(procName, cnx)
            {
                CommandType = CommandType.StoredProcedure
            };

            // step 2: parameterize cmd
            CmdParameterizer(cmd, attrValue);

            Console.WriteLine("####### ####### " + cmd.CommandText);

            // step 3: execute
            return ExecuteNonQuery(cnx, cmd);
        }

        private static DataTableReader ExecProcReader(String procName, IDictionary<System.Enum, Object> attrValue)
        {
            SqlConnection cnx = DBconnect();

            // step 1: create stored procedure command (cmd)
            SqlCommand cmd = new SqlCommand(procName, cnx)
            {
                CommandType = CommandType.StoredProcedure
            };

            // step 2: parameterize cmd
            CmdParameterizer(cmd, attrValue);

            Console.WriteLine("####### ####### "+cmd.CommandText);

            // step 3: execute
            return ExecuteReader(cnx, cmd);
        }

        private static Object ExecProcScalar(String procName, IDictionary<System.Enum, Object> attrValue)
        {
            SqlConnection cnx = DBconnect();

            // step 1: create stored procedure command (cmd)
            SqlCommand cmd = new SqlCommand(procName, cnx)
            {
                CommandType = CommandType.StoredProcedure
            };

            // step 2: parameterize cmd
            CmdParameterizer(cmd, attrValue);

            Console.WriteLine("####### ####### " + cmd.CommandText);

            // step 3: execute
            return ExecuteScalar(cnx, cmd);
        }

        private static bool ExecProcExists(String procName, IDictionary<System.Enum, Object> attrValue)
        {
            SqlConnection cnx = DBconnect();

            // step 1: create stored procedure command (cmd)
            SqlCommand cmd = new SqlCommand(procName, cnx)
            {
                CommandType = CommandType.StoredProcedure
            };

            // step 2: parameterize cmd
            CmdParameterizer(cmd, attrValue);

            Console.WriteLine("####### ####### " + cmd.CommandText);

            // step 3: execute
            return Convert.ToBoolean(ExecuteScalar(cnx, cmd));
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
            catch (SqlException ex)
            {
                ErrorMessenger.Exception(ex);
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
            catch (SqlException ex)
            {
                ErrorMessenger.Exception(ex);
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
            catch (SqlException ex)
            {
                ErrorMessenger.Exception(ex);
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

        private static void CmdParameterizer(SqlCommand cmd, IDictionary<System.Enum, Object> attrValue)
        {
            foreach (KeyValuePair<System.Enum, Object> entry in attrValue)
            {
                cmd.Parameters.AddWithValue("@" + entry.Key, entry.Value);
            }
        }

        public static IDictionary<System.Enum, Object> CleanDict(IDictionary<System.Enum, Object> dict)
        {
            foreach (KeyValuePair<System.Enum, Object> entry in dict)
            {
                if (entry.Value.Equals("")){
                    dict.Remove(entry.Key);

                }
            }
            return dict;
        }

        public static IDictionary<System.Enum, Object> NullifyDict(IDictionary<System.Enum, Object> dict)
        {
            foreach (KeyValuePair<System.Enum, Object> entry in dict)
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

        public static IDictionary<System.Enum, Object> AttrValue()
        {
            return new Dictionary<System.Enum, Object>();
        }


    }
}