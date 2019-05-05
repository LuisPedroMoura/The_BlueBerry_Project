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

        }
    }
}
