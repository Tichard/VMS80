namespace VMS80
{
    internal class Plugins
    {

        public LowFreqMixer m_low_freq_mixer;
        public HiFreqLimiter m_hi_freq_limiter;
        public Compressor m_compressor;

        public bool m_low_freq_mixer_enable;
        public bool m_hi_freq_limiter_enable;
        public bool m_compressor_enable;

        public Plugins()
        {
            m_low_freq_mixer = new LowFreqMixer();
            m_hi_freq_limiter = new HiFreqLimiter();
            m_compressor = new Compressor();

            m_low_freq_mixer_enable = true;
            m_hi_freq_limiter_enable = false;
            m_compressor_enable = false;
        }

        public void process(float[] a_data, int a_nb_samples, int a_nb_channels)
        {
            if (m_low_freq_mixer_enable)
            {
                m_low_freq_mixer.process(a_data, a_nb_samples, a_nb_channels);
            }
            if (m_hi_freq_limiter_enable)
            {
                m_hi_freq_limiter.process(a_data, a_nb_samples, a_nb_channels);
            }
            if (m_compressor_enable)
            {
                m_compressor.process(a_data, a_nb_samples, a_nb_channels);
            }
        }

        public void set_samplerate(int samplerate)
        {
            m_low_freq_mixer.samplerate = samplerate;
            m_hi_freq_limiter.samplerate = samplerate;
            m_compressor.samplerate = samplerate;
        }

    }
}
