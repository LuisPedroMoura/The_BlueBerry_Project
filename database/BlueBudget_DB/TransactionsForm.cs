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
    public partial class TransactionsForm : Form
    {

        string user_email;
        int account_id;
        IDictionary<string, int> categoriesStrInt;
        IDictionary<int, string> categoriesIntStr;
        IDictionary<string, int> transactionDict;
        string none = "--- none ---";

        public TransactionsForm(string user_email, int account_id)
        {
            InitializeComponent();
            this.user_email = user_email;
            this.account_id = account_id;
        }

        private void Transactions_Load(object sender, EventArgs e)
        {
            DefaultTextboxes();
            UpdateCategories();
            PopulateCategoryComboBoxes();
            PopulateTransactionTypesComboBox();
            PopulateWalletsComboBox();
            PopulateTransactionsListBox();

            // disable typing in comboBoxes by changing style
            category_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            subcategory_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filtercategory_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filtersubcategory_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            type_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            wallet_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filtertype_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filterwallet_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // make comboBoxes white again
            category_comboBox.FlatStyle = FlatStyle.Popup;
            subcategory_comboBox.FlatStyle = FlatStyle.Popup;
            filtercategory_comboBox.FlatStyle = FlatStyle.Popup;
            filtersubcategory_comboBox.FlatStyle = FlatStyle.Popup;
            type_comboBox.FlatStyle = FlatStyle.Popup;
            wallet_comboBox.FlatStyle = FlatStyle.Popup;
            filtertype_comboBox.FlatStyle = FlatStyle.Popup;
            filterwallet_comboBox.FlatStyle = FlatStyle.Popup;
        }

        // -------------------------------------------------------------------
        // BUTTONS -----------------------------------------------------------
        // -------------------------------------------------------------------

        private void Save_btn_Click(object sender, EventArgs e)
        {   
            // get fields info
            int category_id = categoriesStrInt[category_comboBox.SelectedItem.ToString()];
            int subcategory_id = subcategory_comboBox.SelectedItem.ToString().Equals(this.none) ||
                subcategory_comboBox.SelectedItem.ToString().Equals("") ?
                -1 : categoriesStrInt[subcategory_comboBox.SelectedItem.ToString()];
            string amt = amount_textBox.ForeColor == Color.Black ? amount_textBox.Text.Substring(1) : "";
            if (amt.Equals(""))
            {
                ErrorMessenger.EmptyField("Amount");
                return;
            }
            double amount = 0.0;
            try
            {
                amount = Double.Parse(amt);
            }
            catch
            {
                ErrorMessenger.WrongFormat("Amount");
                return;
            }
            DateTime date = dateTimePicker.Value;
            string notes = notes_textBox.ForeColor == Color.Black ? notes_textBox.Text : "";
            int transaction_type_id = -1;
            try
            {
                transaction_type_id = DB_API.SelectTransactionTypeIdByName(type_comboBox.SelectedItem.ToString());
            }
            catch (SqlException ex)
            {
                ErrorMessenger.Exception(ex);
            }
            int wallet_id = -1;
            try
            {
                var rdr = DB_API.SelectWalletByName(account_id, wallet_comboBox.SelectedItem.ToString());
                while (rdr.Read())
                {
                    wallet_id = (int)rdr[DB_API.WalletEnt.wallet_id.ToString()];
                    break;
                }
            }
            catch (SqlException ex)
            {
                ErrorMessenger.Exception(ex);
            }
            string location = location_textBox.ForeColor == Color.Black ? location_textBox.Text : "";

            // insert new transaction
            int cat_id = subcategory_id != -1 ? subcategory_id : category_id;
            try
            {
                DB_API.InsertTransaction(account_id, cat_id, wallet_id, transaction_type_id,
                    amount, date, location, notes);
            }
            catch (SqlException ex)
            {
                ErrorMessenger.Exception(ex);
                return;
            }

            // update transactions listBox
            PopulateTransactionsListBox();
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            int transaction_id = 0;
            try
            {
                transaction_id = transactionDict[Transactions_listBox.SelectedItem.ToString()];
            }
            catch
            {
                ErrorMessenger.Warning("A transaction must be selected from the list");
                return;
            }
            try
            {
                DB_API.DeleteTransaction(transaction_id);
            }
            catch (SqlException ex)
            {
                ErrorMessenger.Exception(ex);
                return;
            }

            // populate transactions listBox
            PopulateTransactionsListBox();

        }

        private void Filter_btn_Click(object sender, EventArgs e)
        {
            // get info from textBoxes
            // category and sub_category
            int category_id = filtercategory_comboBox.SelectedItem.ToString().Equals(this.none) ?
                -1 : categoriesStrInt[filtercategory_comboBox.SelectedItem.ToString()];
            int subcategory_id = filtersubcategory_comboBox.SelectedItem.ToString().Equals(this.none) ?
                -1 : categoriesStrInt[filtersubcategory_comboBox.SelectedItem.ToString()];
            int? cat_id = subcategory_id == -1 ? category_id : subcategory_id;
            cat_id = cat_id == -1 ? null : cat_id;
            Console.WriteLine("---> "+category_id + ", " + subcategory_id + ", " + cat_id);
            // start and end dates
            DateTime start_date = filterstartdate_timePicker.Value;
            start_date = new DateTime(start_date.Year, start_date.Month, start_date.Day, 0, 0, 0);
            DateTime end_date = filterenddate_timePicker.Value;
            end_date = new DateTime(end_date.Year, end_date.Month, end_date.Day, 0, 0, 0);
            if (start_date.CompareTo(end_date) == 0)
            {
                end_date = new DateTime(end_date.Year, end_date.Month, end_date.Day + 1, 23, 59, 59, 999);
            }
            if (start_date.CompareTo(end_date) > 0)
            {
                ErrorMessenger.Error("End Date must be equal or greater than Start Date");
                return;
            }
                // type and wallet
            int? transaction_type_id = null;
            if (!filtertype_comboBox.SelectedItem.ToString().Equals(this.none))
            {
                try
                {
                    transaction_type_id = DB_API.SelectTransactionTypeIdByName(filtertype_comboBox.SelectedItem.ToString());
                }
                catch (SqlException ex)
                {
                    ErrorMessenger.Exception(ex);
                    return;
                }
            }
            int? wallet_id = null;
            if (!wallet_comboBox.SelectedItem.ToString().Equals(this.none))
            {
                try
                {
                    var rdr = DB_API.SelectWalletByName(account_id, filterwallet_comboBox.SelectedItem.ToString());
                    while (rdr.Read())
                    {
                        wallet_id = (int)rdr[DB_API.WalletEnt.wallet_id.ToString()];
                        break;
                    }
                }
                catch (SqlException ex)
                {
                    ErrorMessenger.Exception(ex);
                    return;
                }
            }
                // min and max amount
            string minamt = filterminamount_textBox.ForeColor == Color.Black ? filterminamount_textBox.Text.Substring(1) : "";
            string maxamt = filtermaxamount_textBox.ForeColor == Color.Black ? filtermaxamount_textBox.Text.Substring(1) : "";
            double? minamount = null;
            double? maxamount = null;
            if (!minamt.Equals(""))
            {
                try
                {
                    minamount = Double.Parse(minamt);
                }
                catch
                {
                    ErrorMessenger.WrongFormat("Min amount");
                    return;
                }
            }
            if (!maxamt.Equals(""))
            {
                try
                {
                    maxamount = Double.Parse(maxamt);
                }
                catch
                {
                    ErrorMessenger.WrongFormat("Max amount");
                    return;
                }
            }

            // apply filter
            Console.WriteLine("account_id, " + account_id + "\n" +
                                "cat_id, " + cat_id + "\n" +
                                "wallet_id, " + wallet_id + "\n" +
                                "transaction_type_id, " + transaction_type_id + "\n" +
                                "minamount, " + minamount + "\n" +
                                "maxamount, " + maxamount + "\n" +
                                "start_date, " + start_date + "\n" +
                                "end_date, " + end_date);
            PopulateTransactionsListBox(account_id, cat_id, wallet_id, transaction_type_id, minamount, maxamount,
                    start_date, end_date);

        }

        private void Refresh_btn_Click(object sender, EventArgs e)
        {
            PopulateTransactionsListBox();
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // -------------------------------------------------------------------
        // COMBO BOXES -------------------------------------------------------
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
            var filterRes = new List<string>();
            filterRes.Add(this.none);
            foreach (KeyValuePair<string, int> entry in this.categoriesStrInt)
            {
                if (entry.Value % 100 == 0)
                {
                    res.Add(entry.Key);
                    filterRes.Add(entry.Key);
                }
            }
            category_comboBox.DataSource = res;
            filtercategory_comboBox.DataSource = filterRes;
        }

        private void Category_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateSubCategoryComboBox(this.categoriesStrInt[category_comboBox.SelectedItem.ToString()]);
        }

        private void Filtercategory_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filtercategory_comboBox.SelectedItem.ToString().Equals(this.none))
            {
                PopulateFilterSubCategoryComboBox(-1);
            }
            else
            {
                PopulateFilterSubCategoryComboBox(this.categoriesStrInt[filtercategory_comboBox.SelectedItem.ToString()]);
            }
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
            filtersubcategory_comboBox.DataSource = res;
        }

        private void PopulateTransactionTypesComboBox()
        {
            var res = new List<string>();
            var filterRes = new List<string>();
            filterRes.Add(this.none);
            var rdr = DB_API.SelectAllTransactionTypes();
            while (rdr.Read())
            {
                res.Add(rdr[DB_API.TransactionTypeEnt.designation.ToString()].ToString());
                filterRes.Add(rdr[DB_API.TransactionTypeEnt.designation.ToString()].ToString());
            }
            type_comboBox.DataSource = res;
            filtertype_comboBox.DataSource = filterRes;
        }

        private void PopulateWalletsComboBox()
        {
            var res = new List<string>();
            var filterRes = new List<string>();
            filterRes.Add(this.none);
            var rdr = DB_API.SelectAccountWallets(account_id);
            while (rdr.Read())
            {
                res.Add(rdr[DB_API.WalletEnt.name.ToString()].ToString());
                filterRes.Add(rdr[DB_API.WalletEnt.name.ToString()].ToString());
            }
            wallet_comboBox.DataSource = res;
            filterwallet_comboBox.DataSource = filterRes;
        }

        // -------------------------------------------------------------------
        // LIST BOXES --------------------------------------------------------
        // -------------------------------------------------------------------

        private void PopulateTransactionsListBox()
        {
            // get categories from DB
            DataTableReader rdr = null;
            rdr = DB_API.SelectAccountTransactions(account_id);
            PrivatePopulateTransactionsListBox(rdr);
        }

        public void PopulateTransactionsListBox(int account_id, int? cat_id, int? wallet_id, int? transaction_type_id,
            double? minamount, double? maxamount, DateTime? start_date, DateTime? end_date)
        {
            // get categories from DB
            DataTableReader rdr = null;
            rdr = DB_API.SelectFilteredTransactions(account_id, cat_id, wallet_id, null, transaction_type_id,
            minamount, maxamount, start_date, end_date, null);
            PrivatePopulateTransactionsListBox(rdr);
        }

        public void PrivatePopulateTransactionsListBox(DataTableReader rdr)
        {
            // verify if account has no transactions
            if (!rdr.HasRows)
            {
                Transactions_listBox.DataSource = new List<string>();
                return;
            }

            // extract transaction info for listBox
            this.transactionDict = new Dictionary<string, int>();
            var res = new List<string>();
            while (rdr.Read())
            {
                int transaction_id = (int)rdr[DB_API.TransactionEnt.transaction_id.ToString()];
                DateTime date = DateTime.Parse(rdr[DB_API.TransactionEnt.date.ToString()].ToString());
                string amount = rdr[DB_API.TransactionEnt.amount.ToString()].ToString();
                string list_name = "";
                if ((int)rdr[DB_API.TransactionEnt.transaction_type_id.ToString()] == -1)
                {
                    list_name = date.ToString("dd/MM/yyyy") + "  ->       -" + amount + "$";
                }
                else
                {
                    list_name = date.ToString("dd/MM/yyyy") + "  ->        " + amount + "$";
                }
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

            DataTableReader reader = DB_API.SelectFilteredTransactions(transaction_id:transaction_id);
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

            // update TextBoxes
            // category and subcategory
            if (category_id % 100 == 0) // is category
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
            type_comboBox.Text = DB_API.SelectTransactionTypeNameById(transaction_type_id);

            // amount
            amount_textBox.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", amount);
            amount_textBox.ForeColor = Color.Black;
            // date
            dateTimePicker.Value = date;
            // notes and location
            notes_textBox.Text = notes;
            notes_textBox.ForeColor = Color.Black;
            location_textBox.Text = location;
            location_textBox.ForeColor = Color.Black;
        }


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
            amount_textBox.Text = DB_API.Moneyfy(amount_textBox.Text);

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
                notes_textBox.Text = "notes";
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
                location_textBox.Text = "location";
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
            filterminamount_textBox.Text = DB_API.Moneyfy(filterminamount_textBox.Text);

            if (filterminamount_textBox.Text.Equals(""))
            {
                filterminamount_textBox.Text = "min amount";
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
            filtermaxamount_textBox.Text = DB_API.Moneyfy(filtermaxamount_textBox.Text);

            if (filtermaxamount_textBox.Text.Equals(""))
            {
                filtermaxamount_textBox.Text = "max amount";
                filtermaxamount_textBox.ForeColor = Color.Gray;
            }
        }

    }
}
