
namespace VMS80.Plugin
{
    internal class Biquad
    {
        // Biquadratic filters (2nd order) are known to be always stable.
        // So safe higher order filtering is achieved by serializing biquads

        private int m_nb_biqud;

        private double[] coeffs = [];
        private double[] buffer = [];

        public void init_butterworth_low_pass(int a_frequency, int a_samplerate, int a_nb_biqud)
        {
            m_nb_biqud = a_nb_biqud;
            int nb_coeffs = m_nb_biqud * 5;
            int nb_buffer = m_nb_biqud * 2;
            int i, coeffs_idx;
            double a1, a2, b0, b1, b2;
            double ita, q, g;

            // LPF Butterworth 2nd order
            ita = 1.0 / Math.Tan(Math.PI * a_frequency / a_samplerate);
            q = Math.Sqrt(2.0);
            g = 1.0 / (1.0 + q * ita + ita * ita); // a0 normalization

            b0 = g;
            b1 = 2.0 * g;
            b2 = g;
            a1 = 2.0 * (ita * ita - 1.0) * g;
            a2 = -(1.0 - q * ita + ita * ita) * g;

            coeffs = new double[nb_coeffs];
            for (i = 0; i < m_nb_biqud; ++i)
            {
                coeffs_idx = i * 5;

                coeffs[coeffs_idx + 0] = b0;
                coeffs[coeffs_idx + 1] = b1;
                coeffs[coeffs_idx + 2] = b2;
                coeffs[coeffs_idx + 3] = a1;
                coeffs[coeffs_idx + 4] = a2;
            }

            buffer = new double[nb_buffer];
            for (i = 0; i < nb_buffer; ++i)
            {
                buffer[i] = 0;
            }
        }

        public void init_butterworth_high_pass(int a_frequency, int a_samplerate, int a_nb_biqud)
        {
            m_nb_biqud = a_nb_biqud;
            int nb_coeffs = m_nb_biqud * 5;
            int nb_buffer = m_nb_biqud * 2;
            int i, coeffs_idx;
            double a1, a2, b0, b1, b2;
            double ita, q, g;

            // HPF Butterworth 2nd order
            ita = 1.0 / Math.Tan(Math.PI * a_frequency / a_samplerate);
            q = Math.Sqrt(2.0);
            g = 1.0 / (1.0 + q * ita + ita * ita); // a0 normalization

            b0 = (ita * ita) * g;
            b1 = -2.0 * (ita * ita) * g;
            b2 = (ita * ita) * g;
            a1 = 2.0 * (ita * ita - 1.0) * g;
            a2 = -(1.0 - q * ita + ita * ita) * g;

            coeffs = new double[nb_coeffs];
            for (i = 0; i < m_nb_biqud; ++i)
            {
                coeffs_idx = i * 5;

                coeffs[coeffs_idx + 0] = b0;
                coeffs[coeffs_idx + 1] = b1;
                coeffs[coeffs_idx + 2] = b2;
                coeffs[coeffs_idx + 3] = a1;
                coeffs[coeffs_idx + 4] = a2;
            }

            buffer = new double[nb_buffer];
            for (i = 0; i < nb_buffer; ++i)
            {
                buffer[i] = 0;
            }
        }

        public void init_riaa(int a_samplerate)
        {
            m_nb_biqud = 1; // 1 biquad for RIAA emphasis
            int nb_coeffs = m_nb_biqud * 5;
            int nb_buffer = m_nb_biqud * 2;
            double p0, p1, p2, q0, q1, q2;
            double a0, a1, a2, b0, b1, b2;
            double w1, w2, w3, w4, w_ref;
            double num, den, gain;
            double K  = 2.0 * a_samplerate;

            // RIAA corner angular frequencies
            w1 = 1.0 / (2 * Math.PI * 50.05);
            w2 = 1.0 / (2 * Math.PI * 500.5);
            w3 = 1.0 / (2 * Math.PI * 2122.0);
            w4 = 1.0 / (2 * Math.PI * 0.45 * a_samplerate); // additional frequency just before nyquist to smooth high-end

            // Numerator: (s.w2 + 1)(s.w4 + 1) = p0.s^2 + p1.s + p2
            p0 = (w2 * w4);
            p1 = (w2 + w4);
            p2 = 1.0;

            // Denominator: (s.w1 + 1)(s.w3 + 1) = q0.s^2 + q1.s + q2
            q0 = (w1 * w3);
            q1 = (w1 + w3);
            q2 = 1.0;

            // Second-Order system bilinear transform
            a0 = (p0 * K * K + p1 * K + p2);
            a1 = (2 * p2 - 2 * p0 * K * K);
            a2 = (p0 * K * K - p1 * K + p2);

            b0 = (q0 * K * K + q1 * K + q2);
            b1 = (2 * q2 - 2 * q0 * K * K);
            b2 = (q0 * K * K - q1 * K + q2);

            // a0 normalization
            b0 /= a0;
            b1 /= a0;
            b2 /= a0;
            a1 /= a0;
            a2 /= a0;
            a0 = 1.0;

            // Normalize gain to 0dB @ 1kHz
            w_ref = 2 * Math.PI * 1000 / a_samplerate;
            // not sexy but avoid using complex numbers to get the zeros of the transfer function
            num = (b0 * b0) + (b1 * b1) + (b2 * b2)
                + 2.0 * ((b0 * b1) + (b1 * b2)) * Math.Cos(w_ref)
                + 2.0 * (b0 * b2) * Math.Cos(2.0 * w_ref);
            den = (a0 * a0) + (a1 * a1) + (a2 * a2)
                + 2.0 * ((a0 * a1) + (a1 * a2)) * Math.Cos(w_ref)
                + 2.0 * (a0 * a2) * Math.Cos(2.0 * w_ref);
            gain = Math.Sqrt(num / den);
            // Only to numerator
            b0 /= gain;
            b1 /= gain;
            b2 /= gain;

            // Write the coefficients
            coeffs = new double[nb_coeffs];
            coeffs[0] = b0;
            coeffs[1] = b1;
            coeffs[2] = b2;
            coeffs[3] = a1;
            coeffs[4] = a2;

            buffer = new double[nb_buffer];
            for (int i = 0; i < nb_buffer; ++i)
            {
                buffer[i] = 0;
            }
        }

        public float process(float a_data)
        {
            double data_out = (double)a_data;
            double data_in;
            int coeffs_idx, buffer_idx;
            for (int i = 0; i < m_nb_biqud; ++i)
            {
                coeffs_idx = i * 5;
                buffer_idx = i * 2;

                data_in = data_out; // serialize the biquads
                data_out = data_in * coeffs[coeffs_idx + 0] + buffer[buffer_idx + 0];
                buffer[buffer_idx + 0] = data_in * coeffs[coeffs_idx + 1] + data_out * coeffs[coeffs_idx + 3] + buffer[buffer_idx + 1];
                buffer[buffer_idx + 1] = data_in * coeffs[coeffs_idx + 2] + data_out * coeffs[coeffs_idx + 4];
            }

            return (float)data_out;
        }

    }
}
