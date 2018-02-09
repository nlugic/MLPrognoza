
using System.Windows.Forms;

namespace MLPrognoza
{
    public partial class formSlojeviMreze : Form
    {

        private int layersNumber;
        private EditorSloja[] slojevi;

        public int[] NodeCounts
        {
            get
            {
                int[] counts = new int[layersNumber + 1];

                for (int i = 0; i < layersNumber; ++i)
                    counts[i] = slojevi[i].NodeCount;

                counts[layersNumber] = 1;
                return counts;
            }
        }

        public formSlojeviMreze(int numLayers)
        {
            InitializeComponent();

            layersNumber = numLayers;
            GenerateControls(numLayers);
        }

        private void GenerateControls(int layersNumber)
        {
            slojevi = new EditorSloja[layersNumber];
            
            for (int i = 0; i < layersNumber; ++i)
            {
                slojevi[i] = new EditorSloja(i);
                slojevi[i].Left = 60;
                slojevi[i].Top = 45 + 35 * i;
                Controls.Add(slojevi[i]);
                Height += 35;
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Yes;
        }

        private void formSlojeviMreze_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.No;
        }

    }
}
