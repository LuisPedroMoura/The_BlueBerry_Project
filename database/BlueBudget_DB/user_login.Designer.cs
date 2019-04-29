namespace BlueBudget_DB
{
    partial class user_login
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
            this.label1 = new System.Windows.Forms.Label();
            this.accounts_listbox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.balance_label = new System.Windows.Forms.Label();
            this.patrimony_label = new System.Windows.Forms.Label();
            this.back_btn = new System.Windows.Forms.Button();
            this.login_btn = new System.Windows.Forms.Button();
            this.findme_btn = new System.Windows.Forms.Button();
            this.notifications_textbox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.userfindme_textbox = new System.Windows.Forms.TextBox();
            this.user_textbox = new System.Windows.Forms.TextBox();
            this.adduser_btn = new System.Windows.Forms.Button();
            this.deleteuser_btn = new System.Windows.Forms.Button();
            this.balance_textbox = new System.Windows.Forms.TextBox();
            this.patrimony_textbox = new System.Windows.Forms.TextBox();
            this.account_textbox = new System.Windows.Forms.TextBox();
            this.newaccount_btn = new System.Windows.Forms.Button();
            this.deleteaccount_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.associatedusers_listbox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(263, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "MONEY ACCOUNTS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // accounts_listbox
            // 
            this.accounts_listbox.FormattingEnabled = true;
            this.accounts_listbox.Location = new System.Drawing.Point(62, 153);
            this.accounts_listbox.Margin = new System.Windows.Forms.Padding(2);
            this.accounts_listbox.Name = "accounts_listbox";
            this.accounts_listbox.Size = new System.Drawing.Size(168, 134);
            this.accounts_listbox.TabIndex = 1;
            this.accounts_listbox.SelectedIndexChanged += new System.EventHandler(this.Accounts_listbox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(340, 118);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 24);
            this.label2.TabIndex = 2;
            // 
            // balance_label
            // 
            this.balance_label.AutoSize = true;
            this.balance_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.balance_label.Location = new System.Drawing.Point(345, 149);
            this.balance_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.balance_label.Name = "balance_label";
            this.balance_label.Size = new System.Drawing.Size(77, 24);
            this.balance_label.TabIndex = 3;
            this.balance_label.Text = "balance";
            // 
            // patrimony_label
            // 
            this.patrimony_label.AutoSize = true;
            this.patrimony_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patrimony_label.Location = new System.Drawing.Point(345, 184);
            this.patrimony_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.patrimony_label.Name = "patrimony_label";
            this.patrimony_label.Size = new System.Drawing.Size(92, 24);
            this.patrimony_label.TabIndex = 4;
            this.patrimony_label.Text = "patrimony";
            // 
            // back_btn
            // 
            this.back_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_btn.Location = new System.Drawing.Point(652, 379);
            this.back_btn.Margin = new System.Windows.Forms.Padding(2);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(74, 29);
            this.back_btn.TabIndex = 6;
            this.back_btn.Text = "back";
            this.back_btn.UseVisualStyleBackColor = true;
            this.back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // login_btn
            // 
            this.login_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_btn.Location = new System.Drawing.Point(558, 379);
            this.login_btn.Margin = new System.Windows.Forms.Padding(2);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(74, 29);
            this.login_btn.TabIndex = 7;
            this.login_btn.Text = "login";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.Login_btn_Click);
            // 
            // findme_btn
            // 
            this.findme_btn.Location = new System.Drawing.Point(235, 84);
            this.findme_btn.Margin = new System.Windows.Forms.Padding(2);
            this.findme_btn.Name = "findme_btn";
            this.findme_btn.Size = new System.Drawing.Size(56, 20);
            this.findme_btn.TabIndex = 9;
            this.findme_btn.Text = "Find Me!";
            this.findme_btn.UseVisualStyleBackColor = true;
            this.findme_btn.Click += new System.EventHandler(this.Findme_btn_Click);
            // 
            // notifications_textbox
            // 
            this.notifications_textbox.Location = new System.Drawing.Point(62, 349);
            this.notifications_textbox.Name = "notifications_textbox";
            this.notifications_textbox.ReadOnly = true;
            this.notifications_textbox.Size = new System.Drawing.Size(229, 59);
            this.notifications_textbox.TabIndex = 10;
            this.notifications_textbox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 333);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "notifications";
            // 
            // userfindme_textbox
            // 
            this.userfindme_textbox.Location = new System.Drawing.Point(62, 84);
            this.userfindme_textbox.Name = "userfindme_textbox";
            this.userfindme_textbox.Size = new System.Drawing.Size(168, 20);
            this.userfindme_textbox.TabIndex = 12;
            this.userfindme_textbox.Enter += new System.EventHandler(this.Email_textbox_Enter);
            this.userfindme_textbox.Leave += new System.EventHandler(this.Email_textbox_Leave);
            // 
            // user_textbox
            // 
            this.user_textbox.Location = new System.Drawing.Point(482, 226);
            this.user_textbox.Name = "user_textbox";
            this.user_textbox.Size = new System.Drawing.Size(141, 20);
            this.user_textbox.TabIndex = 13;
            this.user_textbox.Enter += new System.EventHandler(this.User_textbox_Enter);
            this.user_textbox.Leave += new System.EventHandler(this.User_textbox_Leave);
            // 
            // adduser_btn
            // 
            this.adduser_btn.Location = new System.Drawing.Point(629, 226);
            this.adduser_btn.Name = "adduser_btn";
            this.adduser_btn.Size = new System.Drawing.Size(90, 20);
            this.adduser_btn.TabIndex = 14;
            this.adduser_btn.Text = "add user";
            this.adduser_btn.UseVisualStyleBackColor = true;
            this.adduser_btn.Click += new System.EventHandler(this.Adduser_btn_Click);
            // 
            // deleteuser_btn
            // 
            this.deleteuser_btn.Location = new System.Drawing.Point(629, 252);
            this.deleteuser_btn.Name = "deleteuser_btn";
            this.deleteuser_btn.Size = new System.Drawing.Size(90, 20);
            this.deleteuser_btn.TabIndex = 15;
            this.deleteuser_btn.Text = "delete user";
            this.deleteuser_btn.UseVisualStyleBackColor = true;
            this.deleteuser_btn.Click += new System.EventHandler(this.Deleteuser_btn_Click);
            // 
            // balance_textbox
            // 
            this.balance_textbox.Location = new System.Drawing.Point(240, 153);
            this.balance_textbox.Name = "balance_textbox";
            this.balance_textbox.ReadOnly = true;
            this.balance_textbox.Size = new System.Drawing.Size(100, 20);
            this.balance_textbox.TabIndex = 16;
            // 
            // patrimony_textbox
            // 
            this.patrimony_textbox.Location = new System.Drawing.Point(240, 188);
            this.patrimony_textbox.Name = "patrimony_textbox";
            this.patrimony_textbox.ReadOnly = true;
            this.patrimony_textbox.Size = new System.Drawing.Size(100, 20);
            this.patrimony_textbox.TabIndex = 17;
            // 
            // account_textbox
            // 
            this.account_textbox.Location = new System.Drawing.Point(482, 149);
            this.account_textbox.Name = "account_textbox";
            this.account_textbox.Size = new System.Drawing.Size(141, 20);
            this.account_textbox.TabIndex = 18;
            this.account_textbox.Enter += new System.EventHandler(this.Account_textbox_Enter);
            this.account_textbox.Leave += new System.EventHandler(this.Account_textbox_Leave);
            // 
            // newaccount_btn
            // 
            this.newaccount_btn.Location = new System.Drawing.Point(629, 148);
            this.newaccount_btn.Name = "newaccount_btn";
            this.newaccount_btn.Size = new System.Drawing.Size(91, 21);
            this.newaccount_btn.TabIndex = 19;
            this.newaccount_btn.Text = "new account";
            this.newaccount_btn.UseVisualStyleBackColor = true;
            this.newaccount_btn.Click += new System.EventHandler(this.Newaccount_btn_Click);
            // 
            // deleteaccount_btn
            // 
            this.deleteaccount_btn.Location = new System.Drawing.Point(629, 171);
            this.deleteaccount_btn.Name = "deleteaccount_btn";
            this.deleteaccount_btn.Size = new System.Drawing.Size(91, 20);
            this.deleteaccount_btn.TabIndex = 20;
            this.deleteaccount_btn.Text = "delete account";
            this.deleteaccount_btn.UseVisualStyleBackColor = true;
            this.deleteaccount_btn.Click += new System.EventHandler(this.Deleteaccount_btn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "user money accounts";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(237, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "associated users";
            // 
            // associatedusers_listbox
            // 
            this.associatedusers_listbox.FormattingEnabled = true;
            this.associatedusers_listbox.Location = new System.Drawing.Point(240, 231);
            this.associatedusers_listbox.Name = "associatedusers_listbox";
            this.associatedusers_listbox.Size = new System.Drawing.Size(182, 56);
            this.associatedusers_listbox.TabIndex = 24;
            this.associatedusers_listbox.SelectedIndexChanged += new System.EventHandler(this.Associatedusers_listbox_SelectedIndexChanged);
            // 
            // user_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 434);
            this.Controls.Add(this.associatedusers_listbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.deleteaccount_btn);
            this.Controls.Add(this.newaccount_btn);
            this.Controls.Add(this.account_textbox);
            this.Controls.Add(this.patrimony_textbox);
            this.Controls.Add(this.balance_textbox);
            this.Controls.Add(this.deleteuser_btn);
            this.Controls.Add(this.adduser_btn);
            this.Controls.Add(this.user_textbox);
            this.Controls.Add(this.userfindme_textbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.notifications_textbox);
            this.Controls.Add(this.findme_btn);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.back_btn);
            this.Controls.Add(this.patrimony_label);
            this.Controls.Add(this.balance_label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.accounts_listbox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "user_login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "user_login";
            this.Load += new System.EventHandler(this.User_login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox accounts_listbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label balance_label;
        private System.Windows.Forms.Label patrimony_label;
        private System.Windows.Forms.Button back_btn;
        private System.Windows.Forms.Button login_btn;
        private System.Windows.Forms.Button findme_btn;
        private System.Windows.Forms.RichTextBox notifications_textbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userfindme_textbox;
        private System.Windows.Forms.TextBox user_textbox;
        private System.Windows.Forms.Button adduser_btn;
        private System.Windows.Forms.Button deleteuser_btn;
        private System.Windows.Forms.TextBox balance_textbox;
        private System.Windows.Forms.TextBox patrimony_textbox;
        private System.Windows.Forms.TextBox account_textbox;
        private System.Windows.Forms.Button newaccount_btn;
        private System.Windows.Forms.Button deleteaccount_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox associatedusers_listbox;
    }
}