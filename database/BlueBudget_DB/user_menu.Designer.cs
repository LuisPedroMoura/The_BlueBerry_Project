namespace BlueBudget_DB
{
    partial class user_menu
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
            this.stock_btn = new System.Windows.Forms.Button();
            this.loans_btn = new System.Windows.Forms.Button();
            this.budget_btn = new System.Windows.Forms.Button();
            this.goal_btn = new System.Windows.Forms.Button();
            this.transactions_btn = new System.Windows.Forms.Button();
            this.settings_btn = new System.Windows.Forms.Button();
            this.back_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // stock_btn
            // 
            this.stock_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stock_btn.Location = new System.Drawing.Point(47, 166);
            this.stock_btn.Margin = new System.Windows.Forms.Padding(2);
            this.stock_btn.Name = "stock_btn";
            this.stock_btn.Size = new System.Drawing.Size(164, 80);
            this.stock_btn.TabIndex = 0;
            this.stock_btn.Text = "Stocks";
            this.stock_btn.UseVisualStyleBackColor = true;
            this.stock_btn.Click += new System.EventHandler(this.stock_btn_Click);
            // 
            // loans_btn
            // 
            this.loans_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loans_btn.Location = new System.Drawing.Point(238, 166);
            this.loans_btn.Margin = new System.Windows.Forms.Padding(2);
            this.loans_btn.Name = "loans_btn";
            this.loans_btn.Size = new System.Drawing.Size(164, 80);
            this.loans_btn.TabIndex = 1;
            this.loans_btn.Text = "Loans";
            this.loans_btn.UseVisualStyleBackColor = true;
            this.loans_btn.Click += new System.EventHandler(this.loans_btn_Click);
            // 
            // budget_btn
            // 
            this.budget_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.budget_btn.Location = new System.Drawing.Point(47, 49);
            this.budget_btn.Margin = new System.Windows.Forms.Padding(2);
            this.budget_btn.Name = "budget_btn";
            this.budget_btn.Size = new System.Drawing.Size(164, 80);
            this.budget_btn.TabIndex = 2;
            this.budget_btn.Text = "Budget";
            this.budget_btn.UseVisualStyleBackColor = true;
            this.budget_btn.Click += new System.EventHandler(this.budget_btn_Click);
            // 
            // goal_btn
            // 
            this.goal_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goal_btn.Location = new System.Drawing.Point(429, 49);
            this.goal_btn.Margin = new System.Windows.Forms.Padding(2);
            this.goal_btn.Name = "goal_btn";
            this.goal_btn.Size = new System.Drawing.Size(164, 80);
            this.goal_btn.TabIndex = 3;
            this.goal_btn.Text = "Goals";
            this.goal_btn.UseVisualStyleBackColor = true;
            this.goal_btn.Click += new System.EventHandler(this.goal_btn_Click);
            // 
            // transactions_btn
            // 
            this.transactions_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transactions_btn.Location = new System.Drawing.Point(238, 49);
            this.transactions_btn.Margin = new System.Windows.Forms.Padding(2);
            this.transactions_btn.Name = "transactions_btn";
            this.transactions_btn.Size = new System.Drawing.Size(164, 80);
            this.transactions_btn.TabIndex = 4;
            this.transactions_btn.Text = "Transactions";
            this.transactions_btn.UseVisualStyleBackColor = true;
            this.transactions_btn.Click += new System.EventHandler(this.transactions_btn_Click);
            // 
            // settings_btn
            // 
            this.settings_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settings_btn.Location = new System.Drawing.Point(429, 166);
            this.settings_btn.Margin = new System.Windows.Forms.Padding(2);
            this.settings_btn.Name = "settings_btn";
            this.settings_btn.Size = new System.Drawing.Size(164, 80);
            this.settings_btn.TabIndex = 5;
            this.settings_btn.Text = "Settings";
            this.settings_btn.UseVisualStyleBackColor = true;
            // 
            // back_btn
            // 
            this.back_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_btn.Location = new System.Drawing.Point(518, 308);
            this.back_btn.Margin = new System.Windows.Forms.Padding(2);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(74, 27);
            this.back_btn.TabIndex = 6;
            this.back_btn.Text = "back";
            this.back_btn.UseVisualStyleBackColor = true;
            this.back_btn.Click += new System.EventHandler(this.back_btn_Click);
            // 
            // user_menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(640, 360);
            this.Controls.Add(this.back_btn);
            this.Controls.Add(this.settings_btn);
            this.Controls.Add(this.transactions_btn);
            this.Controls.Add(this.goal_btn);
            this.Controls.Add(this.budget_btn);
            this.Controls.Add(this.loans_btn);
            this.Controls.Add(this.stock_btn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "user_menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "user_menu";
            this.Load += new System.EventHandler(this.user_menu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button stock_btn;
        private System.Windows.Forms.Button loans_btn;
        private System.Windows.Forms.Button budget_btn;
        private System.Windows.Forms.Button goal_btn;
        private System.Windows.Forms.Button transactions_btn;
        private System.Windows.Forms.Button settings_btn;
        private System.Windows.Forms.Button back_btn;
    }
}