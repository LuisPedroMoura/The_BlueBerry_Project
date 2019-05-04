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
    public partial class stocks : Form
    {

        string user_email;
        int account_id;
        IDictionary<string,double> stock_market = new Dictionary<string, double>
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

        public stocks(string user_email, int account_id)
        {
            InitializeComponent();
            this.user_email = user_email;
            this.account_id = account_id;
            PopulateStockMarketListBox();
        }

        // -------------------------------------------------------------------
        // LIST BOXES --------------------------------------------------------
        // -------------------------------------------------------------------

        public void PopulateStockMarketListBox()
        {
            Stockmarket_listBox.DataSource = this.stock_market;
        }

        // -------------------------------------------------------------------
        // AUXILIAR ----------------------------------------------------------
        // -------------------------------------------------------------------

        public static double RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min*100, max*100)/100;
        }

        private void Stockmarket_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
