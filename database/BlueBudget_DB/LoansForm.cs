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
        }

        private void LoansForm_Load(object sender, EventArgs e)
        {
            DefaultTextboxes();
            PopulateLoansListView();
        }

        // -------------------------------------------------------------------
        // BUTTONS -----------------------------------------------------------
        // -------------------------------------------------------------------

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {   //apaga este comentario
            string loan_name = Name_textBox.ForeColor == Color.Black ? Name_textBox.Text : "";
            string initial_amount = InitialAmount_textBox.ForeColor == Color.Black ? InitialAmount_textBox.Text.Substring(1) : "";
            string current_debt = CurrentDebt_textBox.ForeColor == Color.Black ? CurrentDebt_textBox.Text.Substring(1) : "";
            string interest_str = Interest_textBox.ForeColor == Color.Black ? Interest_textBox.Text : "";
            DateTime term = DateTime.Parse(Enddate_dateTimePicker.Value.ToString());

            // verify if mandatory fields are filled
            if (loan_name.Equals("") || initial_amount.Equals("") || current_debt.Equals(""))
            {
                ErrorMessenger.EmptyField("Name, Initial Amount and Current Debt");
                return;
            }

            // process inserted values
            double init_amt = 0.0;
            double cur_debt = 0.0;
            double interest = 0.0;
            try
            {
                init_amt = Double.Parse(initial_amount);
                cur_debt = Double.Parse(current_debt);
                if (interest_str.Equals(""))
                {
                    interest = 0.0;
                }
                else
                {
                    interest = Double.Parse(interest_str);
                }
            }
            catch
            {
                ErrorMessenger.WrongFormat("A numeric textBox");
                return;
            }

            // add loan
            try
            {
                DB_API.InsertLoan(account_id, loan_name, init_amt, cur_debt, term, interest);
            }
            catch (SqlException ex)
            {
                ErrorMessenger.Exception(ex);
                return;
            }

            // upadte listBox with new user
            PopulateLoansListView();
        }

        private void Pay_btn_Click(object sender, EventArgs e)
        {
            // verify that
            
            // get loan name and payment value
            string name = Name_textBox.ForeColor == Color.Black ? Name_textBox.Text : "";
            double payment = Pay_textBox.ForeColor == Color.Black ? DB_API.UnMoneyfy(Pay_textBox.Text) : 0;
            Console.WriteLine(payment);
            // verify that a name is given
            if (name.Equals(""))
            {
                ErrorMessenger.EmptyField("Name");
                return;
            }

            // make payment
            DB_API.LoanPayment(this.account_id, name, payment);

            // update Loans listView
            PopulateLoansListView();
        }

        // -------------------------------------------------------------------
        // LIST BOXES --------------------------------------------------------
        // -------------------------------------------------------------------

        private void PopulateLoansListView()
        {
            // clear listView items
            Loans_listView.Items.Clear();
            
            // get loans from DB
            var rdr = DB_API.SelectAccountLoans(account_id);

            // populate listView
            while (rdr.Read())
            {
                string name = rdr[DB_API.LoanEnt.name.ToString()].ToString();
                //double account_id = (double)(int)rdr[DB_API.LoanEnt.account_id.ToString()];
                DateTime term = (DateTime)rdr[DB_API.LoanEnt.term.ToString()];
                double initial_amount = Double.Parse(rdr[DB_API.LoanEnt.initial_amount.ToString()].ToString());
                double current_debt = Double.Parse(rdr[DB_API.LoanEnt.current_debt.ToString()].ToString());
                double interest = Double.Parse(rdr[DB_API.LoanEnt.interest.ToString()].ToString());

                var row = new string[] { name, term.ToString("dd/MM/yyyy"), DB_API.Moneyfy(current_debt) };
                var item = new ListViewItem(row);
                item.Tag = new Loan(name, null, initial_amount, current_debt, term, interest);
                Loans_listView.Items.Add(item);
            }
        }

        private void Loans_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value
            Loan loan = Loans_listView.SelectedItems.Count > 0 ? (Loan)Loans_listView.SelectedItems[0].Tag : null;
            if (loan == null) { return; } // to handle listView idiot 2 step item selection process

            // search DB for loan and update textBoxes
            var rdr = DB_API.SelectLoan(account_id, loan.Name);
            while (rdr.Read())
            {
                Name_textBox.Text = rdr[DB_API.LoanEnt.name.ToString()].ToString();
                Name_textBox.ForeColor = Color.Black;
                InitialAmount_textBox.Text = DB_API.Moneyfy(rdr[DB_API.LoanEnt.initial_amount.ToString()].ToString());
                InitialAmount_textBox.ForeColor = Color.Black;
                CurrentDebt_textBox.Text = DB_API.Moneyfy(rdr[DB_API.LoanEnt.current_debt.ToString()].ToString());
                CurrentDebt_textBox.ForeColor = Color.Black;
                Enddate_dateTimePicker.Value = DateTime.Parse(rdr[DB_API.LoanEnt.term.ToString()].ToString());
                Interest_textBox.Text = Double.Parse(rdr[DB_API.LoanEnt.interest.ToString()].ToString()).ToString() + "%";
                Interest_textBox.ForeColor = Color.Black;
            }         
        }

        // -------------------------------------------------------------------
        // TEXT BOXES HINTS --------------------------------------------------
        // -------------------------------------------------------------------

        private void DefaultTextboxes()
        {
            Name_textBox.Text = "loan name";
            Name_textBox.ForeColor = Color.Gray;
            InitialAmount_textBox.Text = "initial amount";
            InitialAmount_textBox.ForeColor = Color.Gray;
            CurrentDebt_textBox.Text = "current debt";
            CurrentDebt_textBox.ForeColor = Color.Gray;
            Interest_textBox.Text = "interest";
            Interest_textBox.ForeColor = Color.Gray;
            Pay_textBox.Text = "payment";
            Pay_textBox.ForeColor = Color.Gray;
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
            InitialAmount_textBox.Text = "";
            InitialAmount_textBox.ForeColor = Color.Black;
        }
        private void Amount_textBox_Leave(object sender, EventArgs e)
        {
            InitialAmount_textBox.Text = DB_API.Moneyfy(InitialAmount_textBox.Text);

            if (InitialAmount_textBox.Text.Equals(""))
            {
                InitialAmount_textBox.Text = "initial amount";
                InitialAmount_textBox.ForeColor = Color.Gray;
            }
        }

        private void CurrentDebt_textBox_Enter(object sender, EventArgs e)
        {
            CurrentDebt_textBox.Text = "";
            CurrentDebt_textBox.ForeColor = Color.Black;
        }
        private void CurrentDebt_textBox_Leave(object sender, EventArgs e)
        {
            CurrentDebt_textBox.Text = DB_API.Moneyfy(CurrentDebt_textBox.Text);

            if (CurrentDebt_textBox.Text.Equals(""))
            {
                CurrentDebt_textBox.Text = "current debt";
                CurrentDebt_textBox.ForeColor = Color.Gray;
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

        private void Pay_textBox_Enter(object sender, EventArgs e)
        {
            Pay_textBox.Text = "";
            Pay_textBox.ForeColor = Color.Black;
        }
        private void Pay_textBox_Leave(object sender, EventArgs e)
        {
            Pay_textBox.Text = DB_API.Moneyfy(Pay_textBox.Text);

            if (Pay_textBox.Text.Equals(""))
            {
                Pay_textBox.Text = "payment";
                Pay_textBox.ForeColor = Color.Gray;
            }
        }

    }
}
