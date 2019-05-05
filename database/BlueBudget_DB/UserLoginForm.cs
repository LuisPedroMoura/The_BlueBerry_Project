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
    public partial class UserLoginForm : Form
    {

        private String CURRENT_USER;
        private IDictionary<String, int> CURRENT_USER_ACCOUNTS = new Dictionary<String, int>();
        private List<String> LISTBOXCONTENT = new List<string>();

        public UserLoginForm()
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

            string user = CURRENT_USER.Equals("") ? userfindme_textbox.Text : CURRENT_USER;
            
            if (user.Equals(""))
            {
                ErrorMessenger.EmptyField("Email");
                return;
            }

            string account_name = account_textbox.ForeColor == Color.Black ? account_textbox.Text : "";

            if (account_name.Equals(""))
            {
                ErrorMessenger.Error("Account must be selected!");
                return;
            }

            int account_id = CURRENT_USER_ACCOUNTS[account_name];
            var exists = DB_API.ExistsMoneyAccount(account_id);
            if (!exists)
            {
                ErrorMessenger.Error("Account does not exist!");
                return;
            }

            var frm = new UserMenuForm(user, account_id)
            {
                Location = this.Location,
                StartPosition = FormStartPosition.Manual
            };
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        // -------------------------------------------------------------------
        // LIST BOXES --------------------------------------------------------
        // -------------------------------------------------------------------

        private void Populate_moneyAccounts_listBox(string email)
        {

            // get money accounts from DB
            var rdr = DB_API.SelectUserMoneyAccounts(email);

            // extract money acounts names for listBox
            var res = new List<String>();
            while (rdr.Read())
            {
                string account_name = rdr[DB_API.MoneyAccountEnt.account_name.ToString()].ToString();
                res.Add(account_name);
                CURRENT_USER_ACCOUNTS[account_name] = (int)rdr[DB_API.MoneyAccountEnt.account_id.ToString()];
            }
            accounts_listbox.DataSource = res;
        }

        private void Populate_associatedUsers_listBox(int account_id)
        {
            var rdr = DB_API.SelectMoneyAccountUsers(account_id);
            var res = new List<String>();
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
            int account_id = CURRENT_USER_ACCOUNTS[account_name];

            // set account_name label
            account_textbox.Text = account_name;
            account_textbox.ForeColor = Color.Black;

            // get account complete info
            var rdr = DB_API.SelectMoneyAccountById(account_id);
            while (rdr.Read())
            {
                balance_textbox.Text = DB_API.Moneyfy(rdr[DB_API.MoneyAccountEnt.balance.ToString()].ToString());
                patrimony_textbox.Text = DB_API.Moneyfy(rdr[DB_API.MoneyAccountEnt.patrimony.ToString()].ToString());
            }

            // get associated user of selected account
            rdr = DB_API.SelectMoneyAccountUsers(account_id);
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

            // UNFINISHED!
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
                ErrorMessenger.EmptyField("Email");
                return;
            }

            // verify if user exists
            var exists = DB_API.ExistsUser(email);
            if (!exists)
            {
                ErrorMessenger.Error("User does not exist!");
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
                ErrorMessenger.EmptyField("Account name");
                return;
            }

            // verify that a user is selected
            if (CURRENT_USER.Equals(""))
            {
                ErrorMessenger.Error("User must be selected to attribute account to!");
                return;
            }

            // verify if user already has same name account
            if (new List<String>(CURRENT_USER_ACCOUNTS.Keys).Contains(account_name))
            {
                ErrorMessenger.Error("User already has an account with the same name!");
                return;
            }

            // insert new account into 'money_accounts' and 'users_money_accounts' tables
            DB_API.InsertMoneyAccount(CURRENT_USER, account_name);

            Populate_moneyAccounts_listBox(CURRENT_USER);
        }

        private void Deleteaccount_btn_Click(object sender, EventArgs e)
        {
            // get textbox value
            string account_name = account_textbox.ForeColor == Color.Black ? account_textbox.Text : "";
            int account_id = CURRENT_USER_ACCOUNTS[account_name];

            // verify if account exists already
            var exists = DB_API.ExistsMoneyAccount(account_id);
            if (!exists)
            {
                ErrorMessenger.Error("Account does not exist!");
                return;
            }

            // delete money account
            DB_API.DeleteMoneyAccount(account_id);

            // repopulate accounts_listbox
            Populate_moneyAccounts_listBox(CURRENT_USER);
        }

        private void Adduser_btn_Click(object sender, EventArgs e)
        {
            // get textbox value
            string new_user = user_textbox.ForeColor == Color.Black ? user_textbox.Text : "";
            string account_name = account_textbox.ForeColor == Color.Black ? account_textbox.Text : "";
            int account_id = CURRENT_USER_ACCOUNTS[account_name];

            // verify if account field is filled
            if ("".Equals(account_name))
            {
                ErrorMessenger.EmptyField("Acount name");
                return;
            }

            // verify if account exists
            var exists = DB_API.ExistsMoneyAccount(account_id);
            if (!exists)
            {
                ErrorMessenger.Error("Account does not exist!");
                return;
            }

            // verify that a user is selected
            if (new_user.Equals(""))
            {
                ErrorMessenger.Error("User must be selected to attribute account to!");
                return;
            }

            // verify if user exists
            exists = DB_API.ExistsUser(new_user);
            if (!exists)
            {
                ErrorMessenger.Error("User does not exist!");
                return;
            }

            // insert new account into 'users_money_accounts table
            DB_API.MoneyAccountAddUser(account_id, new_user);

            Populate_associatedUsers_listBox(account_id);
        }

        private void Deleteuser_btn_Click(object sender, EventArgs e)
        {
            // get textbox value
            string user = user_textbox.ForeColor == Color.Black ? user_textbox.Text : "";
            string account_name = account_textbox.ForeColor == Color.Black ? account_textbox.Text : "";
            int account_id = CURRENT_USER_ACCOUNTS[account_name];

            // verify if account field is filled
            if ("".Equals(account_name))
            {
                ErrorMessenger.EmptyField("Account name");
                return;
            }
            // verify if account exists
            var exists = DB_API.ExistsMoneyAccount(account_id);
            if (!exists)
            {
                ErrorMessenger.Error("Account does not exist!");
                return;
            }

            // verify that a user is selected
            if (user.Equals(""))
            {
                ErrorMessenger.Error("User must be selected to attribute account to!");
                return;
            }

            // verify if user exists
            exists = DB_API.ExistsUser(user);
            if (!exists)
            {
                ErrorMessenger.Error("User does not exist!");
                return;
            }

            // delete user access to account
            DB_API.MoneyAccountRemoveUser(account_id, user);
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

        private void user_login_Load(object sender, EventArgs e)
        {

        }

    }
}
