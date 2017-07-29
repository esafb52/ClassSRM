using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class DailyCheckList : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext dc = new ClassSRMDataContext();

        public DailyCheckList()
        {
            InitializeComponent();
           
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFilter.Checked)
            {
                txtDate1.Enabled = true;
                txtDate2.Enabled = true;
            }
            else
            {
                txtDate1.Enabled = false;
                txtDate2.Enabled = false;
                cmbStudent_EditValueChanged(null, null);
            }
        }

        private void DailyCheckList_Load(object sender, EventArgs e)
        {
            tblSchoolBindingSource.DataSource = from v in dc.tbl_Schools select v;
            cmbClass.ItemIndex = 0;
            txtDate1.EditValue = DateTime.Now;
            txtDate2.EditValue = DateTime.Now;
        }

        private void cmbStudent_EditValueChanged(object sender, EventArgs e)
        {
            int id = (int)cmbStudent.EditValue;
            checkVBindingSource.DataSource = from v in dc.CheckVs where v.StudentId == id orderby v.Date descending select v;
        }

        private void txtDate1_DateTimeChanged(object sender, EventArgs e)
        {
            int id = (int)cmbStudent.EditValue;
            checkVBindingSource.DataSource = dc.Select2Dates(id, txtDate1.Text, txtDate2.Text);
        }

        private void txtDate2_DateTimeChanged(object sender, EventArgs e)
        {
            int id = (int)cmbStudent.EditValue;
            checkVBindingSource.DataSource = dc.Select2Dates(id, txtDate1.Text, txtDate2.Text);
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            tblStudentBindingSource.DataSource = from v in dc.tbl_Students where v.StuClassId == (int)cmbClass.EditValue select v;
            cmbStudent.ItemIndex = 0;
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                int idStu = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StudentId");
                bool exist = (bool)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Exist");
                var date = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Date").ToString();
                if (exist)
                {
                    exist = false;
                }
                else
                {
                    exist = true;
                }
                dc.UpdateCheck(id, idStu, exist, date);
                checkVBindingSource.EndEdit();
                dc = new ClassSRMDataContext();
                XtraMessageBox.Show("حضورغیاب موردنظر با موفقیت ویرایش شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount != 0)
            {
                try
                {
                    int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                    var result = XtraMessageBox.Show("آیا از حذف این حضور غیاب اطمینان دارید؟", "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        dc.DeleteCheck(id);
                        XtraMessageBox.Show("حضورغیاب موردنظر با موفقیت حذف شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbStudent_EditValueChanged(null, null);
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                XtraMessageBox.Show("اطلاعاتی برای حذف وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DailyCheckList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}