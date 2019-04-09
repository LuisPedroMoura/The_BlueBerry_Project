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
    public partial class back_office : Form
    {
        public back_office()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void placeholderTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void back_office_Load(object sender, EventArgs e)
        {

        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lastname_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void users_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<String> collumns = new List<String>();
            collumns.Add("name");
            String tableName = "users";
            List<String> filters = new List<String>();
            DB_API.DBselect(collumns, tableName, filters);
            
        }
    }
}
