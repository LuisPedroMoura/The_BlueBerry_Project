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
    public partial class user_menu : Form
    {

        string user_email;
        int account_id;

        public user_menu(string user_email, int account_id)
        {
            InitializeComponent();
            this.user_email = user_email;
            this.account_id = account_id;
        }

        private void loans_btn_Click(object sender, EventArgs e)
        {
            var frm = new loans();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void budget_btn_Click(object sender, EventArgs e)
        {
            var frm = new budget();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void transactions_btn_Click(object sender, EventArgs e)
        {
            var frm = new transactions();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void goal_btn_Click(object sender, EventArgs e)
        {
            var frm = new goals();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void stock_btn_Click(object sender, EventArgs e)
        {
            var frm = new stocks();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void user_menu_Load(object sender, EventArgs e)
        {

        }
    }
}
