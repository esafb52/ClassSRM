﻿
/****************************** ghost1372.github.io ******************************\
*	Module Name:	Process.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:30 ب.ظ
*	
***********************************************************************************/

using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class Process : DevExpress.XtraEditors.XtraForm
    {

        private int userId1;
        private int userId2;
        private int userId3;

        public Process()
        {
            InitializeComponent();
        }

        private void Process_Load(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            tblSchoolBindingSource.DataSource = dc.SelectSchool();
            cmbClass.ItemIndex = 0;
        }

        private void ChartQuery()
        {
            try
            {
                var dc = new ClassSRMDataContext(Config.connection);
                var queryWin1 = (from tbl_ActPoints in dc.tbl_ActPoints //دریافت دانش آموز برتر
                                 group tbl_ActPoints by new
                                 {
                                     tbl_ActPoints.StudentId
                                 } into g
                                 orderby
                                   (int?)g.Sum(p => p.Score) descending
                                 select new
                                 {
                                     g.Key.StudentId,
                                     AverageScore = (int?)g.Sum(p => p.Score)
                                 }).Take(1).FirstOrDefault();

                var queryWin2 = (from tbl_EvaPoint in dc.tbl_EvaPoints //دریافت دانش آموز برتر
                                 group tbl_EvaPoint by new
                                 {
                                     tbl_EvaPoint.StudentId
                                 } into g
                                 orderby
                                   (int?)g.Sum(p => p.Score) descending
                                 select new
                                 {
                                     g.Key.StudentId,
                                     AverageScore = (int?)g.Sum(p => p.Score)
                                 }).Take(1).FirstOrDefault();
                var queryWin3 = (from T in (((from tbl_ActPoints in dc.tbl_ActPoints
                                              select new
                                              {
                                                  StudentId = (int?)tbl_ActPoints.StudentId,
                                                  Score = (int?)tbl_ActPoints.Score
                                              }).Concat(
                    from tbl_EvaPoints in dc.tbl_EvaPoints
                    select new
                    {
                        StudentId = (int?)tbl_EvaPoints.StudentId,
                        Score = (int?)tbl_EvaPoints.Score
                    })))
                                 group T by new
                                 {
                                     T.StudentId
                                 } into g
                                 orderby g.Sum(p => p.Score) descending
                                 select new
                                 {
                                     g.Key.StudentId,
                                     HighScoreUser = g.Sum(p => p.Score)
                                 }).Take(1).FirstOrDefault();
                userId1 = queryWin1.StudentId.Value;
                userId2 = queryWin2.StudentId.Value;
                userId3 = queryWin3.StudentId.Value;
            }
            catch (Exception)
            {
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var dc = new ClassSRMDataContext(Config.connection);

                int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                var bookName = cmbBook.Text;
                tblEvaPointBindingSource.DataSource = from tbl_EvaPoints in dc.tbl_EvaPoints
                                                      orderby tbl_EvaPoints.Date
                                                      where tbl_EvaPoints.StudentId == id
                                                      &&
                                                      tbl_EvaPoints.Book == bookName
                                                      select tbl_EvaPoints;
                chartControl1.Series["Series 1"].LegendText = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StuName").ToString() + " " + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StuLName").ToString();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("مشکلی پیش آمده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridView1_FocusedRowChanged(null, null);
        }

        private void chkFirstTotal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFirstTotal.Checked)
            {
                try
                {
                    var dc = new ClassSRMDataContext(Config.connection);

                    tblEvaPointBindingSource1.DataSource = from v in dc.tbl_EvaPoints where v.StudentId == userId1 select v; //نمایش پیشرفت نفر برتر
                    chartControl1.Series["Series 2"].LegendText = "نفر برتر فعالیت ها " + (from v in dc.tbl_Students where v.Id == userId1 && v.StuClassId == (int)cmbClass.EditValue select v.StuName + " " + v.StuLName).FirstOrDefault().ToString();
                }
                catch (Exception)
                {
                }
            }
            else
            {
                tblEvaPointBindingSource1.DataSource = null;
            }
        }

        private void chkFirstBook_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFirstBook.Checked)
            {
                try
                {
                    var dc = new ClassSRMDataContext(Config.connection);

                    tblEvaPointBindingSource2.DataSource = from v in dc.tbl_EvaPoints where v.StudentId == userId2 select v; //نمایش پیشرفت نفر برتر
                    chartControl1.Series["Series 3"].LegendText = "نفر برتر دروس " + (from v in dc.tbl_Students where v.Id == userId2 && v.StuClassId == (int)cmbClass.EditValue select v.StuName + " " + v.StuLName).FirstOrDefault().ToString();
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("مشکلی پیش آمده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                tblEvaPointBindingSource2.DataSource = null;
            }
        }

        private void chkFirstAvg_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFirstAvg.Checked)
            {
                try
                {
                    var dc = new ClassSRMDataContext(Config.connection);

                    tblEvaPointBindingSource3.DataSource = from v in dc.tbl_EvaPoints where v.StudentId == userId3 select v; //نمایش پیشرفت نفر برتر
                    chartControl1.Series["Series 4"].LegendText = "نفر برتر کل " + (from v in dc.tbl_Students where v.Id == userId3 && v.StuClassId == (int)cmbClass.EditValue select v.StuName + " " + v.StuLName).FirstOrDefault().ToString();
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("مشکلی پیش آمده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                tblEvaPointBindingSource3.DataSource = null;
            }
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            tblStudentBindingSource.DataSource = dc.SelectStudentByClassIdNoIMG((int)cmbClass.EditValue);
            ChartQuery();
        }

        private void Process_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}