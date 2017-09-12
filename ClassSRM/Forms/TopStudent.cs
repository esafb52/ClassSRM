using System;
using System.Collections;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class TopStudent : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext dc = new ClassSRMDataContext(Config.connection);

        public TopStudent()
        {
            InitializeComponent();
        }

        private void TopStudent_Load(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPage == tbMonth)
            {
                try
                {
                    tblSchoolBindingSource.DataSource = dc.SelectSchool();
                    cmbClass.ItemIndex = 0;
                }
                catch (Exception)
                {
                }
            }
            txtDate.EditValue = DateTime.Now;
        }

        private void txtDate_DateTimeChanged(object sender, EventArgs e)
        {
            cmbClass_EditValueChanged(null, null);
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            string date = txtDate.Text.Substring(0, 7);
            int count = (cmbClass.Properties.DataSource as IList).Count;
            if (count > 0)
                gridControl1.DataSource = dc.SelectTopMonth((int)cmbClass.EditValue, date + "/01", date + "/31");
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Config.ExportPdf(gridControl1, txtDate.Text);
        }

        private void cmbClass2_EditValueChanged(object sender, EventArgs e)
        {
            int count = (cmbClass2.Properties.DataSource as IList).Count;
            if (count > 0)
                gridControl2.DataSource = dc.SelectTopYear((int)cmbClass2.EditValue);
        }

        private void btnExport2_Click(object sender, EventArgs e)
        {
            Config.ExportPdf(gridControl2, txtDate.Text);
        }

        private void TopStudent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}