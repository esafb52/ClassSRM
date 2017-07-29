﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

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

        ClassSRMDataContext dc = new ClassSRMDataContext();

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
            if (isEdit)
            {
                dc.UpdateTask(id, txtCaption.Text, txtDesc.Text, cmbCat.Text, cmbColor.SelectedIndex);
                dc = new ClassSRMDataContext();
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
    }
}