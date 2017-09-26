
/****************************** ghost1372.github.io ******************************\
*	Module Name:	AddTask.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:30 ب.ظ
*	
***********************************************************************************/

using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class AddTask : DevExpress.XtraEditors.XtraForm
    {
        public static bool isEdit = false;
        public static int id = 0;
        public static int label = 0;
        public static string status;
        public static string caption;
        public static string desc;


        public AddTask()
        {
            InitializeComponent();
        }

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbColor.SelectedIndex = cmbCat.SelectedIndex;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            if (isEdit)
            {
                dc.UpdateTask(id, txtCaption.Text, txtDesc.Text, cmbCat.Text, cmbColor.SelectedIndex);
                dc = new ClassSRMDataContext(Config.connection);
                XtraMessageBox.Show("با موفقیت ویرایش شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                dc.InsertTask(txtCaption.Text, txtDesc.Text, cmbCat.Text, cmbColor.SelectedIndex);
                Close();
            }
        }

        private void AddTask_Load(object sender, EventArgs e)
        {
            if (isEdit)
            {
                cmbColor.SelectedIndex = label;
                cmbCat.Text = status;
                txtCaption.Text = caption;

                if (desc != string.Empty)
                    txtDesc.Text = desc;
                else
                    txtDesc.Text = string.Empty;
            }
        }

        private void AddTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}