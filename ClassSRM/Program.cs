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