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
    public partial class transactions : Form
    {

        string user_email;
        int account_id;
        IDictionary<string, int> categoriesStrInt;
        IDictionary<int, string> categoriesIntStr;
        IDictionary<string, int> transactionDict;
        string none = "--- none ---";

        public transactions(string user_email, int account_id)
        {
            InitializeComponent();
            this.user_email = user_email;
            this.account_id = account_id;
            UpdateCategories();
            PopulateCategoryComboBoxes();
        }

        // -------------------------------------------------------------------
        // BUTTONS -----------------------------------------------------------
        // -------------------------------------------------------------------

        private void Save_btn_Click(object sender, EventArgs e)
        {   
            // get fields info
            int category_id = categoriesStrInt[category_comboBox.SelectedItem.ToString()];
            int subcategory_id = category_comboBox.SelectedItem.ToString()==this.none ? -1 : categoriesStrInt[subcategory_comboBox.SelectedItem.ToString()];
            string amount = amount_textBox.ForeColor == Color.Black ? amount_textBox.Text : "";
            DateTime date = dateTimePicker.Value;
            string notes = notes_textBox.ForeColor == Color.Black ? notes_textBox.Text : "";
            int category_type_id;
            try
            {
                category_type_id = DB_API.SelectTransactionTypeIdByName(type_comboBox.SelectedItem.ToString());
            }
            catch (SqlException ex)
            {
                Notifications.Text = ErrorMessenger.Exception(ex);
            }
            int wallet_id;
            try
            {
                var rdr = DB_API.SelectWalletByName(account_id, wallet_comboBox.SelectedItem.ToString());
                while (rdr.Read())
                {
                    wallet_id = (int)rdr[DB_API.WalletEnt.name.ToString()];
                    break;
                }
            }
            catch (SqlException ex)
            {
                Notifications.SelectedText = ErrorMessenger.Exception(ex);
            }
            string location = location_textBox.ForeColor == Color.Black ? location_textBox.Text : "";
            
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {

        }

        private void Filter_btn_Click(object sender, EventArgs e)
        {

        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void transactions_Load(object sender, EventArgs e)
        {

        }

        // -------------------------------------------------------------------
        // LIST AND COMBO BOXES ----------------------------------------------
        // -------------------------------------------------------------------

        private void UpdateCategories()
        {
            this.categoriesStrInt = new Dictionary<string, int>();
            this.categoriesIntStr = new Dictionary<int, string>();
            var rdr = DB_API.SelectAccountCategories(account_id);
            while (rdr.Read())
            {
                string cat_name = rdr[DB_API.CategoryEnt.name.ToString()].ToString();
                int cat_id = (int)rdr[DB_API.CategoryEnt.category_id.ToString()];
                this.categoriesStrInt[cat_name] = cat_id;
                this.categoriesIntStr[cat_id] = cat_name;
            }
        }

        private void PopulateCategoryComboBoxes()
        {
            var res = new List<string>();
            foreach (KeyValuePair<string, int> entry in this.categoriesStrInt)
            {
                if (entry.Value % 100 == 0)
                {
                    res.Add(entry.Key);
                }
            }
            category_comboBox.DataSource = new List<string>(res);
            filtercategory_comboBox.DataSource = new List<string>(res);
        }

        private void Category_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateSubCategoryComboBox(this.categoriesStrInt[category_comboBox.SelectedItem.ToString()]);
        }

        private void Filtercategory_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateFilterSubCategoryComboBox(this.categoriesStrInt[filtercategory_comboBox.SelectedItem.ToString()]);
        }

        private void PopulateSubCategoryComboBox(int cat_id)
        {
            var res = new List<string>();
            res.Add(this.none);
            foreach (KeyValuePair<string, int> entry in this.categoriesStrInt)
            {
                if (entry.Value / 100 == cat_id / 100 && entry.Value != cat_id)
                {
                    res.Add(entry.Key);
                }
            }
            subcategory_comboBox.DataSource = res;
        }

        private void PopulateFilterSubCategoryComboBox(int cat_id)
        {
            var res = new List<string>();
            res.Add(this.none);
            foreach (KeyValuePair<string, int> entry in this.categoriesStrInt)
            {
                if (entry.Value / 100 == cat_id / 100 && entry.Value != cat_id)
                {
                    res.Add(entry.Key);
                }
            }
            subcategory_comboBox.DataSource = res;
        }

        private void PopulateTransactionsListBox()
        {
            // get categories from DB
            var rdr = DB_API.SelectAccountTransactions(account_id);

            // verify if account has no transactions
            if (!rdr.HasRows)
            {
                Notifications.Text = ErrorMessenger.Warning("Account has no transactions!");
                return;
            }

            // extract transaction info for listBox
            this.transactionDict = new Dictionary<string, int>();
            var res = new List<string>();
            while (rdr.Read())
            {
                int transaction_id = (int)rdr[DB_API.TransactionEnt.transaction_id.ToString()];
                string date = rdr[DB_API.TransactionEnt.date.ToString()].ToString();
                string amount = rdr[DB_API.TransactionEnt.amount.ToString()].ToString();
                string list_name = date + "  ->  " + amount;
                res.Add(list_name);
                transactionDict[list_name] = transaction_id;
            }

            Transactions_listBox.DataSource = res;
        }

        private void Transactions_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value
            string selected = Transactions_listBox.SelectedItem.ToString();
            int transaction_id = this.transactionDict[selected];
            int category_id = -1;
            int wallet_id = -1;
            int transaction_type_id = -1;
            double amount = -1.0;
            DateTime date = DateTime.Today;
            string notes = "";
            string location = "";
            
            try
            {
                DataTableReader reader = DB_API.SelectFilteredTransactions(account_id);
                while (reader.Read())
                {
                    category_id = (int)reader[DB_API.TransactionEnt.category_id.ToString()];
                    wallet_id = (int)reader[DB_API.TransactionEnt.wallet_id.ToString()];
                    transaction_type_id = (int)reader[DB_API.TransactionEnt.transaction_type_id.ToString()];
                    amount = Double.Parse(reader[DB_API.TransactionEnt.amount.ToString()].ToString());
                    date = (DateTime)reader[DB_API.TransactionEnt.date.ToString()];
                    notes = reader[DB_API.TransactionEnt.notes.ToString()].ToString();
                    location = reader[DB_API.TransactionEnt.location.ToString()].ToString();
                }
            }
            catch (SqlException ex)
            {
                Notifications.Text = ErrorMessenger.Exception(ex);
                return;
            }

            // update TextBoxes
            // category and subcategory
            if (category_id%100 == 0) // is category
            {
                category_comboBox.Text = categoriesIntStr[category_id];
                subcategory_comboBox.Text = this.none;
            }
            else
            {
                category_comboBox.Text = categoriesIntStr[category_id / 100 * 100];
                subcategory_comboBox.Text = categoriesIntStr[category_id];
            }
            // wallet
            var rdr = DB_API.SelectWallet(wallet_id);
            while (rdr.Read())
            {
                wallet_comboBox.Text = rdr[DB_API.WalletEnt.name.ToString()].ToString();
                break;
            }
            // transaction type
            try
            {
                type_comboBox.Text = DB_API.SelectTransactionTypeNameById(transaction_type_id);
            }
            catch (SqlException ex)
            {
                Notifications.Text = ErrorMessenger.Exception(ex);
            }
            // amount
            amount_textBox.Text = amount.ToString();
            // date
            dateTimePicker.Value = date;
            // notes and location
            notes_textBox.Text = notes;
            location_textBox.Text = location;

        }

        // -------------------------------------------------------------------
        // TEXT BOXES --------------------------------------------------------
        // -------------------------------------------------------------------


        // -------------------------------------------------------------------
        // TEXT BOXES HINTS --------------------------------------------------
        // -------------------------------------------------------------------

        private void DefaultTextboxes()
        {
            amount_textBox.Text = "amount";
            amount_textBox.ForeColor = Color.Gray;
            notes_textBox.Text = "notes";
            notes_textBox.ForeColor = Color.Gray;
            location_textBox.Text = "location";
            location_textBox.ForeColor = Color.Gray;
            filterminamount_textBox.Text = "min amount";
            filterminamount_textBox.ForeColor = Color.Gray;
            filtermaxamount_textBox.Text = "max amount";
            filtermaxamount_textBox.ForeColor = Color.Gray;
        }

        private void Amount_textBox_Enter(object sender, EventArgs e)
        {
            amount_textBox.Text = "";
            amount_textBox.ForeColor = Color.Black;
        }
        private void Amount_textBox_Leave(object sender, EventArgs e)
        {
            if (amount_textBox.Text.Equals(""))
            {
                amount_textBox.Text = "amount";
                amount_textBox.ForeColor = Color.Gray;
            }
        }

        private void Notes_textBox_Enter(object sender, EventArgs e)
        {
            notes_textBox.Text = "";
            notes_textBox.ForeColor = Color.Black;
        }
        private void Notes_textBox_Leave(object sender, EventArgs e)
        {
            if (notes_textBox.Text.Equals(""))
            {
                notes_textBox.Text = "amount";
                notes_textBox.ForeColor = Color.Gray;
            }
        }

        private void Location_textBox_Enter(object sender, EventArgs e)
        {
            location_textBox.Text = "";
            location_textBox.ForeColor = Color.Black;
        }
        private void Location_textBox_Leave(object sender, EventArgs e)
        {
            if (location_textBox.Text.Equals(""))
            {
                location_textBox.Text = "amount";
                location_textBox.ForeColor = Color.Gray;
            }
        }

        private void Filterminamount_textBox_Enter(object sender, EventArgs e)
        {
            filterminamount_textBox.Text = "";
            filterminamount_textBox.ForeColor = Color.Black;
        }
        private void Filterminamount_textBox_Leave(object sender, EventArgs e)
        {
            if (filterminamount_textBox.Text.Equals(""))
            {
                filterminamount_textBox.Text = "amount";
                filterminamount_textBox.ForeColor = Color.Gray;
            }
        }

        private void Filtermaxamount_textBox_Enter(object sender, EventArgs e)
        {
            filtermaxamount_textBox.Text = "";
            filtermaxamount_textBox.ForeColor = Color.Black;
        }
        private void Filtermaxamount_textBox_Leave(object sender, EventArgs e)
        {
            if (filtermaxamount_textBox.Text.Equals(""))
            {
                filtermaxamount_textBox.Text = "amount";
                filtermaxamount_textBox.ForeColor = Color.Gray;
            }
        }

    }
}
