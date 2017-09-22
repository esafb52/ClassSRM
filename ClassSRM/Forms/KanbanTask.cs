
/****************************** ghost1372.github.io ******************************\
*	Module Name:	KanbanTask.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:30 ب.ظ
*	
***********************************************************************************/

using ClassSRM.Forms;
using DevExpress.XtraEditors;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM
{
    public partial class KanBanTask : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext dc = new ClassSRMDataContext(Config.connection);

        public KanBanTask()
        {
            InitializeComponent();
        }

        private void InitData()
        {
            splashScreenManager1.ShowWaitForm();
            var res = from v in dc.tbl_Tasks
                      select new
                      {
                          v.Id,
                          v.Label,
                          v.Status,
                          v.Description,
                          v.Caption
                      };

            gridControl.DataSource = res;
            splashScreenManager1.CloseWaitForm();
        }

        private void tileView_BeforeItemDrag(object sender, DevExpress.XtraGrid.Views.Tile.BeforeItemDragEventArgs e)
        {
            e.Cancel = IsEmptyItem(e.RowHandle);
        }

        private void tileView_ItemDrop(object sender, DevExpress.XtraGrid.Views.Tile.ItemDropEventArgs e)
        {
            var newStatus = e.GroupColumnValue;
            var prevStatus = e.PrevGroupColumnValue;

            if (!prevStatus.Equals(newStatus))
            {
                var id = tileView.GetRowCellValue(e.RowHandle, "Id");
                int lbl = (int)tileView.GetRowCellValue(e.RowHandle, "Label");
                if (newStatus.ToString().Equals("انجام شده"))
                {
                    lbl = 2;
                }
                else if (newStatus.ToString().Equals("درحال انجام"))
                {
                    lbl = 1;
                }
                else if (newStatus.ToString().Equals("برای انجام"))
                {
                    lbl = 0;
                }
                else
                {
                    lbl = (int)tileView.GetRowCellValue(e.RowHandle, "Label");
                }

                dc.UpdateTaskLabel((int)id, newStatus.ToString(), lbl);
                dc.SubmitChanges();
                dc = new ClassSRMDataContext(Config.connection);
                InitData();
            }
        }

        private void tileView_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            if (!IsEmptyItem(e.Item.RowHandle)) return;
        }

        private void tileView_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            switch (e.Item["Label"].Text)
            {
                case "0":
                    e.Item["Label"].Appearance.Normal.BackColor = ColorTranslator.FromHtml("#f06562");

                    break;

                case "1":
                    e.Item["Label"].Appearance.Normal.BackColor = ColorTranslator.FromHtml("#e6c730");
                    break;

                case "2":
                    e.Item["Label"].Appearance.Normal.BackColor = ColorTranslator.FromHtml("#069c47");
                    break;
            }
            if (e.Item["Description"].Text == string.Empty)
            {
                e.Item.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomLeft;
                e.Item["Description"].ImageVisible = false;
            }
            else
            {
                e.Item["Description"].ImageVisible = true;
            }
        }

        private bool IsEmptyItem(int rowHandle)
        {
            var row = tileView.GetRow(rowHandle);
            if (row == null) return false;
            return (int)tileView.GetRowCellValue(rowHandle, "Id") < 0;
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddTask.isEdit = false;
            new AddTask().ShowDialog();
            InitData();
        }

        private void btnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tileView.RowCount == 0)
            {
                XtraMessageBox.Show("اطلاعاتی برای حذف وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var result = XtraMessageBox.Show("برای حذف این وظیفه اطمینان دارید؟", "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dc.DeleteTask((int)tileView.GetRowCellValue(tileView.FocusedRowHandle, "Id"));
                    InitData();
                    XtraMessageBox.Show("با موفقیت حذف شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tileView.RowCount == 0)
            {
                XtraMessageBox.Show("اطلاعاتی برای ویرایش وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                AddTask.id = (int)tileView.GetRowCellValue(tileView.FocusedRowHandle, "Id");
                AddTask.label = (int)tileView.GetRowCellValue(tileView.FocusedRowHandle, "Label");
                AddTask.status = tileView.GetRowCellValue(tileView.FocusedRowHandle, "Status").ToString();
                AddTask.caption = tileView.GetRowCellValue(tileView.FocusedRowHandle, "Caption").ToString();
                AddTask.desc = tileView.GetRowCellValue(tileView.FocusedRowHandle, "Description").ToString();
                AddTask.isEdit = true;
                new AddTask().ShowDialog();
                tblTaskBindingSource.EndEdit();
                dc = new ClassSRMDataContext(Config.connection);
                InitData();
            }
        }

        private void ribbonControl1_SelectedPageChanged(object sender, System.EventArgs e)
        {
            if (ribbonControl1.SelectedPage == ribbonPage1)
            {
                documentViewer1.Visible = false;
                gridControl.Visible = true;
            }
            else if (ribbonControl1.SelectedPage == ribbonPage2)
            {
                documentViewer1.Visible = true;
                printableComponentLink1.CreateDocument();
                gridControl.Visible = false;
            }
        }

        private void KanBanTask_Load(object sender, System.EventArgs e)
        {
            InitData();
        }

        private void KanBanTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}