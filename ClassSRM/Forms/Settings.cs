using System;
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
    public partial class Settings : DevExpress.XtraEditors.XtraForm
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void rdCore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdCore.SelectedIndex==0)
            {
                Config.AddUpdateAppSettings("Tosifi System", "true");
            }
            else
            {
                Config.AddUpdateAppSettings("Tosifi System", "false");
            }
        }

        private void chkAutoRun_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRun.Checked)
            {
                Config.AddUpdateAppSettings("Auto Start", "true");
                Config.RegisterInStartup(true);
            }
            else
            {
                Config.AddUpdateAppSettings("Auto Start", "false");
                Config.RegisterInStartup(false);
            }
        }

        private void chkTopM_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTopM.Checked)
                this.TopMost = true;
            else
                this.TopMost = false;
        }

        private void chkLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLogin.Checked)
            {
                Config.AddUpdateAppSettings("Login", "true");
            }
            else
            {
                Config.AddUpdateAppSettings("Login", "false");
            }
        }

        //Check Config file
        private void CheckConfig()
        {
            var login = Config.ReadSetting("Login");
            var startup = Config.ReadSetting("Auto Start");
            var tosifi = Config.ReadSetting("Tosifi System");
            var font = Config.ReadSetting("Font");
            var fontFamily = Config.ReadSetting("FontFamily");
            var fontSize = Config.ReadSetting("FontSize");

            if (login == "true")
                chkLogin.Checked = true;
            if (startup == "true")
                chkAutoRun.Checked = true;
            if (tosifi == "true")
                rdCore.SelectedIndex = 0;
            else
                rdCore.SelectedIndex = 1;
            if (font == "Custome")
                chkFont.Checked = true;

            fontEdit1.Text = fontFamily;
            fSize.Value = Convert.ToInt32(fontSize);
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            CheckConfig();
        }

        private void chkFont_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFont.Checked)
            {
                fontEdit1.Enabled = true;
                Config.AddUpdateAppSettings("Font", "Custome");
                DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new Font(fontEdit1.Text, fSize.Value);
            }
            else
            {
                fontEdit1.Enabled = false;
                Config.AddUpdateAppSettings("Font", "Default");
                Config.AddUpdateAppSettings("FontFamily", "Tahoma");
                Config.AddUpdateAppSettings("FontSize", "8");
                DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new Font("Tahoma", 8);
            }
        }

        private void fontEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new Font(fontEdit1.Text, fSize.Value);
            Config.AddUpdateAppSettings("FontFamily", fontEdit1.Text);
            Config.AddUpdateAppSettings("FontSize", fSize.Value.ToString());
        }

        private void fSize_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new Font(fontEdit1.Text, fSize.Value);
            Config.AddUpdateAppSettings("FontFamily", fontEdit1.Text);
            Config.AddUpdateAppSettings("FontSize", fSize.Value.ToString());
        }
    }
}