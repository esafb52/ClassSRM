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
    public partial class CompareBarChart : DevExpress.XtraEditors.XtraForm
    {
        ClassSRMDataContext dc = new ClassSRMDataContext();

        public CompareBarChart()
        {
            InitializeComponent();
        }

        private void CompareBarChart_Load(object sender, EventArgs e)
        {
            try
            {
                tblSchoolBindingSource.DataSource = from v in dc.tbl_Schools select v;
            }
            catch (Exception)
            {
            }
        }

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                sumAEBindingSource.DataSource = from v in dc.SumAEs where v.StuClassId == (int)cmbClass.EditValue select v;
                chartControl1.Series["Series 1"].LegendText = cmbClass.Text;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("مشکلی پیش آمده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}