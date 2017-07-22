using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using System.Threading;
using System.Globalization;
using DevExpress.XtraEditors;

namespace ClassSRM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
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
                UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

                //Set Login
                Application.Run(new Form1());
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
