
/****************************** ghost1372.github.io ******************************\
*	Module Name:	DailyCheck.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:30 ب.ظ
*	
***********************************************************************************/

using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class DailyCheck : DevExpress.XtraEditors.XtraForm
    {

        private PersianCalendar pc = new PersianCalendar();
        private string strDate;

        public DailyCheck()
        {
            InitializeComponent();
            strDate = pc.GetYear(DateTime.Now).ToString("0000") + "/" + pc.GetMonth(DateTime.Now).ToString("00") + "/" + pc.GetDayOfMonth(DateTime.Now).ToString("00");
        }

        private void DailyCheck_Load(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

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
            var dc = new ClassSRMDataContext(Config.connection);
            tblStudentBindingSource.DataSource = (from tbl_Student in dc.tbl_Students
                                                  where !dc.tbl_Checks.Any(f => f.StudentId == tbl_Student.Id && (f.Date == strDate)) && tbl_Student.StuClassId == (int)cmbClass.EditValue
                                                  select new {
                                                      tbl_Student.Id,
                                                      tbl_Student.StuName,
                                                      tbl_Student.StuLName,
                                                      tbl_Student.StuFName,
                                                      tbl_Student.StuGender
                                                  });

            lblCount.Text = gridView1.DataRowCount + " نفر بررسی نشده اند";
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

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

        private void DailyCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}