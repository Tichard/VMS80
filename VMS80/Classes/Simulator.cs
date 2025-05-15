using System.Diagnostics;

namespace VMS80
{
    internal class Simulator
    {
        // LATHE CONSTANTS
        private int vinyl_start;
        private int vinyl_stop;

        private int stylus_width;
        private int stylus_angle;
        private int groove_fullscale;

        private readonly float spin_speed;
        private readonly int land;

        private readonly string WORKSPACE = "Z:\\git\\VMS80\\VMS80\\";

        // WORKING ENV
        private float[] m_groove = [];
        private float[] m_pitch = [];
        private float[] m_raw = [];
        private float[] m_land = [];


        private Int64 m_samples_per_revolution;

        // RESULTS
        private float m_computed_filling;
        private Int64 m_computed_samples;
        private float m_min_land;
        private float m_min_depth;
        private float m_max_depth;


        public Simulator()
        {
            vinyl_start = 10; // mm
            vinyl_stop = 2; // mm

            stylus_width = 70; // um
            stylus_angle = 45; // um

            // Kissing groove best value to have 0-depth on out-of-phase 0dBFS signal
            groove_fullscale = (int)Math.Ceiling(Math.Cos(stylus_angle * Math.PI / 180.0) * (stylus_width)) - 1; // um

            spin_speed = (float)(100.0/180.0); // 33.3rpm in seconds
            land = 10; // um

            read_config();
        }

        public void process(float[] a_data, int a_nb_samples, int a_nb_channels)
        {
            m_groove = new float[2 * a_nb_samples];
            m_pitch = new float[a_nb_samples];
            m_raw = new float[a_nb_samples];
            m_land = new float[a_nb_samples];

            compute_groove(a_data, a_nb_samples, a_nb_channels);
            compute_pitch(a_nb_samples);
            compute_land(a_nb_samples);

            // Write data to file so python can plot it
            export_results(a_data, a_nb_samples);
        }

        private void compute_groove(float[] a_data, int a_nb_samples, int a_nb_channels)
        {
            double stylus_radian = stylus_angle * Math.PI / 180.0;
            float modulation = (float)Math.Sin(stylus_radian) * groove_fullscale;
            double depth_ratio = 1.0 / (2.0 * Math.Tan(stylus_radian / 2.0));
            float depth = stylus_width * (float)depth_ratio;
            m_min_depth = depth;
            m_max_depth = depth;

            if (a_nb_channels == 2)
            {
                // compute inner/outer groove and depth from L/R
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    m_groove[2 * i + 0] = a_data[2 * i + 0] * modulation - stylus_width / 2;
                    m_groove[2 * i + 1] = a_data[2 * i + 1] * modulation + stylus_width / 2;
                    depth = (m_groove[2 * i + 1] - m_groove[2 * i + 0]) * (float)depth_ratio;

                    m_min_depth = Math.Min(m_min_depth, depth);
                    m_max_depth = Math.Max(m_max_depth, depth);
                }
            }
            else
            {
                // mono : constant centered width and depth
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    m_groove[2 * i + 0] = a_data[i] * modulation - stylus_width / 2;
                    m_groove[2 * i + 1] = a_data[i] * modulation + stylus_width / 2;
                }
            }
        }

        private void compute_pitch(int a_nb_samples)
        {
            float r_start = vinyl_start * 1000;
            float r_stop = vinyl_stop * 1000;

            float current_pitch = 0;
            float current_rev, prev_rev;
            double the_pitch = 0, dy = 0;

            Int64 idx = 0, idx_reset = 0;


            while (((r_start - current_pitch) > r_stop) && (idx < a_nb_samples - 1))
            {
                if (m_samples_per_revolution + idx < a_nb_samples - 1)
                {
                    current_rev = current_pitch + m_groove[2 * (m_samples_per_revolution + idx)];
                    prev_rev = m_pitch[idx] + m_groove[2 * idx + 1];
                }
                else
                {
                    current_rev = current_pitch - stylus_width / 2;
                    prev_rev = m_pitch[idx] + m_groove[2 * idx + 1];
                }

                // Compare previous inner and current outer groove to increase pitch if needed
                // margin = (current outer) - (previous inner)
                float margin = current_rev - prev_rev;
                if (margin < land) // if next groove if too close to the prev
                {
                    current_pitch += (land - margin); // add what's missing

                    // interpolate if line from here to current_pitch is greater than actual target
                    if (current_pitch - (m_pitch[idx] + dy * m_samples_per_revolution) >= 0)
                    {
                        idx_reset = 2 * m_samples_per_revolution + idx + 1; // save the idx
                    }

                    dy = Math.Max(dy, (current_pitch - m_pitch[idx]) / m_samples_per_revolution);

                }
                if (idx > idx_reset)
                {
                    dy =  (current_pitch - m_pitch[idx]) / m_samples_per_revolution;
                    idx_reset = m_samples_per_revolution + idx + 1; // keep at least that dy for the next revolution
                }
                if (m_samples_per_revolution + idx < a_nb_samples - 1)
                {
                    m_pitch[m_samples_per_revolution + idx] = current_pitch;
                    m_raw[m_samples_per_revolution + idx] = current_pitch;
                }

                // Increment the pitch control
                the_pitch += dy; // Cumulated value MUST be double precision
                m_pitch[idx + 1] = (float)the_pitch;

                ++idx;
            }
            m_computed_samples = idx;
            m_computed_filling = (current_pitch / (r_start - r_stop));

            if (m_computed_samples < a_nb_samples - 1)
            {
                Debug.WriteLine("Not all sample fitted into the vinyl.\n");
            }
        }

        private void compute_land(int a_nb_samples)
        {
            float r_start = vinyl_start * 1000;
            Int64 idx = 0;

            Int64 window_len = Math.Min(m_computed_samples, a_nb_samples - m_samples_per_revolution);

            float current_pitch;
            float current_rev, prev_rev;

            m_min_land = r_start;
            float land;

            while (idx < window_len - 1)
            {
                current_pitch = m_pitch[idx + m_samples_per_revolution];
                current_rev = current_pitch + m_groove[2 * (idx + m_samples_per_revolution)];
                prev_rev = m_pitch[idx] + m_groove[2 * idx + 1];
                land = current_rev - prev_rev;

                m_land[idx] = land;
                m_min_land = Math.Min(m_min_land, land);
                ++idx;
            }
        }

        public double[][] get_groove_section(Int64 a_data_index, Int64 a_data_size)
        {
            double[][] section = new double[a_data_size][];
            double outer_groove, inner_groove;
            double r_start = vinyl_start * 1000;

            if (a_data_index >= 0)
            {
                for (Int64 idx = a_data_index; idx < a_data_index + a_data_size; ++idx)
                {
                    outer_groove = r_start - m_pitch[idx] - m_groove[2 * idx];
                    inner_groove = r_start - m_pitch[idx] - m_groove[2 * idx + 1];
                    section[idx - a_data_index] = [outer_groove, inner_groove];
                }
            }
            else
            {
                for (Int64 idx = 0; idx < a_data_size; ++idx)
                {
                    section[idx] = [Double.NaN, Double.NaN];
                }
            }

            return section;
        }

        public float get_minimal_land()
        {
            return m_min_land;
        }
        public float get_surface_filling()
        {
            return m_computed_filling;
        }
        public float get_minimal_depth()
        {
            return m_min_depth;
        }
        public float get_maximal_depth()
        {
            return m_max_depth;
        }

        public void set_samplerate(int a_samplerate)
        {
            // Because spin speed is constant, a rotation at every radius has the exact same duration
            // Ex : 33.33rpm => 0.03min/revolution = 1.8sec/revolution
            // -> each revolution lasts (1/spin_speed) seconds so has (a_samplerate / spin_speed) samples
            m_samples_per_revolution = (Int64)(a_samplerate / spin_speed) + 1;
        }
        public Int64 get_revolution_size()
        {
            return m_samples_per_revolution;
        }

        private void export_results(float[] a_data, int a_nb_samples)
        {
            float r_start = vinyl_start * 1000;
            //float current_inner, current_outer, prev_inner, prev_outer;
            //Int64 current_idx, prev_idx;
            float polar_idx = 0;

            using StreamWriter outputFile = new(WORKSPACE + "Python\\pitch.data");
            // First line is initial revolution len
            outputFile.WriteLine(r_start + " " + m_samples_per_revolution);

            for (Int64 idx = 0; idx < a_nb_samples; ++idx)
            {
                outputFile.WriteLine(a_data[2 * idx] + " " + a_data[2 * idx + 1] + " "
                                    + m_pitch[idx] + " " + m_groove[2 * idx] + " " + m_groove[2 * idx + 1] + " "
                                    + m_raw[idx] + " " + polar_idx + " " + m_land[idx]);

                polar_idx += (float)(1.0 / m_samples_per_revolution) * (float)(2.0 * Math.PI);

            }
            /*
            for (Int64 i = 0; i < a_nb_samples - m_samples_per_revolution; ++i)
            {
                prev_idx = i;
                current_idx = i + m_samples_per_revolution;
                current_outer = r_start - m_pitch[current_idx] - m_groove[2 * current_idx + 0];
                current_inner = r_start - m_pitch[current_idx] - m_groove[2 * current_idx + 1];
                prev_inner = r_start - m_pitch[prev_idx] - m_groove[2 * prev_idx + 0];
                prev_outer = r_start - m_pitch[prev_idx] - m_groove[2 * prev_idx + 1];
                outputFile.WriteLine(a_data[2 * i] + " " + a_data[2 * i + 1] + " " + current_outer + " "+ current_inner + " " + prev_inner + " " + prev_outer);
            }
            */
        }

        public void render_vinyl_view() { 
            // Execute Python script that will read the file
            Process python = new();
            python.StartInfo.FileName = @"python ";
            python.StartInfo.Arguments = WORKSPACE + "Python\\plot.py";
            python.StartInfo.UseShellExecute = false;
            python.StartInfo.RedirectStandardOutput = false;
            python.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            python.StartInfo.CreateNoWindow = true; //not diplay a windows
            python.Start();
            python.WaitForExit();
        }

        public void clear_results()
        {
            File.Delete(WORKSPACE + "Python\\pitch.data");
        }

        private void read_config()
        {
            try
            {
                // read config setting from USER_SETTINGS file
                string[] lines = File.ReadAllLines(WORKSPACE+"config.txt");

                foreach (string line in lines)
                {
                    int index = line.IndexOf('=');

                    if (index > 0) // if "=" is in the line
                    {
                        string key = line[..index];
                        string value = line[(index + 1)..];

                        switch (key)
                        {
                            case "r_start":
                                vinyl_start = int.Parse(value);
                                break;
                            case "r_stop":
                                vinyl_stop = int.Parse(value);
                                break;
                            case "groove_fullscale":
                                groove_fullscale = int.Parse(value);
                                break;
                            case "stylus_width":
                                stylus_width = int.Parse(value);
                                break;
                            case "stylus_angle":
                                stylus_angle = int.Parse(value);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERR CONFIG : Config file could not be parsed : \n" + e.Message + "\n Please check USER_SETTINGS file\n");
            }
        }
    }

}
