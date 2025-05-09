
namespace VMS80
{
    internal class Biquad
    {
        private int m_order;

        private double[] coeffs = [];
        private double[] buffer = [];

        public void init_hi_pass(float frequency, int order)
        {
            m_order = order;
            int nb_coeffs = order * 5;
            int nb_buffer = order * 2;
            int i, coeffs_idx;
            double a0, a1, a2, b0, b1, b2;

            coeffs = new double[nb_coeffs];

            double ita = 1.0 / Math.Tan(Math.PI * frequency);
            double q = Math.Sqrt(2.0);
            for (i = 0; i < m_order; ++i)
            {
                coeffs_idx = i * 5;

                b0 = 1.0 / (1.0 + q * ita + ita * ita);
                b1 = -2.0 * b0;
                b2 = b0;
                a0 = 1.0;
                a1 = -2.0 * (ita * ita - 1.0) * b0;
                a2 = -(1.0 - q * ita + ita * ita) * b0;

                // Normalize by a0
                coeffs[coeffs_idx + 0] = b0 / a0;
                coeffs[coeffs_idx + 1] = b1 / a0;
                coeffs[coeffs_idx + 2] = b2 / a0;
                coeffs[coeffs_idx + 3] = -a1 / a0;
                coeffs[coeffs_idx + 4] = -a2 / a0;
            }

            buffer = new double[nb_buffer];
            for (i = 0; i < nb_buffer; ++i)
            {
                buffer[i] = 0;
            }
        }

        public void init_low_pass(float frequency, int order)
        {
            m_order = order;
            int nb_coeffs = order * 5;
            int nb_buffer = order * 2;
            int i, coeffs_idx;
            double a0, a1, a2, b0, b1, b2;

            coeffs = new double[nb_coeffs];

            double ita = 1.0 / Math.Tan(Math.PI * frequency);
            double q = Math.Sqrt(2.0);
            for (i = 0; i < m_order; ++i)
            {
                coeffs_idx = i * 5;
                
                b0 = 1.0 / (1.0 + q * ita + ita * ita);
                b1 = 2.0 * b0;
                b2 = b0;
                a0 = 1.0;
                a1 = 2.0 * (ita * ita - 1.0) * b0;
                a2 = -(1.0 - q * ita + ita * ita) * b0;

                // Normalize by a0
                coeffs[coeffs_idx + 0] = b0 / a0;
                coeffs[coeffs_idx + 1] = b1 / a0;
                coeffs[coeffs_idx + 2] = b2 / a0;
                coeffs[coeffs_idx + 3] = -a1 / a0;
                coeffs[coeffs_idx + 4] = -a2 / a0;
            }

            buffer = new double[nb_buffer];
            for (i = 0; i < nb_buffer; ++i)
            {
                buffer[i] = 0;
            }
        }

        public float process(float a_data)
        {
            double data_out = (double)a_data;
            double data_in;
            int coeffs_idx, buffer_idx;
            for (int i = 0; i < m_order; ++i)
            {
                coeffs_idx = i * 5;
                buffer_idx = i * 2;

                data_in = data_out; // serialize the biquads
                data_out = data_in * coeffs[coeffs_idx + 0] + buffer[buffer_idx + 0];
                buffer[buffer_idx + 0] = data_in * coeffs[coeffs_idx + 1] + data_out * coeffs[buffer_idx + 3] + buffer[buffer_idx + 1];
                buffer[buffer_idx + 1] = data_in * coeffs[coeffs_idx + 2] + data_out * coeffs[buffer_idx + 4];
            }

            return (float)data_out;
        }

    }
}
