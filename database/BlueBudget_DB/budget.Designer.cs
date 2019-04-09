namespace BlueBudget_DB
{
    partial class budget
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
            this.categories_listbox = new System.Windows.Forms.ListBox();
            this.back_btn = new System.Windows.Forms.Button();
            this.budget_label = new System.Windows.Forms.Label();
            this.save_btn = new System.Windows.Forms.Button();
            this.cat_listbox_label = new System.Windows.Forms.Label();
            this.newsubcategory_btn = new System.Windows.Forms.Button();
            this.newcategory_btn = new System.Windows.Forms.Button();
            this.category_label = new System.Windows.Forms.Label();
            this.subcategory_label = new System.Windows.Forms.Label();
            this.amount_textbox = new System.Windows.Forms.PlaceholderTextBox();
            this.SuspendLayout();
            // 
            // categories_listbox
            // 
            this.categories_listbox.FormattingEnabled = true;
            this.categories_listbox.ItemHeight = 16;
            this.categories_listbox.Location = new System.Drawing.Point(41, 56);
            this.categories_listbox.Name = "categories_listbox";
            this.categories_listbox.Size = new System.Drawing.Size(261, 292);
            this.categories_listbox.TabIndex = 38;
            // 
            // back_btn
            // 
            this.back_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_btn.Location = new System.Drawing.Point(674, 383);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(86, 29);
            this.back_btn.TabIndex = 37;
            this.back_btn.Text = "back";
            this.back_btn.UseVisualStyleBackColor = true;
            this.back_btn.Click += new System.EventHandler(this.back_btn_Click);
            // 
            // budget_label
            // 
            this.budget_label.AutoSize = true;
            this.budget_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.budget_label.Location = new System.Drawing.Point(481, 11);
            this.budget_label.Name = "budget_label";
            this.budget_label.Size = new System.Drawing.Size(138, 32);
            this.budget_label.TabIndex = 26;
            this.budget_label.Text = "BUDGET";
            this.budget_label.Click += new System.EventHandler(this.user_info_label_Click);
            // 
            // save_btn
            // 
            this.save_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_btn.Location = new System.Drawing.Point(559, 383);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(86, 29);
            this.save_btn.TabIndex = 24;
            this.save_btn.Text = "save";
            this.save_btn.UseVisualStyleBackColor = true;
            // 
            // cat_listbox_label
            // 
            this.cat_listbox_label.AutoSize = true;
            this.cat_listbox_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cat_listbox_label.Location = new System.Drawing.Point(36, 28);
            this.cat_listbox_label.Name = "cat_listbox_label";
            this.cat_listbox_label.Size = new System.Drawing.Size(107, 25);
            this.cat_listbox_label.TabIndex = 39;
            this.cat_listbox_label.Text = "Categories";
            this.cat_listbox_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // newsubcategory_btn
            // 
            this.newsubcategory_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newsubcategory_btn.Location = new System.Drawing.Point(170, 354);
            this.newsubcategory_btn.Name = "newsubcategory_btn";
            this.newsubcategory_btn.Size = new System.Drawing.Size(132, 58);
            this.newsubcategory_btn.TabIndex = 40;
            this.newsubcategory_btn.Text = "New Sub-category";
            this.newsubcategory_btn.UseVisualStyleBackColor = true;
            // 
            // newcategory_btn
            // 
            this.newcategory_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newcategory_btn.Location = new System.Drawing.Point(41, 354);
            this.newcategory_btn.Name = "newcategory_btn";
            this.newcategory_btn.Size = new System.Drawing.Size(132, 58);
            this.newcategory_btn.TabIndex = 41;
            this.newcategory_btn.Text = "New Category";
            this.newcategory_btn.UseVisualStyleBackColor = true;
            // 
            // category_label
            // 
            this.category_label.AutoSize = true;
            this.category_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.category_label.Location = new System.Drawing.Point(382, 78);
            this.category_label.Name = "category_label";
            this.category_label.Size = new System.Drawing.Size(92, 25);
            this.category_label.TabIndex = 42;
            this.category_label.Text = "Category";
            // 
            // subcategory_label
            // 
            this.subcategory_label.AutoSize = true;
            this.subcategory_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subcategory_label.Location = new System.Drawing.Point(382, 121);
            this.subcategory_label.Name = "subcategory_label";
            this.subcategory_label.Size = new System.Drawing.Size(130, 25);
            this.subcategory_label.TabIndex = 43;
            this.subcategory_label.Text = "Sub-category";
            // 
            // amount_textbox
            // 
            this.amount_textbox.Location = new System.Drawing.Point(382, 177);
            this.amount_textbox.Name = "amount_textbox";
            this.amount_textbox.PlaceholderText = "budget amount";
            this.amount_textbox.Size = new System.Drawing.Size(183, 22);
            this.amount_textbox.TabIndex = 30;
            // 
            // budget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.subcategory_label);
            this.Controls.Add(this.category_label);
            this.Controls.Add(this.newcategory_btn);
            this.Controls.Add(this.newsubcategory_btn);
            this.Controls.Add(this.cat_listbox_label);
            this.Controls.Add(this.categories_listbox);
            this.Controls.Add(this.back_btn);
            this.Controls.Add(this.amount_textbox);
            this.Controls.Add(this.budget_label);
            this.Controls.Add(this.save_btn);
            this.Name = "budget";
            this.Text = "budget";
            this.Load += new System.EventHandler(this.budget_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox categories_listbox;
        private System.Windows.Forms.Button back_btn;
        private System.Windows.Forms.PlaceholderTextBox amount_textbox;
        private System.Windows.Forms.Label budget_label;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Label cat_listbox_label;
        private System.Windows.Forms.Button newsubcategory_btn;
        private System.Windows.Forms.Button newcategory_btn;
        private System.Windows.Forms.Label category_label;
        private System.Windows.Forms.Label subcategory_label;
    }
}