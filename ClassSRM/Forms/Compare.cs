
/****************************** ghost1372.github.io ******************************\
*	Module Name:	Compare.cs
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
    public partial class Compare : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext dc = new ClassSRMDataContext(Config.connection);

        public Compare()
        {
            InitializeComponent();
        }

        private void Compare_Load(object sender, EventArgs e)
        {
            try
            {
                tblSchoolBindingSource.DataSource = dc.SelectSchool();
            }
            catch (Exception)
            {
            }
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                tblStudentBindingSource.DataSource = from v in dc.tbl_Students where v.StuClassId == (int)cmbClass.EditValue select v;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("برنامه با مشکل مواجه شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbStudent_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var bookName = cmbBook.EditValue.ToString();
                var q1 = from tbl_EvaPoints in dc.tbl_EvaPoints
                         orderby tbl_EvaPoints.Date
                         where tbl_EvaPoints.StudentId == (int)cmbStudent.EditValue
                         &&
                         tbl_EvaPoints.Book == bookName
                         select tbl_EvaPoints;
                tblEvaPointBindingSource.DataSource = q1;
                chartControl1.Series["Series 1"].LegendText = cmbStudent.Text;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("برنامه با مشکل مواجه شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbStudent2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var bookName = cmbBook.EditValue.ToString();
                var q2 = from tbl_EvaPoints in dc.tbl_EvaPoints
                         orderby tbl_EvaPoints.Date
                         where tbl_EvaPoints.StudentId == (int)cmbStudent2.EditValue
                         &&
                         tbl_EvaPoints.Book == bookName
                         select tbl_EvaPoints;
                tblEvaPointBindingSource1.DataSource = q2;
                chartControl1.Series["Series 2"].LegendText = cmbStudent2.Text;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("برنامه با مشکل مواجه شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbStudent2_EditValueChanged(null, null);
            cmbStudent_EditValueChanged(null, null);
        }

        private void Compare_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}