using System.Diagnostics;
using System.Globalization;

namespace VMS80
{
    public partial class MainForm : Form
    {
        private AudioReader m_audio_reader;
        private Plugins m_plugins;
        private Simulator m_simulator;

        private string m_filepath;

        private float m_gen_frequency;
        private int m_samplerate;

        public MainForm()
        {
            InitializeComponent();

            m_audio_reader = new AudioReader();
            m_plugins = new Plugins();
            m_simulator = new Simulator();

            // Default values
            m_samplerate = 48000;
            m_gen_frequency = 100; // Perfectly fitted groove
            m_filepath = "";

            // Sync form plugins panels
            checkBoxEllipticalFilter.Checked = m_plugins.m_elliptical_filter_enable;
            panelEllipticalFilter.Enabled = checkBoxEllipticalFilter.Checked;
            checkBoxHiFreqLim.Checked = m_plugins.m_hi_freq_liniter_enable;
            panelHiFreqLim.Enabled = checkBoxHiFreqLim.Checked;
            m_plugins.m_compressor_enable = checkBoxCompressor.Checked = m_plugins.m_compressor_enable;
            panelCompressor.Enabled = checkBoxCompressor.Checked;

            inputSineFreq.Text = m_gen_frequency.ToString(CultureInfo.InvariantCulture);
        }

        public void simulate()
        {
            int the_samplerate = m_samplerate;
            int the_nb_samples = 1000000;
            int the_nb_channels = 2;

            float[] the_data;

            if (radioGenerateFreq.Checked)
            {
                generate_sinewave(out the_data, the_nb_samples, the_nb_channels);
            }
            else
            {
                m_audio_reader.read_wav_from_file(m_filepath, out the_data, out the_nb_samples, out the_nb_channels);
            }

            if (the_nb_channels > 2)
            {
                Debug.WriteLine("Unsupported number of channels\n");
            }

            // process the signal
            m_plugins.process(the_data, the_nb_samples, the_nb_channels);

            // Simulate
            m_simulator.set_samplerate(the_samplerate);
            m_simulator.process(the_data, the_nb_samples, the_nb_channels);
        }

        private void generate_sinewave(out float[] a_data, int a_nb_samples, int a_nb_channels)
        {
            a_data = new float[a_nb_samples * a_nb_channels];
            float the_smaplerate = m_samplerate;
            float the_gen_frequency = float.Parse(inputSineFreq.Text, CultureInfo.InvariantCulture);
            Debug.WriteLine("Generating " + the_gen_frequency + "Hz frequency");

            if (a_nb_channels == 2)
            {
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    a_data[2 * i + 0] = (float)Math.Sin(2.0 * Math.PI * the_gen_frequency * i / the_smaplerate);
                    a_data[2 * i + 1] = (float)Math.Sin(2.0 * Math.PI * the_gen_frequency * i / the_smaplerate);
                }
            }
            else
            {
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    a_data[i] = (float)Math.Sin(2.0 * Math.PI * the_gen_frequency * i / m_samplerate);
                }
            }
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();

            file.InitialDirectory = "c:\\";
            file.Filter = "wav files (*.wav)|*.wav";
            file.RestoreDirectory = true;
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
            simulate();

            textBoxMinLand.Text = m_simulator.get_minimal_land().ToString("0.00um");
            textBoxSurfaceFilling.Text = m_simulator.get_surface_filling().ToString("0.00%");

            m_simulator.plot();
            m_simulator.clear_results();
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
            m_plugins.m_elliptical_filter.set_gain(float.Parse(textBoxEllipticalCutOffGain.Text, CultureInfo.InvariantCulture));
        }

        private void textBoxHiFreqLimiterCutOffFrequency_TextChanged(object sender, EventArgs e)
        {
            m_plugins.m_hi_freq_liniter.set_cutoff_frequency(int.Parse(textBoxHiFreqLimiterCutOffFrequency.Text, CultureInfo.InvariantCulture));
        }

        private void textBoxHiFreqLimiterThreshold_TextChanged(object sender, EventArgs e)
        {
            m_plugins.m_hi_freq_liniter.set_threshold(float.Parse(textBoxHiFreqLimiterThreshold.Text, CultureInfo.InvariantCulture));
        }

        private void textBoxHiFreqLimiterGain_TextChanged(object sender, EventArgs e)
        {
            m_plugins.m_hi_freq_liniter.set_gain(float.Parse(textBoxHiFreqLimiterGain.Text, CultureInfo.InvariantCulture));
        }

        private void textBoxCompressorThreshold_TextChanged(object sender, EventArgs e)
        {
            m_plugins.m_compressor.set_threshold(float.Parse(textBoxCompressorThreshold.Text, CultureInfo.InvariantCulture));
        }

        private void textBoxCompressorGain_TextChanged(object sender, EventArgs e)
        {
            m_plugins.m_compressor.set_gain(float.Parse(textBoxCompressorGain.Text, CultureInfo.InvariantCulture));
        }
    }
}
