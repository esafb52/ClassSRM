using DevExpress.LookAndFeel;
using DevExpress.XtraSplashScreen;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace ClassSRM
{
    public partial class Splash : SplashScreen
    {
        //Check Splash is for About or Not

        public static bool isAbout = false;

        public Splash()
        {
            InitializeComponent();

            //Get Application Version and Name
            lblVer.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

            //Close Splash if its About
            if (isAbout)
            {
                labelControl2.Text = "";
                CloseSplash();
            }
        }

        //Read and Set Skin from Config
        private UserLookAndFeel lookAndFeel;

        protected override DevExpress.LookAndFeel.UserLookAndFeel TargetLookAndFeel
        {
            get
            {
                if (lookAndFeel == null)
                {
                    if (Config.ReadSetting("Skin") != "0")
                    {
                        lookAndFeel = new UserLookAndFeel(this);
                        lookAndFeel.UseDefaultLookAndFeel = false;
                        lookAndFeel.SkinName = Config.ReadSetting("Skin");
                    }
                }
                return lookAndFeel;
            }
        }

        //Close Splash
        private async void CloseSplash()
        {
            await Task.Delay(5000);
            SplashScreenManager.CloseDefaultSplashScreen();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion Overrides

        public enum SplashScreenCommand
        {
        }
    }
}