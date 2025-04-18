using System;

namespace VMS80
{
    internal class EllipticalFilter
    {
        private int m_cutoff_frequency;
        private float m_gain;

        public EllipticalFilter()
        {
        }

        public void process(float[] a_data, int a_nb_samples, int a_nb_channels)
        {
        }

        public int get_cutoff_frequency()
        {
            return m_cutoff_frequency;
        }
        public void set_cutoff_frequency(int a_cutoff_frequency)
        {
            m_cutoff_frequency = a_cutoff_frequency;
        }

        public float get_gain()
        {
            return m_gain;
        }
        public void set_gain(float a_gain)
        {
            m_gain = a_gain;
        }
    }
}
