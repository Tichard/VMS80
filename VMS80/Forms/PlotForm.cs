using System.Windows.Forms.DataVisualization.Charting;

namespace VMS80
{
    internal partial class PlotForm : Form
    {
        private readonly Simulator m_simulator;

        private int m_subsection_div;
        private int m_subsection_index;

        private readonly Series m_groove_prev_outer, m_groove_prev_inner;
        private readonly Series m_groove_current_outer, m_groove_current_inner;
        private readonly Series m_groove_next_outer, m_groove_next_inner;
        private readonly ChartArea m_groove_area;

        private readonly int max_zoom = 64;
        private readonly int min_zoom = 1;

        public PlotForm(Simulator a_simulator)
        {
            InitializeComponent();

            m_simulator = a_simulator;

            m_subsection_div = 1;
            m_subsection_index = 0;
            textBoxZoom.Text = m_subsection_div.ToString("x 0");

            // Init Series
            m_groove_prev_outer = new Series
            {
                Name = "Previous outer",
                ChartArea = "GroovePlot",
                Color = Color.Green,
                ChartType = SeriesChartType.Line,
            };
            chartGroove.Series.Add(m_groove_prev_outer);
            m_groove_prev_inner = new Series
            {
                Name = "Previous inner",
                ChartArea = "GroovePlot",
                Color = Color.Green,
                ChartType = SeriesChartType.Line,
            };
            chartGroove.Series.Add(m_groove_prev_inner);

            m_groove_current_outer = new Series
            {
                Name = "Current outer",
                ChartArea = "GroovePlot",
                Color = Color.Blue,
                ChartType = SeriesChartType.Line,
            };
            chartGroove.Series.Add(m_groove_current_outer);
            m_groove_current_inner = new Series
            {
                Name = "Current inner",
                ChartArea = "GroovePlot",
                Color = Color.Blue,
                ChartType = SeriesChartType.Line,
            };
            chartGroove.Series.Add(m_groove_current_inner);

            m_groove_next_outer = new Series
            {
                Name = "Next outer",
                ChartArea = "GroovePlot",
                Color = Color.Red,
                ChartType = SeriesChartType.Line,
            };
            chartGroove.Series.Add(m_groove_next_outer);
            m_groove_next_inner = new Series
            {
                Name = "Next inner",
                ChartArea = "GroovePlot",
                Color = Color.Red,
                ChartType = SeriesChartType.Line,
            };
            chartGroove.Series.Add(m_groove_next_inner);

            m_groove_area = new ChartArea
            {
                Name = "GroovePlot",
                AxisY = { IsStartedFromZero = false }
            };
            chartGroove.ChartAreas.Add(m_groove_area);

            plot();
        }

        private void plot()
        {
            // Set cursor as waiting
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            Int64 revolution_size = m_simulator.get_revolution_size();
            Int64 data_size = revolution_size / (m_subsection_div * 4); // quarter revolution
            Int64 data_index = data_size * m_subsection_index;

            m_groove_prev_outer.Points.Clear();
            m_groove_prev_inner.Points.Clear();
            m_groove_current_outer.Points.Clear();
            m_groove_current_inner.Points.Clear();
            m_groove_next_outer.Points.Clear();
            m_groove_next_inner.Points.Clear();

            double[][] prev_goove = m_simulator.get_groove_section(data_index - revolution_size, data_size);
            double[][] current_goove = m_simulator.get_groove_section(data_index, data_size);
            double[][] next_goove = m_simulator.get_groove_section(data_index + revolution_size, data_size);

            for (Int64 i = 0; i < data_size; ++i)
            {
                Int64 x = data_index + i;
                m_groove_prev_outer.Points.AddXY(x, prev_goove[i][0]);
                m_groove_prev_inner.Points.AddXY(x, prev_goove[i][1]);

                m_groove_current_outer.Points.AddXY(x, current_goove[i][0]);
                m_groove_current_inner.Points.AddXY(x, current_goove[i][1]);

                m_groove_next_outer.Points.AddXY(x, next_goove[i][0]);
                m_groove_next_inner.Points.AddXY(x, next_goove[i][1]);
            }

            m_groove_area.RecalculateAxesScale();

            chartGroove.Show();

            // Restore cursor
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            if (m_subsection_div <= min_zoom)
                return;

            m_subsection_div /= 2;
            m_subsection_index /= 2;
            textBoxZoom.Text = m_subsection_div.ToString("x 0");

            plot();
        }

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            if (m_subsection_div >= max_zoom)
                return;

            m_subsection_div *= 2;
            m_subsection_index *= 2;
            textBoxZoom.Text = m_subsection_div.ToString("x 0");

            plot();
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            m_subsection_index -= 1;
            plot();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            m_subsection_index += 1;
            plot();
        }

        private void buttonRender_Click(object sender, EventArgs e)
        {
            // Set cursor as waiting
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            m_simulator.render_vinyl_view();

            // Restore cursor
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }
    }
}
