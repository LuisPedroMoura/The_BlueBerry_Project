using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueBudget_DB
{
    public partial class UserMenuForm : Form
    {

        string user_email;
        int account_id;

        public UserMenuForm(string user_email, int account_id)
        {
            InitializeComponent();
            this.user_email = user_email;
            this.account_id = account_id;
        }

        private void Loans_btn_Click(object sender, EventArgs e)
        {
            var frm = new LoansForm(user_email, account_id)
            {
                Location = this.Location,
                StartPosition = FormStartPosition.Manual
            };
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Budget_btn_Click(object sender, EventArgs e)
        {
            var frm = new BudgetsForm(user_email, account_id)
            {
                Location = this.Location,
                StartPosition = FormStartPosition.Manual
            };
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void Transactions_btn_Click(object sender, EventArgs e)
        {
            var frm = new TransactionsForm(user_email, account_id)
            {
                Location = this.Location,
                StartPosition = FormStartPosition.Manual
            };
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void Goal_btn_Click(object sender, EventArgs e)
        {
            var frm = new GoalsForm(user_email, account_id)
            {
                Location = this.Location,
                StartPosition = FormStartPosition.Manual
            };
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void Stock_btn_Click(object sender, EventArgs e)
        {
            var frm = new StocksForm(user_email, account_id)
            {
                Location = this.Location,
                StartPosition = FormStartPosition.Manual
            };
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void Statistics_btn_Click(object sender, EventArgs e)
        {
            var frm = new StatisticsForm(user_email, account_id)
            {
                Location = this.Location,
                StartPosition = FormStartPosition.Manual
            };
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }
    }
}
