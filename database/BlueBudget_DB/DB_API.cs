using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBudget_DB
{
    class DB_API
    {

        public enum UserEnt
        {
            user_name,
            email,
            fname,
            mname,
            lname,
            card_number,
            periodicity,
            term,
            active
        };
        public enum RecurrenceEnt
        {
            periodicity,
            designation
        };
        public enum MoneyAccountEnt
        {
            user_email,
            account_id,
            id,
            account_name,
            balance,
            patrimony,
        };
        public enum WalletEnt
        {
            id,
            account_id,
            name,
            balance
        }

        // ----------------------------------------------------------------------------------------------
        // GENERIC METHODS ------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static bool Exists(System.Enum entity, System.Enum column, String value)
        {
            return DB_IO.Exists(entity, column, value);
        }


        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // API METHODS ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        // ----------------------------------------------------------------------------------------------
        // USERS ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static bool ExistsUser(string email)
        {
            return DB_IO.Exists(DB_IO.DB_Interface.pr_exists_user, DB_API.UserEnt.email, email);
        }

        public static void InsertUser(string username, string email, string fname, string mname, string lname,
            string cardNo, string periodicity, string term, string active)
        {
            var attrValue = new Dictionary<System.Enum, String>
            {
                { DB_API.UserEnt.user_name, username },
                { DB_API.UserEnt.email, email },
                { DB_API.UserEnt.fname, fname },
                { DB_API.UserEnt.mname, mname },
                { DB_API.UserEnt.lname, lname },
                { DB_API.UserEnt.card_number, cardNo },
                { DB_API.UserEnt.periodicity, periodicity },
                { DB_API.UserEnt.term, term },
                { DB_API.UserEnt.active, active }
            };
            DB_IO.Insert(DB_IO.DB_Interface.pr_insert_user, attrValue);
        }

        public static void UpdateUser(string username, string email, string fname = null, string mname = null,
            string lname = null, string cardNo = null, string periodicity = null, string term = null, string active = null)
        {
            var attrValue = new Dictionary<System.Enum, String>
            {
                { DB_API.UserEnt.user_name, username },
                { DB_API.UserEnt.email, email },
                { DB_API.UserEnt.fname, fname },
                { DB_API.UserEnt.mname, mname },
                { DB_API.UserEnt.lname, lname },
                { DB_API.UserEnt.card_number, cardNo },
                { DB_API.UserEnt.periodicity, periodicity },
                { DB_API.UserEnt.term, term },
                { DB_API.UserEnt.active, active }
            };
            DB_IO.Update(DB_IO.DB_Interface.pr_update_user, attrValue);
        }

        public static void DeleteUser(string email)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.UserEnt.email] = email;
            DB_IO.Delete(DB_IO.DB_Interface.pr_delete_user, attrValue);
        }

        public static DataTableReader SelectUserByEmail(string email)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.UserEnt.email] = email;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_user, attrValue);
        }

        public static DataTableReader SelectAllUsers()
        {
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_user, DB_API.AttrValue());
        }


        // ----------------------------------------------------------------------------------------------
        // RECURRENCE -----------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static DataTableReader SelectAllRecurrences()
        {
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_recurrences, DB_API.AttrValue());
        }

        public static String SelectRecurrenceById(string id)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.RecurrenceEnt.periodicity] = id;
            var rdr = DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_recurrences, DB_API.AttrValue());
            rdr.Read();
            return rdr[DB_API.RecurrenceEnt.designation.ToString()].ToString();
        }


        // ----------------------------------------------------------------------------------------------
        // MONEY ACCOUNTS -------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static bool ExistsMoneyAccount(string account_id)
        {
            return DB_IO.Exists(DB_IO.DB_Interface.pr_exists_money_account, DB_API.MoneyAccountEnt.account_id, account_id);
        }

        public static void InsertMoneyAccount(string user_email, string account_name, string balance = null, string patrimony = null)
        {
            var attrValue = new Dictionary<System.Enum, String>
            {
                { DB_API.MoneyAccountEnt.user_email, user_email },
                { DB_API.MoneyAccountEnt.account_name, account_name },
                { DB_API.MoneyAccountEnt.balance, balance },
                { DB_API.MoneyAccountEnt.patrimony, patrimony }
            };
            DB_IO.Insert(DB_IO.DB_Interface.pr_insert_user, attrValue);
        }

        public static void DeleteMoneyAccount(string account_id)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.MoneyAccountEnt.account_id] = account_id;
            DB_IO.Delete(DB_IO.DB_Interface.pr_delete_money_account, attrValue);
        }

        public static DataTableReader SelectMoneyAccountById(string account_id)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.MoneyAccountEnt.id] = account_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_money_accounts, attrValue);
        }

        public static DataTableReader SelectUserMoneyAccounts(string email)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.UserEnt.email] = email;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_user_money_accounts, attrValue);
        }

        public static DataTableReader SelectMoneyAccountUsers(string account_id)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.MoneyAccountEnt.id] = account_id;
            return DB_IO.SelectReader(DB_IO.DB_Interface.pr_select_user_money_accounts, attrValue);
        }

        public static void MoneyAccountAddUser(string account_id, string user_email)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.MoneyAccountEnt.account_id] = account_id;
            attrValue[DB_API.MoneyAccountEnt.user_email] = user_email;
            DB_IO.Insert(DB_IO.DB_Interface.pr_money_account_add_user, attrValue);
        }

        public static void MoneyAccountRemoveUser(string account_id, string user_email)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.MoneyAccountEnt.account_id] = account_id;
            attrValue[DB_API.MoneyAccountEnt.user_email] = user_email;
            DB_IO.Delete(DB_IO.DB_Interface.pr_money_account_remove_user, attrValue);
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
