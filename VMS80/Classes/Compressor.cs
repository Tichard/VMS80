using System;
using System.Diagnostics;

namespace VMS80
{
    internal class Compressor
    {
        private float m_threshold;
        private float m_gain;
        private float m_gain_reduction;

        public Compressor()
        {
        }

        public void process(float[] a_data, int a_nb_samples, int a_nb_channels)
        {
        }

        public float get_threshold()
        {
            return m_threshold;
        }
        public void set_threshold(float a_threshold)
        {
            m_threshold = a_threshold;
        }

        public float get_gain()
        {
            return m_gain;
        }
        public void set_gain(float a_gain)
        {
            m_gain = a_gain;
        }
        public float get_gain_reduction()
        {
            return m_gain_reduction;
        }
    }
}
