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
            btnCreate.Enabled = true;
            Config.AddUpdateAppSettings("IsConServer", "true");
            
        }

        private void ConfigServer_FormClosed(object sender, FormClosedEventArgs e)
        {
            var isConServer = Config.ReadSetting("IsConServer");
            if (isConServer == "false")
                Environment.Exit(0);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                Config.ExecuteScript();
            }
            catch (Exception)
            {

                XtraMessageBox.Show("مطمئن شوید که فایل اسکریپت موجود باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Config.DelScript();
            }
        }
    }
}