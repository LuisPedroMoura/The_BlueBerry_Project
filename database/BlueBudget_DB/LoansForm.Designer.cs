namespace BlueBudget_DB
{
    partial class LoansForm
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
            this.Loans_listBox = new System.Windows.Forms.ListBox();
            this.loans_label = new System.Windows.Forms.Label();
            this.Name_textBox = new System.Windows.Forms.TextBox();
            this.Amount_textBox = new System.Windows.Forms.TextBox();
            this.Enddate_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Interest_textBox = new System.Windows.Forms.TextBox();
            this.Save_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Back_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Loans_listBox
            // 
            this.Loans_listBox.FormattingEnabled = true;
            this.Loans_listBox.Location = new System.Drawing.Point(38, 57);
            this.Loans_listBox.Name = "Loans_listBox";
            this.Loans_listBox.Size = new System.Drawing.Size(205, 160);
            this.Loans_listBox.TabIndex = 0;
            this.Loans_listBox.SelectedIndexChanged += new System.EventHandler(this.Loans_listBox_SelectedIndexChanged);
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
            // Name_textBox
            // 
            this.Name_textBox.Location = new System.Drawing.Point(336, 57);
            this.Name_textBox.Name = "Name_textBox";
            this.Name_textBox.Size = new System.Drawing.Size(200, 20);
            this.Name_textBox.TabIndex = 2;
            this.Name_textBox.Enter += new System.EventHandler(this.Name_textBox_Enter);
            this.Name_textBox.Leave += new System.EventHandler(this.Name_textBox_Leave);
            // 
            // Amount_textBox
            // 
            this.Amount_textBox.Location = new System.Drawing.Point(336, 83);
            this.Amount_textBox.Name = "Amount_textBox";
            this.Amount_textBox.Size = new System.Drawing.Size(200, 20);
            this.Amount_textBox.TabIndex = 3;
            this.Amount_textBox.Enter += new System.EventHandler(this.Amount_textBox_Enter);
            this.Amount_textBox.Leave += new System.EventHandler(this.Amount_textBox_Leave);
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
            this.Interest_textBox.Enter += new System.EventHandler(this.Interest_textBox_Enter);
            this.Interest_textBox.Leave += new System.EventHandler(this.Interest_textBox_Leave);
            // 
            // Save_btn
            // 
            this.Save_btn.Location = new System.Drawing.Point(380, 310);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Size = new System.Drawing.Size(75, 23);
            this.Save_btn.TabIndex = 6;
            this.Save_btn.Text = "Save New";
            this.Save_btn.UseVisualStyleBackColor = true;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
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
            this.Back_btn.Location = new System.Drawing.Point(461, 310);
            this.Back_btn.Name = "Back_btn";
            this.Back_btn.Size = new System.Drawing.Size(75, 23);
            this.Back_btn.TabIndex = 8;
            this.Back_btn.Text = "Back";
            this.Back_btn.UseVisualStyleBackColor = true;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // LoansForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.Back_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Save_btn);
            this.Controls.Add(this.Interest_textBox);
            this.Controls.Add(this.Enddate_dateTimePicker);
            this.Controls.Add(this.Amount_textBox);
            this.Controls.Add(this.Name_textBox);
            this.Controls.Add(this.loans_label);
            this.Controls.Add(this.Loans_listBox);
            this.Name = "LoansForm";
            this.Text = "loans";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Loans_listBox;
        private System.Windows.Forms.Label loans_label;
        private System.Windows.Forms.TextBox Name_textBox;
        private System.Windows.Forms.TextBox Amount_textBox;
        private System.Windows.Forms.DateTimePicker Enddate_dateTimePicker;
        private System.Windows.Forms.TextBox Interest_textBox;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Back_btn;
    }
}