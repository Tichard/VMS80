using System;
using System.Diagnostics;

namespace VMS80
{
    internal class Plugins
    {
        public EllipticalFilter m_elliptical_filter;
        public HiFreqLimiter m_hi_freq_liniter;
        public Compressor m_compressor;

        public bool m_elliptical_filter_enable;
        public bool m_hi_freq_liniter_enable;
        public bool m_compressor_enable;

        public Plugins()
        {
            m_elliptical_filter = new EllipticalFilter();
            m_hi_freq_liniter = new HiFreqLimiter();
            m_compressor = new Compressor();

            m_elliptical_filter_enable = true;
            m_hi_freq_liniter_enable = false;
            m_compressor_enable = false;
        }

        public void process(double[] a_data, int a_nb_samples, int a_nb_channels)
        {
            if (m_elliptical_filter_enable)
            {
                m_elliptical_filter.process(a_data, a_nb_samples, a_nb_channels);
            }
            if (m_hi_freq_liniter_enable)
            {
                m_hi_freq_liniter.process(a_data, a_nb_samples, a_nb_channels);
            }
            if (m_compressor_enable)
            {
                m_compressor.process(a_data, a_nb_samples, a_nb_channels);
            }
        }

    }
}
