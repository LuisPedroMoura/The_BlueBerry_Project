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
    public partial class StatisticsForm : Form
    {

        string user_email;
        int account_id;

        public StatisticsForm(string user_email, int account_id)
        {
            InitializeComponent();
            this.user_email = user_email;
            this.account_id = account_id;
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            Year_numericUpDown.Maximum = 3000;
            Year_numericUpDown.Minimum = 0;
            Year_numericUpDown.Value = 2019;
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Stats_btn_Click(object sender, EventArgs e)
        {
            foreach (var series in BalanceChart.Series)
            {
                series.Points.Clear();
            }

            int year = (int)Year_numericUpDown.Value;

            var rdr = DB_API.SelectAnnualStatistics(account_id, year);
            while (rdr.Read())
            {
                Console.WriteLine(rdr[DB_API.Statistics.date_month.ToString()].ToString() + ", "+ rdr[DB_API.Statistics.expenses.ToString()]);
                string month = rdr[DB_API.Statistics.date_month.ToString()].ToString();
                double income = Double.Parse(rdr[DB_API.Statistics.income.ToString()].ToString());
                double expenses = Double.Parse(rdr[DB_API.Statistics.expenses.ToString()].ToString());
                double balance = Double.Parse(rdr[DB_API.Statistics.balance.ToString()].ToString());

                BalanceChart.Series["Income"].Points.AddXY(month, income);
                BalanceChart.Series["Expenses"].Points.AddXY(month, expenses);
                BalanceChart.Series["Balance"].Points.AddXY(month, balance);
            }

        }

    }
}
