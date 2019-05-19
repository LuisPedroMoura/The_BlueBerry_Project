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

        public static void EmptyField(string mandatoryField)
        {
            string message = mandatoryField + " is mandatory";
            string caption = "ERROR";
            MessageBox.Show(message, caption);
        }

        public static void WrongFormat(string field)
        {
            string message = field + " text is incorrectly formatted";
            string caption = "ERROR";
            MessageBox.Show(message, caption);
        }

        public static void InvalidData(string field)
        {
            string message = field + " data is invalid";
            string caption = "ERROR";
            MessageBox.Show(message, caption);
        }

        public static void Exception(SqlException ex)
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
        }

        public static void Warning(string message)
        {
            string caption = "WARNING";
            MessageBox.Show(message, caption);
        }

        public static void Error(string message)
        {
            string caption = "ERROR";
            MessageBox.Show(message, caption);
        }

        public static void Success(string message)
        {
            string caption = "SUCCESS";
            MessageBox.Show(message, caption);
        }

        public static void SuccessfulOperation()
        {
            string message = "Operation was successful";
            string caption = "SUCCESS";
            MessageBox.Show(message, caption);
        }

        public static bool OKCancel(string message)
        {
            string caption = "WARNING";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult click = MessageBox.Show(message, caption, buttons);
            return click == DialogResult.OK ? true : false;
        }


    }
}
