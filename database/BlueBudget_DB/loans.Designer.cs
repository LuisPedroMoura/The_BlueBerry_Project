namespace BlueBudget_DB
{
    partial class loans
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
            this.loans_listBox = new System.Windows.Forms.ListBox();
            this.loans_label = new System.Windows.Forms.Label();
            this.Name_testBox = new System.Windows.Forms.TextBox();
            this.Amount_texxtBox = new System.Windows.Forms.TextBox();
            this.Enddate_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Interest_textBox = new System.Windows.Forms.TextBox();
            this.Save_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Back_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loans_listBox
            // 
            this.loans_listBox.FormattingEnabled = true;
            this.loans_listBox.Location = new System.Drawing.Point(38, 57);
            this.loans_listBox.Name = "loans_listBox";
            this.loans_listBox.Size = new System.Drawing.Size(205, 160);
            this.loans_listBox.TabIndex = 0;
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
            // Name_testBox
            // 
            this.Name_testBox.Location = new System.Drawing.Point(336, 57);
            this.Name_testBox.Name = "Name_testBox";
            this.Name_testBox.Size = new System.Drawing.Size(200, 20);
            this.Name_testBox.TabIndex = 2;
            // 
            // Amount_texxtBox
            // 
            this.Amount_texxtBox.Location = new System.Drawing.Point(336, 83);
            this.Amount_texxtBox.Name = "Amount_texxtBox";
            this.Amount_texxtBox.Size = new System.Drawing.Size(200, 20);
            this.Amount_texxtBox.TabIndex = 3;
            // 
            // Enddate_dateTimePicker
            // 
            this.Enddate_dateTimePicker.Location = new System.Drawing.Point(336, 109);
            this.Enddate_dateTimePicker.Name = "Enddate_dateTimePicker";
            this.Enddate_dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.Enddate_dateTimePicker.TabIndex = 4;
            // 
            // Interest_textBox
            // 
            this.Interest_textBox.Location = new System.Drawing.Point(436, 135);
            this.Interest_textBox.Name = "Interest_textBox";
            this.Interest_textBox.Size = new System.Drawing.Size(100, 20);
            this.Interest_textBox.TabIndex = 5;
            // 
            // Save_btn
            // 
            this.Save_btn.Location = new System.Drawing.Point(336, 299);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Size = new System.Drawing.Size(75, 23);
            this.Save_btn.TabIndex = 6;
            this.Save_btn.Text = "Save New";
            this.Save_btn.UseVisualStyleBackColor = true;
            this.Save_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(389, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "interest";
            // 
            // Back_btn
            // 
            this.Back_btn.Location = new System.Drawing.Point(461, 299);
            this.Back_btn.Name = "Back_btn";
            this.Back_btn.Size = new System.Drawing.Size(75, 23);
            this.Back_btn.TabIndex = 8;
            this.Back_btn.Text = "Back";
            this.Back_btn.UseVisualStyleBackColor = true;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // loans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.Back_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Save_btn);
            this.Controls.Add(this.Interest_textBox);
            this.Controls.Add(this.Enddate_dateTimePicker);
            this.Controls.Add(this.Amount_texxtBox);
            this.Controls.Add(this.Name_testBox);
            this.Controls.Add(this.loans_label);
            this.Controls.Add(this.loans_listBox);
            this.Name = "loans";
            this.Text = "loans";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox loans_listBox;
        private System.Windows.Forms.Label loans_label;
        private System.Windows.Forms.TextBox Name_testBox;
        private System.Windows.Forms.TextBox Amount_texxtBox;
        private System.Windows.Forms.DateTimePicker Enddate_dateTimePicker;
        private System.Windows.Forms.TextBox Interest_textBox;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Back_btn;
    }
}