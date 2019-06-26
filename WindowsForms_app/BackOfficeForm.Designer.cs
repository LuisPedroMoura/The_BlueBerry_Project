namespace BlueBudget_DB
{
    partial class BackOfficeForm
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
            this.add_btn = new System.Windows.Forms.Button();
            this.update_btn = new System.Windows.Forms.Button();
            this.delete_btn = new System.Windows.Forms.Button();
            this.user_info_label = new System.Windows.Forms.Label();
            this.subscrition_label = new System.Windows.Forms.Label();
            this.active_checkBox = new System.Windows.Forms.CheckBox();
            this.back_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.term_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Periodicity_comboBox = new System.Windows.Forms.ComboBox();
            this.username_textbox = new System.Windows.Forms.TextBox();
            this.email_textbox = new System.Windows.Forms.TextBox();
            this.firstname_textbox = new System.Windows.Forms.TextBox();
            this.lastname_textbox = new System.Windows.Forms.TextBox();
            this.middlename_textbox = new System.Windows.Forms.TextBox();
            this.cardnumber_textbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Users_listView = new System.Windows.Forms.ListView();
            this.BOUserName_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BOemail_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // add_btn
            // 
            this.add_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add_btn.Location = new System.Drawing.Point(269, 310);
            this.add_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(64, 24);
            this.add_btn.TabIndex = 4;
            this.add_btn.Text = "add";
            this.add_btn.UseVisualStyleBackColor = true;
            this.add_btn.Click += new System.EventHandler(this.Add_btn_Click);
            // 
            // update_btn
            // 
            this.update_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.update_btn.Location = new System.Drawing.Point(347, 310);
            this.update_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.update_btn.Name = "update_btn";
            this.update_btn.Size = new System.Drawing.Size(64, 24);
            this.update_btn.TabIndex = 5;
            this.update_btn.Text = "update";
            this.update_btn.UseVisualStyleBackColor = true;
            this.update_btn.Click += new System.EventHandler(this.Update_btn_Click);
            // 
            // delete_btn
            // 
            this.delete_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delete_btn.Location = new System.Drawing.Point(425, 310);
            this.delete_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.delete_btn.Name = "delete_btn";
            this.delete_btn.Size = new System.Drawing.Size(64, 24);
            this.delete_btn.TabIndex = 6;
            this.delete_btn.Text = "delete";
            this.delete_btn.UseVisualStyleBackColor = true;
            this.delete_btn.Click += new System.EventHandler(this.Delete_btn_Click);
            // 
            // user_info_label
            // 
            this.user_info_label.AutoSize = true;
            this.user_info_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user_info_label.Location = new System.Drawing.Point(358, 7);
            this.user_info_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.user_info_label.Name = "user_info_label";
            this.user_info_label.Size = new System.Drawing.Size(141, 26);
            this.user_info_label.TabIndex = 10;
            this.user_info_label.Text = "USER INFO";
            // 
            // subscrition_label
            // 
            this.subscrition_label.AutoSize = true;
            this.subscrition_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subscrition_label.Location = new System.Drawing.Point(351, 110);
            this.subscrition_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.subscrition_label.Name = "subscrition_label";
            this.subscrition_label.Size = new System.Drawing.Size(144, 26);
            this.subscrition_label.TabIndex = 17;
            this.subscrition_label.Text = "Subscription";
            // 
            // active_checkBox
            // 
            this.active_checkBox.AutoSize = true;
            this.active_checkBox.Location = new System.Drawing.Point(282, 148);
            this.active_checkBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.active_checkBox.Name = "active_checkBox";
            this.active_checkBox.Size = new System.Drawing.Size(62, 17);
            this.active_checkBox.TabIndex = 18;
            this.active_checkBox.Text = "Active?";
            this.active_checkBox.UseVisualStyleBackColor = true;
            // 
            // back_btn
            // 
            this.back_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_btn.Location = new System.Drawing.Point(503, 310);
            this.back_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(64, 24);
            this.back_btn.TabIndex = 21;
            this.back_btn.Text = "back";
            this.back_btn.UseVisualStyleBackColor = true;
            this.back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(266, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(266, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(266, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(266, 233);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(266, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(556, 266);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "*";
            // 
            // term_dateTimePicker
            // 
            this.term_dateTimePicker.Location = new System.Drawing.Point(384, 266);
            this.term_dateTimePicker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.term_dateTimePicker.Name = "term_dateTimePicker";
            this.term_dateTimePicker.Size = new System.Drawing.Size(168, 20);
            this.term_dateTimePicker.TabIndex = 30;
            // 
            // Periodicity_comboBox
            // 
            this.Periodicity_comboBox.FormattingEnabled = true;
            this.Periodicity_comboBox.Location = new System.Drawing.Point(282, 266);
            this.Periodicity_comboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Periodicity_comboBox.Name = "Periodicity_comboBox";
            this.Periodicity_comboBox.Size = new System.Drawing.Size(92, 21);
            this.Periodicity_comboBox.TabIndex = 31;
            // 
            // username_textbox
            // 
            this.username_textbox.Location = new System.Drawing.Point(282, 44);
            this.username_textbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.username_textbox.Name = "username_textbox";
            this.username_textbox.Size = new System.Drawing.Size(269, 20);
            this.username_textbox.TabIndex = 33;
            this.username_textbox.Enter += new System.EventHandler(this.Username_textbox_Enter);
            this.username_textbox.Leave += new System.EventHandler(this.Username_textbox_Leave_1);
            // 
            // email_textbox
            // 
            this.email_textbox.Location = new System.Drawing.Point(282, 78);
            this.email_textbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.email_textbox.Name = "email_textbox";
            this.email_textbox.Size = new System.Drawing.Size(269, 20);
            this.email_textbox.TabIndex = 34;
            this.email_textbox.Enter += new System.EventHandler(this.Email_textbox_Enter);
            this.email_textbox.Leave += new System.EventHandler(this.Email_textbox_Leave_1);
            // 
            // firstname_textbox
            // 
            this.firstname_textbox.Location = new System.Drawing.Point(282, 170);
            this.firstname_textbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.firstname_textbox.Name = "firstname_textbox";
            this.firstname_textbox.Size = new System.Drawing.Size(138, 20);
            this.firstname_textbox.TabIndex = 35;
            this.firstname_textbox.Enter += new System.EventHandler(this.Firstname_textbox_Enter);
            this.firstname_textbox.Leave += new System.EventHandler(this.Firstname_textbox_Leave_1);
            // 
            // lastname_textbox
            // 
            this.lastname_textbox.Location = new System.Drawing.Point(282, 202);
            this.lastname_textbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lastname_textbox.Name = "lastname_textbox";
            this.lastname_textbox.Size = new System.Drawing.Size(138, 20);
            this.lastname_textbox.TabIndex = 36;
            this.lastname_textbox.Enter += new System.EventHandler(this.Lastname_textbox_Enter);
            this.lastname_textbox.Leave += new System.EventHandler(this.Lastname_textbox_Leave_1);
            // 
            // middlename_textbox
            // 
            this.middlename_textbox.Location = new System.Drawing.Point(425, 169);
            this.middlename_textbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.middlename_textbox.Name = "middlename_textbox";
            this.middlename_textbox.Size = new System.Drawing.Size(126, 20);
            this.middlename_textbox.TabIndex = 37;
            this.middlename_textbox.Enter += new System.EventHandler(this.Middlename_textbox_Enter);
            this.middlename_textbox.Leave += new System.EventHandler(this.Middlename_textbox_Leave_1);
            // 
            // cardnumber_textbox
            // 
            this.cardnumber_textbox.Location = new System.Drawing.Point(282, 233);
            this.cardnumber_textbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cardnumber_textbox.Name = "cardnumber_textbox";
            this.cardnumber_textbox.Size = new System.Drawing.Size(269, 20);
            this.cardnumber_textbox.TabIndex = 38;
            this.cardnumber_textbox.Enter += new System.EventHandler(this.Cardnumber_textbox_Enter);
            this.cardnumber_textbox.Leave += new System.EventHandler(this.Cardnumber_textbox_Leave_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "registered users";
            // 
            // Users_listView
            // 
            this.Users_listView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.Users_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.BOUserName_col,
            this.BOemail_col});
            this.Users_listView.FullRowSelect = true;
            this.Users_listView.Location = new System.Drawing.Point(28, 44);
            this.Users_listView.Name = "Users_listView";
            this.Users_listView.Size = new System.Drawing.Size(211, 290);
            this.Users_listView.TabIndex = 42;
            this.Users_listView.UseCompatibleStateImageBehavior = false;
            this.Users_listView.View = System.Windows.Forms.View.Details;
            this.Users_listView.SelectedIndexChanged += new System.EventHandler(this.Users_listView_SelectedIndexChanged);
            // 
            // BOUserName_col
            // 
            this.BOUserName_col.Text = "Username";
            this.BOUserName_col.Width = 90;
            // 
            // BOemail_col
            // 
            this.BOemail_col.Text = "email";
            this.BOemail_col.Width = 120;
            // 
            // BackOfficeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.Users_listView);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cardnumber_textbox);
            this.Controls.Add(this.middlename_textbox);
            this.Controls.Add(this.lastname_textbox);
            this.Controls.Add(this.firstname_textbox);
            this.Controls.Add(this.email_textbox);
            this.Controls.Add(this.username_textbox);
            this.Controls.Add(this.Periodicity_comboBox);
            this.Controls.Add(this.term_dateTimePicker);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.back_btn);
            this.Controls.Add(this.active_checkBox);
            this.Controls.Add(this.subscrition_label);
            this.Controls.Add(this.user_info_label);
            this.Controls.Add(this.delete_btn);
            this.Controls.Add(this.update_btn);
            this.Controls.Add(this.add_btn);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "BackOfficeForm";
            this.Text = "back_office";
            this.Load += new System.EventHandler(this.Back_office_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button add_btn;
        private System.Windows.Forms.Button update_btn;
        private System.Windows.Forms.Button delete_btn;
        private System.Windows.Forms.Label user_info_label;
        private System.Windows.Forms.Label subscrition_label;
        private System.Windows.Forms.CheckBox active_checkBox;
        private System.Windows.Forms.Button back_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker term_dateTimePicker;
        private System.Windows.Forms.ComboBox Periodicity_comboBox;
        private System.Windows.Forms.TextBox username_textbox;
        private System.Windows.Forms.TextBox email_textbox;
        private System.Windows.Forms.TextBox firstname_textbox;
        private System.Windows.Forms.TextBox lastname_textbox;
        private System.Windows.Forms.TextBox middlename_textbox;
        private System.Windows.Forms.TextBox cardnumber_textbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView Users_listView;
        private System.Windows.Forms.ColumnHeader BOUserName_col;
        private System.Windows.Forms.ColumnHeader BOemail_col;
    }
}