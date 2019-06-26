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
    public partial class BudgetsForm : Form
    {

        string user_email;
        int account_id;
        IDictionary<string, int> categories;
        IDictionary<string, int> budgetsDict;
        List<String> months = new List<String>()
        {
            "---", "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"
        };

        public BudgetsForm(string user_email, int account_id)
        {
            InitializeComponent();
            this.user_email = user_email;
            this.account_id = account_id;
            
        }

        private void Budget_Load(object sender, EventArgs e)
        {
            StartYear_numericBox.Maximum = 5000;
            EndYear_numericBox.Maximum = 5000;
            StartYear_numericBox.Minimum = 0;
            EndYear_numericBox.Minimum = 0;
            StartYear_numericBox.Value = 2019;
            EndYear_numericBox.Value = 2019;

            Category_textBox.ReadOnly = true;
            Subcategory_textBox.ReadOnly = true;
            Type_comboBox.Enabled = false;
            Budget_textBox.ReadOnly = true;
            StartMonth_comboBox.Enabled = false;
            StartYear_numericBox.Enabled = false;
            EndMonth_comboBox.Enabled = false;
            EndYear_numericBox.Enabled = false;

            PopulateCategoriesListBox();
            PopulateComboBoxes();

            // disable typing in comboBoxes by changing style
            StartMonth_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            EndMonth_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // make comboBoxes white again
            StartMonth_comboBox.FlatStyle = FlatStyle.Popup;
            EndMonth_comboBox.FlatStyle = FlatStyle.Popup; 
        }

        // -------------------------------------------------------------------
        // BUTTONS -----------------------------------------------------------
        // -------------------------------------------------------------------

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Newcategory_btn_Click(object sender, EventArgs e)
        {
            Category_textBox.ReadOnly = false;
            Subcategory_textBox.ReadOnly = true;
            Type_comboBox.Enabled = true;
            Type_comboBox.Enabled = true;
            Budget_textBox.ReadOnly = true;
            StartMonth_comboBox.Enabled = false;
            StartYear_numericBox.Enabled = false;
            EndMonth_comboBox.Enabled = false;
            EndYear_numericBox.Enabled = false;
        }

        private void Newsubcategory_btn_Click(object sender, EventArgs e)
        {
            Category_textBox.ReadOnly = true;
            Subcategory_textBox.ReadOnly = false;
            Type_comboBox.Enabled = false;
            Budget_textBox.ReadOnly = true;
            StartMonth_comboBox.Enabled = false;
            StartYear_numericBox.Enabled = false;
            EndMonth_comboBox.Enabled = false;
            EndYear_numericBox.Enabled = false;
        }

        private void NewBudget_btn_Click(object sender, EventArgs e)
        {
            Category_textBox.ReadOnly = true;
            Subcategory_textBox.ReadOnly = true;
            Type_comboBox.Enabled = false;
            Budget_textBox.ReadOnly = false;
            StartMonth_comboBox.Enabled = true;
            StartYear_numericBox.Enabled = true;
            EndMonth_comboBox.Enabled = true;
            EndYear_numericBox.Enabled = true;
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            // get values from textboxes
            string cat_name = Category_textBox.Text;
            int cat_id;
            string sub_cat_name = Subcategory_textBox.Text;
            string type_name = Type_comboBox.SelectedItem.ToString();
            int type_id = DB_API.SelectCategoryTypeIdByDesignation(type_name);

            // save new category
            if (!Category_textBox.ReadOnly)
            {   
                // verify if field is filled
                if (cat_name.Equals(""))
                {
                    ErrorMessenger.EmptyField("Category");
                    return;
                }

                // verify if new name already exists
                var rdr = DB_API.SelectAccountCategories(account_id);
                while (rdr.Read())
                {
                    if (cat_name.Equals(rdr[DB_API.CategoryEnt.name.ToString()].ToString())) {
                        ErrorMessenger.Error("Category name already exists");
                        return;
                    }
                }

                // add new category
                DB_API.AddCategoryToAccount(account_id, cat_name, type_id);
            }

            // save new sub category
            if (!Subcategory_textBox.ReadOnly)
            {
                cat_id = this.categories[cat_name];

                // verify if field is filled
                if (sub_cat_name.Equals(""))
                {
                    ErrorMessenger.EmptyField("Sub-category");
                    return;
                }

                // verify if new name already exists
                var rdr = DB_API.SelectAccountCategories(account_id);
                while (rdr.Read())
                {
                    if (sub_cat_name.Equals(rdr[DB_API.CategoryEnt.name.ToString()].ToString()))
                    {
                        ErrorMessenger.Error("Category name already exists");
                        return;
                    }
                }

                // add new category
                DB_API.AddSubCategoryToAccount(cat_id, account_id, sub_cat_name, type_id);
            }

            // save new budget
            if (Category_textBox.ReadOnly && Subcategory_textBox.ReadOnly)
            {
                if (Budget_textBox.Text.Equals(""))
                {
                    ErrorMessenger.EmptyField("Monthly Budget");
                    return;
                }

                if (StartMonth_comboBox.SelectedIndex == 0 || EndMonth_comboBox.SelectedIndex == 0)
                {
                    ErrorMessenger.EmptyField("Start month and end month");
                    return;
                }

                cat_id = this.categories[cat_name];
                if (!sub_cat_name.Equals(""))
                {
                    cat_id = this.categories[sub_cat_name];
                }

                double amount = DB_API.UnMoneyfy(Budget_textBox.Text);
                int startMonth = StartMonth_comboBox.SelectedIndex;
                int startYear = (int)StartYear_numericBox.Value;
                int endMonth = EndMonth_comboBox.SelectedIndex;
                int endYear = (int)EndYear_numericBox.Value;
                DateTime startDate = DateTime.Parse(startYear + "/" + startMonth + "/01");
                DateTime endDate;
                
                if (endMonth == 2)
                {
                    endDate = DateTime.Parse(endYear + "/" + endMonth + "/28");
                }
                else
                {
                    endDate = DateTime.Parse(endYear + "/"+ endMonth + "/30");
                }

                // verify that endaDate is bigger than startDate
                if (startDate.CompareTo(endDate) > 0)
                {
                    ErrorMessenger.InvalidData("End date");
                    return;
                }

                // insert new budget
                DB_API.InsertBudget(account_id, cat_id, amount, startDate, endDate);
            }

            PopulateCategoriesListBox();
        }

        // -------------------------------------------------------------------
        // LIST AND COMBO BOXES --------------------------------------------------------
        // -------------------------------------------------------------------

        private void Categories_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {

            // get selected item value
            string selected = (string)Categories_listbox.SelectedItem;
            string cat_name = selected;
            string sub_cat_name = "";
            int cat_id;
            int sub_cat_id;
            int cat_type_id = -1;
            string cat_type_name;

            try
            {
                cat_id = this.categories[selected];
                PopulateBudgetsListBox(account_id, cat_id);
            }
            catch
            {
                selected = selected.Substring(5);
                sub_cat_id = this.categories[selected];
                sub_cat_name = selected;
                cat_id = sub_cat_id / 100 * 100;
                foreach (KeyValuePair<string, int> pair in this.categories)
                {
                    if (categories[pair.Key] == cat_id)
                    {
                        cat_name = pair.Key;
                        break;
                    }
                }
                PopulateBudgetsListBox(account_id, sub_cat_id);
            }

            // update TextBoxes
            Category_textBox.Text = cat_name;
            Subcategory_textBox.Text = sub_cat_name;
            var rdr = DB_API.SelectCategory(cat_id);
            while (rdr.Read())
            {
                cat_type_id = (int)rdr[DB_API.CategoryEnt.category_type_id.ToString()];
                break;
            }
            cat_type_name = DB_API.SelectCategoryTypeDesignationById(cat_type_id);
            Type_comboBox.Text = cat_type_name;
        }

        private void Budgets_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value
            string budget = (string)Budgets_listBox.SelectedItem;
            int budget_id = this.budgetsDict[budget];

            // search DB for budgets
            var rdr = DB_API.SelectBudget(budget_id);
            var res = new Dictionary<string, int>();
            while (rdr.Read())
            {
                StartMonth_comboBox.Text = months[DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Month];
                StartYear_numericBox.Value = DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Year;
                EndMonth_comboBox.Text = months[DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Month];
                EndYear_numericBox.Value = DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Year;
                Budget_textBox.Text = DB_API.Moneyfy(rdr[DB_API.BudgetEnt.amount.ToString()].ToString());
            }
        }

        private void PopulateCategoriesListBox()
        {
            // get categories from DB
            var rdr = DB_API.SelectAccountCategories(account_id);

            // verify if account has no categories
            if (!rdr.HasRows)
            {
                ErrorMessenger.Warning("Account has no categories!");
                Categories_listbox.DataSource = new List<String>();
                return;
            }

            // extract category names for listBox
            this.categories = new Dictionary<string, int>();
            var res = new Dictionary<string, int>();
            while (rdr.Read())
            {
                string cat_name = rdr[DB_API.CategoryEnt.name.ToString()].ToString();
                int cat_id = (int)rdr[DB_API.CategoryEnt.category_id.ToString()];
                categories[cat_name] = cat_id;
                if (cat_id % 100 > 0)
                {
                    cat_name = "  -> " + cat_name;
                }
                res[cat_name] = cat_id;
            }
            Categories_listbox.DataSource = new List<string>(res.Keys);
        }

        private void PopulateBudgetsListBox(int account_id, int cat_id)
        {
            // search DB for budgets
            this.budgetsDict = new Dictionary<string, int>();
            var rdr = DB_API.SelectUserCategoryBudgets(account_id, cat_id);
            var res = new Dictionary<string, int>();
            while (rdr.Read())
            {
                string start_month = DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Month.ToString();
                string start_year = DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Year.ToString();
                string end_month = DateTime.Parse(rdr[DB_API.BudgetEnt.end_date.ToString()].ToString()).Month.ToString();
                string end_year = DateTime.Parse(rdr[DB_API.BudgetEnt.end_date.ToString()].ToString()).Year.ToString();
                string amount = rdr[DB_API.BudgetEnt.amount.ToString()].ToString();
                res[start_month + "/" + start_year + "-" + end_month + "/" + end_year + " - " + amount + "$"] = (int)rdr[DB_API.BudgetEnt.budget_id.ToString()];
                budgetsDict[start_month + "/" + start_year + "-" + end_month + "/" + end_year + " - " + amount + "$"] = (int)rdr[DB_API.BudgetEnt.budget_id.ToString()];
            }

            Budgets_listBox.DataSource = new List<string>(res.Keys);
        }

        private void PopulateComboBoxes()
        {
            StartMonth_comboBox.DataSource = months;
            EndMonth_comboBox.DataSource = new List<string>(months);

            var rdr = DB_API.SelectAllCategoryTypes();
            var res = new List<string>();
            while (rdr.Read())
            {
                res.Add(rdr[DB_API.CategoryTypeEnt.designation.ToString()].ToString());
            }
            Type_comboBox.DataSource = res;
        }

        private void Budget_textBox_Enter(object sender, EventArgs e)
        {
            Budget_textBox.Text = "";
        }
        private void Budget_textBox_Leave(object sender, EventArgs e)
        {
            Budget_textBox.Text = DB_API.Moneyfy(Budget_textBox.Text);
        }

        
    }
}
