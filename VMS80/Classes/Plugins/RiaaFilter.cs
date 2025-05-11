using System;
using System.Diagnostics;

namespace VMS80.Plugin
{
    internal class RIAAFilter
    {
        private int m_samplerate;

        private readonly Biquad m_filter_L;
        private readonly Biquad m_filter_R;

        public RIAAFilter()
        {
            m_filter_L = new Biquad();
            m_filter_R = new Biquad();
        }

        public void process(float[] a_data, int a_nb_samples, int a_nb_channels)
        {

            if (a_nb_channels == 2)
            {
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    //a_data[2 * i + 0] = m_filter_L.process(a_data[2 * i + 0]);
                    //a_data[2 * i + 1] = m_filter_R.process(a_data[2 * i + 1]);
                }
            }
            else
            {
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    //a_data[i] = m_filter_L.process(a_data[i]);
                }
            }
        }

        public void set_samplerate(int a_samplerate)
        {
            m_samplerate = a_samplerate;

            //m_filter_L.init_riaa(m_samplerate, nb_biquad);
            //m_filter_R.init_riaa(m_samplerate, nb_biquad);
        }

    }
}
