namespace BlueBudget_DB
{
    partial class transactions
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
            this.Transactions_label = new System.Windows.Forms.Label();
            this.category_comboBox = new System.Windows.Forms.ComboBox();
            this.subcategory_comboBox = new System.Windows.Forms.ComboBox();
            this.amount_textBox = new System.Windows.Forms.TextBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.notes_textBox = new System.Windows.Forms.TextBox();
            this.location_textBox = new System.Windows.Forms.TextBox();
            this.type_comboBox = new System.Windows.Forms.ComboBox();
            this.wallet_comboBox = new System.Windows.Forms.ComboBox();
            this.Save_btn = new System.Windows.Forms.Button();
            this.Delete_btn = new System.Windows.Forms.Button();
            this.Filter_btn = new System.Windows.Forms.Button();
            this.Back_btn = new System.Windows.Forms.Button();
            this.Transactions_listBox = new System.Windows.Forms.ListBox();
            this.filtermaxamount_textBox = new System.Windows.Forms.TextBox();
            this.filterminamount_textBox = new System.Windows.Forms.TextBox();
            this.filterstartdate_timePicker = new System.Windows.Forms.DateTimePicker();
            this.filterendadate_timePicker = new System.Windows.Forms.DateTimePicker();
            this.filtercategory_comboBox = new System.Windows.Forms.ComboBox();
            this.filtersubcategory_comboBox = new System.Windows.Forms.ComboBox();
            this.Filter_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.filterwallet_comboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.filtertype_comboBox = new System.Windows.Forms.ComboBox();
            this.Notifications = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Transactions_label
            // 
            this.Transactions_label.AutoSize = true;
            this.Transactions_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Transactions_label.Location = new System.Drawing.Point(313, 9);
            this.Transactions_label.Name = "Transactions_label";
            this.Transactions_label.Size = new System.Drawing.Size(214, 29);
            this.Transactions_label.TabIndex = 0;
            this.Transactions_label.Text = "TRANSACTIONS";
            // 
            // category_comboBox
            // 
            this.category_comboBox.FormattingEnabled = true;
            this.category_comboBox.Location = new System.Drawing.Point(275, 41);
            this.category_comboBox.Name = "category_comboBox";
            this.category_comboBox.Size = new System.Drawing.Size(138, 21);
            this.category_comboBox.TabIndex = 2;
            this.category_comboBox.SelectedIndexChanged += new System.EventHandler(this.Category_comboBox_SelectedIndexChanged);
            // 
            // subcategory_comboBox
            // 
            this.subcategory_comboBox.FormattingEnabled = true;
            this.subcategory_comboBox.Location = new System.Drawing.Point(419, 41);
            this.subcategory_comboBox.Name = "subcategory_comboBox";
            this.subcategory_comboBox.Size = new System.Drawing.Size(139, 21);
            this.subcategory_comboBox.TabIndex = 3;
            // 
            // amount_textBox
            // 
            this.amount_textBox.Location = new System.Drawing.Point(275, 68);
            this.amount_textBox.Name = "amount_textBox";
            this.amount_textBox.Size = new System.Drawing.Size(77, 20);
            this.amount_textBox.TabIndex = 4;
            this.amount_textBox.Enter += new System.EventHandler(this.Amount_textBox_Enter);
            this.amount_textBox.Leave += new System.EventHandler(this.Amount_textBox_Leave);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(358, 68);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker.TabIndex = 5;
            // 
            // notes_textBox
            // 
            this.notes_textBox.Location = new System.Drawing.Point(275, 94);
            this.notes_textBox.Name = "notes_textBox";
            this.notes_textBox.Size = new System.Drawing.Size(283, 20);
            this.notes_textBox.TabIndex = 6;
            this.notes_textBox.Enter += new System.EventHandler(this.Notes_textBox_Enter);
            this.notes_textBox.Leave += new System.EventHandler(this.Notes_textBox_Leave);
            // 
            // location_textBox
            // 
            this.location_textBox.Location = new System.Drawing.Point(419, 147);
            this.location_textBox.Name = "location_textBox";
            this.location_textBox.Size = new System.Drawing.Size(139, 20);
            this.location_textBox.TabIndex = 7;
            this.location_textBox.Enter += new System.EventHandler(this.Location_textBox_Enter);
            this.location_textBox.Leave += new System.EventHandler(this.Location_textBox_Leave);
            // 
            // type_comboBox
            // 
            this.type_comboBox.FormattingEnabled = true;
            this.type_comboBox.Location = new System.Drawing.Point(275, 120);
            this.type_comboBox.Name = "type_comboBox";
            this.type_comboBox.Size = new System.Drawing.Size(138, 21);
            this.type_comboBox.TabIndex = 8;
            // 
            // wallet_comboBox
            // 
            this.wallet_comboBox.FormattingEnabled = true;
            this.wallet_comboBox.Location = new System.Drawing.Point(419, 120);
            this.wallet_comboBox.Name = "wallet_comboBox";
            this.wallet_comboBox.Size = new System.Drawing.Size(139, 21);
            this.wallet_comboBox.TabIndex = 9;
            // 
            // Save_btn
            // 
            this.Save_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save_btn.Location = new System.Drawing.Point(275, 173);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Size = new System.Drawing.Size(75, 23);
            this.Save_btn.TabIndex = 10;
            this.Save_btn.Text = "Save";
            this.Save_btn.UseVisualStyleBackColor = true;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // Delete_btn
            // 
            this.Delete_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Delete_btn.Location = new System.Drawing.Point(356, 173);
            this.Delete_btn.Name = "Delete_btn";
            this.Delete_btn.Size = new System.Drawing.Size(75, 23);
            this.Delete_btn.TabIndex = 11;
            this.Delete_btn.Text = "Delete";
            this.Delete_btn.UseVisualStyleBackColor = true;
            this.Delete_btn.Click += new System.EventHandler(this.Delete_btn_Click);
            // 
            // Filter_btn
            // 
            this.Filter_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Filter_btn.Location = new System.Drawing.Point(275, 417);
            this.Filter_btn.Name = "Filter_btn";
            this.Filter_btn.Size = new System.Drawing.Size(75, 23);
            this.Filter_btn.TabIndex = 12;
            this.Filter_btn.Text = "Filter";
            this.Filter_btn.UseVisualStyleBackColor = true;
            this.Filter_btn.Click += new System.EventHandler(this.Filter_btn_Click);
            // 
            // Back_btn
            // 
            this.Back_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Back_btn.Location = new System.Drawing.Point(483, 417);
            this.Back_btn.Name = "Back_btn";
            this.Back_btn.Size = new System.Drawing.Size(75, 23);
            this.Back_btn.TabIndex = 13;
            this.Back_btn.Text = "Back";
            this.Back_btn.UseVisualStyleBackColor = true;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // Transactions_listBox
            // 
            this.Transactions_listBox.FormattingEnabled = true;
            this.Transactions_listBox.Location = new System.Drawing.Point(29, 20);
            this.Transactions_listBox.Name = "Transactions_listBox";
            this.Transactions_listBox.Size = new System.Drawing.Size(213, 342);
            this.Transactions_listBox.TabIndex = 14;
            this.Transactions_listBox.SelectedIndexChanged += new System.EventHandler(this.Transactions_listBox_SelectedIndexChanged);
            // 
            // filtermaxamount_textBox
            // 
            this.filtermaxamount_textBox.Location = new System.Drawing.Point(419, 377);
            this.filtermaxamount_textBox.Name = "filtermaxamount_textBox";
            this.filtermaxamount_textBox.Size = new System.Drawing.Size(139, 20);
            this.filtermaxamount_textBox.TabIndex = 15;
            this.filtermaxamount_textBox.Enter += new System.EventHandler(this.Filtermaxamount_textBox_Enter);
            this.filtermaxamount_textBox.Leave += new System.EventHandler(this.Filtermaxamount_textBox_Leave);
            // 
            // filterminamount_textBox
            // 
            this.filterminamount_textBox.Location = new System.Drawing.Point(419, 350);
            this.filterminamount_textBox.Name = "filterminamount_textBox";
            this.filterminamount_textBox.Size = new System.Drawing.Size(139, 20);
            this.filterminamount_textBox.TabIndex = 16;
            this.filterminamount_textBox.Enter += new System.EventHandler(this.Filterminamount_textBox_Enter);
            this.filterminamount_textBox.Leave += new System.EventHandler(this.Filterminamount_textBox_Leave);
            // 
            // filterstartdate_timePicker
            // 
            this.filterstartdate_timePicker.Location = new System.Drawing.Point(358, 271);
            this.filterstartdate_timePicker.Name = "filterstartdate_timePicker";
            this.filterstartdate_timePicker.Size = new System.Drawing.Size(200, 20);
            this.filterstartdate_timePicker.TabIndex = 17;
            // 
            // filterendadate_timePicker
            // 
            this.filterendadate_timePicker.Location = new System.Drawing.Point(358, 297);
            this.filterendadate_timePicker.Name = "filterendadate_timePicker";
            this.filterendadate_timePicker.Size = new System.Drawing.Size(200, 20);
            this.filterendadate_timePicker.TabIndex = 18;
            // 
            // filtercategory_comboBox
            // 
            this.filtercategory_comboBox.FormattingEnabled = true;
            this.filtercategory_comboBox.Location = new System.Drawing.Point(275, 244);
            this.filtercategory_comboBox.Name = "filtercategory_comboBox";
            this.filtercategory_comboBox.Size = new System.Drawing.Size(138, 21);
            this.filtercategory_comboBox.TabIndex = 19;
            this.filtercategory_comboBox.SelectedIndexChanged += new System.EventHandler(this.Category_comboBox_SelectedIndexChanged);
            // 
            // filtersubcategory_comboBox
            // 
            this.filtersubcategory_comboBox.FormattingEnabled = true;
            this.filtersubcategory_comboBox.Location = new System.Drawing.Point(419, 244);
            this.filtersubcategory_comboBox.Name = "filtersubcategory_comboBox";
            this.filtersubcategory_comboBox.Size = new System.Drawing.Size(139, 21);
            this.filtersubcategory_comboBox.TabIndex = 20;
            // 
            // Filter_label
            // 
            this.Filter_label.AutoSize = true;
            this.Filter_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Filter_label.Location = new System.Drawing.Point(371, 217);
            this.Filter_label.Name = "Filter_label";
            this.Filter_label.Size = new System.Drawing.Size(80, 24);
            this.Filter_label.TabIndex = 21;
            this.Filter_label.Text = "FILTER";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(301, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "start date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "end date";
            // 
            // filterwallet_comboBox
            // 
            this.filterwallet_comboBox.FormattingEnabled = true;
            this.filterwallet_comboBox.Location = new System.Drawing.Point(419, 323);
            this.filterwallet_comboBox.Name = "filterwallet_comboBox";
            this.filterwallet_comboBox.Size = new System.Drawing.Size(139, 21);
            this.filterwallet_comboBox.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(352, 350);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "min amount";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(349, 377);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "max amount";
            // 
            // filtertype_comboBox
            // 
            this.filtertype_comboBox.FormattingEnabled = true;
            this.filtertype_comboBox.Location = new System.Drawing.Point(275, 323);
            this.filtertype_comboBox.Name = "filtertype_comboBox";
            this.filtertype_comboBox.Size = new System.Drawing.Size(138, 21);
            this.filtertype_comboBox.TabIndex = 27;
            // 
            // Notifications
            // 
            this.Notifications.Location = new System.Drawing.Point(29, 390);
            this.Notifications.Name = "Notifications";
            this.Notifications.ReadOnly = true;
            this.Notifications.Size = new System.Drawing.Size(213, 59);
            this.Notifications.TabIndex = 28;
            this.Notifications.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 371);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Notifications";
            // 
            // transactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 461);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Notifications);
            this.Controls.Add(this.filtertype_comboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.filterwallet_comboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Filter_label);
            this.Controls.Add(this.filtersubcategory_comboBox);
            this.Controls.Add(this.filtercategory_comboBox);
            this.Controls.Add(this.filterendadate_timePicker);
            this.Controls.Add(this.filterstartdate_timePicker);
            this.Controls.Add(this.filterminamount_textBox);
            this.Controls.Add(this.filtermaxamount_textBox);
            this.Controls.Add(this.Transactions_listBox);
            this.Controls.Add(this.Back_btn);
            this.Controls.Add(this.Filter_btn);
            this.Controls.Add(this.Delete_btn);
            this.Controls.Add(this.Save_btn);
            this.Controls.Add(this.wallet_comboBox);
            this.Controls.Add(this.type_comboBox);
            this.Controls.Add(this.location_textBox);
            this.Controls.Add(this.notes_textBox);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.amount_textBox);
            this.Controls.Add(this.subcategory_comboBox);
            this.Controls.Add(this.category_comboBox);
            this.Controls.Add(this.Transactions_label);
            this.Name = "transactions";
            this.Text = "transactions";
            this.Load += new System.EventHandler(this.transactions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Transactions_label;
        private System.Windows.Forms.ComboBox category_comboBox;
        private System.Windows.Forms.ComboBox subcategory_comboBox;
        private System.Windows.Forms.TextBox amount_textBox;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.TextBox notes_textBox;
        private System.Windows.Forms.TextBox location_textBox;
        private System.Windows.Forms.ComboBox type_comboBox;
        private System.Windows.Forms.ComboBox wallet_comboBox;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Button Delete_btn;
        private System.Windows.Forms.Button Filter_btn;
        private System.Windows.Forms.Button Back_btn;
        private System.Windows.Forms.ListBox Transactions_listBox;
        private System.Windows.Forms.TextBox filtermaxamount_textBox;
        private System.Windows.Forms.TextBox filterminamount_textBox;
        private System.Windows.Forms.DateTimePicker filterstartdate_timePicker;
        private System.Windows.Forms.DateTimePicker filterendadate_timePicker;
        private System.Windows.Forms.ComboBox filtercategory_comboBox;
        private System.Windows.Forms.ComboBox filtersubcategory_comboBox;
        private System.Windows.Forms.Label Filter_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox filterwallet_comboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox filtertype_comboBox;
        private System.Windows.Forms.RichTextBox Notifications;
        private System.Windows.Forms.Label label5;
    }
}