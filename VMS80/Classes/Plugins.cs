namespace VMS80
{
    internal class Plugins
    {

        public Plugin.RIAAFilter m_riaa_filter;
        public Plugin.LowFreqMixer m_low_freq_mixer;
        public Plugin.HiFreqLimiter m_hi_freq_limiter;
        public Plugin.Compressor m_compressor;

        public bool m_riaa_filter_enable;
        public bool m_low_freq_mixer_enable;
        public bool m_hi_freq_limiter_enable;
        public bool m_compressor_enable;

        public Plugins()
        {
            m_riaa_filter = new Plugin.RIAAFilter();
            m_low_freq_mixer = new Plugin.LowFreqMixer();
            m_hi_freq_limiter = new Plugin.HiFreqLimiter();
            m_compressor = new Plugin.Compressor();

            m_riaa_filter_enable = true;
            m_low_freq_mixer_enable = true;
            m_hi_freq_limiter_enable = false;
            m_compressor_enable = false;
        }

        public void process(float[] a_data, int a_nb_samples, int a_nb_channels)
        {
            if (m_riaa_filter_enable)
            {
                m_riaa_filter.process(a_data, a_nb_samples, a_nb_channels);
            }
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

        public void set_samplerate(int a_samplerate)
        {
            m_riaa_filter.set_samplerate(a_samplerate);
            m_low_freq_mixer.set_samplerate(a_samplerate);
            m_hi_freq_limiter.set_samplerate(a_samplerate);
            m_compressor.set_samplerate(a_samplerate);
        }

    }
}
