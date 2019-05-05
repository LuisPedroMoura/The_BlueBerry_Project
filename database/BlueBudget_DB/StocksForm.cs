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
            try
            {
                for (int i = 0; i < quantity; i++)
                {
                    DB_API.InsertPurchasedStock(this.account_id, stock.Company, (double)stock.AskPrice);
                }
            }
            catch( SqlException ex)
            {
                ErrorMessenger.Exception(ex);
            }

            // update MyStocks listView
            PopulateMyStocksListView();

        }

        private void sell_btn_Click(object sender, EventArgs e)
        {
            //get selected item values
            Stock stock = (Stock)MyStocks_listView.SelectedItems[0].Tag;

            // make the sell
            try
            {
                DB_API.DeleteStockByTicker((int)stock.Ticker);
            }
            catch(SqlException ex)
            {
                ErrorMessenger.Exception(ex);
            }

            // update MyStocks listView
            PopulateMyStocksListView();
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
                var row = new string[] { company, ask_price };
                var item = new ListViewItem(row);
                item.Tag = new Stock(null,null, company, askPrice, null, null);
                StockMarket_listView.Items.Add(item);
            }
        }

        private void PopulateMyStocksListView()
        {
            // clear the listView
            MyStocks_listView.Items.Clear();
            // populate listView
            try
            {
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
                    item.Tag = new Stock(ticker, stockTypeId, company, null, purchasePrice, this.account_id);
                    MyStocks_listView.Items.Add(item);
                }
            }
            catch (SqlException ex)
            {
                ErrorMessenger.Exception(ex);
            }
        }

        // -------------------------------------------------------------------
        // AUXILIAR ----------------------------------------------------------
        // -------------------------------------------------------------------

        private static double RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min*100, max*100)/100;
        }

        private IDictionary<string, double> GetStockMarket()
        {
            IDictionary<string, double> stock_market = new Dictionary<string, double>
            {
                { "Microsoft Corporation", RandomNumber(100,200) },
                { "Apple Inc.", RandomNumber(100,200) },
                { "Johnson & Johnson", RandomNumber(100,200) },
                { "JPMorgan Chase & Co.", RandomNumber(100,200) },
                { "ExxonMobil Corporation", RandomNumber(100,200) },
                { "Bank of America Corp.", RandomNumber(100,200) },
                { "Facebook Inc.", RandomNumber(100,200) },
                { "Wal-Mart Stores Inc.", RandomNumber(100,200) },
                { "Amazon.com, Inc.", RandomNumber(100,200) },
                { "Alphabet Inc.", RandomNumber(100,200) },
                { "Berkshire Hathaway Inc", RandomNumber(100,200) },
                { "Alibaba Group Holding Ltd", RandomNumber(100,200) },
                { "Wells Fargo & Co.", RandomNumber(100,200) },
                { "Royal Dutch Shell plc", RandomNumber(100,200) },
                { "Visa Inc.", RandomNumber(100,200) },
                { "Procter & Gamble Co.", RandomNumber(100,200) },
                { "Anheuser-Busch Inbev NV", RandomNumber(100,200) },
                { "AT&T Inc.", RandomNumber(100,200) },
                { "Chevron Corporation", RandomNumber(100,200) },
                { "UnitedHealth Group Inc.", RandomNumber(100,200) },
                { "Pfizer Inc.", RandomNumber(100,200) },
                { "Roche Holding Ltd.", RandomNumber(100,200) }
            };
            return stock_market;
        }

        private void buy_btn_Click(object sender, EventArgs e)
        {

        }

        
    }
}
