using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueBudget_DB
{
    public partial class LoansForm : Form
    {

        string user_email;
        int account_id;

        public LoansForm(string user_email, int account_id)
        {
            InitializeComponent();
            this.user_email = user_email;
            this.account_id = account_id;
            DefaultTextboxes();
            PopulateLoansListBox();
        }

        // -------------------------------------------------------------------
        // BUTTONS -----------------------------------------------------------
        // -------------------------------------------------------------------

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            // get values from text boxes if they were inserted by user
            string loan_name = Name_textBox.ForeColor == Color.Black ? Name_textBox.Text : "";
            double amount = 0.0;
            try
            {
                amount = Amount_textBox.ForeColor == Color.Black ? Double.Parse(Amount_textBox.Text) : 0.0;
            }
            catch
            {
                ErrorMessenger.WrongFormat("Loan amount");
                return;
            }
            double interest = 0.0;
            try
            {
                interest = Interest_textBox.ForeColor == Color.Black ? Double.Parse(Interest_textBox.Text) : 0.0;
            }
            catch
            {
                ErrorMessenger.WrongFormat("Interest");
                return;
            }
            DateTime term = DateTime.Parse(Enddate_dateTimePicker.Value.ToString());

            // verify if name and amount fields are filled
            if (loan_name.Equals("") || amount.Equals(""))
            {
                ErrorMessenger.EmptyField("Name and Amount");
                return;
            }

            // add loan
            try
            {
                DB_API.InsertLoan(account_id, loan_name, amount, term, interest);
                ErrorMessenger.SuccessfulOperation();
            }
            catch (SqlException ex)
            {
                ErrorMessenger.Exception(ex);
                return;
            }


            // upadte listBox with new user
            PopulateLoansListBox();
        }

        // -------------------------------------------------------------------
        // LIST BOXES --------------------------------------------------------
        // -------------------------------------------------------------------

        private void PopulateLoansListBox()
        {
            // get loans from DB
            var rdr = DB_API.SelectAccountLoans(account_id);

            // verify if account has no loans
            if (!rdr.HasRows)
            {
                //notifications_textbox.Text = "WARNING:\nUser has no accounts.";
                Loans_listBox.DataSource = new List<String>();
                return;
            }

            // extract loan names for listBox
            var res = new List<String>();
            while (rdr.Read())
            {
                string loan_name = rdr[DB_API.LoanEnt.name.ToString()].ToString();
                double loan_id = (double)(int)rdr[DB_API.LoanEnt.account_id.ToString()];
                res.Add(loan_name);
                //CURRENT_USER_ACCOUNTS[account_name] = (int)rdr[DB_API.MoneyAccountEnt.account_id.ToString()];
            }
            Loans_listBox.DataSource = res;
        }

        private void Loans_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item
            string loan_name = (string)Loans_listBox.SelectedItem;

            // search DB for loan
            var rdr = DB_API.SelectLoan(account_id, loan_name);
            var res = new List<Object>();
            while (rdr.Read())
            {
                res.Add(rdr[DB_API.LoanEnt.name.ToString()].ToString());
                res.Add(rdr[DB_API.LoanEnt.initial_amount.ToString()].ToString());
                res.Add(DateTime.Parse(rdr[DB_API.LoanEnt.term.ToString()].ToString()));
                res.Add(rdr[DB_API.LoanEnt.interest.ToString()].ToString());
            }

            UpdateTextBoxes(res);
            
        }

        // -------------------------------------------------------------------
        // TEXT BOXES  -------------------------------------------------------
        // -------------------------------------------------------------------

        private void UpdateTextBoxes(List<Object> list)
        {
            Name_textBox.Text = (string)list[0];
            Name_textBox.ForeColor = Color.Black;
            Amount_textBox.Text = (string)list[1];
            Amount_textBox.ForeColor = Color.Black;
            Enddate_dateTimePicker.Value = (DateTime)list[2];
            Interest_textBox.Text = (string)list[3] + "%";
            Interest_textBox.ForeColor = Color.Black;

        }

        // -------------------------------------------------------------------
        // TEXT BOXES HINTS --------------------------------------------------
        // -------------------------------------------------------------------

        private void DefaultTextboxes()
        {
            Name_textBox.Text = "loan name";
            Name_textBox.ForeColor = Color.Gray;
            Amount_textBox.Text = "amount";
            Amount_textBox.ForeColor = Color.Gray;
            Interest_textBox.Text = "interest";
            Interest_textBox.ForeColor = Color.Gray;
        }

        private void Name_textBox_Enter(object sender, EventArgs e)
        {
            Name_textBox.Text = "";
            Name_textBox.ForeColor = Color.Black;
        }
        private void Name_textBox_Leave(object sender, EventArgs e)
        {
            if (Name_textBox.Text.Equals(""))
            {
                Name_textBox.Text = "loan name";
                Name_textBox.ForeColor = Color.Gray;
            }
        }

        private void Amount_textBox_Enter(object sender, EventArgs e)
        {
            Amount_textBox.Text = "";
            Amount_textBox.ForeColor = Color.Black;
        }
        private void Amount_textBox_Leave(object sender, EventArgs e)
        {
            if (Name_textBox.Text.Equals(""))
            {
                Amount_textBox.Text = "amount";
                Amount_textBox.ForeColor = Color.Gray;
            }
        }

        private void Interest_textBox_Enter(object sender, EventArgs e)
        {
            Interest_textBox.Text = "";
            Interest_textBox.ForeColor = Color.Black;
        }
        private void Interest_textBox_Leave(object sender, EventArgs e)
        {
            if (Interest_textBox.Text.Equals(""))
            {
                Interest_textBox.Text = "interest";
                Interest_textBox.ForeColor = Color.Gray;
            }
        }

        
    }
}
