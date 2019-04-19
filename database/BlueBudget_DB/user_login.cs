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
    public partial class user_login : Form
    {
        public user_login()
        {
            InitializeComponent();
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            var frm = new user_menu();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void user_login_Load(object sender, EventArgs e)
        {

        }

        private void accounts_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void username_textbox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void email_login_btn_Click(object sender, EventArgs e)
        {
            if (!username_textbox.Text.Equals(""))
            {
                String[] collumns = { "account_name" };
                String tableName = "Project.users_money_accounts";
                IDictionary<String, String> where = new Dictionary<String, String>();
                //where.Add("user_email",username_textbox.Text);
                //List<String> res = DB_API.DBselect(tableName, collumns, where);
                //accounts_listbox.DataSource = res;
            }
        }

    }
}
