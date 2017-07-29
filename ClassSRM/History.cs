using System;

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
    }
}