using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Security.Cryptography;

namespace ClassSRM.Forms
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        private SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider();
        private string Hash = string.Empty;
        private Byte[] byte1;
        private Byte[] byte2;

        public Login()
        {
            InitializeComponent();

            //**********    Update Database if Script Exist    *********\\

            Config.ExecuteScript();
            Config.DelScript();

            //          *******************************************      \\

            //Todo: Change Temp Folder Path
            ////**********    TEMP FOLDER FOR LETTER GENERATER      **********\\
            //string temp = Application.StartupPath + @"\temp";
            //if (!System.IO.Directory.Exists(temp))
            //    System.IO.Directory.CreateDirectory(temp);

            //Read Login Config
            var login = Config.ReadSetting("Login");
            if (login == "false")
            {
                this.Hide();
                new Form1().ShowDialog();
            }
            else if (login == "0")
            {
                this.Hide();
                new Form1().ShowDialog();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUserName.Select();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //Generate Password Hash and Check Login Data 
                var dc = new ClassSRMDataContext();
                byte1 = UTF8Encoding.UTF8.GetBytes(txtPass.Text);
                byte2 = sha.ComputeHash(byte1);
                Hash = BitConverter.ToString(byte2);
                var checkLogin = (from v in dc.tbl_Users where v.Name == txtUserName.Text && v.Pass == Hash select v);

                if (checkLogin.Count() > 0)
                {
                    this.Hide();
                    new Form1().ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("مشخصات کاربری اشتباه است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Select();
                    txtPass.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}