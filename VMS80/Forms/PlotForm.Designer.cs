namespace VMS80
{
    partial class PlotForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chartGroove = new System.Windows.Forms.DataVisualization.Charting.Chart();
            buttonZoomOut = new Button();
            buttonZoomIn = new Button();
            buttonRender = new Button();
            labelRender = new Label();
            buttonPrev = new Button();
            buttonNext = new Button();
            textBoxZoom = new TextBox();
            ((System.ComponentModel.ISupportInitialize)chartGroove).BeginInit();
            SuspendLayout();
            // 
            // chartGroove
            // 
            chartArea3.Name = "ChartArea1";
            chartGroove.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            chartGroove.Legends.Add(legend3);
            chartGroove.Location = new Point(89, 107);
            chartGroove.Name = "chartGroove";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            chartGroove.Series.Add(series3);
            chartGroove.Size = new Size(989, 450);
            chartGroove.TabIndex = 0;
            chartGroove.Text = "chart1";
            // 
            // buttonZoomOut
            // 
            buttonZoomOut.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonZoomOut.Font = new Font("Lucida Sans Unicode", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            buttonZoomOut.ForeColor = SystemColors.ControlText;
            buttonZoomOut.Location = new Point(459, 69);
            buttonZoomOut.Name = "buttonZoomOut";
            buttonZoomOut.Size = new Size(72, 32);
            buttonZoomOut.TabIndex = 1;
            buttonZoomOut.Text = "x 1/2";
            buttonZoomOut.UseVisualStyleBackColor = true;
            buttonZoomOut.Click += buttonZoomOut_Click;
            // 
            // buttonZoomIn
            // 
            buttonZoomIn.Font = new Font("Lucida Sans Unicode", 12F, FontStyle.Italic);
            buttonZoomIn.ForeColor = SystemColors.ControlText;
            buttonZoomIn.Location = new Point(643, 69);
            buttonZoomIn.Name = "buttonZoomIn";
            buttonZoomIn.Size = new Size(75, 32);
            buttonZoomIn.TabIndex = 2;
            buttonZoomIn.Text = "x 2";
            buttonZoomIn.UseVisualStyleBackColor = true;
            buttonZoomIn.Click += buttonZoomIn_Click;
            // 
            // buttonRender
            // 
            buttonRender.Font = new Font("Lucida Sans Unicode", 12F, FontStyle.Italic);
            buttonRender.ForeColor = SystemColors.ControlText;
            buttonRender.Location = new Point(904, 613);
            buttonRender.Name = "buttonRender";
            buttonRender.Size = new Size(251, 68);
            buttonRender.TabIndex = 3;
            buttonRender.Text = "Render Vinyl View *";
            buttonRender.UseVisualStyleBackColor = true;
            buttonRender.Click += buttonRender_Click;
            // 
            // labelRender
            // 
            labelRender.AutoSize = true;
            labelRender.Font = new Font("Lucida Sans Unicode", 10F, FontStyle.Italic);
            labelRender.ForeColor = SystemColors.ControlLightLight;
            labelRender.Location = new Point(948, 684);
            labelRender.Name = "labelRender";
            labelRender.Size = new Size(207, 17);
            labelRender.TabIndex = 4;
            labelRender.Text = "* Python / MathPlot Required";
            // 
            // buttonPrev
            // 
            buttonPrev.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonPrev.ForeColor = SystemColors.ControlText;
            buttonPrev.Location = new Point(12, 107);
            buttonPrev.Name = "buttonPrev";
            buttonPrev.Size = new Size(71, 450);
            buttonPrev.TabIndex = 5;
            buttonPrev.Text = "<";
            buttonPrev.UseVisualStyleBackColor = true;
            buttonPrev.Click += buttonPrev_Click;
            // 
            // buttonNext
            // 
            buttonNext.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonNext.ForeColor = SystemColors.ControlText;
            buttonNext.Location = new Point(1084, 107);
            buttonNext.Name = "buttonNext";
            buttonNext.Size = new Size(71, 450);
            buttonNext.TabIndex = 6;
            buttonNext.Text = ">";
            buttonNext.UseVisualStyleBackColor = true;
            buttonNext.Click += buttonNext_Click;
            // 
            // textBoxZoom
            // 
            textBoxZoom.Font = new Font("Lucida Sans Unicode", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            textBoxZoom.Location = new Point(537, 69);
            textBoxZoom.Name = "textBoxZoom";
            textBoxZoom.ReadOnly = true;
            textBoxZoom.Size = new Size(100, 32);
            textBoxZoom.TabIndex = 7;
            textBoxZoom.TextAlign = HorizontalAlignment.Center;
            // 
            // PlotForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1167, 710);
            Controls.Add(textBoxZoom);
            Controls.Add(buttonNext);
            Controls.Add(buttonPrev);
            Controls.Add(labelRender);
            Controls.Add(buttonRender);
            Controls.Add(buttonZoomIn);
            Controls.Add(buttonZoomOut);
            Controls.Add(chartGroove);
            ForeColor = Color.White;
            Name = "PlotForm";
            Text = "Plot";
            ((System.ComponentModel.ISupportInitialize)chartGroove).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartGroove;
        private Button buttonZoomOut;
        private Button buttonZoomIn;
        private Button buttonRender;
        private Label labelRender;
        private Button buttonPrev;
        private Button buttonNext;
        private TextBox textBoxZoom;
    }
}