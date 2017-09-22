
/****************************** ghost1372.github.io ******************************\
*	Module Name:	PointActivityList.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:30 ب.ظ
*	
***********************************************************************************/

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class PointActivityList : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext dc = new ClassSRMDataContext(Config.connection);

        public PointActivityList()
        {
            InitializeComponent();
        }

        private void PointActivityList_Load(object sender, EventArgs e)
        {
            tblSchoolBindingSource.DataSource = from v in dc.tbl_Schools select v;
            cmbClass.ItemIndex = 0;
            EditableProgressBar();
        }

        private void cmbStudent_EditValueChanged(object sender, EventArgs e)
        {
            tblActPointBindingSource.DataSource = from v in dc.tbl_ActPoints where v.StudentId == (int)cmbStudent.EditValue select v;
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            tblStudentBindingSource.DataSource = from v in dc.tbl_Students where v.StuClassId == (int)cmbClass.EditValue select v;
            cmbStudent.ItemIndex = 0;
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                int idStu = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StudentId");
                int score = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Score");
                var date = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Date").ToString();
                var desc = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Descr").ToString();

                dc.UpdateActBook(id, idStu, score, date, desc);
                tblActPointBindingSource.EndEdit();
                dc = new ClassSRMDataContext(Config.connection);
                XtraMessageBox.Show("امتیاز موردنظر با موفقیت ویرایش شد", "حطا", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }
        }

        private RepositoryItem editorForDisplay, editorForEditing;

        private void EditableProgressBar()
        {
            editorForDisplay = new RepositoryItemProgressBar();
            editorForEditing = new RepositoryItemSpinEdit();
            gridView1.GridControl.RepositoryItems.AddRange(
              new RepositoryItem[] { editorForDisplay, editorForEditing });
            gridView1.Columns["Score"].ColumnEdit = editorForDisplay;
        }

        private void gridView1_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Score")
                e.RepositoryItem = editorForEditing;
        }

        private void PointActivityList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount != 0)
            {
                try
                {
                    int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                    var result = XtraMessageBox.Show("آیا از حذف این امتیاز اطمینان دارید؟", "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        dc.DeleteActPoint(id);
                        XtraMessageBox.Show("امتیاز موردنظر با موفقیت حذف شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}