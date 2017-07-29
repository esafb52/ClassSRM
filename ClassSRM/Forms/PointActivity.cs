using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ClassSRM.Forms
{
    public partial class PointActivity : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext dc = new ClassSRMDataContext();

        public PointActivity()
        {
            InitializeComponent();
            txtDate.EditValue = DateTime.Now;
        }

        private void PointActivity_Load(object sender, EventArgs e)
        {
            getAllItemsName();
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
                    int val = getValeofItem(cmbScore.Text);
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

        private void getAllItemsName()
        {
            XDocument doc = XDocument.Load("Activity.xml");
            var items = doc
                .Element(XName.Get("Activity"))
                .Elements(XName.Get("ActivityPoint"));
            var itemName = items.Select(ele => ele.Element(XName.Get("Name")).Value);
            foreach (string name in itemName)
            {
                cmbScore.Properties.Items.Add(name);
            }
            cmbScore.SelectedIndex = 0;
        }

        private int getValeofItem(string itemName)
        {
            XDocument doc = XDocument.Load("Activity.xml");
            var values = doc.Descendants("ActivityPoint")
                .Where(i => i.Element("Name").Value == itemName)
                .Select(i => i.Element("Value").Value).FirstOrDefault();
            if (values != null)
                return Convert.ToInt32(values);
            else
                return 0;
        }

        private void PointActivity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}