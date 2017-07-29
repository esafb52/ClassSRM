using System;
using System.Windows.Forms;

namespace ClassSRM
{
    public partial class History : DevExpress.XtraEditors.XtraForm
    {
        public History()
        {
            InitializeComponent();
        }

        private void History_Load(object sender, EventArgs e)
        {
            txtHistory.Text = Properties.Resources.History;
        }

        private void History_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}