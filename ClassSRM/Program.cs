
/****************************** ghost1372.github.io ******************************\
*	Module Name:	Program.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:29 ب.ظ
*	
***********************************************************************************/

using ClassSRM.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace ClassSRM
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //Set Culture to Farsi for Localization
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fa");
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;

            //Force to Run Only One exe
            bool instancecountone = false;
            Mutex mtx = new Mutex(true, Application.ProductName, out instancecountone);
            if (instancecountone)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                BonusSkins.Register();
                SkinManager.EnableFormSkins();
                UserLookAndFeel.Default.SetSkinStyle(Config.ReadSetting("Skin"));

                //Set Login
                Application.Run(new Login());
                Application.DoEvents();
                mtx.ReleaseMutex();
            }
            else
            {
                XtraMessageBox.Show("برنامه درحال اجرا است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}