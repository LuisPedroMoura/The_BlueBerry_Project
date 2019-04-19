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
            defaultTextboxes();
            CURRENT_USER = userfindme_textbox.ForeColor == Color.Black ? userfindme_textbox.Text : ""; // empty string
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

        // -------------------------------------------------------------------
        // LIST BOXES --------------------------------------------------------
        // -------------------------------------------------------------------

        private void accounts_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value
            string account_name = (string)accounts_listbox.SelectedItem;
            string account_id = CURRENT_USER_ACCOUNTS[account_name].ToString();
            // set account_name label
            account_textbox.Text = account_name;
            account_textbox.ForeColor = Color.Black;
            // calculate(sum) wallets balance
            String[] columns = { "balance" };
            List<IDictionary<String, String>> where = new List<IDictionary<string, string>>();
            where.Add(new Dictionary<String, String> { { "account_id", account_id } });
            DataTableReader rdr = DB_API.DBselect("Project.wallets", columns, where);
            Decimal balance = 0.00m;
            while (rdr.Read())
            {
                balance += (Decimal)rdr["balance"];
            }
            balance_textbox.Text = balance.ToString() +"€";
            // calculate account patrimony (add stocks and subtract loan to balance)
            // create sql query with join for stocks value
            string sql =    "SELECT Project.stocks.bid_price " +
                            "FROM " +
                            "   Project.purchased_stocks LEFT JOIN Project.stocks " +
                            "   ON Project.purchased_stocks.company=Project.stocks.company " +
                            "WHERE Project.purchased_stocks.account_id=" + account_id;
            rdr = DB_API.DBselect(sql);
            Decimal stocks_value = 0.0m;
            while (rdr.Read())
            {
                stocks_value += (Decimal)rdr["bid_price"];
            }
            // calculate loans value
            Decimal loans_value = 0.0m;
            rdr = DB_API.DBselect("Project.loans", new string[] { "amount" }, where);
            while (rdr.Read())
            {
                loans_value += (Decimal)rdr["amount"];
            }
            Decimal patrimony = balance + stocks_value - loans_value;
            patrimony_textbox.Text = patrimony.ToString() + "€";
            // get associated user of selected account
            columns = new string[] { "user_email" };
            rdr = DB_API.DBselect("Project.users_money_accounts", columns, where);
            List<String> res = new List<string>();
            while (rdr.Read())
            {
                res.Add(rdr["user_email"].ToString());
            }
            associatedusers_listbox.DataSource = res;
        }


        private void associatedusers_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value and paste it to textbox
            user_textbox.Text = (string)associatedusers_listbox.SelectedItem;
            user_textbox.ForeColor = Color.Black;
        }

        // -------------------------------------------------------------------
        // BUTTONS -----------------------------------------------------------
        // -------------------------------------------------------------------
        private void findme_btn_Click(object sender, EventArgs e)
        {
            // read value from textbox
            string email = userfindme_textbox.ForeColor == Color.Black ? userfindme_textbox.Text : "";
            CURRENT_USER = email;

            // verify if user exists
            var exists = DB_API.Exists("Project.users", "email", DB_API.Str(email));
            if (!exists)
            {
                notifications_textbox.Text = "ERROR:\nUser does not exist!";
                return;
            }

            // choose columns to be selected
            var columns = new String[] { "account_name", "account_id" };

            // choose filter to be applied
            var where = new List<IDictionary<String, String>> { new Dictionary<String, String> { { "user_email", DB_API.Str(email) } } };
            
            // read query result
            var rdr = DB_API.DBselect("Project.users_money_accounts", columns, where);

            // verify if user has accounts
            if (!rdr.HasRows)
            {
                notifications_textbox.Text = "WARNING:\nUser has no accounts.";
                return;
            }

            // read query results
            var res = new List<String>();
            while (rdr.Read())
            {
                CURRENT_USER_ACCOUNTS[rdr["account_name"].ToString()] = (int)rdr["account_id"];
                res.Add(rdr["account_name"].ToString());
            }

            LISTBOXCONTENT = res;
            accounts_listbox.DataSource = res;
            notifications_textbox.Text = "SUCCESS:\nFound user!";
        }


        private void newaccount_btn_Click(object sender, EventArgs e)
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

            // insert new account into 'money_accounts' table
            IDictionary<String, String> attrValue = new Dictionary<string, string>();
            attrValue["account_name"] = DB_API.Str(account_name);
            attrValue["patrimony"] = 0.0.ToString();
            string account_id = DB_API.DBinsertGetID("Project.money_accounts", attrValue).ToString();

            // insert new account into 'users_money_accounts' table
            attrValue.Clear();
            attrValue["account_name"] = DB_API.Str(account_name);
            attrValue["account_id"] = account_id;
            attrValue["user_email"] = DB_API.Str(CURRENT_USER);
            DB_API.DBinsert("Project.users_money_accounts", attrValue);

            CURRENT_USER_ACCOUNTS[account_name] = int.Parse(account_id);
            LISTBOXCONTENT.Add(account_name);
// O PROBLEMA É A REATRIBUIÇÃO... ALGO DE BINDING... VER PORQUE E COMO FNCIONA NO BACK OFFICE!
            accounts_listbox.DataSource = LISTBOXCONTENT;
            notifications_textbox.Text = "SUCCESS:\nNew account was added!";
        }

        private void deleteaccount_btn_Click(object sender, EventArgs e)
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
            where[0]["account_id"] = account_id;
            DB_API.DBdelete("Project.money_accounts", where);
        }

        private void adduser_btn_Click(object sender, EventArgs e)
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
            exists = DB_API.Exists("Project.users", "email", DB_API.Str(user_email));
            if (!exists)
            {
                notifications_textbox.Text = "ERROR:\nUser does not exist!";
                return;
            }

            // insert new account into 'users_money_accounts' tablee
            IDictionary<String, String> attrValue = new Dictionary<string, string>();        
            attrValue["account_name"] = DB_API.Str(account_name);
            attrValue["account_id"] = account_id;
            attrValue["user_email"] = DB_API.Str(user_email);
            DB_API.DBinsert("Project.users_money_accounts", attrValue);

            associatedusers_listbox.DataSource += account_name;
            notifications_textbox.Text = "SUCCESS:\nNew account was added!";
        }

        private void deleteuser_btn_Click(object sender, EventArgs e)
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
            exists = DB_API.Exists("Project.users", "email", DB_API.Str(user_email));
            if (!exists)
            {
                notifications_textbox.Text = "ERROR:\nUser does not exist!";
                return;
            }

            // delete user access to account
            var where = DB_API.where();
            where[0]["account_id"] = account_id;
            where[0]["user_email"] = DB_API.Str(user_email);
            DB_API.DBdelete("Project.users_money_accounts", where);
        }


        // -------------------------------------------------------------------
        // TEXT BOXES HINTS --------------------------------------------------
        // -------------------------------------------------------------------

        private void defaultTextboxes()
        {
            userfindme_textbox.Text = "email";
            userfindme_textbox.ForeColor = Color.Gray;
            account_textbox.Text = "account name";
            account_textbox.ForeColor = Color.Gray;
            user_textbox.Text = "associated user email";
            user_textbox.ForeColor = Color.Gray;
        }


        private void email_textbox_Enter(object sender, EventArgs e)
        {
            userfindme_textbox.Text = "";
            userfindme_textbox.ForeColor = Color.Black;
        }
        private void email_textbox_Leave(object sender, EventArgs e)
        {
            if (userfindme_textbox.Text.Equals(""))
            {
                userfindme_textbox.Text = "email";
                userfindme_textbox.ForeColor = Color.Gray;
            }
        }

        private void user_textbox_Enter(object sender, EventArgs e)
        {
            user_textbox.Text = "";
            user_textbox.ForeColor = Color.Black;
        }
        private void user_textbox_Leave(object sender, EventArgs e)
        {
            if (user_textbox.Text.Equals(""))
            {
                user_textbox.Text = "associated user email";
                user_textbox.ForeColor = Color.Gray;
            }
        }

        private void account_textbox_Enter(object sender, EventArgs e)
        {
            account_textbox.Text = "";
            account_textbox.ForeColor = Color.Black;
        }
        private void account_textbox_Leave(object sender, EventArgs e)
        {
            if (account_textbox.Text.Equals(""))
            {
                account_textbox.Text = "account name";
                account_textbox.ForeColor = Color.Gray;
            }
        }


        
    }
}
