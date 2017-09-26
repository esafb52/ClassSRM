
/****************************** ghost1372.github.io ******************************\
*	Module Name:	PointActivity.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:30 ب.ظ
*	
***********************************************************************************/

using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ClassSRM.Forms
{
    public partial class PointActivity : DevExpress.XtraEditors.XtraForm
    {
        public PointActivity()
        {
            InitializeComponent();
            txtDate.EditValue = DateTime.Now;
        }

        private void PointActivity_Load(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            var dc = new ClassSRMDataContext(Config.connection);
            getAllItemsName();
            if (Config.ReadSetting("ActivityPoint").Equals("Free"))
                rd.SelectedIndex = 0;
            else
                rd.SelectedIndex = 1;

            rd_SelectedIndexChanged(null, null);

            tblSchoolBindingSource.DataSource = dc.SelectSchool();
            cmbClass.ItemIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var dc = new ClassSRMDataContext(Config.connection);
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
            var dc = new ClassSRMDataContext(Config.connection);
            var query = dc.SelectCurScore((int)cmbStudent.EditValue).First();
            lblCurScore.Text = query.ScoreSum.ToString();
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