using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueBudget_DB
{
    public partial class GoalsForm : Form
    {

        string user_email;
        int account_id;
        string none = "--- none ---";
        List<string> user_goals;
        IDictionary<string, int> categories;

        public GoalsForm(string user_email, int account_id)
        {
            InitializeComponent();
            this.user_email = user_email;
            this.account_id = account_id;
        }

        private void Goals_Load(object sender, EventArgs e)
        {
            UpdateCategories();
            PopulateCategoryComboBox();
            PopulateGoalsListBox();
            DefaultTextboxes();

            // disable typing in comboBoxes by changing style
            Categories_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            Subcategories_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            // make comboBoxes white again
            Categories_comboBox.FlatStyle = FlatStyle.Popup;
            Subcategories_comboBox.FlatStyle = FlatStyle.Popup;
        }


        // -------------------------------------------------------------------
        // BUTTONS -----------------------------------------------------------
        // -------------------------------------------------------------------

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            // get values from textboxes
            string goal_name = goalname_textBox.ForeColor == Color.Black ? goalname_textBox.Text : "";
            string amount = goalamount_textBox.ForeColor == Color.Black ? goalamount_textBox.Text : "";
            double goal_amount = DB_API.UnMoneyfy(amount);
            int cat_id = categories[Categories_comboBox.SelectedItem.ToString()];
            DateTime term = term_dateTimePicker.Value;

            // verify if field is filled
            if (goal_name.Equals(""))
            {
                ErrorMessenger.EmptyField("Goal name");
                return;
            }
            if (amount.Equals(""))
            {
                ErrorMessenger.EmptyField("Goal amount");
                return;
            }

            // add new goal
            if (!Subcategories_comboBox.SelectedItem.ToString().Equals(this.none))
            {
                cat_id = categories[Subcategories_comboBox.SelectedItem.ToString()];
            }
            try
            {
                DB_API.InsertGoal(account_id, cat_id, goal_name, goal_amount, term);
            }
            catch (SqlException ex)
            {
                ErrorMessenger.Exception(ex);
            }

            PopulateGoalsListBox();
        }

        // -------------------------------------------------------------------
        // LIST AND COMBO BOXES ----------------------------------------------
        // -------------------------------------------------------------------

        private void UpdateCategories()
        {
            this.categories = new Dictionary<string, int>();
            var rdr = DB_API.SelectAccountCategories(account_id);
            while (rdr.Read())
            {
                string cat_name = rdr[DB_API.CategoryEnt.name.ToString()].ToString();
                int cat_id = (int)rdr[DB_API.CategoryEnt.category_id.ToString()];
                this.categories[cat_name] = cat_id;
            }
        }

        private void PopulateCategoryComboBox()
        {
            var res = new List<string>();
            foreach (KeyValuePair<string, int> entry in this.categories)
            {
                if (entry.Value % 100 == 0)
                {
                    res.Add(entry.Key);
                }
            }
            Categories_comboBox.DataSource = res;
        }

        private void Categories_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Categories_comboBox.SelectedItem.ToString().Equals(this.none))
            {
                return;
            }
            PopulateSubCategoryComboBox(this.categories[Categories_comboBox.SelectedItem.ToString()]);
        }

        private void Goals_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value
            string goal_name = Goals_listBox.SelectedItem.ToString();

            var rdr = DB_API.SelectGoal(account_id, goal_name);
            while (rdr.Read())
            {
                int category_id = (int)rdr[DB_API.GoalEnt.category_id.ToString()];
                string cat_name = "";
                string sub_cat_name = "";
                if (category_id % 100 == 0)
                {
                    foreach (KeyValuePair<string, int> entry in this.categories)
                    {
                        if (entry.Value == category_id)
                        {
                            cat_name = entry.Key;
                            sub_cat_name = this.none;
                            break;
                        }
                    }
                }
                else
                {
                    int cat_id = -1;
                    foreach (KeyValuePair<string, int> entry in this.categories)
                    {
                        if (entry.Value == category_id)
                        {
                            sub_cat_name = entry.Key;
                            cat_id = category_id / 100 * 100;
                            break;
                        }
                    }
                    foreach (KeyValuePair<string, int> entry in this.categories)
                    {
                        if (entry.Value == cat_id)
                        {
                            cat_name = entry.Key;
                            break;
                        }
                    }
                }

                // update TextBoxes
                Categories_comboBox.Text = cat_name;
                Subcategories_comboBox.Text = sub_cat_name;
                goalname_textBox.Text = goal_name;
                goalname_textBox.ForeColor = Color.Black;
                double goal_amount = Double.Parse(rdr[DB_API.GoalEnt.amount.ToString()].ToString());
                goalamount_textBox.Text = goal_amount.ToString();
                goalamount_textBox.ForeColor = Color.Black;
                term_dateTimePicker.Value = DateTime.Parse(rdr[DB_API.GoalEnt.term.ToString()].ToString());

                // change GoalState Label
                rdr = DB_API.SelectWalletByName(account_id, goal_name);
                double balance = 0.0;
                while (rdr.Read())
                {
                    balance = Double.Parse(rdr[DB_API.WalletEnt.balance.ToString()].ToString());
                }

                if (balance < goal_amount)
                {
                    Goalstate_label2.Text = "Keep going!\r\n" + (balance / goal_amount * 100) + "% complete!\r\n" + (goal_amount - balance) + "$ to go!";
                }
                if (balance >= goal_amount)
                {
                    Goalstate_label2.Text = "Congratulations!\r\nGoal reached!\r\nSaved " + balance + "!";
                }
            }

        }

        private void PopulateGoalsListBox()
        {
            // get categories from DB
            var rdr = DB_API.SelectAccountGoals(account_id);

            // extract goal names for listBox
            this.user_goals = new List<string>();
            var res = new List<string>();
            while (rdr.Read())
            {
                string goal_name = rdr[DB_API.CategoryEnt.name.ToString()].ToString();
                user_goals.Add(goal_name);
                res.Add(goal_name);
            }
            Goals_listBox.DataSource = new List<string>(res);
        }

        private void PopulateSubCategoryComboBox(int cat_id)
        {
            var res = new List<string>();
            res.Add(this.none);
            foreach (KeyValuePair<string, int> entry in this.categories)
            {
                if (entry.Value / 100 == cat_id / 100 && entry.Value != cat_id)
                {
                    res.Add(entry.Key);
                }
            }
            Subcategories_comboBox.DataSource = res;
        }

        // -------------------------------------------------------------------
        // TEXTBOX HINTS -----------------------------------------------------
        // -------------------------------------------------------------------

        private void DefaultTextboxes()
        {
            goalname_textBox.Text = "goal name";
            goalname_textBox.ForeColor = Color.Gray;
            goalamount_textBox.Text = "goal value";
            goalamount_textBox.ForeColor = Color.Gray;
            Goalstate_label2.Text = "";
        }

        private void Goalname_textBox_Enter(object sender, EventArgs e)
        {
            goalname_textBox.Text = "";
            goalname_textBox.ForeColor = Color.Black;
        }
        private void Goalname_textBox_Leave(object sender, EventArgs e)
        {
            if (goalname_textBox.Text.Equals(""))
            {
                goalname_textBox.Text = "goal name";
                goalname_textBox.ForeColor = Color.Gray;
            }
        }

        private void Goalamount_textBox_Enter(object sender, EventArgs e)
        {
            goalamount_textBox.Text = "";
            goalamount_textBox.ForeColor = Color.Black;
        }
        private void Goalamount_textBox_Leave(object sender, EventArgs e)
        {
            goalamount_textBox.Text = DB_API.Moneyfy(goalamount_textBox.Text);

            if (goalamount_textBox.Text.Equals(""))
            {
                goalamount_textBox.Text = "goal value";
                goalamount_textBox.ForeColor = Color.Gray;
            }
        }

        
    }
}
