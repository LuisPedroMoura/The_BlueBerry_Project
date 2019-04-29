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
            account_id,
            account_name,
            balance,
            patrimony,
            //user_email
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

        public static bool ExistsUser(System.Enum column, String value)
        {
            return DB_IO.Exists(DB_IO.Entity.user, column, value);
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
            DB_IO.Insert(DB_IO.Entity.user, attrValue);
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
            DB_IO.Update(DB_IO.Entity.user, attrValue);
        }

        public static void DeleteUser(string email)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.UserEnt.email] = email;
            DB_IO.Delete(DB_IO.Entity.user, attrValue);
        }

        public static DataTableReader SelectUser(string email)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.UserEnt.email] = email;
            return DB_IO.SelectReader(DB_IO.Entity.user, attrValue);
        }

        public static DataTableReader SelectAllUsers()
        {
            return DB_IO.SelectReader(DB_IO.Entity.user, DB_API.AttrValue());
        }


        // ----------------------------------------------------------------------------------------------
        // RECURRENCE -----------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static DataTableReader SelectAllRecurrences()
        {
            return DB_IO.SelectReader(DB_IO.Entity.recurrence, DB_API.AttrValue());
        }

        public static String SelectRecurrenceById(string id)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.RecurrenceEnt.periodicity] = id;
            var rdr = DB_IO.SelectReader(DB_IO.Entity.recurrence, DB_API.AttrValue());
            rdr.Read();
            return rdr[DB_API.RecurrenceEnt.designation.ToString()].ToString();
        }


        // ----------------------------------------------------------------------------------------------
        // RECURRENCE -----------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------------

        public static DataTableReader SelectUserMoneyAccounts(string email)
        {
            var attrValue = DB_API.AttrValue();
            attrValue[DB_API.UserEnt.email] = email;
            return DB_IO.SelectReader(DB_IO.Entity.money_account, attrValue);
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
