using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class DailyCheck : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext dc = new ClassSRMDataContext(Config.connection);

        private PersianCalendar pc = new PersianCalendar();
        private string strDate;

        public DailyCheck()
        {
            InitializeComponent();
            strDate = pc.GetYear(DateTime.Now).ToString("0000") + "/" + pc.GetMonth(DateTime.Now).ToString("00") + "/" + pc.GetDayOfMonth(DateTime.Now).ToString("00");
        }

        private void DailyCheck_Load(object sender, EventArgs e)
        {
            tblSchoolBindingSource.DataSource = dc.SelectSchool();
            cmbClass.ItemIndex = 0;
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            
            //ثبت غیبت با کلید اسپیس
            if (e.KeyCode == Keys.Space)
            {
                var dc = new ClassSRMDataContext(Config.connection);
                dc.InsertCheck((int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id"), false, strDate);
                gridView1.DeleteSelectedRows();
                gridView1.SelectRow(0);
                lblCount.Text = gridView1.DataRowCount + " نفر بررسی نشده اند";

                if (gridView1.DataRowCount == 0)
                {
                    Close();
                }
            }
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();

            var list = (from tbl_Student in dc.tbl_Students
                        where !dc.tbl_Checks.Any(f => f.StudentId == tbl_Student.Id && (f.Date == strDate)) && tbl_Student.StuClassId == (int)cmbClass.EditValue
                        select tbl_Student).ToList();

            gridControl1.DataSource = list;
            lblCount.Text = gridView1.DataRowCount + " نفر بررسی نشده اند";
            splashScreenManager1.CloseWaitForm();
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            dc.InsertCheck((int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id"), true, strDate);
            gridView1.DeleteSelectedRows();
            gridView1.SelectRow(0);
            lblCount.Text = gridView1.DataRowCount + " نفر بررسی نشده اند";
            if (gridView1.DataRowCount == 0)
            {
                Close();
            }
        }

        private void btnDeActive_Click(object sender, EventArgs e)
        {
            dc.InsertCheck((int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id"), false, strDate);
            gridView1.DeleteSelectedRows();
            gridView1.SelectRow(0);
            lblCount.Text = gridView1.DataRowCount + " نفر بررسی نشده اند";

            if (gridView1.DataRowCount == 0)
            {
                Close();
            }
        }

        private void DailyCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}