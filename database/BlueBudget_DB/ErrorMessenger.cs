using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBudget_DB
{
    class ErrorMessenger
    {

        public static string EmptyField(string mandatoryField)
        {
            return "ERROR:\n" + mandatoryField + " is mandatory";
        }

        public static string WrongFormat(string field)
        {
            return "ERROR:\n" + field + " text is incorrectly formatted";
        }

        public static string Exception(SqlException ex)
        {
            switch (ex.Number)
            {
                case 2601:
                    return "EXCEPTION ERROR:\nDuplicate already in database.";
                default:
                    return "EXCEPTION ERROR:\nAn error has occurred!\nError.number: " + ex.Number;
            }
        }

        public static string Warning(string message)
        {
            return "WARNING:\n" + message;
        }


    }
}
