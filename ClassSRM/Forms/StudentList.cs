
/****************************** ghost1372.github.io ******************************\
*	Module Name:	StudentList.cs
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class StudentList : DevExpress.XtraEditors.XtraForm
    {
        public StudentList()
        {
            InitializeComponent();
        }

        private void StudentList_Load(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            tblSchoolBindingSource.DataSource = dc.SelectSchool();
            cmbClass.ItemIndex = 0;
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            loadStudent();
        }

        private void loadStudent()
        {
            try
            {
                var dc = new ClassSRMDataContext(Config.connection);

                tblStudentBindingSource.DataSource = dc.SelectStudentByClassIdNoIMG((int)cmbClass.EditValue);
            }
            catch (InvalidOperationException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount != 0)
            {
                try
                {
                    var dc = new ClassSRMDataContext(Config.connection);

                    byte[] imageData = null;
                    imageData = imageToByteArray(img.Image);
                    int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                    dc.UpdateStudent(id, (int)cmbClass.EditValue, txtName.Text, txtLName.Text, txtFName.Text, cmbGender.Text, imageData);
                    tblStudentBindingSource.EndEdit();
                    dc = new ClassSRMDataContext(Config.connection);
                    XtraMessageBox.Show("دانش آموز موردنظر با موفقیت ویرایش شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadStudent();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("اطلاعاتی برای ویرایش وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.RowCount != 0)
            {
                try
                {
                    txtName.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StuName").ToString();
                    txtFName.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StuFName").ToString();
                    txtLName.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StuLName").ToString();
                    cmbGender.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StuGender").ToString();
                    byte[] data = null;
                    data = ((System.Data.Linq.Binary)(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StuImage"))).ToArray();

                    using (MemoryStream stream = new MemoryStream(data))
                    {
                        img.Image = Image.FromStream(stream);
                    }
                }
                catch (Exception)
                {
                }
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
                    var result = XtraMessageBox.Show("با حذف دانش آموز، تمامی امتیازات آن حذف خواهند شد.آیا ادامه می دهید؟", "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        dc.DeleteStudent(id);
                        XtraMessageBox.Show("دانش آموز موردنظر با موفقیت حذف شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadStudent();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("اطلاعاتی برای حذف وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //تبدیل عکس به بایت برای ذخیره در دیتابیس
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            img.LoadImage();
        }

        private void StudentList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}