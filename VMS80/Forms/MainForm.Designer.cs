namespace VMS80
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textImportFilePath = new TextBox();
            btnImportFile = new Button();
            inputSineFreq = new TextBox();
            radioGenerateFreq = new RadioButton();
            buttonSimulate = new Button();
            radioImportAudio = new RadioButton();
            labelTitle = new Label();
            labelSineFreq = new Label();
            labelMinLand = new Label();
            labelSurfaceFilling = new Label();
            textBoxMinLand = new TextBox();
            textBoxSurfaceFilling = new TextBox();
            panelLowFreqMixer = new Panel();
            textBoxLowFreqMixerGain = new TextBox();
            textBoxLowFreqMixerCutOffFrequency = new TextBox();
            labelLowFreqMixerGain = new Label();
            labelLowFreqMixerCutOffFrequency = new Label();
            checkBoxLowFreqMixer = new CheckBox();
            checkBoxHiFreqLim = new CheckBox();
            panelHiFreqLim = new Panel();
            textBoxHiFreqLimiterThreshold = new TextBox();
            textBoxHiFreqLimiterGain = new TextBox();
            textBoxHiFreqLimiterCutOffFrequency = new TextBox();
            labelHiFreqLimThreshold = new Label();
            labelHiFreqLimGain = new Label();
            labelHiFreqLimCutOffFrequency = new Label();
            checkBoxCompressor = new CheckBox();
            panelCompressor = new Panel();
            textBoxCompressorGain = new TextBox();
            textBoxCompressorThreshold = new TextBox();
            labelCompressorGain = new Label();
            labelCompressorThreshold = new Label();
            textBoxMaxDepth = new TextBox();
            textBoxMinDepth = new TextBox();
            labelMaxDepth = new Label();
            labelMinDepth = new Label();
            buttonPlot = new Button();
            checkBoxRIAA = new CheckBox();
            panelLowFreqMixer.SuspendLayout();
            panelHiFreqLim.SuspendLayout();
            panelCompressor.SuspendLayout();
            SuspendLayout();
            // 
            // textImportFilePath
            // 
            textImportFilePath.Font = new Font("Lucida Sans Unicode", 9F, FontStyle.Italic);
            textImportFilePath.Location = new Point(56, 113);
            textImportFilePath.Name = "textImportFilePath";
            textImportFilePath.ReadOnly = true;
            textImportFilePath.Size = new Size(310, 26);
            textImportFilePath.TabIndex = 3;
            textImportFilePath.Text = "no file selected ...";
            // 
            // btnImportFile
            // 
            btnImportFile.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnImportFile.Location = new Point(372, 112);
            btnImportFile.Name = "btnImportFile";
            btnImportFile.Size = new Size(138, 25);
            btnImportFile.TabIndex = 2;
            btnImportFile.Text = "Import Wave File";
            btnImportFile.UseVisualStyleBackColor = true;
            btnImportFile.Click += btnImportFile_Click;
            // 
            // inputSineFreq
            // 
            inputSineFreq.Font = new Font("Lucida Sans Unicode", 9F, FontStyle.Italic);
            inputSineFreq.Location = new Point(757, 111);
            inputSineFreq.Name = "inputSineFreq";
            inputSineFreq.Size = new Size(61, 26);
            inputSineFreq.TabIndex = 4;
            inputSineFreq.TextChanged += inputSineFreq_TextChanged;
            // 
            // radioGenerateFreq
            // 
            radioGenerateFreq.AutoSize = true;
            radioGenerateFreq.Checked = true;
            radioGenerateFreq.Font = new Font("Lucida Sans Unicode", 14.25F, FontStyle.Italic);
            radioGenerateFreq.ForeColor = SystemColors.ControlLightLight;
            radioGenerateFreq.Location = new Point(576, 68);
            radioGenerateFreq.Name = "radioGenerateFreq";
            radioGenerateFreq.Size = new Size(257, 27);
            radioGenerateFreq.TabIndex = 5;
            radioGenerateFreq.TabStop = true;
            radioGenerateFreq.Text = "Generate Full-Scale Sine";
            radioGenerateFreq.UseVisualStyleBackColor = true;
            // 
            // buttonSimulate
            // 
            buttonSimulate.Font = new Font("Lucida Sans Unicode", 14.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            buttonSimulate.Location = new Point(286, 574);
            buttonSimulate.Name = "buttonSimulate";
            buttonSimulate.Size = new Size(509, 58);
            buttonSimulate.TabIndex = 3;
            buttonSimulate.Text = "Simulate";
            buttonSimulate.UseVisualStyleBackColor = true;
            buttonSimulate.Click += buttonSimulate_Click;
            // 
            // radioImportAudio
            // 
            radioImportAudio.AutoSize = true;
            radioImportAudio.Font = new Font("Lucida Sans Unicode", 14.25F, FontStyle.Italic);
            radioImportAudio.ForeColor = SystemColors.ControlLightLight;
            radioImportAudio.Location = new Point(30, 68);
            radioImportAudio.Name = "radioImportAudio";
            radioImportAudio.Size = new Size(237, 27);
            radioImportAudio.TabIndex = 4;
            radioImportAudio.Text = "Import Audio WAV File";
            radioImportAudio.UseVisualStyleBackColor = true;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Lucida Sans Unicode", 20F, FontStyle.Italic);
            labelTitle.ForeColor = SystemColors.ControlLightLight;
            labelTitle.Location = new Point(56, 9);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(105, 34);
            labelTitle.TabIndex = 7;
            labelTitle.Text = "VMS80";
            // 
            // labelSineFreq
            // 
            labelSineFreq.AutoSize = true;
            labelSineFreq.Font = new Font("Lucida Sans Unicode", 10F, FontStyle.Italic);
            labelSineFreq.ForeColor = SystemColors.ControlLightLight;
            labelSineFreq.Location = new Point(603, 116);
            labelSineFreq.Name = "labelSineFreq";
            labelSineFreq.Size = new Size(144, 17);
            labelSineFreq.TabIndex = 8;
            labelSineFreq.Text = "Sine Frequency (Hz)";
            // 
            // labelMinLand
            // 
            labelMinLand.AutoSize = true;
            labelMinLand.ForeColor = SystemColors.ControlLightLight;
            labelMinLand.Location = new Point(31, 680);
            labelMinLand.Name = "labelMinLand";
            labelMinLand.Size = new Size(164, 17);
            labelMinLand.TabIndex = 9;
            labelMinLand.Text = "Minimal land achieved:";
            // 
            // labelSurfaceFilling
            // 
            labelSurfaceFilling.AutoSize = true;
            labelSurfaceFilling.ForeColor = SystemColors.ControlLightLight;
            labelSurfaceFilling.Location = new Point(89, 721);
            labelSurfaceFilling.Name = "labelSurfaceFilling";
            labelSurfaceFilling.Size = new Size(106, 17);
            labelSurfaceFilling.TabIndex = 10;
            labelSurfaceFilling.Text = "Surface filling:";
            // 
            // textBoxMinLand
            // 
            textBoxMinLand.Location = new Point(201, 677);
            textBoxMinLand.Name = "textBoxMinLand";
            textBoxMinLand.ReadOnly = true;
            textBoxMinLand.Size = new Size(70, 28);
            textBoxMinLand.TabIndex = 11;
            // 
            // textBoxSurfaceFilling
            // 
            textBoxSurfaceFilling.Location = new Point(201, 715);
            textBoxSurfaceFilling.Name = "textBoxSurfaceFilling";
            textBoxSurfaceFilling.ReadOnly = true;
            textBoxSurfaceFilling.Size = new Size(70, 28);
            textBoxSurfaceFilling.TabIndex = 12;
            // 
            // panelLowFreqMixer
            // 
            panelLowFreqMixer.Controls.Add(textBoxLowFreqMixerGain);
            panelLowFreqMixer.Controls.Add(textBoxLowFreqMixerCutOffFrequency);
            panelLowFreqMixer.Controls.Add(labelLowFreqMixerGain);
            panelLowFreqMixer.Controls.Add(labelLowFreqMixerCutOffFrequency);
            panelLowFreqMixer.Location = new Point(12, 247);
            panelLowFreqMixer.Name = "panelLowFreqMixer";
            panelLowFreqMixer.Size = new Size(1061, 76);
            panelLowFreqMixer.TabIndex = 13;
            // 
            // textBoxLowFreqMixerGain
            // 
            textBoxLowFreqMixerGain.Location = new Point(905, 27);
            textBoxLowFreqMixerGain.Name = "textBoxLowFreqMixerGain";
            textBoxLowFreqMixerGain.Size = new Size(100, 28);
            textBoxLowFreqMixerGain.TabIndex = 3;
            textBoxLowFreqMixerGain.TextChanged += textBoxLowFreqMixerCutOffGain_TextChanged;
            // 
            // textBoxLowFreqMixerCutOffFrequency
            // 
            textBoxLowFreqMixerCutOffFrequency.Location = new Point(225, 27);
            textBoxLowFreqMixerCutOffFrequency.Name = "textBoxLowFreqMixerCutOffFrequency";
            textBoxLowFreqMixerCutOffFrequency.Size = new Size(100, 28);
            textBoxLowFreqMixerCutOffFrequency.TabIndex = 2;
            textBoxLowFreqMixerCutOffFrequency.TextChanged += textBoxLowFreqMixerCutOffFrequency_TextChanged;
            // 
            // labelLowFreqMixerGain
            // 
            labelLowFreqMixerGain.AutoSize = true;
            labelLowFreqMixerGain.ForeColor = SystemColors.ControlLightLight;
            labelLowFreqMixerGain.Location = new Point(816, 30);
            labelLowFreqMixerGain.Name = "labelLowFreqMixerGain";
            labelLowFreqMixerGain.Size = new Size(70, 17);
            labelLowFreqMixerGain.TabIndex = 1;
            labelLowFreqMixerGain.Text = "Gain (dB)";
            // 
            // labelLowFreqMixerCutOffFrequency
            // 
            labelLowFreqMixerCutOffFrequency.AutoSize = true;
            labelLowFreqMixerCutOffFrequency.ForeColor = SystemColors.ControlLightLight;
            labelLowFreqMixerCutOffFrequency.Location = new Point(44, 30);
            labelLowFreqMixerCutOffFrequency.Name = "labelLowFreqMixerCutOffFrequency";
            labelLowFreqMixerCutOffFrequency.Size = new Size(168, 17);
            labelLowFreqMixerCutOffFrequency.TabIndex = 0;
            labelLowFreqMixerCutOffFrequency.Text = "Cut-Off Frequency (Hz)";
            // 
            // checkBoxLowFreqMixer
            // 
            checkBoxLowFreqMixer.AutoSize = true;
            checkBoxLowFreqMixer.ForeColor = SystemColors.ControlLightLight;
            checkBoxLowFreqMixer.Location = new Point(12, 220);
            checkBoxLowFreqMixer.Name = "checkBoxLowFreqMixer";
            checkBoxLowFreqMixer.Size = new Size(176, 21);
            checkBoxLowFreqMixer.TabIndex = 14;
            checkBoxLowFreqMixer.Text = "Low-Frequency Mixer";
            checkBoxLowFreqMixer.UseVisualStyleBackColor = true;
            checkBoxLowFreqMixer.CheckedChanged += checkBoxLowFreqMixer_CheckedChanged;
            // 
            // checkBoxHiFreqLim
            // 
            checkBoxHiFreqLim.AutoSize = true;
            checkBoxHiFreqLim.ForeColor = SystemColors.ControlLightLight;
            checkBoxHiFreqLim.Location = new Point(12, 329);
            checkBoxHiFreqLim.Name = "checkBoxHiFreqLim";
            checkBoxHiFreqLim.Size = new Size(189, 21);
            checkBoxHiFreqLim.TabIndex = 16;
            checkBoxHiFreqLim.Text = "High-Frequency Limiter";
            checkBoxHiFreqLim.UseVisualStyleBackColor = true;
            checkBoxHiFreqLim.CheckedChanged += checkBoxHiFreqLim_CheckedChanged;
            // 
            // panelHiFreqLim
            // 
            panelHiFreqLim.Controls.Add(textBoxHiFreqLimiterThreshold);
            panelHiFreqLim.Controls.Add(textBoxHiFreqLimiterGain);
            panelHiFreqLim.Controls.Add(textBoxHiFreqLimiterCutOffFrequency);
            panelHiFreqLim.Controls.Add(labelHiFreqLimThreshold);
            panelHiFreqLim.Controls.Add(labelHiFreqLimGain);
            panelHiFreqLim.Controls.Add(labelHiFreqLimCutOffFrequency);
            panelHiFreqLim.Location = new Point(12, 356);
            panelHiFreqLim.Name = "panelHiFreqLim";
            panelHiFreqLim.Size = new Size(1061, 76);
            panelHiFreqLim.TabIndex = 15;
            // 
            // textBoxHiFreqLimiterThreshold
            // 
            textBoxHiFreqLimiterThreshold.Location = new Point(564, 32);
            textBoxHiFreqLimiterThreshold.Name = "textBoxHiFreqLimiterThreshold";
            textBoxHiFreqLimiterThreshold.Size = new Size(100, 28);
            textBoxHiFreqLimiterThreshold.TabIndex = 19;
            textBoxHiFreqLimiterThreshold.TextChanged += textBoxHiFreqLimiterThreshold_TextChanged;
            // 
            // textBoxHiFreqLimiterGain
            // 
            textBoxHiFreqLimiterGain.Location = new Point(905, 32);
            textBoxHiFreqLimiterGain.Name = "textBoxHiFreqLimiterGain";
            textBoxHiFreqLimiterGain.Size = new Size(100, 28);
            textBoxHiFreqLimiterGain.TabIndex = 4;
            textBoxHiFreqLimiterGain.TextChanged += textBoxHiFreqLimiterGain_TextChanged;
            // 
            // textBoxHiFreqLimiterCutOffFrequency
            // 
            textBoxHiFreqLimiterCutOffFrequency.Location = new Point(225, 32);
            textBoxHiFreqLimiterCutOffFrequency.Name = "textBoxHiFreqLimiterCutOffFrequency";
            textBoxHiFreqLimiterCutOffFrequency.Size = new Size(100, 28);
            textBoxHiFreqLimiterCutOffFrequency.TabIndex = 19;
            textBoxHiFreqLimiterCutOffFrequency.TextChanged += textBoxHiFreqLimiterCutOffFrequency_TextChanged;
            // 
            // labelHiFreqLimThreshold
            // 
            labelHiFreqLimThreshold.AutoSize = true;
            labelHiFreqLimThreshold.ForeColor = SystemColors.ControlLightLight;
            labelHiFreqLimThreshold.Location = new Point(432, 35);
            labelHiFreqLimThreshold.Name = "labelHiFreqLimThreshold";
            labelHiFreqLimThreshold.Size = new Size(109, 17);
            labelHiFreqLimThreshold.TabIndex = 4;
            labelHiFreqLimThreshold.Text = "Threshold (dB)";
            // 
            // labelHiFreqLimGain
            // 
            labelHiFreqLimGain.AutoSize = true;
            labelHiFreqLimGain.ForeColor = SystemColors.ControlLightLight;
            labelHiFreqLimGain.Location = new Point(816, 35);
            labelHiFreqLimGain.Name = "labelHiFreqLimGain";
            labelHiFreqLimGain.Size = new Size(70, 17);
            labelHiFreqLimGain.TabIndex = 3;
            labelHiFreqLimGain.Text = "Gain (dB)";
            // 
            // labelHiFreqLimCutOffFrequency
            // 
            labelHiFreqLimCutOffFrequency.AutoSize = true;
            labelHiFreqLimCutOffFrequency.ForeColor = SystemColors.ControlLightLight;
            labelHiFreqLimCutOffFrequency.Location = new Point(44, 35);
            labelHiFreqLimCutOffFrequency.Name = "labelHiFreqLimCutOffFrequency";
            labelHiFreqLimCutOffFrequency.Size = new Size(168, 17);
            labelHiFreqLimCutOffFrequency.TabIndex = 2;
            labelHiFreqLimCutOffFrequency.Text = "Cut-Off Frequency (Hz)";
            // 
            // checkBoxCompressor
            // 
            checkBoxCompressor.AutoSize = true;
            checkBoxCompressor.ForeColor = SystemColors.ControlLightLight;
            checkBoxCompressor.Location = new Point(12, 438);
            checkBoxCompressor.Name = "checkBoxCompressor";
            checkBoxCompressor.Size = new Size(111, 21);
            checkBoxCompressor.TabIndex = 18;
            checkBoxCompressor.Text = "Compressor";
            checkBoxCompressor.UseVisualStyleBackColor = true;
            checkBoxCompressor.CheckedChanged += checkBoxCompressor_CheckedChanged;
            // 
            // panelCompressor
            // 
            panelCompressor.Controls.Add(textBoxCompressorGain);
            panelCompressor.Controls.Add(textBoxCompressorThreshold);
            panelCompressor.Controls.Add(labelCompressorGain);
            panelCompressor.Controls.Add(labelCompressorThreshold);
            panelCompressor.Location = new Point(12, 465);
            panelCompressor.Name = "panelCompressor";
            panelCompressor.Size = new Size(1061, 76);
            panelCompressor.TabIndex = 17;
            // 
            // textBoxCompressorGain
            // 
            textBoxCompressorGain.Location = new Point(905, 27);
            textBoxCompressorGain.Name = "textBoxCompressorGain";
            textBoxCompressorGain.Size = new Size(100, 28);
            textBoxCompressorGain.TabIndex = 19;
            textBoxCompressorGain.TextChanged += textBoxCompressorGain_TextChanged;
            // 
            // textBoxCompressorThreshold
            // 
            textBoxCompressorThreshold.Location = new Point(564, 27);
            textBoxCompressorThreshold.Name = "textBoxCompressorThreshold";
            textBoxCompressorThreshold.Size = new Size(100, 28);
            textBoxCompressorThreshold.TabIndex = 4;
            textBoxCompressorThreshold.TextChanged += textBoxCompressorThreshold_TextChanged;
            // 
            // labelCompressorGain
            // 
            labelCompressorGain.AutoSize = true;
            labelCompressorGain.ForeColor = SystemColors.ControlLightLight;
            labelCompressorGain.Location = new Point(816, 30);
            labelCompressorGain.Name = "labelCompressorGain";
            labelCompressorGain.Size = new Size(70, 17);
            labelCompressorGain.TabIndex = 3;
            labelCompressorGain.Text = "Gain (dB)";
            // 
            // labelCompressorThreshold
            // 
            labelCompressorThreshold.AutoSize = true;
            labelCompressorThreshold.ForeColor = SystemColors.ControlLightLight;
            labelCompressorThreshold.Location = new Point(432, 30);
            labelCompressorThreshold.Name = "labelCompressorThreshold";
            labelCompressorThreshold.Size = new Size(109, 17);
            labelCompressorThreshold.TabIndex = 2;
            labelCompressorThreshold.Text = "Threshold (dB)";
            // 
            // textBoxMaxDepth
            // 
            textBoxMaxDepth.Location = new Point(440, 715);
            textBoxMaxDepth.Name = "textBoxMaxDepth";
            textBoxMaxDepth.ReadOnly = true;
            textBoxMaxDepth.Size = new Size(70, 28);
            textBoxMaxDepth.TabIndex = 22;
            // 
            // textBoxMinDepth
            // 
            textBoxMinDepth.Location = new Point(440, 677);
            textBoxMinDepth.Name = "textBoxMinDepth";
            textBoxMinDepth.ReadOnly = true;
            textBoxMinDepth.Size = new Size(70, 28);
            textBoxMinDepth.TabIndex = 21;
            // 
            // labelMaxDepth
            // 
            labelMaxDepth.AutoSize = true;
            labelMaxDepth.ForeColor = SystemColors.ControlLightLight;
            labelMaxDepth.Location = new Point(348, 718);
            labelMaxDepth.Name = "labelMaxDepth";
            labelMaxDepth.Size = new Size(86, 17);
            labelMaxDepth.TabIndex = 20;
            labelMaxDepth.Text = "Max Depth:";
            // 
            // labelMinDepth
            // 
            labelMinDepth.AutoSize = true;
            labelMinDepth.ForeColor = SystemColors.ControlLightLight;
            labelMinDepth.Location = new Point(348, 680);
            labelMinDepth.Name = "labelMinDepth";
            labelMinDepth.Size = new Size(82, 17);
            labelMinDepth.TabIndex = 19;
            labelMinDepth.Text = "Min Depth:";
            // 
            // buttonPlot
            // 
            buttonPlot.Font = new Font("Lucida Sans Unicode", 14.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            buttonPlot.Location = new Point(867, 730);
            buttonPlot.Name = "buttonPlot";
            buttonPlot.Size = new Size(206, 47);
            buttonPlot.TabIndex = 23;
            buttonPlot.Text = "Plot results";
            buttonPlot.UseVisualStyleBackColor = true;
            buttonPlot.Click += buttonPlot_Click;
            // 
            // checkBoxRIAA
            // 
            checkBoxRIAA.AutoSize = true;
            checkBoxRIAA.ForeColor = SystemColors.ControlLightLight;
            checkBoxRIAA.Location = new Point(12, 178);
            checkBoxRIAA.Name = "checkBoxRIAA";
            checkBoxRIAA.Size = new Size(127, 21);
            checkBoxRIAA.TabIndex = 24;
            checkBoxRIAA.Text = "RIAA pre-filter";
            checkBoxRIAA.UseVisualStyleBackColor = true;
            checkBoxRIAA.CheckedChanged += checkBoxRIAA_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1085, 789);
            Controls.Add(checkBoxRIAA);
            Controls.Add(buttonPlot);
            Controls.Add(textBoxMaxDepth);
            Controls.Add(textBoxMinDepth);
            Controls.Add(labelMaxDepth);
            Controls.Add(labelMinDepth);
            Controls.Add(checkBoxCompressor);
            Controls.Add(panelCompressor);
            Controls.Add(checkBoxHiFreqLim);
            Controls.Add(panelHiFreqLim);
            Controls.Add(checkBoxLowFreqMixer);
            Controls.Add(panelLowFreqMixer);
            Controls.Add(textBoxSurfaceFilling);
            Controls.Add(textBoxMinLand);
            Controls.Add(labelSurfaceFilling);
            Controls.Add(labelMinLand);
            Controls.Add(labelSineFreq);
            Controls.Add(labelTitle);
            Controls.Add(inputSineFreq);
            Controls.Add(radioGenerateFreq);
            Controls.Add(btnImportFile);
            Controls.Add(textImportFilePath);
            Controls.Add(radioImportAudio);
            Controls.Add(buttonSimulate);
            Font = new Font("Lucida Sans Unicode", 10F, FontStyle.Italic);
            Name = "MainForm";
            Text = "VMS80";
            panelLowFreqMixer.ResumeLayout(false);
            panelLowFreqMixer.PerformLayout();
            panelHiFreqLim.ResumeLayout(false);
            panelHiFreqLim.PerformLayout();
            panelCompressor.ResumeLayout(false);
            panelCompressor.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnImportFile;
        private TextBox textImportFilePath;
        private TextBox inputSineFreq;
        private RadioButton radioGenerateFreq;
        private Button buttonSimulate;
        private RadioButton radioImportAudio;
        private Label labelTitle;
        private Label labelSineFreq;
        private Label labelMinLand;
        private Label labelSurfaceFilling;
        private TextBox textBoxMinLand;
        private TextBox textBoxSurfaceFilling;
        private Panel panelLowFreqMixer;
        private CheckBox checkBoxLowFreqMixer;
        private CheckBox checkBoxHiFreqLim;
        private Panel panelHiFreqLim;
        private CheckBox checkBoxCompressor;
        private Panel panelCompressor;
        private Label labelLowFreqMixerGain;
        private Label labelLowFreqMixerCutOffFrequency;
        private Label labelCompressorGain;
        private Label labelCompressorThreshold;
        private Label labelHiFreqLimThreshold;
        private Label labelHiFreqLimGain;
        private Label labelHiFreqLimCutOffFrequency;
        private TextBox textBoxLowFreqMixerGain;
        private TextBox textBoxLowFreqMixerCutOffFrequency;
        private TextBox textBoxHiFreqLimiterThreshold;
        private TextBox textBoxHiFreqLimiterGain;
        private TextBox textBoxHiFreqLimiterCutOffFrequency;
        private TextBox textBoxCompressorGain;
        private TextBox textBoxCompressorThreshold;
        private TextBox textBoxMaxDepth;
        private TextBox textBoxMinDepth;
        private Label labelMaxDepth;
        private Label labelMinDepth;
        private Button buttonPlot;
        private CheckBox checkBoxRIAA;
    }
}
