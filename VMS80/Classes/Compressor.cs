using System;
using System.Diagnostics;

namespace VMS80
{
    internal class Compressor
    {
        private double m_threshold;
        private double m_gain;
        private double m_gain_reduction;

        public Compressor()
        {

        }

        public void process(double[] a_data, int a_nb_samples, int a_nb_channels)
        {

        }

        public double get_threshold()
        {
            return m_threshold;
        }
        public void set_threshold(double a_threshold)
        {
            m_threshold = a_threshold;
        }

        public double get_gain()
        {
            return m_gain;
        }
        public void set_gain(double a_gain)
        {
            m_gain = a_gain;
        }
        public double get_gain_reduction()
        {
            return m_gain_reduction;
        }
    }
}
