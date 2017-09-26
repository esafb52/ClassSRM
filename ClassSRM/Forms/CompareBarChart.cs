
/****************************** ghost1372.github.io ******************************\
*	Module Name:	CompareBarChart.cs
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
    public partial class CompareBarChart : DevExpress.XtraEditors.XtraForm
    {

        public CompareBarChart()
        {
            InitializeComponent();
        }

        private void CompareBarChart_Load(object sender, EventArgs e)
        {
            try
            {
                var dc = new ClassSRMDataContext(Config.connection);

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
                var dc = new ClassSRMDataContext(Config.connection);

                sumAEBindingSource.DataSource = from v in dc.SumAEs where v.StuClassId == (int)cmbClass.EditValue select v;
                chartControl1.Series["Series 1"].LegendText = cmbClass.Text;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("مشکلی پیش آمده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void CompareBarChart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}