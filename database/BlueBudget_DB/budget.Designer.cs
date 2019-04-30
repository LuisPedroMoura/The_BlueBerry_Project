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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // categories_listbox
            // 
            this.categories_listbox.FormattingEnabled = true;
            this.categories_listbox.Location = new System.Drawing.Point(31, 46);
            this.categories_listbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.categories_listbox.Name = "categories_listbox";
            this.categories_listbox.Size = new System.Drawing.Size(222, 238);
            this.categories_listbox.TabIndex = 38;
            // 
            // back_btn
            // 
            this.back_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_btn.Location = new System.Drawing.Point(506, 311);
            this.back_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(64, 24);
            this.back_btn.TabIndex = 37;
            this.back_btn.Text = "back";
            this.back_btn.UseVisualStyleBackColor = true;
            this.back_btn.Click += new System.EventHandler(this.back_btn_Click);
            // 
            // budget_label
            // 
            this.budget_label.AutoSize = true;
            this.budget_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.budget_label.Location = new System.Drawing.Point(361, 9);
            this.budget_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.budget_label.Name = "budget_label";
            this.budget_label.Size = new System.Drawing.Size(109, 26);
            this.budget_label.TabIndex = 26;
            this.budget_label.Text = "BUDGET";
            this.budget_label.Click += new System.EventHandler(this.user_info_label_Click);
            // 
            // save_btn
            // 
            this.save_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_btn.Location = new System.Drawing.Point(419, 311);
            this.save_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(64, 24);
            this.save_btn.TabIndex = 24;
            this.save_btn.Text = "save";
            this.save_btn.UseVisualStyleBackColor = true;
            // 
            // cat_listbox_label
            // 
            this.cat_listbox_label.AutoSize = true;
            this.cat_listbox_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cat_listbox_label.Location = new System.Drawing.Point(27, 23);
            this.cat_listbox_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cat_listbox_label.Name = "cat_listbox_label";
            this.cat_listbox_label.Size = new System.Drawing.Size(86, 20);
            this.cat_listbox_label.TabIndex = 39;
            this.cat_listbox_label.Text = "Categories";
            this.cat_listbox_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // newsubcategory_btn
            // 
            this.newsubcategory_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newsubcategory_btn.Location = new System.Drawing.Point(144, 288);
            this.newsubcategory_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.newsubcategory_btn.Name = "newsubcategory_btn";
            this.newsubcategory_btn.Size = new System.Drawing.Size(109, 47);
            this.newsubcategory_btn.TabIndex = 40;
            this.newsubcategory_btn.Text = "New Sub-category";
            this.newsubcategory_btn.UseVisualStyleBackColor = true;
            // 
            // newcategory_btn
            // 
            this.newcategory_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newcategory_btn.Location = new System.Drawing.Point(31, 288);
            this.newcategory_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.newcategory_btn.Name = "newcategory_btn";
            this.newcategory_btn.Size = new System.Drawing.Size(109, 47);
            this.newcategory_btn.TabIndex = 41;
            this.newcategory_btn.Text = "New Category";
            this.newcategory_btn.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(419, 161);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 44;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(419, 249);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 45;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(419, 116);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(121, 20);
            this.textBox2.TabIndex = 46;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(419, 70);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(121, 20);
            this.textBox3.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(364, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Category";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(343, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Sub-category";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(372, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 50;
            this.label3.Text = "Budget";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(350, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Recurrence";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(419, 207);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 52;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(382, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "Type";
            // 
            // budget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.newcategory_btn);
            this.Controls.Add(this.newsubcategory_btn);
            this.Controls.Add(this.cat_listbox_label);
            this.Controls.Add(this.categories_listbox);
            this.Controls.Add(this.back_btn);
            this.Controls.Add(this.budget_label);
            this.Controls.Add(this.save_btn);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "budget";
            this.Text = "budget";
            this.Load += new System.EventHandler(this.budget_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox categories_listbox;
        private System.Windows.Forms.Button back_btn;
        private System.Windows.Forms.Label budget_label;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Label cat_listbox_label;
        private System.Windows.Forms.Button newsubcategory_btn;
        private System.Windows.Forms.Button newcategory_btn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label5;
    }
}