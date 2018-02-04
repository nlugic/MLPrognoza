using MLPrognoza.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        private void btnStartDownload_Click(object sender, EventArgs e)
        {
            FileDownloader.DownloadWeatherData(nudYear.Value.ToString(), pbDownload);
        }
    }
}
