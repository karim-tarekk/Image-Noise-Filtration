using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZGraphTools;

namespace ImageFilters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[,] ImageMatrix;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
        }

        private void btnZGraph_Click(object sender, EventArgs e)
        {
            int windowsize;
            windowsize = int.Parse(textBox1.Text);
            double[] AlphaX = new double[windowsize / 2];//X-axis for Window size
            double[] AlphaYC = new double[windowsize / 2];//Y-axis for Time of each window
            double[] AlphaYH = new double[windowsize / 2];//Y-axis for Time of each window
            int counter = 3;
            int index = 0;
            while (counter <= windowsize)//For Counting Sort
            {
                int Start = System.Environment.TickCount;
                ImageOperations.SelectFilter(ImageMatrix, windowsize, 0, 1, 1);
                int End = System.Environment.TickCount;
                double Time = End - Start;
                Time /= 1000;
                AlphaX[index] = counter;
                AlphaYC[index] = Time;
                counter += 2;
                index++;
            }
            counter = 3;
            index = 0;
            while (counter <= windowsize)//For Heap Sort
            {
                int Start = System.Environment.TickCount;
                ImageOperations.SelectFilter(ImageMatrix, windowsize, 0, 2, 1);
                int End = System.Environment.TickCount;
                double Time = End - Start;
                Time /= 1000;
                AlphaYH[index] = Time;
                counter += 2;
                index++;
            }
            double[] AdaptiveX = new double[windowsize / 2];//X-axis for Window size
            double[] AdaptiveYC = new double[windowsize / 2];//Y-axis for Time of each window
            double[] AdaptiveYQ = new double[windowsize / 2];//Y-axis for Time of each window
            counter = 3;
            index = 0;
            while (counter <= windowsize)//For Counting Sort
            {
                int Start = System.Environment.TickCount;
                ImageOperations.SelectFilter(ImageMatrix, windowsize, 0, 1, 2);
                int End = System.Environment.TickCount;
                double Time = End - Start;
                Time /= 1000;
                AdaptiveX[index] = counter;
                AdaptiveYC[index] = Time;
                counter += 2;
                index++;
            }
            counter = 3;
            index = 0;
            while (counter <= windowsize)//For Quick Sort
            {
                int Start = System.Environment.TickCount;
                ImageOperations.SelectFilter(ImageMatrix, windowsize, 0, 2, 2);
                int End = System.Environment.TickCount;
                double Time = End - Start;
                Time /= 1000;
                AdaptiveYQ[index] = Time;
                counter += 2;
                index++;
            }
            ZGraphForm alpha = new ZGraphForm("Alpha-Trim Graph", "Window Size", "Time in Seconds");
            alpha.add_curve("Counting Sort", AlphaX, AlphaYC, Color.Red);
            alpha.add_curve("Heap Sort", AlphaX, AlphaYH, Color.Blue);
            alpha.Show();


            ZGraphForm adaptive = new ZGraphForm("Adaptive  Median Graph", "Window Size", "Time in Seconds");
            adaptive.add_curve("Counting Sort", AdaptiveX, AdaptiveYC, Color.Red);
            adaptive.add_curve("Quick Sort", AdaptiveX, AdaptiveYQ, Color.Blue);
            adaptive.Show();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            int windowsize, trimNumber, filtertype;
            //(byte[,] ImageMatrix, int n, int t, int SortType, int FilterType)
            if (comboBox1.SelectedIndex.Equals(0))
            {
                windowsize = int.Parse(textBox1.Text);
                trimNumber = int.Parse(textBox2.Text);
                filtertype = 1;
                if (comboBox2.SelectedIndex.Equals(0))
                {
                    ImageMatrix = ImageOperations.SelectFilter(ImageMatrix, windowsize, trimNumber, 1, filtertype);
                }
                else if (comboBox2.SelectedIndex.Equals(1))
                {
                    ImageMatrix = ImageOperations.SelectFilter(ImageMatrix, windowsize, trimNumber, 2, filtertype);
                }
            }
            else if (comboBox1.SelectedIndex.Equals(1))
            { 
                windowsize = int.Parse(textBox1.Text);
                trimNumber = 0;
                filtertype = 2;
                if (comboBox2.SelectedIndex.Equals(0))
                {
                    ImageMatrix = ImageOperations.SelectFilter(ImageMatrix, windowsize, trimNumber, 1, filtertype);
                }
                else if (comboBox2.SelectedIndex.Equals(1))
                {
                    ImageMatrix = ImageOperations.SelectFilter(ImageMatrix, windowsize, trimNumber, 2, filtertype);
                }
            }
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox1.Items.Add("1. Alpha-Trim Filter");
            comboBox1.Items.Add("2. Adaptive Median Filter");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox2.Items.Clear();
            comboBox2.ResetText();
            if (comboBox1.SelectedIndex.Equals(0))
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                comboBox2.Items.Add("1. Counting Sort");
                comboBox2.Items.Add("2. Heap Sort");
            }
            else if (comboBox1.SelectedIndex.Equals(1))
            {
                textBox1.Enabled = true;
                textBox2.Enabled = false;
                comboBox2.Items.Add("1. Counting Sort");
                comboBox2.Items.Add("2. Quick Sort");
            }
        }
    }
}