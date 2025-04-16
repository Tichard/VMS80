using System.Diagnostics;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System;
using IronPython;
using Microsoft.Scripting;
using Windows.Web.AtomPub;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;


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

        private double spin_speed;
        private int land;
        private double gain;

        private int m_samplerate;

        private double m_computed_filling;
        private Int64 m_computed_samples;
        private double m_min_land;

        private string WORKSPACE = "Z:\\git\\VMS80\\VMS80\\";

        public Simulator()
        {
            vinyl_start = 10; // mm
            vinyl_stop = 2; // mm

            stylus_width = 70; // um
            stylus_angle = 45; // um
            groove_fullscale = 10; // um

            spin_speed = 100.0/180.0; // 33.3rpm in seconds
            land = 10; // um
            gain = 1;

            m_samplerate = 48000;

            read_config();
        }

        public void compute_groove(double[] a_groove, double[] a_data, int a_nb_samples, int a_nb_channels)
        {
            double modulation = Math.Sin(stylus_angle) * groove_fullscale;
            if (a_nb_channels == 2)
            {
                // compute inner and outer from L/R
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    a_groove[2 * i + 0] = a_data[2 * i + 0] * modulation - stylus_width / 2;
                    a_groove[2 * i + 1] = a_data[2 * i + 1] * modulation + stylus_width / 2;
                }
            }
            else
            {
                // mono or unknown : constant centered width
                for (int i = 0; i < a_nb_samples; ++i)
                {
                    a_groove[2 * i + 0] = a_data[i] * modulation - stylus_width / 2;
                    a_groove[2 * i + 1] = a_data[i] * modulation + stylus_width / 2;
                }
            }
        }

        public void compute_pitch(double[] a_pitch, double[] a_raw, double[] a_groove, int a_nb_samples)
        {
            double r_start = vinyl_start * 1000;
            double r_stop = vinyl_stop * 1000;
            double r;

            double revolution;
            Int64 revolution_len;

            double current_pitch = 0;
            double peak = 0;
            double peak_pitch = 0;

            Int64 idx = 0;
            Int64 peak_idx = 1;

            double current_rev = 0;
            double prev_rev = 0;
            double delta = 0;
            double last_delta = 0;

            while (((r_start - current_pitch) > r_stop) && (idx < a_nb_samples - 1))
            {
                r = (r_start - current_pitch) / 1000000.0; // actual pitch radius in meter
                revolution = (2.0 * Math.PI * r); // circular length of a current revolution
                revolution_len = (Int64)Math.Round((revolution / (spin_speed * r)) * (double)m_samplerate); // number of sample on the current revolution

                if (revolution_len + idx < a_nb_samples - 1)
                {
                    current_rev = current_pitch + a_groove[2 * (revolution_len + idx)];
                    prev_rev = a_pitch[idx] + a_groove[2 * idx + 1];
                }
                else
                {
                    current_rev = current_pitch - stylus_width / 2;
                    prev_rev = a_pitch[idx] + a_groove[2 * idx + 1];
                }

                // Compare previous inner and current outer groove to increase pitch if needed
                // margin = (current outer) - (previous inner)
                double margin = current_rev - prev_rev;
                if (margin < land) // if next groove if too close to the prev
                {
                    current_pitch += (land - margin); // add what's missing

                    // interpolate if line from here to current_pitch will cross under peak
                    double next_peak = ((current_pitch - a_pitch[idx]) / revolution_len) * (peak_idx - idx) + a_pitch[idx];

                    // Target peak if straight line doesn't go under peak_pitch
                    if (next_peak >= peak_pitch)
                    {
                        peak = current_pitch;
                    }
                    else
                    {
                        // continue in straight line to next peak
                        delta = (peak - a_pitch[idx]) / (peak_idx - idx);
                        // if derivative is bigger, overwrite pitch
                        if (delta > last_delta)
                        {
                            Int64 end = Math.Min(peak_idx, a_nb_samples - 1);
                            for (int i = 0; i <= (end - idx); ++i)
                            {
                                a_pitch[idx + i] = Math.Max(a_pitch[idx + i], a_pitch[idx] + i * delta);
                            }
                            last_delta = delta;
                        }
                    }

                    peak_pitch = current_pitch;
                    peak_idx = idx + revolution_len;

                }
                if (revolution_len + idx < a_nb_samples - 1)
                {
                    a_pitch[revolution_len + idx] = current_pitch;
                    a_raw[revolution_len + idx] = current_pitch;
                }

                // Smooth the pitch control
                delta = peak - a_pitch[idx];
                a_pitch[idx + 1] = Math.Max(a_pitch[idx + 1], a_pitch[idx] + delta / (peak_idx - idx));

                ++idx;
            }
            m_computed_samples = idx;
            m_computed_filling = (current_pitch / (r_start - r_stop));

            if (m_computed_samples < a_nb_samples - 1)
            {
                Debug.WriteLine("Not all sample fitted into the vinyl.\n");
            }
        }

        public void compute_land(double[] a_land, double[] a_pitch, double[] a_groove, int a_nb_samples)
        {
            double r_start = vinyl_start * 1000;
            double r_stop = vinyl_stop * 1000;
            double r;

            r = r_start / 1000000.0; // actual pitch radius in meter
            double revolution = (2.0 * Math.PI * r); // circular length of a current revolution
            Int64 revolution_len = (Int64)Math.Round((revolution / (spin_speed * r)) * (double)m_samplerate); // number of sample on the current revolution
            Int64 idx = 0;

            Int64 window_len = Math.Min(m_computed_samples, a_nb_samples - revolution_len);

            double current_pitch = 0;
            double current_rev = 0;
            double prev_rev = 0;

            m_min_land = r_start;
            double land;

            while (idx < window_len - 1)
            {
                r = (r_start - current_pitch) / 1000000.0; // actual pitch radius in meter
                revolution = (2.0 * Math.PI * r); // circular length of a current revolution
                revolution_len = (Int64)Math.Round((revolution / (spin_speed * r)) * (double)m_samplerate); // number of sample on the current revolution

                current_pitch = a_pitch[idx + revolution_len];
                current_rev = current_pitch + a_groove[2 * (idx + revolution_len)];
                prev_rev = a_pitch[idx] + a_groove[2 * idx + 1];
                land = current_rev - prev_rev;

                a_land[idx] = land;
                m_min_land = Math.Min(m_min_land, land);
                ++idx;
            }
        }

        public double get_minimal_land()
        {
            return m_min_land;
        }
        public double get_surface_filling()
        {
            return m_computed_filling;
        }

        public void set_samplerate(int a_samplerate)
        {
            m_samplerate = a_samplerate;
        }


        public void export_to_python(double[] a_data, double[] a_pitch, double[] a_groove, double[] a_raw, double[] a_land, int a_nb_samples)
        {
            double r_start = vinyl_start * 1000;
            //double current_inner, current_outer, prev_inner, prev_outer;
            //Int64 current_idx, prev_idx;
            double polar_idx = 0;

            double r = r_start / 1000000.0; // actual pitch radius in meter
            double revolution = (2.0 * Math.PI * r); // circular length of a current revolution
            Int64 revolution_len = (Int64)Math.Round((revolution / (spin_speed * r)) * (double)m_samplerate); // number of sample on the current revolution
            double current_pitch = 0;

            using (StreamWriter outputFile = new StreamWriter(WORKSPACE + "Python\\picth.data"))
            {
                // First line is initial revolution len
                outputFile.WriteLine(r_start + " " + revolution_len);

                for (Int64 idx = 0; idx < a_nb_samples; ++idx)
                {
                    current_pitch = a_pitch[idx];
                    r = (r_start - current_pitch) / 1000000.0; // actual pitch radius in meter
                    revolution = (2.0 * Math.PI * r); // circular length of a current revolution
                    revolution_len = (Int64)Math.Round((revolution / (spin_speed * r)) * (double)m_samplerate); // number of sample on the current revolution

                    outputFile.WriteLine(a_data[2 * idx] + " " + a_data[2 * idx + 1] + " "
                                        + a_pitch[idx] + " " + a_groove[2 * idx] + " " + a_groove[2 * idx + 1] + " "
                                        + a_raw[idx] + " " + polar_idx + " " + a_land[idx]);

                    polar_idx = polar_idx + (1.0 / revolution_len) * (2.0 * Math.PI);

                }
                /*
                for (Int64 i = 0; i < a_nb_samples - revolution_len; ++i)
                {
                    prev_idx = i;
                    current_idx = i + revolution_len;
                    current_outer = r_start - a_pitch[current_idx] - a_groove[2 * current_idx + 0];
                    current_inner = r_start - a_pitch[current_idx] - a_groove[2 * current_idx + 1];
                    prev_inner = r_start - a_pitch[prev_idx] - a_groove[2 * prev_idx + 0];
                    prev_outer = r_start - a_pitch[prev_idx] - a_groove[2 * prev_idx + 1];
                    outputFile.WriteLine(a_data[2 * i] + " " + a_data[2 * i + 1] + " " + current_outer + " "+ current_inner + " " + prev_inner + " " + prev_outer);
                }
                */
            }
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
                        string key = line.Substring(0, index - 1);
                        string value = line.Substring(index + 1);

                        switch (key)
                        {
                            case "r_start":
                                vinyl_start = int.Parse(value);
                                Console.WriteLine(vinyl_start);
                                break;
                            case "r_stop":
                                vinyl_stop = int.Parse(value);
                                Console.WriteLine(vinyl_stop);
                                break;
                            case "groove_fullscale":
                                groove_fullscale = int.Parse(value);
                                Console.WriteLine(groove_fullscale);
                                break;
                            case "stylus_width":
                                stylus_width = int.Parse(value);
                                Console.WriteLine(stylus_width);
                                break;
                            case "stylus_angle":
                                stylus_angle = int.Parse(value);
                                Console.WriteLine(stylus_angle);
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
