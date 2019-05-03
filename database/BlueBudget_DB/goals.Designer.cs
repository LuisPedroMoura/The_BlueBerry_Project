namespace BlueBudget_DB
{
    partial class goals
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
            this.Goals_label = new System.Windows.Forms.Label();
            this.Goals_listBox = new System.Windows.Forms.ListBox();
            this.goalname_textBox = new System.Windows.Forms.TextBox();
            this.goalamount_textBox = new System.Windows.Forms.TextBox();
            this.term_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Save_btn = new System.Windows.Forms.Button();
            this.Back_btn = new System.Windows.Forms.Button();
            this.Categories_comboBox = new System.Windows.Forms.ComboBox();
            this.Subcategories_comboBox = new System.Windows.Forms.ComboBox();
            this.term_label = new System.Windows.Forms.Label();
            this.Notifications = new System.Windows.Forms.RichTextBox();
            this.Notifications_label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Goals_label
            // 
            this.Goals_label.AutoSize = true;
            this.Goals_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Goals_label.Location = new System.Drawing.Point(247, 9);
            this.Goals_label.Name = "Goals_label";
            this.Goals_label.Size = new System.Drawing.Size(99, 29);
            this.Goals_label.TabIndex = 0;
            this.Goals_label.Text = "GOALS";
            // 
            // Goals_listBox
            // 
            this.Goals_listBox.FormattingEnabled = true;
            this.Goals_listBox.Location = new System.Drawing.Point(47, 72);
            this.Goals_listBox.Name = "Goals_listBox";
            this.Goals_listBox.Size = new System.Drawing.Size(207, 147);
            this.Goals_listBox.TabIndex = 1;
            this.Goals_listBox.SelectedIndexChanged += new System.EventHandler(this.Goals_listBox_SelectedIndexChanged);
            // 
            // goalname_textBox
            // 
            this.goalname_textBox.Location = new System.Drawing.Point(292, 99);
            this.goalname_textBox.Name = "goalname_textBox";
            this.goalname_textBox.Size = new System.Drawing.Size(248, 20);
            this.goalname_textBox.TabIndex = 2;
            // 
            // goalamount_textBox
            // 
            this.goalamount_textBox.Location = new System.Drawing.Point(419, 125);
            this.goalamount_textBox.Name = "goalamount_textBox";
            this.goalamount_textBox.Size = new System.Drawing.Size(121, 20);
            this.goalamount_textBox.TabIndex = 3;
            // 
            // term_dateTimePicker
            // 
            this.term_dateTimePicker.Location = new System.Drawing.Point(340, 151);
            this.term_dateTimePicker.Name = "term_dateTimePicker";
            this.term_dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.term_dateTimePicker.TabIndex = 4;
            // 
            // Save_btn
            // 
            this.Save_btn.Location = new System.Drawing.Point(340, 308);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Size = new System.Drawing.Size(75, 23);
            this.Save_btn.TabIndex = 5;
            this.Save_btn.Text = "Save New";
            this.Save_btn.UseVisualStyleBackColor = true;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // Back_btn
            // 
            this.Back_btn.Location = new System.Drawing.Point(465, 308);
            this.Back_btn.Name = "Back_btn";
            this.Back_btn.Size = new System.Drawing.Size(75, 23);
            this.Back_btn.TabIndex = 7;
            this.Back_btn.Text = "Back";
            this.Back_btn.UseVisualStyleBackColor = true;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // Categories_comboBox
            // 
            this.Categories_comboBox.FormattingEnabled = true;
            this.Categories_comboBox.Location = new System.Drawing.Point(292, 72);
            this.Categories_comboBox.Name = "Categories_comboBox";
            this.Categories_comboBox.Size = new System.Drawing.Size(121, 21);
            this.Categories_comboBox.TabIndex = 8;
            this.Categories_comboBox.SelectedIndexChanged += new System.EventHandler(this.Categories_comboBox_SelectedIndexChanged);
            // 
            // Subcategories_comboBox
            // 
            this.Subcategories_comboBox.FormattingEnabled = true;
            this.Subcategories_comboBox.Location = new System.Drawing.Point(419, 72);
            this.Subcategories_comboBox.Name = "Subcategories_comboBox";
            this.Subcategories_comboBox.Size = new System.Drawing.Size(121, 21);
            this.Subcategories_comboBox.TabIndex = 9;
            // 
            // term_label
            // 
            this.term_label.AutoSize = true;
            this.term_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.term_label.Location = new System.Drawing.Point(287, 151);
            this.term_label.Name = "term_label";
            this.term_label.Size = new System.Drawing.Size(47, 13);
            this.term_label.TabIndex = 10;
            this.term_label.Text = "deadline";
            // 
            // Notifications
            // 
            this.Notifications.Location = new System.Drawing.Point(47, 270);
            this.Notifications.Name = "Notifications";
            this.Notifications.Size = new System.Drawing.Size(207, 61);
            this.Notifications.TabIndex = 11;
            this.Notifications.Text = "";
            // 
            // Notifications_label
            // 
            this.Notifications_label.AutoSize = true;
            this.Notifications_label.Location = new System.Drawing.Point(44, 254);
            this.Notifications_label.Name = "Notifications_label";
            this.Notifications_label.Size = new System.Drawing.Size(65, 13);
            this.Notifications_label.TabIndex = 12;
            this.Notifications_label.Text = "Notifications";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(365, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 24);
            this.label2.TabIndex = 14;
            this.label2.Text = "Goal State Label";
            // 
            // goals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Notifications_label);
            this.Controls.Add(this.Notifications);
            this.Controls.Add(this.term_label);
            this.Controls.Add(this.Subcategories_comboBox);
            this.Controls.Add(this.Categories_comboBox);
            this.Controls.Add(this.Back_btn);
            this.Controls.Add(this.Save_btn);
            this.Controls.Add(this.term_dateTimePicker);
            this.Controls.Add(this.goalamount_textBox);
            this.Controls.Add(this.goalname_textBox);
            this.Controls.Add(this.Goals_listBox);
            this.Controls.Add(this.Goals_label);
            this.Name = "goals";
            this.Text = "goals";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Goals_label;
        private System.Windows.Forms.ListBox Goals_listBox;
        private System.Windows.Forms.TextBox goalname_textBox;
        private System.Windows.Forms.TextBox goalamount_textBox;
        private System.Windows.Forms.DateTimePicker term_dateTimePicker;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Button Back_btn;
        private System.Windows.Forms.ComboBox Categories_comboBox;
        private System.Windows.Forms.ComboBox Subcategories_comboBox;
        private System.Windows.Forms.Label term_label;
        private System.Windows.Forms.RichTextBox Notifications;
        private System.Windows.Forms.Label Notifications_label;
        private System.Windows.Forms.Label label2;
    }
}