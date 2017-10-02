
/****************************** ghost1372.github.io ******************************\
*	Module Name:	DailyCheckList.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:30 ب.ظ
*	
***********************************************************************************/

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class DailyCheckList : DevExpress.XtraEditors.XtraForm
    {

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
            var dc = new ClassSRMDataContext(Config.connection);

            tblSchoolBindingSource.DataSource = dc.SelectSchool();
            cmbClass.ItemIndex = 0;
            txtDate1.EditValue = DateTime.Now;
            txtDate2.EditValue = DateTime.Now;
        }

        private void cmbStudent_EditValueChanged(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            int count = (cmbClass.Properties.DataSource as IList).Count;
            if (count > 0)
            {
                int id = (int)cmbStudent.EditValue;
                checkVBindingSource.DataSource = from v in dc.CheckVs
                                                 where v.StudentId == id
                                                 orderby v.Date descending
                                                 select new
                                                 {
                                                     v.Id,
                                                     v.StuName,
                                                     v.StuLName,
                                                     v.StuFName,
                                                     v.StudentId,
                                                     v.Exist,
                                                     v.Date
                                                 };
            }
        }

        private void txtDate1_DateTimeChanged(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            if (chkFilter.Checked)
            {
                int count = (cmbClass.Properties.DataSource as IList).Count;
                if (count > 0)
                {
                    int id = (int)cmbStudent.EditValue;
                    checkVBindingSource.DataSource = dc.Select2Dates(id, txtDate1.Text, txtDate2.Text);
                }
            }
        }

        private void txtDate2_DateTimeChanged(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            if (chkFilter.Checked)
            {
                int count = (cmbClass.Properties.DataSource as IList).Count;
                if (count > 0)
                {
                    int id = (int)cmbStudent.EditValue;
                    checkVBindingSource.DataSource = dc.Select2Dates(id, txtDate1.Text, txtDate2.Text);
                }
            }
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            int count = (cmbClass.Properties.DataSource as IList).Count;
            if (count > 0)
            {
                tblStudentBindingSource.DataSource = dc.SelectStudentByClassIdNoIMG((int)cmbClass.EditValue);
                cmbStudent.ItemIndex = 0;

            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                var dc = new ClassSRMDataContext(Config.connection);

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
                dc = new ClassSRMDataContext(Config.connection);
                XtraMessageBox.Show("حضورغیاب موردنظر با موفقیت ویرایش شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }


        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            if (gridView1.RowCount != 0)
            {
                try
                {
                    int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                    var result = XtraMessageBox.Show("آیا از حذف این حضور غیاب اطمینان دارید؟", "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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