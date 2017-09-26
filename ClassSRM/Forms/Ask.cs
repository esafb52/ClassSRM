
/****************************** ghost1372.github.io ******************************\
*	Module Name:	Ask.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:30 ب.ظ
*	
***********************************************************************************/

using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class Ask : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext dc = new ClassSRMDataContext(Config.connection);

        public Ask()
        {
            InitializeComponent();
        }

        private void Ask_Load(object sender, EventArgs e)
        {
            tblSchoolBindingSource.DataSource = dc.SelectSchool();
            cmbClass.ItemIndex = 0;
            cmbBook_SelectedIndexChanged(null, null);
        }

        private void cmbBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Todo: Fix
            int count = (cmbClass.Properties.DataSource as IList).Count;
            if (count > 0)
            {
                var list = dc.SelectAskStatus((int)cmbClass.EditValue, cmbBook.Text);
                //(from tbl_Student in dc.tbl_Students
                // where !dc.tbl_Quastions.Any(f => f.StudentId == tbl_Student.Id && f.Book == cmbBook.Text) && tbl_Student.StuClassId == (int)cmbClass.EditValue
                // select tbl_Student);
                gridControl1.DataSource = list;

                if (list.Any())
                {
                    //dc.DeleteAllQuastion((int)cmbClass.EditValue);

                    //gridControl1.DataSource = list;
                    btnSave.Enabled = false;
                }
                else
                {

                    btnSave.Enabled = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
            dc.AddQuastion((int)cmbClass.EditValue, id, cmbBook.Text);
            gridView1.DeleteSelectedRows();
            Hide();
            Close();
            PointBook.isQuastion = true;
            PointBook.bookIndex = cmbBook.SelectedIndex;
            PointBook.StudentId = id;
            PointBook.SchoolId = (int)cmbClass.EditValue;
            new PointBook().ShowDialog();
        }

        private void Ask_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}