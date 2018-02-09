using MLPrognoza.Data;
using MLPrognoza.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MLPrognoza.NN;
using Accord.Controls;
using Accord.Math;

namespace MLPrognoza
{
    public partial class formGlavna : Form
    {

        private int[] nodeCounts;
        private ICollection<WeatherModel> weatherModelData;

        private NeuralNetwork network;
        private Chart chart;
        private double[,] realTemparatures; 

        public formGlavna()
        {
            InitializeComponent();

            nodeCounts = new int[(int)nudHiddenLayers.Value + 1];
            nodeCounts[nodeCounts.Length - 1] = 1;
            nodeCounts[0] = nodeCounts[1] = nodeCounts[2] = 20;
            weatherModelData = null;

            chart = new Chart();
            gbDataChart.Controls.Add(chart);
            chart.AddDataSeries("data", Color.Red, Chart.SeriesType.Line, 1);
            chart.AddDataSeries("solution", Color.Blue, Chart.SeriesType.Line, 1);
            
            chart.Location = new Point(10, 20);
            chart.Width = gbDataChart.Width - 20;
            chart.Height = gbDataChart.Height - 30;
        }

        private void formGlavna_Load(object sender, EventArgs e)
        {
            IEnumerable<WeatherStation> cbLocationData = WeatherData.GetWeatherStationData("../../isd-history.csv", "RI");
            cbLocation.DataSource = cbLocationData;
            cbLocation.DisplayMember = "Name";
        }

        private void btnStartDownload_Click(object sender, EventArgs e)
        {
            WeatherStation selectedStation = cbLocation.SelectedValue as WeatherStation;
            string stationName = selectedStation.USAF.ToString() + '-' + selectedStation.WBAN.ToString();

            ICollection<string> downloadedFiles = FileDownloader.DownloadWeatherData((int)nudYearStart.Value, (int)nudYearEnd.Value, stationName, pbDownload);

            Thread worker = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                
                weatherModelData = WeatherData.GetWeatherModelData(downloadedFiles);
            });

            worker.Start();
            worker.Join();

            pbDownload.Value = 0;
            gbSettings.Enabled = true;

            realTemparatures = new double[weatherModelData.Count, 2];
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocation.SelectedItem != null)
            {
                WeatherStation selectedStation = cbLocation.SelectedItem as WeatherStation;
                nudYearStart.Minimum = selectedStation.BeginDate.Year;
                nudYearStart.Maximum = selectedStation.EndDate.Year;
                nudYearStart.Enabled = true;
                nudYearEnd.Minimum = selectedStation.BeginDate.Year;
                nudYearEnd.Maximum = selectedStation.EndDate.Year;
                nudYearEnd.Enabled = true;
                btnStartDownload.Enabled = true;
            }
            else
            {
                nudYearStart.Enabled = false;
                nudYearEnd.Enabled = false;
                btnStartDownload.Enabled = false;
            }
        }

        private void nudYearStart_ValueChanged(object sender, EventArgs e)
        {
            nudYearEnd.Minimum = nudYearStart.Value;
        }

        private void nudYearEnd_ValueChanged(object sender, EventArgs e)
        {
            nudYearStart.Maximum = nudYearEnd.Value;
        }

        private void btnEditLayers_Click(object sender, EventArgs e)
        {
            formSlojeviMreze frm = new formSlojeviMreze((int)nudHiddenLayers.Value);

            if (frm.ShowDialog() == DialogResult.Yes)
                nodeCounts = frm.NodeCounts;
        }

        private void nudHiddenLayers_ValueChanged(object sender, EventArgs e)
        {
            nodeCounts = new int[(int)nudHiddenLayers.Value + 1];
            nodeCounts[nodeCounts.Length - 1] = 1;
        }
        
        private void btnStartLearning_Click(object sender, EventArgs e)
        {
            network = new NeuralNetwork(0, nodeCounts, 
                rbBernoulli.Checked ? FunctionType.BERNOULLI : FunctionType.GAUSSIAN,
                (double)nudSigmoidValue.Value);
            network.initData(weatherModelData.ToList());

            pbLearning.Minimum = 0;
            pbLearning.Maximum = (int)nudIterations.Value + 1;
            for (int i = 0; i < weatherModelData.Count; i++)
            {
                realTemparatures[i, 1] = network.outputs[i][0].Scale(-1, 1, -100, 100); 
                realTemparatures[i, 0] = i;
            }
            Accord.Range rn = new Accord.Range(-100, 100);
            chart.RangeY = new Accord.Range(rn.Min, rn.Max);
            chart.RangeX = new Accord.Range(0, realTemparatures.Length / 2);
            network.Train((int)nudIterations.Value, epochEvent, FinishLearning);

        }

        public void epochEvent(double[,] epoch, double error)
        {
            chart.UpdateDataSeries("data", realTemparatures);
            chart.UpdateDataSeries("solution", epoch);
            chart.Update();
            lblLearninngData.Text = "Iteracija: " + (pbLearning.Value + 1) + " Odstupanje: " + error.ToString();
            lblLearninngData.Update();
            pbLearning.Value++;
        }

        public void FinishLearning()
        {
            pbLearning.Value = 0;
        }
    }
}
