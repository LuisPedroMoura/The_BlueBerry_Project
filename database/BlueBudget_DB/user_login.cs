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

        private String CURRENT_USER;
        private IDictionary<String, int> CURRENT_USER_ACCOUNTS = new Dictionary<String, int>();
        private List<String> LISTBOXCONTENT = new List<string>();

        public user_login()
        {
            InitializeComponent();
            DefaultTextboxes();
            CURRENT_USER = userfindme_textbox.ForeColor == Color.Black ? userfindme_textbox.Text : ""; // empty string
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_btn_Click(object sender, EventArgs e)
        {
            var frm = new user_menu
            {
                Location = this.Location,
                StartPosition = FormStartPosition.Manual
            };
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void User_login_Load(object sender, EventArgs e)
        {

        }

        // -------------------------------------------------------------------
        // LIST BOXES --------------------------------------------------------
        // -------------------------------------------------------------------

        private void Populate_moneyAccounts_listBox(String email)
        {

            // get money accounts from DB
            var rdr = DB_API.SelectUserMoneyAccounts(email);

            // verify if user has accounts
            if (!rdr.HasRows)
            {
                notifications_textbox.Text = "WARNING:\nUser has no accounts.";
                return;
            }

            // extract money acounts names for listBox
            while (rdr.Read())
            {
                string account_name = rdr[DB_API.MoneyAccountEnt.account_name.ToString()].ToString();
                if (!res.Contains(account_name))
                {
                    res.Add(account_name);
                    CURRENT_USER_ACCOUNTS[account_name] = (int)rdr[DB_API.MoneyAccountEnt.id.ToString()];
                }
            }
            accounts_listbox.DataSource = res;
        }

        private void Populate_associatedUsers_listBox(String account_name)
        {
            var res = new List<String>();
            var where = DB_API.Where();
            where[DB_API.MoneyAccountEnt.account_name] = account_name;
            var rdr = DB_API.DBselect(DB_API.Entity.money_account, DB_API.Where());
            while (rdr.Read())
            {
                res.Add(rdr[DB_API.MoneyAccountEnt.user_email.ToString()].ToString());
            }
            associatedusers_listbox.DataSource = res;
        }


        private void Accounts_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value
            string account_name = (string)accounts_listbox.SelectedItem;
            string account_id = CURRENT_USER_ACCOUNTS[account_name].ToString();

            // set account_name label
            account_textbox.Text = account_name;
            account_textbox.ForeColor = Color.Black;

            // get account info
            var where = DB_API.Where();
            where[DB_API.MoneyAccountEnt.account_id] = account_id;
            var rdr = DB_API.DBselect(DB_API.Entity.money_account, where);
            while (rdr.Read())
            {
                balance_textbox.Text = ((Decimal)rdr[DB_API.MoneyAccountEnt.balance.ToString()]).ToString() + "€";
                patrimony_textbox.Text = ((Decimal)rdr[DB_API.MoneyAccountEnt.patrimony.ToString()]).ToString() + "€";
            }

            // get associated user of selected account
            rdr = DB_API.DBselect("Project.users_money_accounts", where);
            List<String> res = new List<string>();
            while (rdr.Read())
            {
                res.Add(rdr["user_email"].ToString());
            }
            associatedusers_listbox.DataSource = res;
        }


        private void Associatedusers_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value and paste it to textbox
            user_textbox.Text = (string)associatedusers_listbox.SelectedItem;
            user_textbox.ForeColor = Color.Black;
        }

        // -------------------------------------------------------------------
        // BUTTONS -----------------------------------------------------------
        // -------------------------------------------------------------------
        private void Findme_btn_Click(object sender, EventArgs e)
        {
            // read value from textbox
            string email = userfindme_textbox.ForeColor == Color.Black ? userfindme_textbox.Text : "";
            CURRENT_USER = email;

            // verify an email was given
            if (email.Equals(""))
            {
                notifications_textbox.Text = "ERROR:\nEmail field is mandatory!";
                return;
            }

            // verify if user exists
            var exists = DB_API.DBexists(DB_API.Entity.user, DB_API.UserEnt.email, email);
            if (!exists)
            {
                notifications_textbox.Text = "ERROR:\nUser does not exist!";
                return;
            }

            // populate account_listBox
            Populate_moneyAccounts_listBox(email);
        }


        private void Newaccount_btn_Click(object sender, EventArgs e)
        {
            // get textbox value
            string account_name = account_textbox.ForeColor == Color.Black ? account_textbox.Text : "";

            // verify that a name is given
            if ("".Equals(account_name))
            {
                notifications_textbox.Text = "ERROR:\nAccount name is mandatory!";
                return;
            }
            // verify that a user is selected
            if (CURRENT_USER.Equals(""))
            {
                notifications_textbox.Text = "ERROR:\nUser must be selected to attribute account to!";
                return;
            }

            // verify if user already has same name account
            if (new List<String>(CURRENT_USER_ACCOUNTS.Keys).Contains(account_name))
            {
                notifications_textbox.Text = "ERROR:\nUser already has an account with the same name!";
                return;
            }

            // insert new account into 'money_accounts' and 'users_money_accounts' tables
            IDictionary<String, String> attrValue = new Dictionary<string, string>();
            attrValue["user_email"] = CURRENT_USER;
            attrValue["account_name"] = account_name;
            attrValue["patrimony"] = 0.0.ToString();
            DB_API.DBexecProc("pr_insert_money_account", attrValue);

            //CURRENT_USER_ACCOUNTS[account_name] = int.Parse(account_id);
            LISTBOXCONTENT.Add(account_name);
// O PROBLEMA É A REATRIBUIÇÃO... ALGO DE BINDING... VER PORQUE E COMO FNCIONA NO BACK OFFICE!
            accounts_listbox.DataSource = LISTBOXCONTENT;
            notifications_textbox.Text = "SUCCESS:\nNew account was added!";
        }

        private void Deleteaccount_btn_Click(object sender, EventArgs e)
        {
            // get textbox value
            string account_name = account_textbox.ForeColor == Color.Black ? account_textbox.Text : "";
            string account_id = CURRENT_USER_ACCOUNTS[account_name].ToString();
            // verify if account exists already
            var exists = DB_API.Exists("Project.money_accounts", "account_id", account_id);
            if (!exists)
            {
                notifications_textbox.Text = "ERROR:\nAccount does not exist!";
                return;
            }
            var where = DB_API.where();
            where["account_id"] = account_id;
            DB_API.DBexecProc("pr_delete_money_account", where);
        }

        private void Adduser_btn_Click(object sender, EventArgs e)
        {
            // get textbox value
            string user_email = user_textbox.ForeColor == Color.Black ? user_textbox.Text : "";
            string account_name = account_textbox.ForeColor == Color.Black ? account_textbox.Text : "";
            string account_id = CURRENT_USER_ACCOUNTS[account_name].ToString();

            // verify if account field is filled
            if ("".Equals(account_name))
            {
                notifications_textbox.Text = "ERROR:\nAccount name is mandatory!";
                return;
            }

            // verify if account exists
            var exists = DB_API.Exists("Project.money_accounts", "account_id", account_id);
            if (!exists)
            {
                notifications_textbox.Text = "ERROR:\nAccount does not exist!";
                return;
            }

            // verify if user exists
            exists = DB_API.Exists("Project.users", "email", user_email);
            if (!exists)
            {
                notifications_textbox.Text = "ERROR:\nUser does not exist!";
                return;
            }

            // insert new account into 'users_money_accounts' tablee
            IDictionary<String, String> attrValue = new Dictionary<string, string>();        
            attrValue["account_name"] = account_name;
            attrValue["account_id"] = account_id;
            attrValue["user_email"] = user_email;
            DB_API.DBexecProc("Project.users_money_accounts", attrValue);

            associatedusers_listbox.DataSource += account_name;
            notifications_textbox.Text = "SUCCESS:\nNew account was added!";
        }

        private void Deleteuser_btn_Click(object sender, EventArgs e)
        {
            // get textbox value
            string user_email = user_textbox.ForeColor == Color.Black ? user_textbox.Text : "";
            string account_name = account_textbox.ForeColor == Color.Black ? account_textbox.Text : "";
            string account_id = CURRENT_USER_ACCOUNTS[account_name].ToString();
            // verify if account field is filled
            if ("".Equals(account_name))
            {
                notifications_textbox.Text = "ERROR:\nAccount name is mandatory!";
                return;
            }
            // verify if account exists
            var exists = DB_API.Exists("Project.money_accounts", "account_id", account_id);
            if (!exists)
            {
                notifications_textbox.Text = "ERROR:\nAccount does not exist!";
                return;
            }

            // verify if user exists
            exists = DB_API.Exists("Project.users", "email", user_email);
            if (!exists)
            {
                notifications_textbox.Text = "ERROR:\nUser does not exist!";
                return;
            }

            // delete user access to account
            var where = DB_API.where();
            where["account_id"] = account_id;
            where["user_email"] = user_email;
            DB_API.DBexecProc("pr_delete_money_account", where);
        }


        // -------------------------------------------------------------------
        // TEXT BOXES HINTS --------------------------------------------------
        // -------------------------------------------------------------------

        private void DefaultTextboxes()
        {
            userfindme_textbox.Text = "email";
            userfindme_textbox.ForeColor = Color.Gray;
            account_textbox.Text = "account name";
            account_textbox.ForeColor = Color.Gray;
            user_textbox.Text = "associated user email";
            user_textbox.ForeColor = Color.Gray;
        }


        private void Email_textbox_Enter(object sender, EventArgs e)
        {
            userfindme_textbox.Text = "";
            userfindme_textbox.ForeColor = Color.Black;
        }
        private void Email_textbox_Leave(object sender, EventArgs e)
        {
            if (userfindme_textbox.Text.Equals(""))
            {
                userfindme_textbox.Text = "email";
                userfindme_textbox.ForeColor = Color.Gray;
            }
        }

        private void User_textbox_Enter(object sender, EventArgs e)
        {
            user_textbox.Text = "";
            user_textbox.ForeColor = Color.Black;
        }
        private void User_textbox_Leave(object sender, EventArgs e)
        {
            if (user_textbox.Text.Equals(""))
            {
                user_textbox.Text = "associated user email";
                user_textbox.ForeColor = Color.Gray;
            }
        }

        private void Account_textbox_Enter(object sender, EventArgs e)
        {
            account_textbox.Text = "";
            account_textbox.ForeColor = Color.Black;
        }
        private void Account_textbox_Leave(object sender, EventArgs e)
        {
            if (account_textbox.Text.Equals(""))
            {
                account_textbox.Text = "account name";
                account_textbox.ForeColor = Color.Gray;
            }
        }


        
    }
}
