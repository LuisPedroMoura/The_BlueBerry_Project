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
            PopulateTransactionsListView();

            // disable typing in comboBoxes by changing style
            category_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            subcategory_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filtercategory_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filtersubcategory_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            type_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            wallet_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            wallet2_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filtertype_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filterwallet_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // make comboBoxes white again
            category_comboBox.FlatStyle = FlatStyle.Popup;
            subcategory_comboBox.FlatStyle = FlatStyle.Popup;
            filtercategory_comboBox.FlatStyle = FlatStyle.Popup;
            filtersubcategory_comboBox.FlatStyle = FlatStyle.Popup;
            type_comboBox.FlatStyle = FlatStyle.Popup;
            wallet_comboBox.FlatStyle = FlatStyle.Popup;
            wallet2_comboBox.FlatStyle = FlatStyle.Popup;
            filtertype_comboBox.FlatStyle = FlatStyle.Popup;
            filterwallet_comboBox.FlatStyle = FlatStyle.Popup;
        }

        // -------------------------------------------------------------------
        // BUTTONS -----------------------------------------------------------
        // -------------------------------------------------------------------

        private void Save_btn_Click(object sender, EventArgs e)
        {   
            // get categories fields info
            int category_id = categoriesStrInt[category_comboBox.SelectedItem.ToString()];
            int subcategory_id = subcategory_comboBox.SelectedItem.ToString().Equals(this.none) ||
                subcategory_comboBox.SelectedItem.ToString().Equals("") ?
                -1 : categoriesStrInt[subcategory_comboBox.SelectedItem.ToString()];
            // get amount
            string amt = amount_textBox.ForeColor == Color.Black ? amount_textBox.Text.Substring(1) : "";
            if (amt.Equals(""))
            {
                ErrorMessenger.EmptyField("Amount");
                return;
            }
            double amount = DB_API.UnMoneyfy(amount_textBox.Text);
            // get date
            DateTime date = dateTimePicker.Value;
            // get notes and location
            string notes = notes_textBox.ForeColor == Color.Black ? notes_textBox.Text : "";
            string location = location_textBox.ForeColor == Color.Black ? location_textBox.Text : "";
            // get transaction type
            int transaction_type_id = -1;
            transaction_type_id = DB_API.SelectTransactionTypeIdByName(type_comboBox.SelectedItem.ToString());
            // get wallet
            string from_wallet_name = wallet_comboBox.SelectedItem.ToString();
            string to_wallet_name = wallet2_comboBox.SelectedItem.ToString();
            int from_wallet_id = -1;
            int to_wallet_id = -1;
            var rdr = DB_API.SelectWalletByName(account_id, from_wallet_name);
            while (rdr.Read())
            {
                from_wallet_id = (int)rdr[DB_API.WalletEnt.wallet_id.ToString()];
                break;
            }
            rdr = DB_API.SelectWalletByName(account_id, to_wallet_name);
            while (rdr.Read())
            {
                to_wallet_id = (int)rdr[DB_API.WalletEnt.wallet_id.ToString()];
                break;
            }

            // before inserting change amount sign if it is an expense
            // insert new transaction
            int cat_id = subcategory_id != -1 ? subcategory_id : category_id;
            DB_API.InsertTransaction(account_id, cat_id, from_wallet_id, to_wallet_id, transaction_type_id,
                    amount, date, location, notes);
            
            // update transactions listBox
            PopulateTransactionsListView();
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            int transaction_id = 0;
            try
            {
                transaction_id = (int)((Transaction)Transactions_listView.SelectedItems[0].Tag).TransactionID;
            }
            catch
            {
                ErrorMessenger.Warning("A transaction must be selected from the list");
                return;
            }
            DB_API.DeleteTransaction(transaction_id);

            // populate transactions listBox
            PopulateTransactionsListView();

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
                transaction_type_id = DB_API.SelectTransactionTypeIdByName(filtertype_comboBox.SelectedItem.ToString());
            }
            int? wallet_id = null;
            if (!wallet_comboBox.SelectedItem.ToString().Equals(this.none))
            {
                var rdr = DB_API.SelectWalletByName(account_id, filterwallet_comboBox.SelectedItem.ToString());
                while (rdr.Read())
                {
                    wallet_id = (int)rdr[DB_API.WalletEnt.wallet_id.ToString()];
                    break;
                }
            }

            // min and max amount
            string minamt = filterminamount_textBox.ForeColor == Color.Black ? filterminamount_textBox.Text.Substring(1) : "";
            string maxamt = filtermaxamount_textBox.ForeColor == Color.Black ? filtermaxamount_textBox.Text.Substring(1) : "";
            double? minamount = null; 
            double? maxamount = null;
            if (!minamt.Equals(""))
            {
                Console.WriteLine(minamt);
                minamount = DB_API.UnMoneyfy(minamt);
            }
            if (!maxamt.Equals(""))
            {
                Console.WriteLine(maxamt);
                maxamount = DB_API.UnMoneyfy(maxamt);
            }

            if (minamount != null && maxamount != null && minamount > maxamount)
            {
                ErrorMessenger.Error("Min amount must be less or equal than Max amount");
                return;
            }

            // apply filter
            PopulateTransactionsListView(account_id, cat_id, wallet_id, transaction_type_id, minamount, maxamount,
                    start_date, end_date);
        }

        private void Refresh_btn_Click(object sender, EventArgs e)
        {
            PopulateTransactionsListView();
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

        private void type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (type_comboBox.SelectedItem.ToString().Equals("transfer"))
            {
                wallet2_comboBox.Enabled = true;
            }
            else
            {
                wallet2_comboBox.Enabled = false;
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
            var fromres = new List<string>();
            var tores = new List<string>();
            var filterRes = new List<string>();
            filterRes.Add(this.none);
            var rdr = DB_API.SelectAccountWallets(account_id);
            while (rdr.Read())
            {
                fromres.Add(rdr[DB_API.WalletEnt.name.ToString()].ToString());
                tores.Add(rdr[DB_API.WalletEnt.name.ToString()].ToString());
                filterRes.Add(rdr[DB_API.WalletEnt.name.ToString()].ToString());
            }
            wallet_comboBox.DataSource = fromres;
            wallet2_comboBox.DataSource = tores;
            filterwallet_comboBox.DataSource = filterRes;
        }

        // -------------------------------------------------------------------
        // LIST BOXES --------------------------------------------------------
        // -------------------------------------------------------------------

        private void PopulateTransactionsListView()
        {
            // get categories from DB
            DataTableReader rdr = null;
            rdr = DB_API.SelectAccountTransactions(account_id);
            PrivatePopulateTransactionsListView(rdr);
            
        }

        public void PopulateTransactionsListView(int account_id, int? cat_id, int? wallet_id, int? transaction_type_id,
            double? minamount, double? maxamount, DateTime? start_date, DateTime? end_date)
        {
            // get categories from DB
            DataTableReader rdr = null;
            rdr = DB_API.SelectFilteredTransactions(account_id, cat_id, wallet_id, null, transaction_type_id,
            minamount, maxamount, start_date, end_date, null);
            PrivatePopulateTransactionsListView(rdr);
        }

        private void PrivatePopulateTransactionsListView(DataTableReader rdr)
        {
            // clear Transactions listView
            Transactions_listView.Items.Clear();

            while (rdr.Read())
            {
                int transaction_id = (int)rdr[DB_API.TransactionEnt.transaction_id.ToString()];
                int category_id = (int)rdr[DB_API.TransactionEnt.category_id.ToString()];
                int account_id = (int)rdr[DB_API.TransactionEnt.account_id.ToString()];
                int transaction_type_id = (int)rdr[DB_API.TransactionEnt.transaction_type_id.ToString()];
                int wallet_id = (int)rdr[DB_API.TransactionEnt.wallet_id.ToString()];
                DateTime date = (DateTime)rdr[DB_API.TransactionEnt.date.ToString()];
                string category = categoriesIntStr[category_id];
                double amount = Double.Parse(rdr[DB_API.TransactionEnt.amount.ToString()].ToString());
                string notes = rdr[DB_API.TransactionEnt.notes.ToString()].ToString();
                string location = rdr[DB_API.TransactionEnt.location.ToString()].ToString();

                // correct amount for negative or positive if expense or income
                amount = transaction_type_id >= 0 ? amount : -amount;

                if (transaction_type_id == 0)
                {
                    int recipient_wallet_id = (int)rdr[DB_API.TransactionEnt.recipient_wallet_id.ToString()];
                    var row1 = new string[] { transaction_id.ToString(), date.ToString("dd/MM/yyyy"), "Transfer", DB_API.Moneyfy(-amount) };
                    var row2 = new string[] { transaction_id.ToString(), date.ToString("dd/MM/yyyy"), "Transfer", DB_API.Moneyfy(amount) };
                    var item1 = new ListViewItem(row1);
                    var item2 = new ListViewItem(row2);
                    item1.Tag = new Transaction(transaction_id, -amount, date, notes, location, category_id, account_id, transaction_type_id, wallet_id, recipient_wallet_id);
                    item2.Tag = new Transaction(transaction_id, amount, date, notes, location, category_id, account_id, transaction_type_id, wallet_id, recipient_wallet_id);
                    Transactions_listView.Items.Add(item1);
                    Transactions_listView.Items.Add(item2);
                }
                else
                {
                    var row = new string[] { transaction_id.ToString(), date.ToString("dd/MM/yyyy"), category, DB_API.Moneyfy(amount) };
                    var item = new ListViewItem(row);
                    item.Tag = new Transaction(transaction_id, amount, date, notes, location, category_id, account_id, transaction_type_id, wallet_id, null);
                    Transactions_listView.Items.Add(item);
                }

                
            }
        }

        private void Transactions_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected item value
            Transaction tr = Transactions_listView.SelectedItems.Count > 0 ?
                (Transaction)Transactions_listView.SelectedItems[0].Tag : null;
            if (tr == null) { return; } // to handle listView idiot 2 step item selection process

            // update TextBoxes
            // category and subcategory
            int category_id = (int)tr.CategoryID;
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

            // transaction type
            type_comboBox.Text = DB_API.SelectTransactionTypeNameById((int)tr.TransactionTypeID);

            // wallet
            var rdr = DB_API.SelectWallet((int)tr.WalletID);
            while (rdr.Read())
            {
                wallet_comboBox.Text = rdr[DB_API.WalletEnt.name.ToString()].ToString();
                break;
            }
            if ((int)tr.TransactionTypeID == 0)
            {
                rdr = DB_API.SelectWallet((int)tr.RecipientWalletID);
                while (rdr.Read())
                {
                    wallet2_comboBox.Text = rdr[DB_API.WalletEnt.name.ToString()].ToString();
                    break;
                }
            }
            

            // amount
            amount_textBox.Text = DB_API.Moneyfy(tr.Amount);
            amount_textBox.ForeColor = Color.Black;
            // date
            dateTimePicker.Value = (DateTime)tr.Date;
            // notes and location
            notes_textBox.Text = tr.Notes;
            notes_textBox.ForeColor = Color.Black;
            location_textBox.Text = tr.Location;
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
