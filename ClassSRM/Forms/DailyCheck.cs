using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Globalization;

namespace ClassSRM.Forms
{
    public partial class DailyCheck : DevExpress.XtraEditors.XtraForm
    {
        ClassSRMDataContext dc = new ClassSRMDataContext();

        private PersianCalendar pc = new PersianCalendar();
        string strDate;
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
            //ثبت حضور با کلید اینتر
            if (e.KeyCode == Keys.Enter)
            {
                var dc = new ClassSRMDataContext();
                dc.InsertCheck((int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id"), true, strDate);
                gridView1.DeleteSelectedRows();
                gridView1.SelectRow(0);
                lblCount.Text = gridView1.DataRowCount + " نفر بررسی نشده اند";
                if (gridView1.DataRowCount == 0)
                {
                    Close();
                }
            }
            //ثبت غیبت با کلید اسپیس
            if (e.KeyCode == Keys.Space)
            {
                var dc = new ClassSRMDataContext();
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
    }
}