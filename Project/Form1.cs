using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        double x, y;

        public OP()
        {
            InitializeComponent();
        }

        private double[] ConvertImageToSignal(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;
            double[] signal = new double[width * height];

            BitmapData bmpData = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            IntPtr ptr = bmpData.Scan0;

            unsafe
            {
                byte* bytes = (byte*)ptr;

                for (int y = 0; y < height; y++)
                {
                    byte* row = bytes + (y * bmpData.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        int offset = x * 3; // Assuming 24bpp RGB format

                        double intensity = (row[offset] + row[offset + 1] + row[offset + 2]) / 3.0; // Assuming grayscale intensity
                        signal[y * width + x] = intensity;
                    }
                }
            }

            image.UnlockBits(bmpData);

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
            double[] signal = ConvertImageToSignal(originalImage);
            double[] transformedSignal = PerformDTFT(signal);

            // Convert the complex values to magnitudes
            double[] magnitudes = new double[transformedSignal.Length];
            for (int i = 0; i < transformedSignal.Length; i++)
            {
                magnitudes[i] = transformedSignal[i].Magnitude();
            }

            // Create an instance of the ChartForm and pass the magnitudes data
            Form2 chartForm = new Form2(magnitudes);
            chartForm.ShowDialog();
        }

        private double[] PerformDTFT(double[] signal)
        {
            int N = signal.Length;
            Complex32[] complexSignal = new Complex32[N];

            // Convert the signal to complex values
            for (int i = 0; i < N; i++)
            {
                complexSignal[i] = new Complex32((float)signal[i], 0);
            }

            // Perform DTFT on the complex signal using FFT
            Fourier.Forward(complexSignal, FourierOptions.NoScaling);

            // Calculate the magnitude spectrum
            double[] magnitudeSpectrum = complexSignal.Select(c => (double)c.Magnitude).ToArray();

            return magnitudeSpectrum;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double[] transformedSignal = PerformFT(originalImage);

            // Create and show the ChartForm to display the transformed signal
            Form3 chartForm = new Form3(transformedSignal);
            chartForm.Show();
        }



        private double[] PerformFT(Bitmap image)
        {
            // Convert the image to a 1D signal array
            double[] signal = ConvertImageToSignal(image);

            // Perform the Fourier transform on the signal using FFT
            Complex32[] complexSignal = new Complex32[signal.Length];
            for (int i = 0; i < signal.Length; i++)
            {
                complexSignal[i] = new Complex32((float)signal[i], 0);
            }
            Fourier.Forward(complexSignal, FourierOptions.Default);

            return complexSignal.Select(c => (double)c.Magnitude).ToArray();
        }
    }
}