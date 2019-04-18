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
            default_textBoxes();
            
        }

        private void back_office_Load(object sender, EventArgs e)
        {

        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void add_btn_Click(object sender, EventArgs e)
        {
            string username = username_textbox.Text;
            string email = email_textbox.Text;
            string fname = firstname_textbox.Text;
            string mname = middlename_textbox.Text;
            string lname = lastname_textbox.Text;
            string cardNo = cardnumber_textbox.Text;
            string periodicity = (string)periodicity_comboBox.SelectedItem;
            string term = term_dateTimePicker.Value.ToString();

            // for insertions
            List<IDictionary<String, String>> attrValue = new List<IDictionary<string, string>>();
            attrValue.Add(new Dictionary<String, String> { { "email", DB_API.Str(email) } });
            // for selections
            List<IDictionary<String, String>> where = new List<IDictionary<string, string>>();
            where.Add(new Dictionary<String, String>());

            // verify if email exists
            if (email.Equals(""))
            {
                Console.WriteLine("email is a mandatory field");
                return;
            }

            // verify if user exists
            String[] columns = { "*" };
            DataTableReader rdr = DB_API.DBselect("Project.users", columns, attrValue);
            if (rdr.HasRows)
            {
                Console.WriteLine("User already in database");
                return;
            }

            // add user to 'users' table
            attrValue[0].Add("[user_name]", DB_API.Str(username));
            DB_API.DBinsert("Project.users", attrValue[0]);

            // user is subscribed
            if (active_checkBox.Checked)
            {
                // get periodicity id
                String[] coll = { "periodicity" };
                String tableName = "Project.recurrence";
                where[0].Add("designation", DB_API.Str(periodicity));
                List<String> res = new List<String>();
                rdr = DB_API.DBselect(tableName, coll, where);
                while (rdr.Read())
                {
                    res.Add(((int)rdr["periodicity"]).ToString());
                }
                periodicity = res[0];

                // add user to 'subscriptions' table
                attrValue.Clear();
                IDictionary<String, String> dict = new Dictionary<String, String>
                {
                    { "email", DB_API.Str(email) },
                    { "fname", DB_API.Str(fname) },
                    { "mname", DB_API.Str(mname) },
                    { "lname", DB_API.Str(lname) },
                    { "card_number", DB_API.Str(cardNo) },
                    { "periodicity", periodicity },
                    { "term", DB_API.Str(term) },
                    { "active", "1" }
                };
                dict = DB_API.CleanDict(dict);
                attrValue.Add(dict);
                
                DB_API.DBinsert("Project.subscriptions", attrValue[0]);
            }
            populate_listBox();
        }


        private void update_btn_Click(object sender, EventArgs e)
        {
            string username = username_textbox.Text;
            string email = email_textbox.Text;
            string fname = firstname_textbox.Text;
            string mname = middlename_textbox.Text;
            string lname = lastname_textbox.Text;
            string cardNo = cardnumber_textbox.Text;
            string periodicity = periodicity_comboBox.SelectedText;
            string term = term_dateTimePicker.Text.ToString();

            IDictionary<String, String> set = new Dictionary<String, String>();
            List<IDictionary<String, String>> where = new List<IDictionary<String, String>>();
            where.Add(new Dictionary<String, String>());


            // verify if email exists
            if (email.Equals(""))
            {
                Console.WriteLine("email is a mandatory field");
                return;
            }

            // verify if user exists
            String[] columns = { "*" };
            where[0].Add("email", email);
            DataTableReader rdr = DB_API.DBselect("Project.users", columns, where);
            if (rdr.HasRows)
            {
                Console.WriteLine("New user. Use 'add' button to save.");
                return;
            }

            // alter username
            set.Add("[user_name]", DB_API.Str(username));
            DB_API.DBupdate("users", set, where);

            // update subscription
            if (active_checkBox.Checked)
            {
                // get periodicity id
                String[] coll = { "periodicity" };
                String tableName = "Project.recurrence";
                where[0].Clear();
                where[0].Add("designation", DB_API.Str(periodicity));
                List<String> res = new List<String>();
                rdr = DB_API.DBselect(tableName, coll, where);
                while (rdr.Read())
                {
                    res.Add(((int)rdr["periodicity"]).ToString());
                }
                periodicity = res[0];

                // update subscriptions table
                set = new Dictionary<String, String>
                {
                    { "card_number", DB_API.Str(cardNo) },
                    { "fname", DB_API.Str(fname) },
                    { "mname", DB_API.Str(mname) },
                    { "lname", DB_API.Str(lname) },
                    { "term", DB_API.Str(term) },
                    { "periodicity", periodicity },
                    { "active", "1" }
                };
                set = DB_API.nullifyDict(set);

                DB_API.DBupdate("Project.subscriptions", set, where);
            }
            else
            {
                set = new Dictionary<String, String>
                {
                    { "card_number", "null" },
                    { "fname", "null" },
                    { "mname", "null" },
                    { "lname", "null" },
                    { "term", "null" },
                    { "periodicity", "null" },
                    { "active", "0" }
                };

                DB_API.DBupdate("Project.subscriptions", set, where);
            }

            populate_listBox();
        }


        private void delete_btn_Click(object sender, EventArgs e)
        {
            string email = DB_API.Str(email_textbox.Text);

            List<IDictionary<String, String>> where = new List<IDictionary<String, String>>();

            // verify if email exists
            if (email.Equals(""))
            {
                Console.WriteLine("email is a mandatory field");
                return;
            }

            //// verify if user exists
            //String[] columns = { "*" };
            //where.Add("email", email);
            //List<String> res = DB_API.DBselect("Project.users", columns, where);
            //if (res.Count == 0)
            //{
            //    Console.WriteLine("User does not exist, cannot be deleted.");
            //    return;
            //}

            //// delete user in 'subscriptions' table
            //where.Add("email", email);
            //DB_API.DBdelete("Project.subscriptions", where);

            // delete user form 'users' table
            DB_API.DBdelete("Project.users", where);

            populate_listBox();
        }

        private void users_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear_textBoxes();

            string email = (string)users_listbox.SelectedItem;
            String[] columns = { "[user_name]" };
            List<IDictionary<String, String>> where = new List<IDictionary<string, string>>();
            where.Add(new Dictionary<String, String> { { "email", DB_API.Str(email) } });
            List<String> res = new List<String>();
            res.Add(email);
            DataTableReader rdr = DB_API.DBselect("Project.users", columns, where);
            while (rdr.Read())
            {
                res.Add((string)rdr["user_name"]);
            }
            String[] col = { "*" };
            rdr = DB_API.DBselect("Project.subscriptions", col, where);
            while (rdr.Read())
            {
                res.Add((string)rdr["fname"]);
                res.Add((string)rdr["mname"]);
                res.Add((string)rdr["lname"]);
                res.Add((string)rdr["card_number"]);
                res.Add(((int)rdr["periodicity"]).ToString());
                res.Add(((DateTime)rdr["term"]).ToString());
                res.Add(((int)rdr["active"]).ToString());
            }

            update_textBoxes(res);
        }

        private void populate_listBox()
        {
            String[] columns = { "email" };
            String tableName = "Project.users";
            List<IDictionary<String, String>> where = new List<IDictionary<string, string>>();
            List<String> res = new List<String>();
            DataTableReader rdr = DB_API.DBselect(tableName, columns, where);
            while (rdr.Read())
            {
                res.Add((string)rdr["email"]);
            }
            users_listbox.DataSource = res;
        }

        private void populate_comboBox()
        {
            String[] columns = { "designation" };
            String tableName = "Project.recurrence";
            List<IDictionary<String, String>> where = new List<IDictionary<string, string>>();
            List<String> res = new List<String>();
            DataTableReader rdr = DB_API.DBselect(tableName, columns, where);
            while (rdr.Read())
            {
                res.Add((string)rdr["designation"]);
            }
            periodicity_comboBox.DataSource = res;
        }

        private void update_textBoxes(List<String> values)
        {
            if (users_listbox.SelectedIndex > -1) {
                
                foreach (string str in values)
                {
                    Console.WriteLine(str);
                }
                email_textbox.Text = values[0];
                if (values.Count > 1)
                {
                    username_textbox.Text = values[1];
                }
                if (values.Count > 2)
                {
                    extra_textBox.Text = values[2];
                    firstname_textbox.Text = values[2];
                    middlename_textbox.Text = values[3];
                    lastname_textbox.Text = values[4];
                    cardnumber_textbox.Text = values[5];
                    String[] columns = { "designation" };
                    List<IDictionary<String, String>> where = new List<IDictionary<string, string>>();
                    where.Add(new Dictionary<String, String> { { "periodicity", values[6] } });
                    List<String> res = new List<String>();
                    DataTableReader rdr = DB_API.DBselect("Project.recurrence", columns, where);
                    while (rdr.Read())
                    {
                        res.Add((string)rdr["designation"]);
                    }
                    periodicity_comboBox.Text = res[0];
                    term_dateTimePicker.Value = DateTime.Parse(values[7]);

                    
                }
                if (values.Count > 2 && values[8].Equals("1"))
                {
                    active_checkBox.Checked = true;
                }
                else
                {
                    active_checkBox.Checked = false;
                }
            }
        }

        private void clear_textBoxes()
        {
            Console.WriteLine("#####################" + "CLEAR TEXBOX");
            email_textbox.Text = "";
            Console.WriteLine("username has (before clear): " + username_textbox.Text);
            username_textbox.Text = "";
            Console.WriteLine("username has (after clear): " + username_textbox.Text);
            default_textBoxes();
            firstname_textbox.Text = "";
            middlename_textbox.Text = "";
            lastname_textbox.Text = "";
            cardnumber_textbox.Text = "";
            populate_comboBox();
            term_dateTimePicker.Value = DateTime.Now;
            active_checkBox.Checked = false;
        }

        private void default_textBoxes()
        {
            extra_textBox.Text = "default";
            extra_textBox.ForeColor = Color.Gray;
        }

        private void extra_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //extra_textBox.Text = "";
            //extra_textBox.ForeColor = Color.Black;
        }

        private void extra_textBox_MouseClick(object sender, MouseEventArgs e)
        {
            extra_textBox.Text = "";
            extra_textBox.ForeColor = Color.Black;
        }

        private void extra_textBox_Leave(object sender, EventArgs e)
        {
            if (extra_textBox.Text.Equals(""))
            {
                default_textBoxes();
            }
        }
    }
}
