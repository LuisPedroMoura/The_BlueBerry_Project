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
    public partial class back_office : Form
    {
        public back_office()
        {
            InitializeComponent();

            // populate listbox with users
            Populate_listBox();
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
            int periodicity = DB_API.SelectRecurenceIdbyDesignation((string)periodicity_comboBox.SelectedItem);
            DateTime term = DateTime.Parse(term_dateTimePicker.Value.ToString());

            // verify if email field is filled
            if (email.Equals(""))
            {
                notifications_textbox.Text = "ERROR:\nEmail is a mandatory field.";
                return;
            }

            // verify that all mandatory fields are filled (if subscripition is checked)
            if (active_checkBox.Checked)
            {
                if ("".Equals(fname) || "".Equals(lname) || "".Equals(cardNo) || "".Equals(periodicity))
                {
                    notifications_textbox.Text = "ERROR:\nMandatory fields (*) must be filled.";
                    return;
                }
            }

            // verify if user exists, if so, cannot be added, only updated
            var exists = DB_API.ExistsUser(email);
            if (exists)
            {
                notifications_textbox.Text = "ERROR:\nUser already exists!";
                return;
            }

            // add user
            DB_API.InsertUser(username, email, fname, mname, lname, cardNo, periodicity, term, active_checkBox.Checked);
            notifications_textbox.Text = "SUCCESS:\n New user was added to database!";

            // upadte listBox with new user
            Populate_listBox();
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
            int periodicity = DB_API.SelectRecurenceIdbyDesignation((string)periodicity_comboBox.SelectedItem);
            DateTime term = DateTime.Parse(term_dateTimePicker.Value.ToString());

            // verify if email field is filled
            if (email.Equals(""))
            {
                notifications_textbox.Text = "ERROR:\nEmail is a mandatory field!";
                return;
            }

            // verify that all mandatory fields are filled (if subscripition is checked)
            if (active_checkBox.Checked)
            {
                if ("".Equals(fname) || "".Equals(lname) || "".Equals(cardNo) || "".Equals(periodicity))
                {
                    notifications_textbox.Text = "ERROR:\nMandatory fields (*) must be filled.";
                    return;
                }
            }

            // verify if user exists, so it can be updated
            var exists = DB_API.ExistsUser(email);
            if (!exists)
            {
                notifications_textbox.Text = "ERROR:\nUser does not exist. Unable to update!";
                return;
            }

            // update user
            DB_API.UpdateUser(username, email, fname, mname, lname, cardNo, periodicity, term, active_checkBox.Checked);
            
              
            // update listBox
            Populate_listBox();
            notifications_textbox.Text = "SUCCESS:\n User " + email + " was updated!";
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            string email = email_textbox.ForeColor == Color.Black ? email_textbox.Text : "";

            // verify if email field is filled
            if (email.Equals(""))
            {
                notifications_textbox.Text = "ERROR:\nEmail is a mandatory field";
                return;
            }

            // delete user form 'users' table
            DB_API.DeleteUser(email);

            // update listBox
            Populate_listBox();
            notifications_textbox.Text = "SUCCESS:\n User " + email + " was deleted!";
        }

        // -------------------------------------------------------------------
        // LIST BOX ----------------------------------------------------------
        // -------------------------------------------------------------------

        private void Users_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value
            string email = (string)users_listbox.SelectedItem;

            // search DB for user
            var rdr = DB_API.SelectUserByEmail(email);
            var res = new List<Object>();
            while (rdr.Read())
            {
                res.Add(rdr[DB_API.UserEnt.email.ToString()].ToString());
                res.Add(rdr[DB_API.UserEnt.user_name.ToString()].ToString());
                res.Add(rdr[DB_API.UserEnt.fname.ToString()].ToString());
                res.Add(rdr[DB_API.UserEnt.mname.ToString()].ToString());
                res.Add(rdr[DB_API.UserEnt.lname.ToString()].ToString());
                res.Add(rdr[DB_API.UserEnt.card_number.ToString()].ToString());
                if (rdr[DB_API.UserEnt.active.ToString()].ToString().Equals(""))
                {
                    res.Add(1); // periodicity
                    res.Add(DateTime.Today); // term
                    res.Add(false); // active
                }
                else
                {
                    res.Add(rdr[DB_API.UserEnt.periodicity.ToString()]);
                    res.Add(DateTime.Parse(rdr[DB_API.UserEnt.term.ToString()].ToString()));
                    res.Add(Boolean.Parse(rdr[DB_API.UserEnt.active.ToString()].ToString()));
                }
            }

            Update_textBoxes(res);
        }

        private void Populate_listBox()
        {
            var rdr = DB_API.SelectAllUsers();
            var res = new List<String>();
            while (rdr.Read())
            {
                res.Add(rdr[DB_API.UserEnt.email.ToString()].ToString());
            }
            users_listbox.DataSource = res;
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
            periodicity_comboBox.DataSource = res;
        }

        private void Update_textBoxes(List<Object> values)
        {
            DefaultTextboxes();

            email_textbox.Text = (string)values[0]; email_textbox.ForeColor = Color.Black;

            if (values.Count > 1)
            {
                username_textbox.Text = (string)values[1];
                username_textbox.ForeColor = !"".Equals(values[1]) ? Color.Black : Color.Gray;
            }

            if (values.Count > 2)
            {
                firstname_textbox.Text = (string)values[2];
                firstname_textbox.ForeColor = !"".Equals(values[2]) ? Color.Black : Color.Gray;

                middlename_textbox.Text = (string)values[3];
                middlename_textbox.ForeColor = !"".Equals(values[3]) ? Color.Black : Color.Gray;

                lastname_textbox.Text = (string)values[4];
                lastname_textbox.ForeColor = !"".Equals(values[4]) ? Color.Black : Color.Gray;

                cardnumber_textbox.Text = (string)values[5];
                cardnumber_textbox.ForeColor = !"".Equals(values[5]) ? Color.Black : Color.Gray;

                periodicity_comboBox.Text = DB_API.SelectRecurrenceById((int)values[6]);
                if (values[7].Equals(""))
                {
                    values[7] = DateTime.Today.ToString();
                }
                term_dateTimePicker.Value = (DateTime)values[7];
            }
            
            active_checkBox.Checked = (bool)values[8];
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

        private void Back_office_Load(object sender, EventArgs e)
        {

        }
    }
}
