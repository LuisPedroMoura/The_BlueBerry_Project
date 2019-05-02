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
    public partial class budget : Form
    {

        string user_email;
        int account_id;
        IDictionary<string, int> categories;
        IDictionary<string, int> budgets;
        List<String> months = new List<String>()
        {
            "---", "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"
        };

        public budget(string user_email, int account_id)
        {
            InitializeComponent();
            this.user_email = user_email;
            this.account_id = account_id;
            PopulateCategoriesListBox();
            PopulateComboBoxes();
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
            Type_comboBox.Enabled = true;
        }

        private void Newsubcategory_btn_Click(object sender, EventArgs e)
        {
            Subcategory_textBox.ReadOnly = false;
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            // save new category
            if (!Category_textBox.ReadOnly)
            {
                string name = Category_textBox.ForeColor == Color.Black ? Category_textBox.Text : "";
                int type_id = DB_API.SelectCategoryTypeByDesignation(Type_comboBox.SelectedItem.ToString());

                // verify if field is filled
                if (Category_textBox.Text.Equals(""))
                {
                    Notifications.Text = ErrorMessenger.EmptyField("Category");
                    return;
                }

                // add new category
                try
                {
                    DB_API.AddCategoryToAccount(account_id, name, type_id);
                }
                catch (SqlException ex)
                {
                    Notifications.Text = ErrorMessenger.Exception(ex);
                }
            }

            // save new sub category
            if (!Subcategory_textBox.ReadOnly)
            {
                string category_name = Category_textBox.ForeColor == Color.Black ? Category_textBox.Text : "";
                int category_id = this.categories[category_name];
                string subCategory_name = Subcategory_textBox.ForeColor == Color.Black ? Subcategory_textBox.Text : "";
                int type_id = DB_API.SelectCategoryTypeByDesignation(Type_comboBox.SelectedItem.ToString());

                // verify if field is filled
                if (Subcategory_textBox.Text.Equals(""))
                {
                    Notifications.Text = ErrorMessenger.EmptyField("Sub-category");
                    return;
                }

                // add new category
                try
                {
                    DB_API.AddSubCategoryToAccount(category_id, account_id, subCategory_name, type_id);
                }
                catch (SqlException ex)
                {
                    Notifications.Text = ErrorMessenger.Exception(ex);
                }
            }

            // save new budget
            if (Category_textBox.ReadOnly && Subcategory_textBox.ReadOnly)
            {
                string category_name = Category_textBox.ForeColor == Color.Black ? Category_textBox.Text : "";
                int category_id = this.categories[category_name];
                double amount;
                try
                {
                    amount = Double.Parse(Budget_textBox.Text);
                }
                catch
                {
                    Notifications.Text = ErrorMessenger.WrongFormat("Budget");
                    return;
                }
                int startMonth = StartMonth_comboBox.SelectedIndex;
                int startYear = (int)StartYear_numericBox.Value;
                int endMonth = EndMonth_comboBox.SelectedIndex;
                int endYear = (int)EndYear_numericBox.Value;
                DateTime startDate = DateTime.Parse("01/" + startMonth + "/" + startYear);
                DateTime endDate;
                if (endMonth == 2)
                {
                    endDate = DateTime.Parse("28/" + endMonth + "/" + endYear);
                }
                else
                {
                    endDate = DateTime.Parse("30/" + endMonth + "/" + endYear);
                }

                // insert new budget
                try
                {
                    DB_API.InsertBudget(account_id, category_id, amount, startDate, endDate);
                }
                catch (SqlException ex)
                {
                    Notifications.Text = ErrorMessenger.Exception(ex);
                }
            }
            
            // block fields
            Category_textBox.ReadOnly = true;
            Subcategory_textBox.ReadOnly = true;
            Type_comboBox.Enabled = false;
        }

        // -------------------------------------------------------------------
        // LIST AND COMBO BOXES --------------------------------------------------------
        // -------------------------------------------------------------------

        private void Categories_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value
            string category = (string)Categories_listbox.SelectedItem;
            Console.WriteLine(category);
            Console.WriteLine(categories.ToString());
            int category_id = this.categories[category];

            // search DB for budgets
            var rdr = DB_API.SelectUserCategoryBudgets(account_id, category_id);
            var res = new Dictionary<string, int>();
            while (rdr.Read())
            {
                string start_month = DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Month.ToString();
                string start_year = DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Year.ToString();
                string end_month = DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Month.ToString();
                string end_year = DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Year.ToString();
                string amount = rdr[DB_API.BudgetEnt.amount.ToString()].ToString();
                res[start_month + "/" + start_year + "-" + end_month + "/" + end_year + " - " + amount] = (int)rdr[DB_API.BudgetEnt.budget_id.ToString()];
            }

            Budgets_listBox.DataSource = new List<string>(res.Keys);
            this.budgets = res;
        }

        private void Budgets_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value
            string budget = (string)Budgets_listBox.SelectedItem;
            int budget_id = this.budgets[budget];

            // search DB for budgets
            var rdr = DB_API.SelectBudget(budget_id);
            var res = new Dictionary<string, int>();
            while (rdr.Read())
            {
                StartMonth_comboBox.Text = DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Month.ToString();
                StartMonth_comboBox.ForeColor = Color.Black;
                StartYear_numericBox.Value = DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Year;
                StartYear_numericBox.ForeColor = Color.Black;
                EndMonth_comboBox.Text = DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Month.ToString();
                EndMonth_comboBox.ForeColor = Color.Black;
                EndYear_numericBox.Value = DateTime.Parse(rdr[DB_API.BudgetEnt.start_date.ToString()].ToString()).Year;
                EndYear_numericBox.ForeColor = Color.Black;
                Budget_textBox.Text = rdr[DB_API.BudgetEnt.amount.ToString()].ToString();
                Budget_textBox.ForeColor = Color.Black;
            }
        }

        private void PopulateCategoriesListBox()
        {
            // get categories from DB
            var rdr = DB_API.SelectAccountCategories(account_id);

            // verify if account has no categories
            if (!rdr.HasRows)
            {
                //notifications_textbox.Text = "WARNING:\nUser has no accounts.";
                Categories_listbox.DataSource = new List<String>();
                return;
            }

            // extract category names for listBox
            this.categories = new Dictionary<string, int>();
            var res = new Dictionary<string, int>();
            while (rdr.Read())
            {
                string cat_name = rdr[DB_API.CategoryEnt.name.ToString()].ToString();
                double cat_id = (double)(int)rdr[DB_API.CategoryEnt.category_id.ToString()];
                if (cat_id % 100 > 0)
                {
                    cat_name = "  -> " + cat_name;
                }
                res[cat_name] = (int)cat_id;
                categories[cat_name] = (int)cat_id;
            }
            Categories_listbox.DataSource = new List<string>(res.Keys);
            //this.categories = new Dictionary<string, int>(res);
        }

        private void PopulateComboBoxes()
        {
            StartMonth_comboBox.DataSource = months;
            EndMonth_comboBox.DataSource = months;
            StartYear_numericBox.Value = 2019;
            EndYear_numericBox.Value = 2019;
        }

        private void budget_Load(object sender, EventArgs e)
        {

        }

        
    }
}
