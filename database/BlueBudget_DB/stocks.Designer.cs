namespace BlueBudget_DB
{
    partial class stocks
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
            this.Stocks_label = new System.Windows.Forms.Label();
            this.Stockmarket_listBox = new System.Windows.Forms.ListBox();
            this.Mystocks_listBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.company_textBox = new System.Windows.Forms.TextBox();
            this.bidprice_textBox = new System.Windows.Forms.TextBox();
            this.askprice_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.purchased_textBox = new System.Windows.Forms.TextBox();
            this.quantity_numericupdown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.stocktype_textBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buy_btn = new System.Windows.Forms.Button();
            this.sell_btn = new System.Windows.Forms.Button();
            this.back_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.quantity_numericupdown)).BeginInit();
            this.SuspendLayout();
            // 
            // Stocks_label
            // 
            this.Stocks_label.AutoSize = true;
            this.Stocks_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stocks_label.Location = new System.Drawing.Point(229, 9);
            this.Stocks_label.Name = "Stocks_label";
            this.Stocks_label.Size = new System.Drawing.Size(119, 29);
            this.Stocks_label.TabIndex = 0;
            this.Stocks_label.Text = "STOCKS";
            // 
            // Stockmarket_listBox
            // 
            this.Stockmarket_listBox.FormattingEnabled = true;
            this.Stockmarket_listBox.Location = new System.Drawing.Point(40, 71);
            this.Stockmarket_listBox.Name = "Stockmarket_listBox";
            this.Stockmarket_listBox.Size = new System.Drawing.Size(155, 264);
            this.Stockmarket_listBox.TabIndex = 1;
            this.Stockmarket_listBox.SelectedIndexChanged += new System.EventHandler(this.Stockmarket_listBox_SelectedIndexChanged);
            // 
            // Mystocks_listBox
            // 
            this.Mystocks_listBox.FormattingEnabled = true;
            this.Mystocks_listBox.Location = new System.Drawing.Point(201, 71);
            this.Mystocks_listBox.Name = "Mystocks_listBox";
            this.Mystocks_listBox.Size = new System.Drawing.Size(155, 264);
            this.Mystocks_listBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Stock Market";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "My Stocks";
            // 
            // company_textBox
            // 
            this.company_textBox.Location = new System.Drawing.Point(362, 71);
            this.company_textBox.Name = "company_textBox";
            this.company_textBox.ReadOnly = true;
            this.company_textBox.Size = new System.Drawing.Size(206, 20);
            this.company_textBox.TabIndex = 5;
            // 
            // bidprice_textBox
            // 
            this.bidprice_textBox.Location = new System.Drawing.Point(362, 113);
            this.bidprice_textBox.Name = "bidprice_textBox";
            this.bidprice_textBox.ReadOnly = true;
            this.bidprice_textBox.Size = new System.Drawing.Size(100, 20);
            this.bidprice_textBox.TabIndex = 6;
            // 
            // askprice_textBox
            // 
            this.askprice_textBox.Location = new System.Drawing.Point(468, 113);
            this.askprice_textBox.Name = "askprice_textBox";
            this.askprice_textBox.ReadOnly = true;
            this.askprice_textBox.Size = new System.Drawing.Size(100, 20);
            this.askprice_textBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Company";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(359, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Bid Price";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(465, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Ask Price";
            // 
            // purchased_textBox
            // 
            this.purchased_textBox.Location = new System.Drawing.Point(362, 193);
            this.purchased_textBox.Name = "purchased_textBox";
            this.purchased_textBox.ReadOnly = true;
            this.purchased_textBox.Size = new System.Drawing.Size(100, 20);
            this.purchased_textBox.TabIndex = 12;
            // 
            // quantity_numericupdown
            // 
            this.quantity_numericupdown.Location = new System.Drawing.Point(468, 193);
            this.quantity_numericupdown.Name = "quantity_numericupdown";
            this.quantity_numericupdown.Size = new System.Drawing.Size(100, 20);
            this.quantity_numericupdown.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(359, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Purchased for";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(468, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Quantity";
            // 
            // stocktype_textBox
            // 
            this.stocktype_textBox.Location = new System.Drawing.Point(362, 152);
            this.stocktype_textBox.Name = "stocktype_textBox";
            this.stocktype_textBox.ReadOnly = true;
            this.stocktype_textBox.Size = new System.Drawing.Size(206, 20);
            this.stocktype_textBox.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(359, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Stock Type";
            // 
            // buy_btn
            // 
            this.buy_btn.Location = new System.Drawing.Point(362, 233);
            this.buy_btn.Name = "buy_btn";
            this.buy_btn.Size = new System.Drawing.Size(75, 23);
            this.buy_btn.TabIndex = 19;
            this.buy_btn.Text = "Buy";
            this.buy_btn.UseVisualStyleBackColor = true;
            // 
            // sell_btn
            // 
            this.sell_btn.Location = new System.Drawing.Point(442, 233);
            this.sell_btn.Name = "sell_btn";
            this.sell_btn.Size = new System.Drawing.Size(75, 23);
            this.sell_btn.TabIndex = 20;
            this.sell_btn.Text = "Sell";
            this.sell_btn.UseVisualStyleBackColor = true;
            // 
            // back_btn
            // 
            this.back_btn.Location = new System.Drawing.Point(493, 312);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(75, 23);
            this.back_btn.TabIndex = 21;
            this.back_btn.Text = "Back";
            this.back_btn.UseVisualStyleBackColor = true;
            // 
            // stocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.back_btn);
            this.Controls.Add(this.sell_btn);
            this.Controls.Add(this.buy_btn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.stocktype_textBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.quantity_numericupdown);
            this.Controls.Add(this.purchased_textBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.askprice_textBox);
            this.Controls.Add(this.bidprice_textBox);
            this.Controls.Add(this.company_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Mystocks_listBox);
            this.Controls.Add(this.Stockmarket_listBox);
            this.Controls.Add(this.Stocks_label);
            this.Name = "stocks";
            this.Text = "Stocks";
            ((System.ComponentModel.ISupportInitialize)(this.quantity_numericupdown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Stocks_label;
        private System.Windows.Forms.ListBox Stockmarket_listBox;
        private System.Windows.Forms.ListBox Mystocks_listBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox company_textBox;
        private System.Windows.Forms.TextBox bidprice_textBox;
        private System.Windows.Forms.TextBox askprice_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox purchased_textBox;
        private System.Windows.Forms.NumericUpDown quantity_numericupdown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox stocktype_textBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buy_btn;
        private System.Windows.Forms.Button sell_btn;
        private System.Windows.Forms.Button back_btn;
    }
}