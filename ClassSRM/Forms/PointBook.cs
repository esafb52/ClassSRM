
/****************************** ghost1372.github.io ******************************\
*	Module Name:	PointBook.cs
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

namespace ClassSRM.Forms
{
    public partial class PointBook : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext dc = new ClassSRMDataContext(Config.connection);

        public static bool isQuastion = false;
        public static int bookIndex = 0;
        public static int StudentId = 0;
        public static int SchoolId = 0;

        public PointBook()
        {
            InitializeComponent();
            txtDate.EditValue = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var val = 0;
                var tosifi = Config.ReadSetting("Tosifi System");
                if (tosifi == "true")
                {
                    switch (cmbScore2.SelectedIndex)
                    {
                        case 0:
                            val = 20;
                            break;

                        case 1:
                            val = 18;
                            break;

                        case 2:
                            val = 15;
                            break;

                        case 3:
                            val = 10;
                            break;
                    }
                    dc.InsertEvaBook((int)cmbStudent.EditValue, val, cmbBook.Text, txtDate.Text, txtDesc.Text);
                    if (val > 17)
                    {
                        dc.InsertActPoint((int)cmbStudent.EditValue, 2, txtDate.Text, txtDesc.Text);
                    }
                    else if (val <= 17 && val >= 15)
                    {
                        dc.InsertActPoint((int)cmbStudent.EditValue, 1, txtDate.Text, txtDesc.Text);
                    }
                }
                else
                {
                    val = (int)txtScore.Value;
                    dc.InsertEvaBook((int)cmbStudent.EditValue, (int)txtScore.Value, cmbBook.Text, txtDate.Text, txtDesc.Text);
                    if (val > 17)
                    {
                        dc.InsertActPoint((int)cmbStudent.EditValue, 2, txtDate.Text, txtDesc.Text);
                    }
                    else if (val <= 17 && val >= 15)
                    {
                        dc.InsertActPoint((int)cmbStudent.EditValue, 1, txtDate.Text, txtDesc.Text);
                    }
                }

                XtraMessageBox.Show("امتیاز موردنظر با موفقیت ثبت شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PointBook_Load(object sender, EventArgs e)
        {
            var tosifi = Config.ReadSetting("Tosifi System");
            if (tosifi == "true")
            {
                cmbScore2Visibility.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                cmbScoreVisibility.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                cmbScore2Visibility.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                cmbScoreVisibility.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            tblSchoolBindingSource.DataSource =dc.SelectSchool();
            cmbClass.ItemIndex = 0;
           

            if (isQuastion)
            {
                cmbBook.SelectedIndex = bookIndex;
                cmbClass.EditValue = SchoolId;
                tblStudentBindingSource.DataSource = dc.SelectStudentByClassId((int)cmbClass.EditValue);
                cmbStudent.EditValue = StudentId;
            }
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            int count = (cmbClass.Properties.DataSource as IList).Count;
            if (count > 0)
            {
                tblStudentBindingSource.DataSource = dc.SelectStudentByClassId((int)cmbClass.EditValue);
                cmbStudent.ItemIndex = 0;
            }
        }

        private void PointBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}