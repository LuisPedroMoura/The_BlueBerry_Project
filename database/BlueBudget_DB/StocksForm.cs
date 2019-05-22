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
    public partial class StocksForm : Form
    {

        string user_email;
        int account_id;
        
        public StocksForm(string user_email, int account_id)
        {
            InitializeComponent();
            this.user_email = user_email;
            this.account_id = account_id;   
        }

        private void Stocks_Load(object sender, EventArgs e)
        {
            PopulateStockMarketListView();
            PopulateMyStocksListView();
        }

        // -------------------------------------------------------------------
        // BUTTONS -----------------------------------------------------------
        // -------------------------------------------------------------------

        private void Buy_btn_Click(object sender, EventArgs e)
        {
            // ge selected item values
            Stock stock = (Stock) StockMarket_listView.SelectedItems[0].Tag;
            int quantity = (int)quantity_numericupdown.Value;

            // make the buy (insert new purchased stock)
            for (int i = 0; i < quantity; i++)
            {
                DB_API.InsertPurchasedStock(this.account_id, stock.Company, (double)stock.AskPrice);
            }

            // update MyStocks listView
            PopulateMyStocksListView();

        }

        private void Sell_btn_Click(object sender, EventArgs e)
        {
            // get selected item value
            Stock stock = MyStocks_listView.SelectedItems.Count > 0 ?
                (Stock)MyStocks_listView.SelectedItems[0].Tag : null;
            if (stock == null) { return; } // to handle listView idiot 2 step item selection process

            // get the current price
            double askPrice = 0.0;
            for (int i = 0; i < StockMarket_listView.Items.Count; i++)
            {
                Stock s = (Stock)StockMarket_listView.Items[i].Tag;
                if (s.Company.Equals(stock.Company))
                {
                    askPrice = (double)s.AskPrice;
                    break;
                }
            }

            // make the sell
            Console.WriteLine(stock.PurchasePrice + ", " + askPrice);
            DB_API.DeleteStockByTicker(account_id, (int)stock.Ticker, (double)stock.PurchasePrice, askPrice);

            // update MyStocks listView
            PopulateMyStocksListView();
        }

        private void SellAll_btn_Click(object sender, EventArgs e)
        {
            // get selected item value
            Stock stock = MyStocks_listView.SelectedItems.Count > 0 ?
                (Stock)MyStocks_listView.SelectedItems[0].Tag : null;
            if (stock == null) { return; } // to handle listView idiot 2 step item selection process

            // get the current price
            double askPrice = 0.0;
            for (int i = 0; i < StockMarket_listView.Items.Count; i++)
            {
                Stock s = (Stock)StockMarket_listView.Items[i].Tag;
                if (s.Company.Equals(stock.Company)){
                    askPrice = (double)s.AskPrice;
                    break;
                }
            }

            // make the sell
            Console.WriteLine(stock.PurchasePrice + ", " + askPrice);
            DB_API.DeleteStocksByCompany(account_id, stock.Company, (double)stock.PurchasePrice, askPrice);

            // update MyStocks listView
            PopulateMyStocksListView();
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // -------------------------------------------------------------------
        // LIST VIEW BOXES ---------------------------------------------------
        // -------------------------------------------------------------------

        private void PopulateStockMarketListView()
        {
            // clear the listView
            StockMarket_listView.Items.Clear();

            // populate listView
            var rdr = DB_API.SelectAllStocks();
            while (rdr.Read()) {
                string company = rdr[DB_API.StockEnt.company.ToString()].ToString();
                double askPrice = Double.Parse(rdr[DB_API.StockEnt.ask_price.ToString()].ToString());
                string ask_price = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", askPrice);
                string stock_type = rdr[DB_API.StockEnt.stock_type.ToString()].ToString();
                var row = new string[] { company, ask_price, stock_type };
                var item = new ListViewItem(row);
                item.Tag = new Stock(null, null, stock_type, company, askPrice, null, null);
                StockMarket_listView.Items.Add(item);
            }
        }

        private void PopulateMyStocksListView()
        {
            // clear the listView
            MyStocks_listView.Items.Clear();

            // populate listView
            var rdr = DB_API.SelectAllAccountPurchasedStocks(this.account_id);
            while (rdr.Read())
            {
                int ticker = (int)rdr[DB_API.StockEnt.ticker.ToString()];
                string company = rdr[DB_API.StockEnt.company.ToString()].ToString();
                int stockTypeId = (int)rdr[DB_API.StockEnt.stock_type_id.ToString()];
                double purchasePrice = Double.Parse(rdr[DB_API.StockEnt.purchase_price.ToString()].ToString());
                string purchase_price = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", purchasePrice);

                var row = new string[] { ticker.ToString(), company, purchase_price };
                var item = new ListViewItem(row);
                item.Tag = new Stock(ticker, stockTypeId, null, company, null, purchasePrice, this.account_id);
                MyStocks_listView.Items.Add(item);
            }
        }
    }
}
