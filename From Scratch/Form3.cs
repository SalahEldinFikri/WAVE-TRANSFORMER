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
    public partial class Form3 : Form
    {
        private double[] transformedSignal;

        public Form3(double[] transformedSignal)
        {
            InitializeComponent();
            this.transformedSignal = transformedSignal;
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            chart2.Series.Clear();
            chart2.Series.Add("Transformed Signal");
            chart2.Series["Transformed Signal"].ChartType = SeriesChartType.Line;

            // Adjust the range of the x-axis
            chart2.ChartAreas[0].AxisX.Minimum = 0; // Set the minimum value
            chart2.ChartAreas[0].AxisX.Maximum = 500; // Set the maximum value
            chart2.ChartAreas[0].AxisY.Minimum = 0; // Set the minimum value
            chart2.ChartAreas[0].AxisY.Maximum = 500; // Set the maximum value
            for (int i = 0; i < transformedSignal.Length; i++)
            {
                chart2.Series["Transformed Signal"].Points.AddXY(i, transformedSignal[i]);
            }

            
        }
    }
}
