
/****************************** ghost1372.github.io ******************************\
*	Module Name:	PointBookList.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:30 ب.ظ
*	
***********************************************************************************/

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class PointBookList : DevExpress.XtraEditors.XtraForm
    {
        public PointBookList()
        {
            InitializeComponent();
        }

        private void PointBookList_Load(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            tblSchoolBindingSource.DataSource = dc.SelectSchool();
            cmbClass.ItemIndex = 0;

            EditableProgressBar();
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            tblStudentBindingSource.DataSource =dc.SelectStudentByClassIdNoIMG((int)cmbClass.EditValue);
            cmbStudent.ItemIndex = 0;
        }

        private void cmbStudent_EditValueChanged(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            tblEvaPointBindingSource.DataSource = from v in dc.tbl_EvaPoints where v.StudentId == (int)cmbStudent.EditValue select v;
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                var dc = new ClassSRMDataContext(Config.connection);

                int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                int idStu = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StudentId");
                int score = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Score");
                var date = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Date").ToString();
                var book = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Book").ToString();
                var desc = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Descr").ToString();

                dc.UpdateEvaBook(id, idStu, score, book, date, desc);
                tblEvaPointBindingSource.EndEdit();
                dc = new ClassSRMDataContext(Config.connection);
                XtraMessageBox.Show("امتیاز موردنظر با موفقیت ویرایش شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount != 0)
            {
                try
                {
                    var dc = new ClassSRMDataContext(Config.connection);
                    int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                    var result = XtraMessageBox.Show("آیا از حذف این امتیاز اطمینان دارید؟", "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        dc.DeleteEvaBook(id);
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

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var tosifi = Config.ReadSetting("Tosifi System");
            if (tosifi == "true")
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle) return;
                if (e.Column.FieldName != "Score") return;

                int score = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["Score"]));
                if (score > 18)
                {
                    e.DisplayText = "خیلی خوب";
                }
                else if (score <= 18 && score >= 16)
                {
                    e.DisplayText = "خوب";
                }
                else if (score <= 15 && score >= 10)
                {
                    e.DisplayText = "قابل قبول";
                }
                else
                {
                    e.DisplayText = "نیاز به تلاش بیشتر";
                }
            }
        }

        private RepositoryItem editorForDisplay, editorForEditing;

        private void gridView1_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Score")
                e.RepositoryItem = editorForEditing;
        }

        private void PointBookList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void EditableProgressBar()
        {
            editorForDisplay = new RepositoryItemProgressBar();
            editorForEditing = new RepositoryItemSpinEdit();
            gridView1.GridControl.RepositoryItems.AddRange(
              new RepositoryItem[] { editorForDisplay, editorForEditing });
            gridView1.Columns["Score"].ColumnEdit = editorForDisplay;
        }
    }
}