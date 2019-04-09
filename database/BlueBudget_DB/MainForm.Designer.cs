namespace BlueBudget_DB
{
    partial class MainForm
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
            System.Windows.Forms.Button back_office_btn;
            this.users_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            back_office_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // back_office_btn
            // 
            back_office_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            back_office_btn.Location = new System.Drawing.Point(161, 189);
            back_office_btn.Name = "back_office_btn";
            back_office_btn.Size = new System.Drawing.Size(162, 109);
            back_office_btn.TabIndex = 0;
            back_office_btn.Text = "Back Office";
            back_office_btn.UseVisualStyleBackColor = true;
            back_office_btn.Click += new System.EventHandler(this.back_office_btn_Click);
            // 
            // users_btn
            // 
            this.users_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.users_btn.Location = new System.Drawing.Point(462, 189);
            this.users_btn.Name = "users_btn";
            this.users_btn.Size = new System.Drawing.Size(162, 109);
            this.users_btn.TabIndex = 1;
            this.users_btn.Text = "Users";
            this.users_btn.UseVisualStyleBackColor = true;
            this.users_btn.Click += new System.EventHandler(this.users_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(229, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 51);
            this.label1.TabIndex = 2;
            this.label1.Text = "BLUE BUDGET";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.users_btn);
            this.Controls.Add(back_office_btn);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blue Budget";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button users_btn;
        private System.Windows.Forms.Label label1;
    }
}

