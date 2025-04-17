using System.Diagnostics;
using System.Globalization;

namespace VMS80
{
    public partial class MainForm : Form
    {
        private Plugins m_plugins;
        private Simulator m_simulator;

        private string m_filepath;

        private double m_gen_frequency;
        private int m_samplerate;

        public MainForm()
        {
            InitializeComponent();

            m_plugins = new Plugins();
            m_simulator = new Simulator();

            // Default values
            m_samplerate = 48000;
            m_gen_frequency = 99.89; // Kissing groove
            m_filepath = "";

            // Sync form plugins panels
            checkBoxEllipticalFilter.Checked = m_plugins.m_elliptical_filter_enable;
            panelEllipticalFilter.Enabled = checkBoxEllipticalFilter.Checked;
            checkBoxHiFreqLim.Checked = m_plugins.m_hi_freq_liniter_enable;
            panelHiFreqLim.Enabled = checkBoxHiFreqLim.Checked;
            m_plugins.m_compressor_enable = checkBoxCompressor.Checked = m_plugins.m_compressor_enable;
            panelCompressor.Enabled = checkBoxCompressor.Checked;

            inputSineFreq.Text = m_gen_frequency.ToString();
        }

        public void simulate()
        {
            int the_samplerate = m_samplerate;
            int the_nb_samples = 1000000;
            int the_nb_channels = 2;

            m_simulator.set_samplerate(the_samplerate);

            double[] the_data;

            if (radioGenerateFreq.Checked)
            {
                the_data = new double[the_nb_samples * the_nb_channels];
                generate_sinewave(the_data, the_nb_samples, the_nb_channels);
            }
            else
            {
                the_data = new double[the_nb_samples * the_nb_channels];
                // read wavefile
            }

            if (the_nb_channels > 2)
            {
                Console.WriteLine("Unsupported number of channels\n");
            }

            double[] a_groove = new double[2 * the_nb_samples];
            double[] a_pitch = new double[the_nb_samples];
            double[] a_raw = new double[the_nb_samples];
            double[] a_land = new double[the_nb_samples];

            // process the signal
            m_plugins.process(the_data, the_nb_samples, the_nb_channels);

            // Simulate
            m_simulator.compute_groove(a_groove, the_data, the_nb_samples, the_nb_channels);
            m_simulator.compute_pitch(a_pitch, a_raw, a_groove, the_nb_samples);
            m_simulator.compute_land(a_land, a_pitch, a_groove, the_nb_samples);

            // Write data to file so python can plot it
            m_simulator.export_results(the_data, a_pitch, a_groove, a_raw, a_land, the_nb_samples);
        }

        private void generate_sinewave(double[] a_data, int a_nb_samples, int a_nb_channels)
        {
            double the_smaplerate = m_samplerate;
            double the_gen_frequency = double.Parse(inputSineFreq.Text, CultureInfo.InvariantCulture);
            if (a_nb_channels == 2)
            {
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    a_data[2 * i + 0] = Math.Sin(2.0 * Math.PI * the_gen_frequency * i / the_smaplerate);
                    a_data[2 * i + 1] = Math.Sin(2.0 * Math.PI * the_gen_frequency * i / the_smaplerate);
                }
            }
            else
            {
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    a_data[i] = Math.Sin(2.0 * Math.PI * the_gen_frequency * i / m_samplerate);
                }
            }
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();

            file.InitialDirectory = "c:\\";
            file.Filter = "wav files (*.wav)|*.wav";
            file.RestoreDirectory = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                string m_filepath = file.FileName;
                textImportFilePath.Text = m_filepath;
            }
        }

        private void inputSineFreq_TextChanged(object sender, EventArgs e)
        {
        }

        private void buttonSimulate_Click(object sender, EventArgs e)
        {
            simulate();

            textBoxMinLand.Text = m_simulator.get_minimal_land().ToString("0.00um");
            textBoxSurfaceFilling.Text = m_simulator.get_surface_filling().ToString("0.00%");

            m_simulator.plot();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void checkBoxEllipticalFilter_CheckedChanged(object sender, EventArgs e)
        {
            m_plugins.m_elliptical_filter_enable = checkBoxEllipticalFilter.Checked;
            panelEllipticalFilter.Enabled = checkBoxEllipticalFilter.Checked;
        }

        private void checkBoxHiFreqLim_CheckedChanged(object sender, EventArgs e)
        {
            m_plugins.m_hi_freq_liniter_enable = checkBoxHiFreqLim.Checked;
            panelHiFreqLim.Enabled = checkBoxHiFreqLim.Checked;
        }

        private void checkBoxCompressor_CheckedChanged(object sender, EventArgs e)
        {
            m_plugins.m_compressor_enable = checkBoxCompressor.Checked;
            panelCompressor.Enabled = checkBoxCompressor.Checked;
        }

        private void textBoxEllipticalCutOffFrequency_TextChanged(object sender, EventArgs e)
        {
            m_plugins.m_elliptical_filter.set_cutoff_frequency(int.Parse(textBoxEllipticalCutOffFrequency.Text, CultureInfo.InvariantCulture));
        }

        private void textBoxEllipticalCutOffGain_TextChanged(object sender, EventArgs e)
        {
            m_plugins.m_elliptical_filter.set_gain(double.Parse(textBoxEllipticalCutOffGain.Text, CultureInfo.InvariantCulture));
        }

        private void textBoxHiFreqLimiterCutOffFrequency_TextChanged(object sender, EventArgs e)
        {
            m_plugins.m_hi_freq_liniter.set_cutoff_frequency(int.Parse(textBoxHiFreqLimiterCutOffFrequency.Text, CultureInfo.InvariantCulture));
        }

        private void textBoxHiFreqLimiterThreshold_TextChanged(object sender, EventArgs e)
        {
            m_plugins.m_hi_freq_liniter.set_threshold(double.Parse(textBoxHiFreqLimiterThreshold.Text, CultureInfo.InvariantCulture));
        }

        private void textBoxHiFreqLimiterGain_TextChanged(object sender, EventArgs e)
        {
            m_plugins.m_hi_freq_liniter.set_gain(double.Parse(textBoxHiFreqLimiterGain.Text, CultureInfo.InvariantCulture));
        }

        private void textBoxCompressorThreshold_TextChanged(object sender, EventArgs e)
        {
            m_plugins.m_compressor.set_threshold(double.Parse(textBoxCompressorThreshold.Text, CultureInfo.InvariantCulture));
        }

        private void textBoxCompressorGain_TextChanged(object sender, EventArgs e)
        {
            m_plugins.m_compressor.set_gain(double.Parse(textBoxCompressorGain.Text, CultureInfo.InvariantCulture));
        }
    }
}
