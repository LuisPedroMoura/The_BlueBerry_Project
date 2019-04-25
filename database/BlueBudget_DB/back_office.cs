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
            populate_listBox();
            populate_comboBox();
            defaultTextboxes();
        }

        // -------------------------------------------------------------------
        // BUTTONS -----------------------------------------------------------
        // -------------------------------------------------------------------

        private void back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void add_btn_Click(object sender, EventArgs e)
        {
            // get values from text boxes if they where inserted by user
            string username = username_textbox.ForeColor == Color.Black ? username_textbox.Text : "";
            string email = email_textbox.ForeColor == Color.Black ? email_textbox.Text : "";
            string fname = firstname_textbox.ForeColor == Color.Black ? firstname_textbox.Text : "";
            string mname = middlename_textbox.ForeColor == Color.Black ? middlename_textbox.Text : "";
            string lname = lastname_textbox.ForeColor == Color.Black ? lastname_textbox.Text : "";
            string cardNo = cardnumber_textbox.ForeColor == Color.Black ? cardnumber_textbox.Text : "";
            string periodicity = (string)periodicity_comboBox.SelectedItem;
            string term = term_dateTimePicker.Value.ToString();

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

            // create data structures for insertions
            var attrValue = DB_API.attrValue();
            // create data structures for selections
            var where = DB_API.where();

            // verify if user exists, if so, cannot be added, only updated
            var exists = DB_API.Exists("Project.users", "email", email);
            if (exists)
            {
                notifications_textbox.Text = "ERROR:\nUser already exists!";
                return;
            }

            // add user
            attrValue = new Dictionary<String, String>
                {
                    { "user_name", username },
                    { "email", email },
                    { "fname", fname },
                    { "mname", mname },
                    { "lname", lname },
                    { "card_number", cardNo },
                    { "periodicity", periodicity },
                    { "term", term },
                    { "active", "1" }
                };
            DB_API.DBexecProc("pr_insert_user", attrValue);

            // upadte listBox with new user
            populate_listBox();
            notifications_textbox.Text = "SUCCESS:\n New user was added to database!";
        }


        private void update_btn_Click(object sender, EventArgs e)
        {
            // get values from text boxes if they where inserted by user
            string username = username_textbox.ForeColor == Color.Black ? username_textbox.Text : "";
            string email = email_textbox.ForeColor == Color.Black ? email_textbox.Text : "";
            string fname = firstname_textbox.ForeColor == Color.Black ? firstname_textbox.Text : "";
            string mname = middlename_textbox.ForeColor == Color.Black ? middlename_textbox.Text : "";
            string lname = lastname_textbox.ForeColor == Color.Black ? lastname_textbox.Text : "";
            string cardNo = cardnumber_textbox.ForeColor == Color.Black ? cardnumber_textbox.Text : "";
            string periodicity = (string)periodicity_comboBox.SelectedItem;
            string term = term_dateTimePicker.Value.ToString();

            // create data structures for insertions
            var attrValue = DB_API.attrValue();

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
            var exists = DB_API.Exists("Project.users", "email", email);
            if (!exists)
            {
                notifications_textbox.Text = "ERROR:\nUser does not exist. Unable to update!";
                return;
            }

            // update user
            attrValue = new Dictionary<String, String>
                    {
                        { "user_name", username },
                        { "email", email },
                        { "fname", fname },
                        { "mname", mname },
                        { "lname", lname },
                        { "card_number", cardNo },
                        { "periodicity", periodicity },
                        { "term", term },
                        { "active", "1" }
                    };
            DB_API.DBexecProc("pr_update_user", attrValue);
              
            // update listBox
            populate_listBox();
            notifications_textbox.Text = "SUCCESS:\n User " + email + " was updated!";
        }


        private void delete_btn_Click(object sender, EventArgs e)
        {
            string email = email_textbox.ForeColor == Color.Black ? email_textbox.Text : "";

            // verify if email field is filled
            if (email.Equals(""))
            {
                notifications_textbox.Text = "ERROR:\nEmail is a mandatory field";
                return;
            }

            List<IDictionary<String, String>> where = new List<IDictionary<String, String>>();
            where.Add(new Dictionary<String, String> { { "email", email } });

            // delete user form 'users' table
            DB_API.DBexecProc("pr_delete_user", where[0]);

            populate_listBox();
        }

        // -------------------------------------------------------------------
        // LIST BOX ----------------------------------------------------------
        // -------------------------------------------------------------------

        private void users_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value
            string email = (string)users_listbox.SelectedItem;
            // search DB for user
            String[] columns = { "[user_name]" };
            var where = DB_API.where();
            where["email"] = email;
            var res = new List<String>();
            res.Add(email);
            var rdr = DB_API.DBselect("Project.users", columns, where);
            while (rdr.Read())
            {
                res.Add((string)rdr["user_name"]);
            }
            // search DB for subscriptions
            rdr = DB_API.DBselect("Project.subscriptions", new string[] { "*" }, where);
            while (rdr.Read())
            {
                res.Add(rdr["fname"].ToString());
                res.Add(rdr["mname"].ToString());
                res.Add(rdr["lname"].ToString());
                res.Add(rdr["card_number"].ToString());
                res.Add(rdr["periodicity"].ToString());
                res.Add(rdr["term"].ToString());
                res.Add(rdr["active"].ToString());
            }
            update_textBoxes(res);
        }

        private void populate_listBox()
        {
            var res = new List<String>();
            var rdr = DB_API.DBselect("Project.users", new String[] { "email" }, DB_API.where());
            while (rdr.Read())
            {
                res.Add(rdr["email"].ToString());
            }
            users_listbox.DataSource = res;
        }

        // -------------------------------------------------------------------
        // TEXT BOXES --------------------------------------------------------
        // -------------------------------------------------------------------

        private void populate_comboBox()
        {
            var res = new List<String>();
            var rdr = DB_API.DBselect("Project.recurrence", new string[] { "designation" }, DB_API.where());
            while (rdr.Read())
            {
                res.Add((string)rdr["designation"]);
            }
            periodicity_comboBox.DataSource = res;
        }

        private void update_textBoxes(List<String> values)
        {
            defaultTextboxes();

            email_textbox.Text = values[0]; email_textbox.ForeColor = Color.Black;

            if (values.Count > 1)
            {
                username_textbox.Text = values[1];
                username_textbox.ForeColor = !"".Equals(values[1]) ? Color.Black : Color.Gray;
            }

            if (values.Count > 2)
            {
                firstname_textbox.Text = values[2];
                firstname_textbox.ForeColor = !"".Equals(values[2]) ? Color.Black : Color.Gray;

                middlename_textbox.Text = values[3];
                middlename_textbox.ForeColor = !"".Equals(values[3]) ? Color.Black : Color.Gray;

                lastname_textbox.Text = values[4];
                lastname_textbox.ForeColor = !"".Equals(values[4]) ? Color.Black : Color.Gray;

                cardnumber_textbox.Text = values[5];
                cardnumber_textbox.ForeColor = !"".Equals(values[5]) ? Color.Black : Color.Gray;

                var where = DB_API.where();
                where["periodicity"] = values[6];
                var res = new List<String>();
                var rdr = DB_API.DBselect("Project.recurrence", new String[] { "designation" }, where);
                while (rdr.Read())
                {
                    res.Add((string)rdr["designation"]);
                }
                periodicity_comboBox.Text = res[0];

                term_dateTimePicker.Value = DateTime.Parse(values[7]);
                    
            }
            
            if (values.Count > 2 && values[8].Equals("True"))
            {
                active_checkBox.Checked = true;
            }
            else
            {
                active_checkBox.Checked = false;
            }
        }

        // -------------------------------------------------------------------
        // TEXT BOXES HINTS --------------------------------------------------
        // -------------------------------------------------------------------

        private void defaultTextboxes()
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
            populate_comboBox();
            term_dateTimePicker.Value = DateTime.Now;
            active_checkBox.Checked = false;
        }

        private void email_textbox_Enter(object sender, EventArgs e)
        {
            email_textbox.Text = "";
            email_textbox.ForeColor = Color.Black;
        }
        private void email_textbox_Leave_1(object sender, EventArgs e)
        {
            if (email_textbox.Text.Equals(""))
            {
                email_textbox.Text = "email";
                email_textbox.ForeColor = Color.Gray;
            }
        }

        private void username_textbox_Enter(object sender, EventArgs e)
        {
            username_textbox.Text = "";
            username_textbox.ForeColor = Color.Black;
        }
        private void username_textbox_Leave_1(object sender, EventArgs e)
        {
            if (username_textbox.Text.Equals(""))
            {
                username_textbox.Text = "username";
                username_textbox.ForeColor = Color.Gray;
            }
        }

        private void firstname_textbox_Enter(object sender, EventArgs e)
        {
            firstname_textbox.Text = "";
            firstname_textbox.ForeColor = Color.Black;
        }
        private void firstname_textbox_Leave_1(object sender, EventArgs e)
        {
            if (firstname_textbox.Text.Equals(""))
            {
                firstname_textbox.Text = "first name";
                firstname_textbox.ForeColor = Color.Gray;
            }
        }

        private void middlename_textbox_Enter(object sender, EventArgs e)
        {
            middlename_textbox.Text = "";
            middlename_textbox.ForeColor = Color.Black;
        }
        private void middlename_textbox_Leave_1(object sender, EventArgs e)
        {
            if (middlename_textbox.Text.Equals(""))
            {
                middlename_textbox.Text = "middle name";
                middlename_textbox.ForeColor = Color.Gray;
            }
        }

        private void lastname_textbox_Enter(object sender, EventArgs e)
        {
            lastname_textbox.Text = "";
            lastname_textbox.ForeColor = Color.Black;
        }
        private void lastname_textbox_Leave_1(object sender, EventArgs e)
        {
            if (lastname_textbox.Text.Equals(""))
            {
                lastname_textbox.Text = "last name";
                lastname_textbox.ForeColor = Color.Gray;
            }
        }

        private void cardnumber_textbox_Enter(object sender, EventArgs e)
        {
            cardnumber_textbox.Text = "";
            cardnumber_textbox.ForeColor = Color.Black;
        }
        private void cardnumber_textbox_Leave_1(object sender, EventArgs e)
        {
            if (cardnumber_textbox.Text.Equals(""))
            {
                cardnumber_textbox.Text = "card number";
                cardnumber_textbox.ForeColor = Color.Gray;
            }
        }

        private void back_office_Load(object sender, EventArgs e)
        {

        }
    }
}
