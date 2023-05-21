using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project
{
    public partial class Form2 : Form
    {
        private double[] transformedSignal;

        public Form2(double[] transformedSignal)
        {
            InitializeComponent();
            this.transformedSignal = transformedSignal;

        }

        private void chart1_Click(object sender, EventArgs e)
        {

            chart1.Series.Clear();
            chart1.Series.Add("Transformed Signal");
            chart1.Series["Transformed Signal"].ChartType = SeriesChartType.Line;

            // Adjust the range of the x,y-axis
            chart1.ChartAreas[0].AxisX.Minimum = -10; // Set the minimum value
            chart1.ChartAreas[0].AxisX.Maximum = 10; // Set the maximum value
            chart1.ChartAreas[0].AxisY.Minimum = -10; // Set the minimum value
            chart1.ChartAreas[0].AxisY.Maximum = 10; // Set the maximum value

            for (int i = 0; i < transformedSignal.Length; i++)
            {
                chart1.Series["Transformed Signal"].Points.AddXY(i, transformedSignal[i]);
            }


        }
    }
}
