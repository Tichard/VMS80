namespace VMS80
{
    internal partial class PlotForm : Form
    {
        private readonly Simulator m_simulator;

        private int m_subsection_div;
        private int m_subsection_index;

        public PlotForm(Simulator a_simulator)
        {
            InitializeComponent();

            m_simulator = a_simulator;

            m_subsection_div = 1;
            m_subsection_index = 0;
            textBoxZoom.Text = m_subsection_div.ToString("x 0");

            plot();
        }

        private void plot()
        {
            Int64 data_size = m_simulator.get_revolution_size() / m_subsection_div;
            Int64 data_index = data_size * m_subsection_index;

            m_simulator.get_data(data_index, data_size);
        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            if (m_subsection_div <= 1)
                return;

            m_subsection_div /= 2;
            m_subsection_index /= 2;
            textBoxZoom.Text = m_subsection_div.ToString("x 0");

            plot();
        }

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            if (m_subsection_div >= 32)
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
            Cursor.Current = Cursors.WaitCursor;

            m_simulator.render_vinyl_view();

            // Restore cursor
            Cursor.Current = Cursors.Default;
        }
    }
}
