﻿namespace BlueBudget_DB
{
    partial class UserMenuForm
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
            this.Statistics_btn = new System.Windows.Forms.Button();
            this.back_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // stock_btn
            // 
            this.stock_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stock_btn.Location = new System.Drawing.Point(28, 195);
            this.stock_btn.Margin = new System.Windows.Forms.Padding(2);
            this.stock_btn.Name = "stock_btn";
            this.stock_btn.Size = new System.Drawing.Size(164, 80);
            this.stock_btn.TabIndex = 0;
            this.stock_btn.Text = "Stocks";
            this.stock_btn.UseVisualStyleBackColor = true;
            this.stock_btn.Click += new System.EventHandler(this.Stock_btn_Click);
            // 
            // loans_btn
            // 
            this.loans_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loans_btn.Location = new System.Drawing.Point(219, 195);
            this.loans_btn.Margin = new System.Windows.Forms.Padding(2);
            this.loans_btn.Name = "loans_btn";
            this.loans_btn.Size = new System.Drawing.Size(164, 80);
            this.loans_btn.TabIndex = 1;
            this.loans_btn.Text = "Loans";
            this.loans_btn.UseVisualStyleBackColor = true;
            this.loans_btn.Click += new System.EventHandler(this.Loans_btn_Click);
            // 
            // budget_btn
            // 
            this.budget_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.budget_btn.Location = new System.Drawing.Point(28, 78);
            this.budget_btn.Margin = new System.Windows.Forms.Padding(2);
            this.budget_btn.Name = "budget_btn";
            this.budget_btn.Size = new System.Drawing.Size(164, 80);
            this.budget_btn.TabIndex = 2;
            this.budget_btn.Text = "Budget";
            this.budget_btn.UseVisualStyleBackColor = true;
            this.budget_btn.Click += new System.EventHandler(this.Budget_btn_Click);
            // 
            // goal_btn
            // 
            this.goal_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goal_btn.Location = new System.Drawing.Point(410, 78);
            this.goal_btn.Margin = new System.Windows.Forms.Padding(2);
            this.goal_btn.Name = "goal_btn";
            this.goal_btn.Size = new System.Drawing.Size(164, 80);
            this.goal_btn.TabIndex = 3;
            this.goal_btn.Text = "Goals";
            this.goal_btn.UseVisualStyleBackColor = true;
            this.goal_btn.Click += new System.EventHandler(this.Goal_btn_Click);
            // 
            // transactions_btn
            // 
            this.transactions_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transactions_btn.Location = new System.Drawing.Point(219, 78);
            this.transactions_btn.Margin = new System.Windows.Forms.Padding(2);
            this.transactions_btn.Name = "transactions_btn";
            this.transactions_btn.Size = new System.Drawing.Size(164, 80);
            this.transactions_btn.TabIndex = 4;
            this.transactions_btn.Text = "Transactions";
            this.transactions_btn.UseVisualStyleBackColor = true;
            this.transactions_btn.Click += new System.EventHandler(this.Transactions_btn_Click);
            // 
            // Statistics_btn
            // 
            this.Statistics_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Statistics_btn.Location = new System.Drawing.Point(410, 195);
            this.Statistics_btn.Margin = new System.Windows.Forms.Padding(2);
            this.Statistics_btn.Name = "Statistics_btn";
            this.Statistics_btn.Size = new System.Drawing.Size(164, 80);
            this.Statistics_btn.TabIndex = 5;
            this.Statistics_btn.Text = "Statistics";
            this.Statistics_btn.UseVisualStyleBackColor = true;
            this.Statistics_btn.Click += new System.EventHandler(this.Statistics_btn_Click);
            // 
            // back_btn
            // 
            this.back_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_btn.Location = new System.Drawing.Point(500, 307);
            this.back_btn.Margin = new System.Windows.Forms.Padding(2);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(74, 27);
            this.back_btn.TabIndex = 6;
            this.back_btn.Text = "back";
            this.back_btn.UseVisualStyleBackColor = true;
            this.back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(171, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 37);
            this.label1.TabIndex = 7;
            this.label1.Text = "BLUE BUDGET";
            // 
            // UserMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.back_btn);
            this.Controls.Add(this.Statistics_btn);
            this.Controls.Add(this.transactions_btn);
            this.Controls.Add(this.goal_btn);
            this.Controls.Add(this.budget_btn);
            this.Controls.Add(this.loans_btn);
            this.Controls.Add(this.stock_btn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UserMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "user_menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button stock_btn;
        private System.Windows.Forms.Button loans_btn;
        private System.Windows.Forms.Button budget_btn;
        private System.Windows.Forms.Button goal_btn;
        private System.Windows.Forms.Button transactions_btn;
        private System.Windows.Forms.Button Statistics_btn;
        private System.Windows.Forms.Button back_btn;
        private System.Windows.Forms.Label label1;
    }
}