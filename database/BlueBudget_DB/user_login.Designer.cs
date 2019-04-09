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
            this.account_name_label = new System.Windows.Forms.Label();
            this.back_btn = new System.Windows.Forms.Button();
            this.login_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(202, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(389, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "YOUR MONEY ACCOUNTS";
            // 
            // accounts_listbox
            // 
            this.accounts_listbox.FormattingEnabled = true;
            this.accounts_listbox.ItemHeight = 16;
            this.accounts_listbox.Location = new System.Drawing.Point(101, 125);
            this.accounts_listbox.Name = "accounts_listbox";
            this.accounts_listbox.Size = new System.Drawing.Size(305, 164);
            this.accounts_listbox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(483, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 29);
            this.label2.TabIndex = 2;
            // 
            // balance_label
            // 
            this.balance_label.AutoSize = true;
            this.balance_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.balance_label.Location = new System.Drawing.Point(461, 176);
            this.balance_label.Name = "balance_label";
            this.balance_label.Size = new System.Drawing.Size(98, 29);
            this.balance_label.TabIndex = 3;
            this.balance_label.Text = "balance";
            // 
            // patrimony_label
            // 
            this.patrimony_label.AutoSize = true;
            this.patrimony_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patrimony_label.Location = new System.Drawing.Point(461, 230);
            this.patrimony_label.Name = "patrimony_label";
            this.patrimony_label.Size = new System.Drawing.Size(118, 29);
            this.patrimony_label.TabIndex = 4;
            this.patrimony_label.Text = "patrimony";
            // 
            // account_name_label
            // 
            this.account_name_label.AutoSize = true;
            this.account_name_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.account_name_label.Location = new System.Drawing.Point(461, 125);
            this.account_name_label.Name = "account_name_label";
            this.account_name_label.Size = new System.Drawing.Size(169, 29);
            this.account_name_label.TabIndex = 5;
            this.account_name_label.Text = "account_name";
            // 
            // back_btn
            // 
            this.back_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_btn.Location = new System.Drawing.Point(663, 377);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(99, 36);
            this.back_btn.TabIndex = 6;
            this.back_btn.Text = "back";
            this.back_btn.UseVisualStyleBackColor = true;
            this.back_btn.Click += new System.EventHandler(this.back_btn_Click);
            // 
            // login_btn
            // 
            this.login_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_btn.Location = new System.Drawing.Point(530, 377);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(99, 36);
            this.login_btn.TabIndex = 7;
            this.login_btn.Text = "login";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // user_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.back_btn);
            this.Controls.Add(this.account_name_label);
            this.Controls.Add(this.patrimony_label);
            this.Controls.Add(this.balance_label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.accounts_listbox);
            this.Controls.Add(this.label1);
            this.Name = "user_login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "user_login";
            this.Load += new System.EventHandler(this.user_login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox accounts_listbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label balance_label;
        private System.Windows.Forms.Label patrimony_label;
        private System.Windows.Forms.Label account_name_label;
        private System.Windows.Forms.Button back_btn;
        private System.Windows.Forms.Button login_btn;
    }
}