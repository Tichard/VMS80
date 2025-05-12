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
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            chartGroove = new System.Windows.Forms.DataVisualization.Charting.Chart();
            buttonZoomOut = new Button();
            buttonZoomIn = new Button();
            buttonRender = new Button();
            labelRender = new Label();
            buttonPrev = new Button();
            buttonNext = new Button();
            textBoxZoom = new TextBox();
            labelPlot = new Label();
            ((System.ComponentModel.ISupportInitialize)chartGroove).BeginInit();
            SuspendLayout();
            // 
            // chartGroove
            // 
            chartGroove.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            legend4.Name = "Legend";
            chartGroove.Legends.Add(legend4);
            chartGroove.Location = new Point(89, 50);
            chartGroove.Name = "chartGroove";
            chartGroove.Size = new Size(989, 450);
            chartGroove.TabIndex = 0;
            chartGroove.Text = "chart1";
            // 
            // buttonZoomOut
            // 
            buttonZoomOut.Anchor = AnchorStyles.Top;
            buttonZoomOut.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonZoomOut.Font = new Font("Lucida Sans Unicode", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            buttonZoomOut.ForeColor = SystemColors.ControlText;
            buttonZoomOut.Location = new Point(455, 12);
            buttonZoomOut.Name = "buttonZoomOut";
            buttonZoomOut.Size = new Size(72, 32);
            buttonZoomOut.TabIndex = 1;
            buttonZoomOut.Text = "x 1/2";
            buttonZoomOut.UseVisualStyleBackColor = true;
            buttonZoomOut.Click += buttonZoomOut_Click;
            // 
            // buttonZoomIn
            // 
            buttonZoomIn.Anchor = AnchorStyles.Top;
            buttonZoomIn.Font = new Font("Lucida Sans Unicode", 12F, FontStyle.Italic);
            buttonZoomIn.ForeColor = SystemColors.ControlText;
            buttonZoomIn.Location = new Point(639, 12);
            buttonZoomIn.Name = "buttonZoomIn";
            buttonZoomIn.Size = new Size(75, 32);
            buttonZoomIn.TabIndex = 2;
            buttonZoomIn.Text = "x 2";
            buttonZoomIn.UseVisualStyleBackColor = true;
            buttonZoomIn.Click += buttonZoomIn_Click;
            // 
            // buttonRender
            // 
            buttonRender.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonRender.Font = new Font("Lucida Sans Unicode", 12F, FontStyle.Italic);
            buttonRender.ForeColor = SystemColors.ControlText;
            buttonRender.Location = new Point(904, 522);
            buttonRender.Name = "buttonRender";
            buttonRender.Size = new Size(251, 68);
            buttonRender.TabIndex = 3;
            buttonRender.Text = "Render Vinyl View *";
            buttonRender.UseVisualStyleBackColor = true;
            buttonRender.Click += buttonRender_Click;
            // 
            // labelRender
            // 
            labelRender.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelRender.AutoSize = true;
            labelRender.Font = new Font("Lucida Sans Unicode", 10F, FontStyle.Italic);
            labelRender.ForeColor = SystemColors.ControlLightLight;
            labelRender.Location = new Point(942, 593);
            labelRender.Name = "labelRender";
            labelRender.Size = new Size(213, 17);
            labelRender.TabIndex = 4;
            labelRender.Text = "* Python (matplotlib) required";
            // 
            // buttonPrev
            // 
            buttonPrev.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            buttonPrev.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonPrev.ForeColor = SystemColors.ControlText;
            buttonPrev.Location = new Point(12, 50);
            buttonPrev.Name = "buttonPrev";
            buttonPrev.Size = new Size(71, 450);
            buttonPrev.TabIndex = 5;
            buttonPrev.Text = "<";
            buttonPrev.UseVisualStyleBackColor = true;
            buttonPrev.Click += buttonPrev_Click;
            // 
            // buttonNext
            // 
            buttonNext.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            buttonNext.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonNext.ForeColor = SystemColors.ControlText;
            buttonNext.Location = new Point(1084, 50);
            buttonNext.Name = "buttonNext";
            buttonNext.Size = new Size(71, 450);
            buttonNext.TabIndex = 6;
            buttonNext.Text = ">";
            buttonNext.UseVisualStyleBackColor = true;
            buttonNext.Click += buttonNext_Click;
            // 
            // textBoxZoom
            // 
            textBoxZoom.Anchor = AnchorStyles.Top;
            textBoxZoom.Font = new Font("Lucida Sans Unicode", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            textBoxZoom.Location = new Point(533, 12);
            textBoxZoom.Name = "textBoxZoom";
            textBoxZoom.ReadOnly = true;
            textBoxZoom.Size = new Size(100, 32);
            textBoxZoom.TabIndex = 7;
            textBoxZoom.TextAlign = HorizontalAlignment.Center;
            // 
            // labelPlot
            // 
            labelPlot.AutoSize = true;
            labelPlot.Font = new Font("Lucida Sans Unicode", 12F, FontStyle.Italic);
            labelPlot.Location = new Point(12, 15);
            labelPlot.Name = "labelPlot";
            labelPlot.Size = new Size(374, 20);
            labelPlot.TabIndex = 8;
            labelPlot.Text = "Groove Land Comparison (Quarter Revolution)";
            // 
            // PlotForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1167, 620);
            Controls.Add(labelPlot);
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
        private Label labelPlot;
    }
}