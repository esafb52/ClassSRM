using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class Gifts : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext dc = new ClassSRMDataContext(Config.connection);

        private PersianCalendar pc = new PersianCalendar();
        private string strCurMonth, strYear;
        private int PrevMonth;

        public Gifts()
        {
            InitializeComponent();
        }

        private void Gifts_Load(object sender, EventArgs e)
        {
            CreateCustomDate();
            try
            {
                tblSchoolBindingSource.DataSource = from v in dc.tbl_Schools select v;
                cmbClass.ItemIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridControl1.DataSource = dc.SelectGifts((int)cmbClass.EditValue, strYear + "/" + strCurMonth + "/" + "01", strYear + "/" + strCurMonth + "/" + "31", strYear + "/" + PrevMonth.ToString("00") + "/" + "01", strYear + "/" + PrevMonth.ToString("00") + "/" + "31");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Config.ExportPdf(gridControl1);
        }

        private void Gifts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void CreateCustomDate()
        {
            strCurMonth = pc.GetMonth(DateTime.Now).ToString("00");
            strYear = pc.GetYear(DateTime.Now).ToString("0000");
            PrevMonth = Convert.ToInt32(strCurMonth) - 1;
        }
    }
}