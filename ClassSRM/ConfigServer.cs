
/****************************** ghost1372.github.io ******************************\
*	Module Name:	ConfigServer.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 9, 3, 01:17 ب.ظ
*	
***********************************************************************************/

using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace ClassSRM
{
    public partial class ConfigServer : DevExpress.XtraEditors.XtraForm
    {
        public ConfigServer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (cmbServer.SelectedIndex)
            {
                case 0:
                    Config.AddUpdateAppSettings("ConServer", @"Data Source=.;Initial Catalog=ClassSRM;Integrated Security=True");
                    break;

                case 1:
                    Config.AddUpdateAppSettings("ConServer", @"Data Source=.\SQLExpress;Initial Catalog=ClassSRM;Integrated Security=True");
                    break;

                case 2:
                    Config.AddUpdateAppSettings("ConServer", @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ClassSRM.mdf;Integrated Security=True");
                    break;
            }
            Config.AddUpdateAppSettings("IsConServer", "true");
        }

        private void ConfigServer_FormClosed(object sender, FormClosedEventArgs e)
        {
            var isConServer = Config.ReadSetting("IsConServer");
            if (isConServer == "false")
                Environment.Exit(0);
        }
    }
}