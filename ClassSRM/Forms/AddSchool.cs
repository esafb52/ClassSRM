using DevExpress.XtraEditors;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class AddSchool : DevExpress.XtraEditors.XtraForm
    {
        private PersianCalendar pc = new PersianCalendar();
        private string strDate;

        public AddSchool()
        {
            InitializeComponent();
            GenerateEducateYear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var dc = new ClassSRMDataContext();
                dc.InsertSchool(txtSchool.Text, txtAdmin.Text, cmbClass.Text, txtDate.Text);
                XtraMessageBox.Show("مدرسه موردنظر با موفقیت ثبت شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //تولید سال تحصیلی مثال: 95-96
        private void GenerateEducateYear()
        {
            strDate = pc.GetYear(DateTime.Now).ToString("0000");
            string Year = strDate.Substring(2, 2);
            int NextYear = Convert.ToInt32(Year) + 1;
            txtDate.Text = Year + "-" + NextYear;
            txtSchool.Focus();
            txtSchool.Select();
        }

        private void AddSchool_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}