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

namespace ClassSRM.Forms
{
    public partial class Process : DevExpress.XtraEditors.XtraForm
    {
        ClassSRMDataContext dc = new ClassSRMDataContext();

        private int userId1;
        private int userId2;
        private int userId3;

        public Process()
        {
            InitializeComponent();
        }

        private void Process_Load(object sender, EventArgs e)
        {
            tblSchoolBindingSource.DataSource = dc.SelectSchool();
            cmbClass.ItemIndex = 0;

        }

        private void ChartQuery()
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
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
                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception)
            {
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
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
                    tblEvaPointBindingSource1.DataSource = from v in dc.tbl_EvaPoints where v.StudentId == userId1 select v; //نمایش پیشرفت نفر برتر
                    chartControl1.Series["Series 2"].LegendText = "نفر برتر فعالیت ها " + (from v in dc.tbl_Students where v.Id == userId1 select v.StuName + " " + v.StuLName).FirstOrDefault().ToString();
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("مشکلی پیش آمده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    tblEvaPointBindingSource2.DataSource = from v in dc.tbl_EvaPoints where v.StudentId == userId2 select v; //نمایش پیشرفت نفر برتر
                    chartControl1.Series["Series 3"].LegendText = "نفر برتر دروس " + (from v in dc.tbl_Students where v.Id == userId2 select v.StuName + " " + v.StuLName).FirstOrDefault().ToString();
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
                    tblEvaPointBindingSource3.DataSource = from v in dc.tbl_EvaPoints where v.StudentId == userId3 select v; //نمایش پیشرفت نفر برتر
                    chartControl1.Series["Series 4"].LegendText = "نفر برتر کل " + (from v in dc.tbl_Students where v.Id == userId3 select v.StuName + " " + v.StuLName).FirstOrDefault().ToString();
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
            tblStudentBindingSource.DataSource = from v in dc.tbl_Students where v.StuClassId == (int)cmbClass.EditValue select v;
            ChartQuery();
        }
    }
}