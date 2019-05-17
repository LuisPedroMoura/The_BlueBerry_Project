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
            this.quantity_numericupdown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.buy_btn = new System.Windows.Forms.Button();
            this.sell_btn = new System.Windows.Forms.Button();
            this.back_btn = new System.Windows.Forms.Button();
            this.StockMarket_listView = new System.Windows.Forms.ListView();
            this.SMCompany_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AskPrice_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StockType_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MyStocks_listView = new System.Windows.Forms.ListView();
            this.Ticker_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Company_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PurchasePrice_col = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SellAll_btn = new System.Windows.Forms.Button();
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
            this.label1.Location = new System.Drawing.Point(21, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Stock Market";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(324, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "My Stocks";
            // 
            // quantity_numericupdown
            // 
            this.quantity_numericupdown.Location = new System.Drawing.Point(642, 65);
            this.quantity_numericupdown.Name = "quantity_numericupdown";
            this.quantity_numericupdown.Size = new System.Drawing.Size(100, 20);
            this.quantity_numericupdown.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(639, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Quantity";
            // 
            // buy_btn
            // 
            this.buy_btn.Location = new System.Drawing.Point(642, 91);
            this.buy_btn.Name = "buy_btn";
            this.buy_btn.Size = new System.Drawing.Size(100, 23);
            this.buy_btn.TabIndex = 19;
            this.buy_btn.Text = "Buy";
            this.buy_btn.UseVisualStyleBackColor = true;
            this.buy_btn.Click += new System.EventHandler(this.Buy_btn_Click);
            // 
            // sell_btn
            // 
            this.sell_btn.Location = new System.Drawing.Point(642, 152);
            this.sell_btn.Name = "sell_btn";
            this.sell_btn.Size = new System.Drawing.Size(100, 23);
            this.sell_btn.TabIndex = 20;
            this.sell_btn.Text = "Sell";
            this.sell_btn.UseVisualStyleBackColor = true;
            this.sell_btn.Click += new System.EventHandler(this.Sell_btn_Click);
            // 
            // back_btn
            // 
            this.back_btn.Location = new System.Drawing.Point(667, 305);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(75, 23);
            this.back_btn.TabIndex = 21;
            this.back_btn.Text = "Back";
            this.back_btn.UseVisualStyleBackColor = true;
            this.back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // StockMarket_listView
            // 
            this.StockMarket_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SMCompany_col,
            this.AskPrice_col,
            this.StockType_col});
            this.StockMarket_listView.FullRowSelect = true;
            this.StockMarket_listView.Location = new System.Drawing.Point(24, 65);
            this.StockMarket_listView.Name = "StockMarket_listView";
            this.StockMarket_listView.Size = new System.Drawing.Size(297, 264);
            this.StockMarket_listView.TabIndex = 22;
            this.StockMarket_listView.UseCompatibleStateImageBehavior = false;
            this.StockMarket_listView.View = System.Windows.Forms.View.Details;
            // 
            // SMCompany_col
            // 
            this.SMCompany_col.Text = "Company";
            this.SMCompany_col.Width = 150;
            // 
            // AskPrice_col
            // 
            this.AskPrice_col.Text = "Ask Price";
            this.AskPrice_col.Width = 70;
            // 
            // StockType_col
            // 
            this.StockType_col.Text = "Stock Type";
            this.StockType_col.Width = 80;
            // 
            // MyStocks_listView
            // 
            this.MyStocks_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Ticker_col,
            this.Company_col,
            this.PurchasePrice_col});
            this.MyStocks_listView.FullRowSelect = true;
            this.MyStocks_listView.Location = new System.Drawing.Point(327, 65);
            this.MyStocks_listView.Name = "MyStocks_listView";
            this.MyStocks_listView.Size = new System.Drawing.Size(293, 263);
            this.MyStocks_listView.TabIndex = 23;
            this.MyStocks_listView.UseCompatibleStateImageBehavior = false;
            this.MyStocks_listView.View = System.Windows.Forms.View.Details;
            // 
            // Ticker_col
            // 
            this.Ticker_col.Text = "ticker";
            this.Ticker_col.Width = 50;
            // 
            // Company_col
            // 
            this.Company_col.Text = "Company";
            this.Company_col.Width = 150;
            // 
            // PurchasePrice_col
            // 
            this.PurchasePrice_col.Text = "Purchase Price";
            this.PurchasePrice_col.Width = 90;
            // 
            // SellAll_btn
            // 
            this.SellAll_btn.Location = new System.Drawing.Point(642, 181);
            this.SellAll_btn.Name = "SellAll_btn";
            this.SellAll_btn.Size = new System.Drawing.Size(100, 23);
            this.SellAll_btn.TabIndex = 24;
            this.SellAll_btn.Text = "Sell All";
            this.SellAll_btn.UseVisualStyleBackColor = true;
            this.SellAll_btn.Click += new System.EventHandler(this.SellAll_btn_Click);
            // 
            // StocksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 366);
            this.Controls.Add(this.SellAll_btn);
            this.Controls.Add(this.MyStocks_listView);
            this.Controls.Add(this.StockMarket_listView);
            this.Controls.Add(this.back_btn);
            this.Controls.Add(this.sell_btn);
            this.Controls.Add(this.buy_btn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.quantity_numericupdown);
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
        private System.Windows.Forms.NumericUpDown quantity_numericupdown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buy_btn;
        private System.Windows.Forms.Button sell_btn;
        private System.Windows.Forms.Button back_btn;
        private System.Windows.Forms.ListView StockMarket_listView;
        private System.Windows.Forms.ColumnHeader SMCompany_col;
        private System.Windows.Forms.ColumnHeader AskPrice_col;
        private System.Windows.Forms.ListView MyStocks_listView;
        private System.Windows.Forms.ColumnHeader Ticker_col;
        private System.Windows.Forms.ColumnHeader Company_col;
        private System.Windows.Forms.ColumnHeader PurchasePrice_col;
        private System.Windows.Forms.ColumnHeader StockType_col;
        private System.Windows.Forms.Button SellAll_btn;
    }
}