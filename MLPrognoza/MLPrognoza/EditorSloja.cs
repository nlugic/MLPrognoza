using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLPrognoza
{
    public partial class EditorSloja : UserControl
    {

        public int NodeCount
        {
            get { return (int)nudLayerNodes.Value; }
        }

        public EditorSloja(int layerIndex)
        {
            InitializeComponent();

            lblLayer.Text += layerIndex + ":";
        }
        
    }
}
