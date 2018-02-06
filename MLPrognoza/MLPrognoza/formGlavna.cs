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

namespace MLPrognoza
{
    public partial class formGlavna : Form
    {
        public formGlavna()
        {
            InitializeComponent();
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

            ICollection<WeatherModel> weatherModelData = null;

            Thread worker = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                weatherModelData = WeatherData.GetWeatherModelData(downloadedFiles);
            });

            worker.Start();
            worker.Join();

            pbDownload.Value = 0;

            // nastavak obrade podataka tj kalemljenje neuronske mreze
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
    }
}
