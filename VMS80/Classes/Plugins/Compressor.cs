using System;
using System.Diagnostics;

namespace VMS80.Plugin
{
    internal class Compressor
    {
        private int m_samplerate;

        private float m_threshold;
        private float m_gain;
        private float m_gain_reduction;

        public Compressor()
        {
            // Default value
            set_threshold(0);
            set_gain(0);
        }

        public void process(float[] a_data, int a_nb_samples, int a_nb_channels)
        {
            if (a_nb_channels == 2)
            {
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    a_data[2 * i + 0] *= m_gain;
                    a_data[2 * i + 1] *= m_gain;
                }
            }
            else
            {
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    a_data[i] *= m_gain;
                }
            }
        }

        public void set_samplerate(int a_samplerate)
        {
            m_samplerate = a_samplerate;
        }

        public float get_threshold()
        {
            return (float)(20.0 * Math.Log10(m_threshold));
        }
        public void set_threshold(float a_threshold)
        {
            m_threshold = (float)Math.Pow(10.0, a_threshold / 20.0);
        }

        public float get_gain()
        {
            return (float)(20.0*Math.Log10(m_gain));
        }
        public void set_gain(float a_gain)
        {
            m_gain = (float)Math.Pow(10.0, a_gain / 20.0);
        }
        public float get_gain_reduction()
        {
            return (float)(20.0 * Math.Log10(m_gain_reduction));
        }
    }
}
