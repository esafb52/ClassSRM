
/****************************** ghost1372.github.io ******************************\
*	Module Name:	History.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:29 ب.ظ
*	
***********************************************************************************/

using System;
using System.Windows.Forms;

namespace ClassSRM
{
    public partial class History : DevExpress.XtraEditors.XtraForm
    {
        public History()
        {
            InitializeComponent();
        }

        private void History_Load(object sender, EventArgs e)
        {
            txtHistory.Text = Properties.Resources.History;
        }

        private void History_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}