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
    public partial class BackOfficeForm : Form
    {

        public BackOfficeForm()
        {
            InitializeComponent();
        }

        private void Back_office_Load(object sender, EventArgs e)
        {
            // style comboBoxes
            Periodicity_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Periodicity_comboBox.FlatStyle = FlatStyle.Popup;

            // populate listbox with users
            PopulateUsersListView();
            Populate_comboBox();
            DefaultTextboxes();
        }

        // -------------------------------------------------------------------
        // BUTTONS -----------------------------------------------------------
        // -------------------------------------------------------------------

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            // get values from text boxes if they were inserted by user
            string username = username_textbox.ForeColor == Color.Black ? username_textbox.Text : "";
            string email = email_textbox.ForeColor == Color.Black ? email_textbox.Text : "";
            string fname = firstname_textbox.ForeColor == Color.Black ? firstname_textbox.Text : "";
            string mname = middlename_textbox.ForeColor == Color.Black ? middlename_textbox.Text : "";
            string lname = lastname_textbox.ForeColor == Color.Black ? lastname_textbox.Text : "";
            string cardNo = cardnumber_textbox.ForeColor == Color.Black ? cardnumber_textbox.Text : "";
            int periodicity = DB_API.SelectRecurenceIdbyDesignation((string)Periodicity_comboBox.SelectedItem);
            DateTime term = DateTime.Parse(term_dateTimePicker.Value.ToString());

            // verify if email field is filled
            if (email.Equals(""))
            {
                ErrorMessenger.EmptyField("Email");
                return;
            }

            // verify that all mandatory fields are filled (if subscripition is checked)
            if (active_checkBox.Checked)
            {
                if ("".Equals(fname) || "".Equals(lname) || "".Equals(cardNo) || "".Equals(periodicity))
                {
                    ErrorMessenger.EmptyField("Every field marked (*)");
                    return;
                }
            }

            // verify if user exists, if so, cannot be added, only updated
            var exists = DB_API.ExistsUser(email);
            if (exists)
            {
                ErrorMessenger.Error("User already exists!");
                return;
            }

            // add user
            DB_API.InsertUser(username, email, fname, mname, lname, cardNo, periodicity, term, active_checkBox.Checked);

            // upadte listBox with new user
            PopulateUsersListView();
        }

        private void Update_btn_Click(object sender, EventArgs e)
        {
            // get values from text boxes if they attrValue inserted by user
            string username = username_textbox.ForeColor == Color.Black ? username_textbox.Text : "";
            string email = email_textbox.ForeColor == Color.Black ? email_textbox.Text : "";
            string fname = firstname_textbox.ForeColor == Color.Black ? firstname_textbox.Text : "";
            string mname = middlename_textbox.ForeColor == Color.Black ? middlename_textbox.Text : "";
            string lname = lastname_textbox.ForeColor == Color.Black ? lastname_textbox.Text : "";
            string cardNo = cardnumber_textbox.ForeColor == Color.Black ? cardnumber_textbox.Text : "";
            int periodicity = DB_API.SelectRecurenceIdbyDesignation((string)Periodicity_comboBox.SelectedItem);
            DateTime term = DateTime.Parse(term_dateTimePicker.Value.ToString());

            // verify if email field is filled
            if (email.Equals(""))
            {
                ErrorMessenger.EmptyField("Email");
                return;
            }

            // verify that all mandatory fields are filled (if subscripition is checked)
            if (active_checkBox.Checked)
            {
                if ("".Equals(fname) || "".Equals(lname) || "".Equals(cardNo) || "".Equals(periodicity))
                {
                    ErrorMessenger.EmptyField("Every field marked (*)");
                    return;
                }
            }

            // verify if user exists, so it can be updated
            var exists = DB_API.ExistsUser(email);
            if (!exists)
            {
                ErrorMessenger.Error("User does not exist. Unable to update!");
                return;
            }

            // update user
            DB_API.UpdateUser(username, email, fname, mname, lname, cardNo, periodicity, term, active_checkBox.Checked);
            
              
            // update listBox
            PopulateUsersListView();
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            string email = email_textbox.ForeColor == Color.Black ? email_textbox.Text : "";

            // verify if email field is filled
            if (email.Equals(""))
            {
                ErrorMessenger.EmptyField("Email");
                return;
            }

            // delete user form 'users' table
            DB_API.DeleteUser(email);

            // update listBox
            PopulateUsersListView();
            DefaultTextboxes();
        }

        // -------------------------------------------------------------------
        // LIST BOX ----------------------------------------------------------
        // -------------------------------------------------------------------

        private void PopulateUsersListView()
        {
            // clear the listView
            Users_listView.Items.Clear();

            // populate the listView
            var rdr = DB_API.SelectAllUsers();
            var res = new List<String>();
            while (rdr.Read())
            {
                string username = rdr[DB_API.UserEnt.user_name.ToString()].ToString();
                string email = rdr[DB_API.UserEnt.email.ToString()].ToString();
                var row = new string[] { username, email };
                var item = new ListViewItem(row);
                item.Tag = new User(username, email);
                Users_listView.Items.Add(item);
            }
        }

        private void Users_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value
            User user = Users_listView.SelectedItems.Count > 0 ? (User)Users_listView.SelectedItems[0].Tag : null;
            if (user == null) { return; } // to handle listView idiot 2 step item selection process
            string email = user.Email;

            // search DB for user
            var rdr = DB_API.SelectUserByEmail(email);
            var res = new List<Object>();
            while (rdr.Read())
            {

                Console.WriteLine("---> email: " + rdr[DB_API.UserEnt.email.ToString()].ToString());
                email_textbox.Text = rdr[DB_API.UserEnt.email.ToString()].ToString();
                email_textbox.ForeColor = email_textbox.Text.Equals("") ? Color.Gray : Color.Black;
                email_textbox.Text = email_textbox.Text.Equals("") ? "email" : email_textbox.Text;
                username_textbox.Text = rdr[DB_API.UserEnt.user_name.ToString()].ToString();
                username_textbox.ForeColor = username_textbox.Text.Equals("") ? Color.Gray : Color.Black;
                username_textbox.Text = username_textbox.Text.Equals("") ? "username" : username_textbox.Text;
                firstname_textbox.Text = rdr[DB_API.UserEnt.fname.ToString()].ToString();
                firstname_textbox.ForeColor = firstname_textbox.Text.Equals("") ? Color.Gray : Color.Black;
                firstname_textbox.Text = firstname_textbox.Text.Equals("") ? "first name" : firstname_textbox.Text;
                middlename_textbox.Text = rdr[DB_API.UserEnt.mname.ToString()].ToString();
                middlename_textbox.ForeColor = middlename_textbox.Text.Equals("") ? Color.Gray : Color.Black;
                middlename_textbox.Text = middlename_textbox.Text.Equals("") ? "middle name" : middlename_textbox.Text;
                lastname_textbox.Text = rdr[DB_API.UserEnt.lname.ToString()].ToString();
                lastname_textbox.ForeColor = lastname_textbox.Text.Equals("") ? Color.Gray : Color.Black;
                lastname_textbox.Text = lastname_textbox.Text.Equals("") ? "last name" : lastname_textbox.Text;
                cardnumber_textbox.Text = rdr[DB_API.UserEnt.card_number.ToString()].ToString();
                cardnumber_textbox.ForeColor = cardnumber_textbox.Text.Equals("") ? Color.Gray : Color.Black;
                cardnumber_textbox.Text = cardnumber_textbox.Text.Equals("") ? "card number" : cardnumber_textbox.Text;
                bool activeSubscription = (bool)rdr[DB_API.UserEnt.active_subscription.ToString()];
                Console.WriteLine("---> active subscription: " + activeSubscription);
                if (!activeSubscription)
                {
                    Periodicity_comboBox.Text = DB_API.SelectRecurrenceById(1);
                    term_dateTimePicker.Value = DateTime.Today;
                    active_checkBox.Checked = false;
                }
                else
                {
                    Console.WriteLine("---> " + rdr[DB_API.UserEnt.periodicity.ToString()].ToString());
                    Periodicity_comboBox.Text = DB_API.SelectRecurrenceById((int)rdr[DB_API.UserEnt.periodicity.ToString()]);
                    term_dateTimePicker.Value = (DateTime)rdr[DB_API.UserEnt.term.ToString()];
                    active_checkBox.Checked = true;
                }
            }
        }

        // -------------------------------------------------------------------
        // TEXT BOXES --------------------------------------------------------
        // -------------------------------------------------------------------

        private void Populate_comboBox()
        {
            var rdr = DB_API.SelectAllRecurrences();
            var res = new List<String>();
            while (rdr.Read())
            {
                res.Add((string)rdr[DB_API.RecurrenceEnt.designation.ToString()]);
            }
            Periodicity_comboBox.DataSource = res;
        }

        // -------------------------------------------------------------------
        // TEXT BOXES HINTS --------------------------------------------------
        // -------------------------------------------------------------------

        private void DefaultTextboxes()
        {
            email_textbox.Text = "email";
            email_textbox.ForeColor = Color.Gray;
            username_textbox.Text = "username";
            username_textbox.ForeColor = Color.Gray;
            firstname_textbox.Text = "fisrt name";
            firstname_textbox.ForeColor = Color.Gray;
            middlename_textbox.Text = "middle name";
            middlename_textbox.ForeColor = Color.Gray;
            lastname_textbox.Text = "last name";
            lastname_textbox.ForeColor = Color.Gray;
            cardnumber_textbox.Text = "card number";
            cardnumber_textbox.ForeColor = Color.Gray;
            Populate_comboBox();
            term_dateTimePicker.Value = DateTime.Now;
            active_checkBox.Checked = false;
        }

        private void Email_textbox_Enter(object sender, EventArgs e)
        {
            email_textbox.Text = "";
            email_textbox.ForeColor = Color.Black;
        }
        private void Email_textbox_Leave_1(object sender, EventArgs e)
        {
            if (email_textbox.Text.Equals(""))
            {
                email_textbox.Text = "email";
                email_textbox.ForeColor = Color.Gray;
            }
        }

        private void Username_textbox_Enter(object sender, EventArgs e)
        {
            username_textbox.Text = "";
            username_textbox.ForeColor = Color.Black;
        }
        private void Username_textbox_Leave_1(object sender, EventArgs e)
        {
            if (username_textbox.Text.Equals(""))
            {
                username_textbox.Text = "username";
                username_textbox.ForeColor = Color.Gray;
            }
        }

        private void Firstname_textbox_Enter(object sender, EventArgs e)
        {
            firstname_textbox.Text = "";
            firstname_textbox.ForeColor = Color.Black;
        }
        private void Firstname_textbox_Leave_1(object sender, EventArgs e)
        {
            if (firstname_textbox.Text.Equals(""))
            {
                firstname_textbox.Text = "first name";
                firstname_textbox.ForeColor = Color.Gray;
            }
        }

        private void Middlename_textbox_Enter(object sender, EventArgs e)
        {
            middlename_textbox.Text = "";
            middlename_textbox.ForeColor = Color.Black;
        }
        private void Middlename_textbox_Leave_1(object sender, EventArgs e)
        {
            if (middlename_textbox.Text.Equals(""))
            {
                middlename_textbox.Text = "middle name";
                middlename_textbox.ForeColor = Color.Gray;
            }
        }

        private void Lastname_textbox_Enter(object sender, EventArgs e)
        {
            lastname_textbox.Text = "";
            lastname_textbox.ForeColor = Color.Black;
        }
        private void Lastname_textbox_Leave_1(object sender, EventArgs e)
        {
            if (lastname_textbox.Text.Equals(""))
            {
                lastname_textbox.Text = "last name";
                lastname_textbox.ForeColor = Color.Gray;
            }
        }

        private void Cardnumber_textbox_Enter(object sender, EventArgs e)
        {
            cardnumber_textbox.Text = "";
            cardnumber_textbox.ForeColor = Color.Black;
        }
        private void Cardnumber_textbox_Leave_1(object sender, EventArgs e)
        {
            if (cardnumber_textbox.Text.Equals(""))
            {
                cardnumber_textbox.Text = "card number";
                cardnumber_textbox.ForeColor = Color.Gray;
            }
        }

        
    }
}
