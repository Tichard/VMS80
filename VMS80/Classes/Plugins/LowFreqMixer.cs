﻿using System;

namespace VMS80.Plugin
{
    internal class LowFreqMixer
    {
        private int m_samplerate;

        private int m_cutoff_frequency;
        private float m_gain;

        private readonly Biquad m_low_pass_L;
        private readonly Biquad m_low_pass_R;
        private readonly Biquad m_high_pass_L;
        private readonly Biquad m_high_pass_R;

        private readonly int nb_biquad = 4;

        public LowFreqMixer()
        {
            m_low_pass_L = new Biquad();
            m_low_pass_R = new Biquad();
            m_high_pass_L = new Biquad();
            m_high_pass_R = new Biquad();

            // Default value
            set_cutoff_frequency(200);
            set_gain(0);
        }

        public void process(float[] a_data, int a_nb_samples, int a_nb_channels)
        {
            if (a_nb_channels == 2)
            {
                float low_L, high_L, low_R, high_R;
                float low_mono;

                for (int i = 0; i < a_nb_samples; ++i)
                {
                    low_L = m_low_pass_L.process(a_data[2 * i + 0]);
                    high_L = m_high_pass_L.process(a_data[2 * i + 0]);
                    low_R = m_low_pass_R.process(a_data[2 * i + 1]);
                    high_R = m_high_pass_R.process(a_data[2 * i + 1]);

                    // Mix the low bands to get Mono
                    low_mono = (low_L + low_R) / 2;

                    a_data[2 * i + 0] = (high_L + low_mono) * m_gain;
                    a_data[2 * i + 1] = (high_R + low_mono) * m_gain;
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

            // update biquads
            set_cutoff_frequency(m_cutoff_frequency);
        }

        public int get_cutoff_frequency()
        {
            return m_cutoff_frequency;
        }
        public void set_cutoff_frequency(int a_cutoff_frequency)
        {
            m_cutoff_frequency = a_cutoff_frequency;

            // Sum of even number of Butterworth 2nd-order filters results in a perfeclty flat amplitude response
            m_low_pass_L.init_butterworth_low_pass(a_cutoff_frequency, m_samplerate, nb_biquad);
            m_high_pass_L.init_butterworth_high_pass(a_cutoff_frequency, m_samplerate, nb_biquad);
            m_low_pass_R.init_butterworth_low_pass(a_cutoff_frequency, m_samplerate, nb_biquad);
            m_high_pass_R.init_butterworth_high_pass(a_cutoff_frequency, m_samplerate, nb_biquad);
        }

        public float get_gain()
        {
            return (float)(20.0 * Math.Log10(m_gain));
        }
        public void set_gain(float a_gain)
        {
            m_gain = (float)Math.Pow(10.0, a_gain / 20.0);
        }
    }
}
