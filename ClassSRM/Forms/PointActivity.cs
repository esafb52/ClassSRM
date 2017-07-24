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
    public partial class PointActivity : DevExpress.XtraEditors.XtraForm
    {
        ClassSRMDataContext dc = new ClassSRMDataContext();

        public PointActivity()
        {
            InitializeComponent();
        }

        private void PointActivity_Load(object sender, EventArgs e)
        {
            if (Config.ReadSetting("ActivityPoint").Equals("Free"))
                rd.SelectedIndex = 0;
            else
                rd.SelectedIndex = 1;

            rd_SelectedIndexChanged(null, null);

            tblSchoolBindingSource.DataSource = from v in dc.tbl_Schools select v;
            cmbClass.ItemIndex = 0;
            tblStudentBindingSource.DataSource = from v in dc.tbl_Students where v.StuClassId == (int)cmbClass.EditValue select v;
            cmbStudent.ItemIndex = 0;
            var query = from tbl_ActPoints in (from tbl_ActPoints in dc.tbl_ActPoints
                                               where tbl_ActPoints.StudentId == (int)cmbStudent.EditValue
                                               select new
                                               {
                                                   tbl_ActPoints.Score,
                                                   Dummy = "x"
                                               })
                        group tbl_ActPoints by new { tbl_ActPoints.Dummy } into g
                        select new
                        {
                            ScoreSum = (int?)g.Sum(p => p.Score)
                        };
            if (query.FirstOrDefault() == null)
            {
                lblCurScore.Text = "0";
            }
            else
            {
                lblCurScore.Text = query.FirstOrDefault().ScoreSum.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (rd.SelectedIndex == 0)
                {

                    dc.InsertActPoint((int)cmbStudent.EditValue, (int)txtScore.Value, txtDate.Text, txtDesc.Text);
                    XtraMessageBox.Show("امتیاز موردنظر با موفقیت ثبت شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cmbStudent_EditValueChanged(null, null);
                }
                else
                {
                    var val = 0;
                    switch (cmbScore.SelectedIndex)
                     {
                            case 0:
                                val = 1;
                                break;

                            case 1:
                                val = 1;
                                break;

                            case 2:
                                val = 2;
                                break;

                            case 3:
                                val = 1;
                                break;
                    }
                     dc.InsertActPoint((int)cmbStudent.EditValue, val, txtDate.Text, txtDesc.Text);
                    XtraMessageBox.Show("امتیاز موردنظر با موفقیت ثبت شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cmbStudent_EditValueChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbStudent_EditValueChanged(object sender, EventArgs e)
        {
            tblStudentBindingSource.DataSource = from v in dc.tbl_Students where v.StuClassId == (int)cmbClass.EditValue select v;
            var query = from tbl_ActPoints in (from tbl_ActPoints in dc.tbl_ActPoints
                                               where tbl_ActPoints.StudentId == (int)cmbStudent.EditValue
                                               select new
                                               {
                                                   tbl_ActPoints.Score,
                                                   Dummy = "x"
                                               })
                        group tbl_ActPoints by new { tbl_ActPoints.Dummy } into g
                        select new
                        {
                            ScoreSum = (int?)g.Sum(p => p.Score)
                        };
            if (query.FirstOrDefault() == null)
            {
                lblCurScore.Text = "0";
            }
            else
            {
                lblCurScore.Text = query.FirstOrDefault().ScoreSum.ToString();
            }
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            tblStudentBindingSource.DataSource = from v in dc.tbl_Students where v.StuClassId == (int)cmbClass.EditValue select v;
            cmbStudent.ItemIndex = 0;
            var query = from tbl_ActPoints in (from tbl_ActPoints in dc.tbl_ActPoints
                                               where tbl_ActPoints.StudentId == (int)cmbStudent.EditValue
                                               select new
                                               {
                                                   tbl_ActPoints.Score,
                                                   Dummy = "x"
                                               })
                        group tbl_ActPoints by new { tbl_ActPoints.Dummy } into g
                        select new
                        {
                            ScoreSum = (int?)g.Sum(p => p.Score)
                        };

            if (query.FirstOrDefault() == null)
            {
                lblCurScore.Text = "0";
            }
            else
            {
                lblCurScore.Text = query.FirstOrDefault().ScoreSum.ToString();
            }
        }

        private void rd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rd.SelectedIndex == 0)
            {
                Config.AddUpdateAppSettings("ActivityPoint", "Free");
                lyScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lyScore2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                Config.AddUpdateAppSettings("ActivityPoint", "Default");
                lyScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lyScore2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void cmbScore_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDesc.Text = cmbScore.Text;
        }
    }
}