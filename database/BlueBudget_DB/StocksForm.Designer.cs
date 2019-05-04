namespace BlueBudget_DB
{
    partial class StocksForm
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
            this.StockMarket_listView = new System.Windows.Forms.ListView();
            this.Stock_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AskPrice_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView1 = new System.Windows.Forms.ListView();
            this.Ticker_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Company_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BidPrice_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.quantity_numericupdown)).BeginInit();
            this.SuspendLayout();
            // 
            // Stocks_label
            // 
            this.Stocks_label.AutoSize = true;
            this.Stocks_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stocks_label.Location = new System.Drawing.Point(322, 9);
            this.Stocks_label.Name = "Stocks_label";
            this.Stocks_label.Size = new System.Drawing.Size(119, 29);
            this.Stocks_label.TabIndex = 0;
            this.Stocks_label.Text = "STOCKS";
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
            this.label2.Location = new System.Drawing.Point(231, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "My Stocks";
            // 
            // company_textBox
            // 
            this.company_textBox.Location = new System.Drawing.Point(508, 89);
            this.company_textBox.Name = "company_textBox";
            this.company_textBox.ReadOnly = true;
            this.company_textBox.Size = new System.Drawing.Size(206, 20);
            this.company_textBox.TabIndex = 5;
            // 
            // bidprice_textBox
            // 
            this.bidprice_textBox.Location = new System.Drawing.Point(508, 131);
            this.bidprice_textBox.Name = "bidprice_textBox";
            this.bidprice_textBox.ReadOnly = true;
            this.bidprice_textBox.Size = new System.Drawing.Size(100, 20);
            this.bidprice_textBox.TabIndex = 6;
            // 
            // askprice_textBox
            // 
            this.askprice_textBox.Location = new System.Drawing.Point(614, 131);
            this.askprice_textBox.Name = "askprice_textBox";
            this.askprice_textBox.ReadOnly = true;
            this.askprice_textBox.Size = new System.Drawing.Size(100, 20);
            this.askprice_textBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(508, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Company";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(505, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Bid Price";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(611, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Ask Price";
            // 
            // purchased_textBox
            // 
            this.purchased_textBox.Location = new System.Drawing.Point(508, 211);
            this.purchased_textBox.Name = "purchased_textBox";
            this.purchased_textBox.ReadOnly = true;
            this.purchased_textBox.Size = new System.Drawing.Size(100, 20);
            this.purchased_textBox.TabIndex = 12;
            // 
            // quantity_numericupdown
            // 
            this.quantity_numericupdown.Location = new System.Drawing.Point(614, 211);
            this.quantity_numericupdown.Name = "quantity_numericupdown";
            this.quantity_numericupdown.Size = new System.Drawing.Size(100, 20);
            this.quantity_numericupdown.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(505, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Purchased for";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(614, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Quantity";
            // 
            // stocktype_textBox
            // 
            this.stocktype_textBox.Location = new System.Drawing.Point(508, 170);
            this.stocktype_textBox.Name = "stocktype_textBox";
            this.stocktype_textBox.ReadOnly = true;
            this.stocktype_textBox.Size = new System.Drawing.Size(206, 20);
            this.stocktype_textBox.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(505, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Stock Type";
            // 
            // buy_btn
            // 
            this.buy_btn.Location = new System.Drawing.Point(508, 251);
            this.buy_btn.Name = "buy_btn";
            this.buy_btn.Size = new System.Drawing.Size(75, 23);
            this.buy_btn.TabIndex = 19;
            this.buy_btn.Text = "Buy";
            this.buy_btn.UseVisualStyleBackColor = true;
            // 
            // sell_btn
            // 
            this.sell_btn.Location = new System.Drawing.Point(588, 251);
            this.sell_btn.Name = "sell_btn";
            this.sell_btn.Size = new System.Drawing.Size(75, 23);
            this.sell_btn.TabIndex = 20;
            this.sell_btn.Text = "Sell";
            this.sell_btn.UseVisualStyleBackColor = true;
            // 
            // back_btn
            // 
            this.back_btn.Location = new System.Drawing.Point(639, 313);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(75, 23);
            this.back_btn.TabIndex = 21;
            this.back_btn.Text = "Back";
            this.back_btn.UseVisualStyleBackColor = true;
            // 
            // StockMarket_listView
            // 
            this.StockMarket_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Stock_col,
            this.AskPrice_col});
            this.StockMarket_listView.Location = new System.Drawing.Point(40, 72);
            this.StockMarket_listView.Name = "StockMarket_listView";
            this.StockMarket_listView.Size = new System.Drawing.Size(187, 264);
            this.StockMarket_listView.TabIndex = 22;
            this.StockMarket_listView.UseCompatibleStateImageBehavior = false;
            this.StockMarket_listView.View = System.Windows.Forms.View.Details;
            // 
            // Stock_col
            // 
            this.Stock_col.Text = "Stocks";
            this.Stock_col.Width = 120;
            // 
            // AskPrice_col
            // 
            this.AskPrice_col.Text = "Ask Price";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Ticker_col,
            this.Company_col,
            this.BidPrice_col});
            this.listView1.Location = new System.Drawing.Point(234, 73);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(244, 263);
            this.listView1.TabIndex = 23;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Ticker_col
            // 
            this.Ticker_col.Text = "ticker";
            // 
            // Company_col
            // 
            this.Company_col.Text = "Company";
            this.Company_col.Width = 120;
            // 
            // BidPrice_col
            // 
            this.BidPrice_col.Text = "Bid Price";
            // 
            // StocksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 366);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.StockMarket_listView);
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
            this.Controls.Add(this.Stocks_label);
            this.Name = "StocksForm";
            this.Text = "Stocks";
            this.Load += new System.EventHandler(this.Stocks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.quantity_numericupdown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Stocks_label;
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
        private System.Windows.Forms.ListView StockMarket_listView;
        private System.Windows.Forms.ColumnHeader Stock_col;
        private System.Windows.Forms.ColumnHeader AskPrice_col;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Ticker_col;
        private System.Windows.Forms.ColumnHeader Company_col;
        private System.Windows.Forms.ColumnHeader BidPrice_col;
    }
}