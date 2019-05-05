using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public static string InvalidData(string field)
        {
            return "ERROR:\n" + field + " data is invalid";
        }

        public static string Exception(SqlException ex)
        {
            string message;
            string caption = "EXCEPTION ERROR";

            switch (ex.Number)
            {
                case 2601:
                    message = "Duplicate already in database.";
                    break;
                default:
                    message = "An error has occurred!\nError.number: " + ex.Number;
                    break;
            }
            MessageBox.Show(message, caption);
            return "";

        }

        public static string Warning(string message)
        {
            return "WARNING:\n" + message;
        }

        public static string Error(string message)
        {
            return "ERROR:\n" + message;
        }

        public static string Success(string message)
        {
            return "SUCCESS:\n" + message;
        }

        public static string SuccessfulOperation()
        {
            return "SUCCESS:\nOperation was successful";
        }


    }
}
