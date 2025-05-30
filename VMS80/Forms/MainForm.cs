﻿using System.Diagnostics;
using System.Globalization;

namespace VMS80
{
    public partial class MainForm : Form
    {
        private readonly Plugins m_plugins;
        private readonly Simulator m_simulator;

        private string m_filepath;

        public MainForm()
        {
            InitializeComponent();

            m_plugins = new Plugins();
            m_simulator = new Simulator();

            // Default values
            m_filepath = "";

            // Sync form plugins panels
            checkBoxRIAA.Checked = m_plugins.m_riaa_filter_enable;
            checkBoxLowFreqMixer.Checked = m_plugins.m_low_freq_mixer_enable;
            panelLowFreqMixer.Enabled = checkBoxLowFreqMixer.Checked;
            checkBoxHiFreqLim.Checked = m_plugins.m_hi_freq_limiter_enable;
            panelHiFreqLim.Enabled = checkBoxHiFreqLim.Checked;
            m_plugins.m_compressor_enable = checkBoxCompressor.Checked;
            panelCompressor.Enabled = checkBoxCompressor.Checked;

            textBoxLowFreqMixerCutOffFrequency.Text = m_plugins.m_low_freq_mixer.get_cutoff_frequency().ToString();
            textBoxLowFreqMixerGain.Text = m_plugins.m_low_freq_mixer.get_gain().ToString("0.00", CultureInfo.InvariantCulture);
            textBoxHiFreqLimiterCutOffFrequency.Text = m_plugins.m_hi_freq_limiter.get_cutoff_frequency().ToString();
            textBoxHiFreqLimiterThreshold.Text = m_plugins.m_hi_freq_limiter.get_threshold().ToString("0.00", CultureInfo.InvariantCulture);
            textBoxHiFreqLimiterGain.Text = m_plugins.m_hi_freq_limiter.get_gain().ToString("0.00", CultureInfo.InvariantCulture);
            textBoxCompressorThreshold.Text = m_plugins.m_compressor.get_threshold().ToString("0.00", CultureInfo.InvariantCulture);
            textBoxCompressorGain.Text = m_plugins.m_compressor.get_gain().ToString("0.00", CultureInfo.InvariantCulture);

            inputSineFreq.Text = 100.ToString(CultureInfo.InvariantCulture);  // Perfectly fitted groove
            inputTargetLand.Text = m_simulator.get_target_land().ToString();
        }

        ~MainForm()
        {
            // Destroy generated file by simulation
            m_simulator.clear_results();
        }

        public void simulate()
        {
            int the_samplerate = 48000;
            int the_nb_samples = 1000000;
            int the_nb_channels = 2;

            float[] the_data;

            if (radioGenerateFreq.Checked)
            {
                generate_sinewave(out the_data, the_nb_samples, the_nb_channels, the_samplerate);
            }
            else
            {
                AudioReader.read_wav_from_file(m_filepath, out the_data, out the_nb_samples, out the_nb_channels, out the_samplerate);
            }

            if (the_nb_channels > 2)
            {
                Debug.WriteLine("Unsupported number of channels\n");
            }

            // process the signal
            m_plugins.set_samplerate(the_samplerate);
            m_plugins.process(the_data, the_nb_samples, the_nb_channels);

            // Simulate
            m_simulator.set_samplerate(the_samplerate);
            m_simulator.set_target_land(int.Parse(inputTargetLand.Text, CultureInfo.InvariantCulture));
            m_simulator.process(the_data, the_nb_samples, the_nb_channels);
        }

        private void generate_sinewave(out float[] a_data, int a_nb_samples, int a_nb_channels, int a_samplerate)
        {
            a_data = new float[a_nb_samples * a_nb_channels];
            float the_gen_frequency = 100;
            try
            {
                float.Parse(inputSineFreq.Text, CultureInfo.InvariantCulture);
            }
            catch { }
            float the_phase = checkBoxPhase.Checked ? -1 : 1;
            Debug.WriteLine("Generating " + the_gen_frequency + "Hz frequency");

            if (a_nb_channels == 2)
            {
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    a_data[2 * i] = (float)Math.Sin(2.0 * Math.PI * the_gen_frequency * i / a_samplerate);
                    a_data[2 * i + 1] = the_phase * (float)Math.Sin(2.0 * Math.PI * the_gen_frequency * i / a_samplerate);
                }
            }
            else
            {
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    a_data[i] = (float)Math.Sin(2.0 * Math.PI * the_gen_frequency * i / a_samplerate);
                }
            }
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new()
            {
                InitialDirectory = "c:\\",
                Filter = "wav files (*.wav)|*.wav",
                RestoreDirectory = true
            };
            if (file.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                m_filepath = file.FileName;
                textImportFilePath.Text = m_filepath;
            }
        }

        private void inputSineFreq_TextChanged(object sender, EventArgs e)
        {
        }

        private void buttonSimulate_Click(object sender, EventArgs e)
        {
            // Set cursor as waiting
            Cursor.Current = Cursors.WaitCursor;

            simulate();

            textBoxMinLand.Text = m_simulator.get_minimal_land().ToString("0.0μm", CultureInfo.InvariantCulture);
            textBoxSurfaceFilling.Text = m_simulator.get_surface_filling().ToString("0.00%", CultureInfo.InvariantCulture);
            textBoxMinDepth.Text = m_simulator.get_minimal_depth().ToString("0.0μm", CultureInfo.InvariantCulture);
            textBoxMaxDepth.Text = m_simulator.get_maximal_depth().ToString("0.0μm", CultureInfo.InvariantCulture);

            // Restore cursor
            Cursor.Current = Cursors.Default;
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            PlotForm the_plot = new(m_simulator);
            the_plot.Show();
        }

        private void checkBoxRIAA_CheckedChanged(object sender, EventArgs e)
        {
            m_plugins.m_riaa_filter_enable = checkBoxRIAA.Checked;
        }

        private void checkBoxLowFreqMixer_CheckedChanged(object sender, EventArgs e)
        {
            m_plugins.m_low_freq_mixer_enable = checkBoxLowFreqMixer.Checked;
            panelLowFreqMixer.Enabled = checkBoxLowFreqMixer.Checked;
        }

        private void checkBoxHiFreqLim_CheckedChanged(object sender, EventArgs e)
        {
            m_plugins.m_hi_freq_limiter_enable = checkBoxHiFreqLim.Checked;
            panelHiFreqLim.Enabled = checkBoxHiFreqLim.Checked;
        }

        private void checkBoxCompressor_CheckedChanged(object sender, EventArgs e)
        {
            m_plugins.m_compressor_enable = checkBoxCompressor.Checked;
            panelCompressor.Enabled = checkBoxCompressor.Checked;
        }

        private void textBoxLowFreqMixerCutOffFrequency_TextChanged(object sender, EventArgs e)
        {
            try
            {
                m_plugins.m_low_freq_mixer.set_cutoff_frequency(int.Parse(textBoxLowFreqMixerCutOffFrequency.Text, CultureInfo.InvariantCulture));
            }
            catch { }
        }

        private void textBoxLowFreqMixerCutOffGain_TextChanged(object sender, EventArgs e)
        {
            try
            {
                m_plugins.m_low_freq_mixer.set_gain(float.Parse(textBoxLowFreqMixerGain.Text, CultureInfo.InvariantCulture));
            }
            catch { }
        }

        private void textBoxHiFreqLimiterCutOffFrequency_TextChanged(object sender, EventArgs e)
        {
            try
            {
                m_plugins.m_hi_freq_limiter.set_cutoff_frequency(int.Parse(textBoxHiFreqLimiterCutOffFrequency.Text, CultureInfo.InvariantCulture));
            }
            catch { }
        }

        private void textBoxHiFreqLimiterThreshold_TextChanged(object sender, EventArgs e)
        {
            try
            {
                m_plugins.m_hi_freq_limiter.set_threshold(float.Parse(textBoxHiFreqLimiterThreshold.Text, CultureInfo.InvariantCulture));
            }
            catch { }
        }

        private void textBoxHiFreqLimiterGain_TextChanged(object sender, EventArgs e)
        {
            try
            {
                m_plugins.m_hi_freq_limiter.set_gain(float.Parse(textBoxHiFreqLimiterGain.Text, CultureInfo.InvariantCulture));
            }
            catch { }
        }

        private void textBoxCompressorThreshold_TextChanged(object sender, EventArgs e)
        {
            try
            {
                m_plugins.m_compressor.set_threshold(float.Parse(textBoxCompressorThreshold.Text, CultureInfo.InvariantCulture));
            }
            catch { }
        }

        private void textBoxCompressorGain_TextChanged(object sender, EventArgs e)
        {
            try
            {
                m_plugins.m_compressor.set_gain(float.Parse(textBoxCompressorGain.Text, CultureInfo.InvariantCulture));
            }
            catch { }
        }
    }
}
