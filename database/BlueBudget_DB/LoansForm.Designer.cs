namespace BlueBudget_DB
{
    partial class LoansForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loans_label = new System.Windows.Forms.Label();
            this.Name_textBox = new System.Windows.Forms.TextBox();
            this.InitialAmount_textBox = new System.Windows.Forms.TextBox();
            this.Enddate_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Interest_textBox = new System.Windows.Forms.TextBox();
            this.Save_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Back_btn = new System.Windows.Forms.Button();
            this.Pay_textBox = new System.Windows.Forms.TextBox();
            this.Pay_btn = new System.Windows.Forms.Button();
            this.Loans_listView = new System.Windows.Forms.ListView();
            this.LOANname_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LOANterm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LOANcurDebt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CurrentDebt_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loans_label
            // 
            this.loans_label.AutoSize = true;
            this.loans_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loans_label.Location = new System.Drawing.Point(239, 9);
            this.loans_label.Name = "loans_label";
            this.loans_label.Size = new System.Drawing.Size(99, 29);
            this.loans_label.TabIndex = 1;
            this.loans_label.Text = "LOANS";
            // 
            // Name_textBox
            // 
            this.Name_textBox.Location = new System.Drawing.Point(359, 57);
            this.Name_textBox.Name = "Name_textBox";
            this.Name_textBox.Size = new System.Drawing.Size(200, 20);
            this.Name_textBox.TabIndex = 2;
            this.Name_textBox.Enter += new System.EventHandler(this.Name_textBox_Enter);
            this.Name_textBox.Leave += new System.EventHandler(this.Name_textBox_Leave);
            // 
            // InitialAmount_textBox
            // 
            this.InitialAmount_textBox.Location = new System.Drawing.Point(359, 83);
            this.InitialAmount_textBox.Name = "InitialAmount_textBox";
            this.InitialAmount_textBox.Size = new System.Drawing.Size(200, 20);
            this.InitialAmount_textBox.TabIndex = 3;
            this.InitialAmount_textBox.Enter += new System.EventHandler(this.Amount_textBox_Enter);
            this.InitialAmount_textBox.Leave += new System.EventHandler(this.Amount_textBox_Leave);
            // 
            // Enddate_dateTimePicker
            // 
            this.Enddate_dateTimePicker.Location = new System.Drawing.Point(359, 139);
            this.Enddate_dateTimePicker.Name = "Enddate_dateTimePicker";
            this.Enddate_dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.Enddate_dateTimePicker.TabIndex = 4;
            // 
            // Interest_textBox
            // 
            this.Interest_textBox.Location = new System.Drawing.Point(459, 165);
            this.Interest_textBox.Name = "Interest_textBox";
            this.Interest_textBox.Size = new System.Drawing.Size(100, 20);
            this.Interest_textBox.TabIndex = 5;
            this.Interest_textBox.Enter += new System.EventHandler(this.Interest_textBox_Enter);
            this.Interest_textBox.Leave += new System.EventHandler(this.Interest_textBox_Leave);
            // 
            // Save_btn
            // 
            this.Save_btn.Location = new System.Drawing.Point(403, 310);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Size = new System.Drawing.Size(75, 23);
            this.Save_btn.TabIndex = 6;
            this.Save_btn.Text = "Save New";
            this.Save_btn.UseVisualStyleBackColor = true;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(412, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "interest";
            // 
            // Back_btn
            // 
            this.Back_btn.Location = new System.Drawing.Point(484, 310);
            this.Back_btn.Name = "Back_btn";
            this.Back_btn.Size = new System.Drawing.Size(75, 23);
            this.Back_btn.TabIndex = 8;
            this.Back_btn.Text = "Back";
            this.Back_btn.UseVisualStyleBackColor = true;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // Pay_textBox
            // 
            this.Pay_textBox.Location = new System.Drawing.Point(483, 216);
            this.Pay_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.Pay_textBox.Name = "Pay_textBox";
            this.Pay_textBox.Size = new System.Drawing.Size(76, 20);
            this.Pay_textBox.TabIndex = 9;
            // 
            // Pay_btn
            // 
            this.Pay_btn.Location = new System.Drawing.Point(483, 239);
            this.Pay_btn.Margin = new System.Windows.Forms.Padding(2);
            this.Pay_btn.Name = "Pay_btn";
            this.Pay_btn.Size = new System.Drawing.Size(75, 23);
            this.Pay_btn.TabIndex = 10;
            this.Pay_btn.Text = "Pay";
            this.Pay_btn.UseVisualStyleBackColor = true;
            this.Pay_btn.Click += new System.EventHandler(this.Pay_btn_Click);
            // 
            // Loans_listView
            // 
            this.Loans_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LOANname_col,
            this.LOANterm,
            this.LOANcurDebt});
            this.Loans_listView.FullRowSelect = true;
            this.Loans_listView.Location = new System.Drawing.Point(37, 57);
            this.Loans_listView.Name = "Loans_listView";
            this.Loans_listView.Size = new System.Drawing.Size(284, 276);
            this.Loans_listView.TabIndex = 11;
            this.Loans_listView.UseCompatibleStateImageBehavior = false;
            this.Loans_listView.View = System.Windows.Forms.View.Details;
            this.Loans_listView.SelectedIndexChanged += new System.EventHandler(this.Loans_listView_SelectedIndexChanged);
            // 
            // LOANname_col
            // 
            this.LOANname_col.Text = "Name";
            this.LOANname_col.Width = 120;
            // 
            // LOANterm
            // 
            this.LOANterm.Text = "Term";
            this.LOANterm.Width = 70;
            // 
            // LOANcurDebt
            // 
            this.LOANcurDebt.Text = "Current Debt";
            this.LOANcurDebt.Width = 90;
            // 
            // CurrentDebt_textBox
            // 
            this.CurrentDebt_textBox.Location = new System.Drawing.Point(359, 110);
            this.CurrentDebt_textBox.Name = "CurrentDebt_textBox";
            this.CurrentDebt_textBox.Size = new System.Drawing.Size(200, 20);
            this.CurrentDebt_textBox.TabIndex = 12;
            this.CurrentDebt_textBox.Enter += new System.EventHandler(this.CurrentDebt_textBox_Enter);
            this.CurrentDebt_textBox.Leave += new System.EventHandler(this.CurrentDebt_textBox_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(342, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(342, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(342, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "*";
            // 
            // LoansForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CurrentDebt_textBox);
            this.Controls.Add(this.Loans_listView);
            this.Controls.Add(this.Pay_btn);
            this.Controls.Add(this.Pay_textBox);
            this.Controls.Add(this.Back_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Save_btn);
            this.Controls.Add(this.Interest_textBox);
            this.Controls.Add(this.Enddate_dateTimePicker);
            this.Controls.Add(this.InitialAmount_textBox);
            this.Controls.Add(this.Name_textBox);
            this.Controls.Add(this.loans_label);
            this.Name = "LoansForm";
            this.Text = "loans";
            this.Load += new System.EventHandler(this.LoansForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label loans_label;
        private System.Windows.Forms.TextBox Name_textBox;
        private System.Windows.Forms.TextBox InitialAmount_textBox;
        private System.Windows.Forms.DateTimePicker Enddate_dateTimePicker;
        private System.Windows.Forms.TextBox Interest_textBox;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Back_btn;
        private System.Windows.Forms.TextBox Pay_textBox;
        private System.Windows.Forms.Button Pay_btn;
        private System.Windows.Forms.ListView Loans_listView;
        private System.Windows.Forms.ColumnHeader LOANname_col;
        private System.Windows.Forms.ColumnHeader LOANterm;
        private System.Windows.Forms.ColumnHeader LOANcurDebt;
        private System.Windows.Forms.TextBox CurrentDebt_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}