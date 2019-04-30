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
            this.save_btn = new System.Windows.Forms.Button();
            this.goalstate_label = new System.Windows.Forms.Label();
            this.back_btn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.term_label = new System.Windows.Forms.Label();
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
            this.Goals_listBox.Size = new System.Drawing.Size(207, 173);
            this.Goals_listBox.TabIndex = 1;
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
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(340, 308);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(75, 23);
            this.save_btn.TabIndex = 5;
            this.save_btn.Text = "Save New";
            this.save_btn.UseVisualStyleBackColor = true;
            // 
            // goalstate_label
            // 
            this.goalstate_label.AutoSize = true;
            this.goalstate_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goalstate_label.Location = new System.Drawing.Point(368, 126);
            this.goalstate_label.Name = "goalstate_label";
            this.goalstate_label.Size = new System.Drawing.Size(45, 16);
            this.goalstate_label.TabIndex = 6;
            this.goalstate_label.Text = "label1";
            this.goalstate_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // back_btn
            // 
            this.back_btn.Location = new System.Drawing.Point(465, 308);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(75, 23);
            this.back_btn.TabIndex = 7;
            this.back_btn.Text = "Back";
            this.back_btn.UseVisualStyleBackColor = true;
            this.back_btn.Click += new System.EventHandler(this.back_btn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(292, 72);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 8;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(419, 72);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 9;
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
            // goals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.term_label);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.back_btn);
            this.Controls.Add(this.goalstate_label);
            this.Controls.Add(this.save_btn);
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
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Label goalstate_label;
        private System.Windows.Forms.Button back_btn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label term_label;
    }
}