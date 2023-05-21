using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;


namespace Project
{
    public partial class OP : Form
    {
        private Bitmap originalImage;

        public OP()
        {
            InitializeComponent();
        }

        private double[] ConvertImageToSignal(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;
            double[] signal = new double[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    double intensity = (pixelColor.R + pixelColor.G + pixelColor.B) / 3.0; // Assuming grayscale intensity
                    signal[y * width + x] = intensity;
                }
            }

            return signal;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(openFileDialog.FileName);
                pictureBox1.Image = originalImage;
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Perform DFT operation on the signal
            double[] transformedSignal = PerformDFT(originalImage);

            // Create and show the ChartForm to display the transformed signal
            Form2 chartForm = new Form2(transformedSignal);
            chartForm.ShowDialog();
        }
       
        private void button3_Click(object sender, EventArgs e)
        {

            // Perform Fourier transform on the signal
            double[] transformedSignal = PerformFT(originalImage);

            // Create and show the ChartForm to display the transformed signal
            Form3 chartForm = new Form3(transformedSignal);
            chartForm.Show();
        }

        private double[] PerformFT(Bitmap image)
        {
            // Convert the image to a 1D signal array
            double[] signal = ConvertImageToSignal(image);

            // Perform FT operation on the signal
            double[] transformedSignal = YourFTFunction(signal);

            return transformedSignal; // Return the transformed signal
        }

        private double[] YourFTFunction(double[] signal)
        {
            int N = signal.Length;
            double[] transformedSignal = new double[N];

            for (int k = 0; k < N; k++)
            {
                Complex sum = Complex.Zero;

                for (int n = 0; n < N; n++)
                {
                    double theta = -2 * Math.PI * k * n / N;
                    Complex phasor = Complex.Exp(Complex.ImaginaryOne * theta);
                    sum += signal[n] * phasor;
                }
                transformedSignal[k] = sum.Real; // Store the real part of the complex value
            }

            return transformedSignal; // Return the transformed signal
        }

       

        private double[] PerformDFT(Bitmap image)
        {
            // Convert the image to a 1D signal array
            double[] signal = ConvertImageToSignal(image);

            // Perform DFT operation on the signal
            double[] transformedSignal = YourDFTFunction(signal);

            return transformedSignal; // Return the transformed signal
        }

        private double[] YourDFTFunction(double[] signal)
        {
            int N = signal.Length;
            double[] transformedSignal = new double[N];

            for (int k = 0; k < N; k++)
            {
                Complex sum = Complex.Zero;

                for (int n = 0; n < N; n++)
                {
                    double theta = -2 * Math.PI * k * n / N;
                    Complex phasor = Complex.Exp(Complex.ImaginaryOne * theta);
                    sum += signal[n] * phasor;
                }

                transformedSignal[k] = sum.Magnitude; // Store the magnitude of the complex value
            }

            return transformedSignal; // Return the transformed signal
        }

        
    }
}