namespace BlueBudget_DB
{
    partial class StatisticsForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.BalanceChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Back_btn = new System.Windows.Forms.Button();
            this.Year_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Stats_btn = new System.Windows.Forms.Button();
            this.Year_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BalanceChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Year_numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // BalanceChart
            // 
            chartArea1.Name = "ChartArea1";
            this.BalanceChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.BalanceChart.Legends.Add(legend1);
            this.BalanceChart.Location = new System.Drawing.Point(3, 5);
            this.BalanceChart.Margin = new System.Windows.Forms.Padding(4);
            this.BalanceChart.Name = "BalanceChart";
            this.BalanceChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            this.BalanceChart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series1.Legend = "Legend1";
            series1.Name = "Income";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            series2.Legend = "Legend1";
            series2.Name = "Expenses";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Blue;
            series3.Legend = "Legend1";
            series3.Name = "Balance";
            this.BalanceChart.Series.Add(series1);
            this.BalanceChart.Series.Add(series2);
            this.BalanceChart.Series.Add(series3);
            this.BalanceChart.Size = new System.Drawing.Size(797, 391);
            this.BalanceChart.TabIndex = 0;
            this.BalanceChart.Text = "Balance Chart";
            title1.Name = "Balance Chart";
            title1.Text = "Balance Chart";
            this.BalanceChart.Titles.Add(title1);
            // 
            // Back_btn
            // 
            this.Back_btn.Location = new System.Drawing.Point(672, 409);
            this.Back_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Back_btn.Name = "Back_btn";
            this.Back_btn.Size = new System.Drawing.Size(100, 28);
            this.Back_btn.TabIndex = 1;
            this.Back_btn.Text = "Back";
            this.Back_btn.UseVisualStyleBackColor = true;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // Year_numericUpDown
            // 
            this.Year_numericUpDown.Location = new System.Drawing.Point(229, 414);
            this.Year_numericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.Year_numericUpDown.Name = "Year_numericUpDown";
            this.Year_numericUpDown.Size = new System.Drawing.Size(160, 22);
            this.Year_numericUpDown.TabIndex = 2;
            // 
            // Stats_btn
            // 
            this.Stats_btn.Location = new System.Drawing.Point(397, 410);
            this.Stats_btn.Margin = new System.Windows.Forms.Padding(4);
            this.Stats_btn.Name = "Stats_btn";
            this.Stats_btn.Size = new System.Drawing.Size(117, 28);
            this.Stats_btn.TabIndex = 3;
            this.Stats_btn.Text = "Get Statistics";
            this.Stats_btn.UseVisualStyleBackColor = true;
            this.Stats_btn.Click += new System.EventHandler(this.Stats_btn_Click);
            // 
            // Year_label
            // 
            this.Year_label.AutoSize = true;
            this.Year_label.Location = new System.Drawing.Point(185, 415);
            this.Year_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Year_label.Name = "Year_label";
            this.Year_label.Size = new System.Drawing.Size(36, 17);
            this.Year_label.TabIndex = 4;
            this.Year_label.Text = "year";
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Year_label);
            this.Controls.Add(this.Stats_btn);
            this.Controls.Add(this.Year_numericUpDown);
            this.Controls.Add(this.Back_btn);
            this.Controls.Add(this.BalanceChart);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StatisticsForm";
            this.Text = "StatisticsForm";
            this.Load += new System.EventHandler(this.StatisticsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BalanceChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Year_numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart BalanceChart;
        private System.Windows.Forms.Button Back_btn;
        private System.Windows.Forms.NumericUpDown Year_numericUpDown;
        private System.Windows.Forms.Button Stats_btn;
        private System.Windows.Forms.Label Year_label;
    }
}