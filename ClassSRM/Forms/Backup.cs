using DevExpress.XtraEditors;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class Backup : DevExpress.XtraEditors.XtraForm
    {
        private PersianCalendar PC = new PersianCalendar();
        private string strShamsi;
        private bool isWorking = false;
        public static bool isForce = false;

        public Backup()
        {
            InitializeComponent();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            strShamsi = PC.GetYear(DateTime.Now).ToString("0000") + "_" + PC.GetMonth(DateTime.Now).ToString("00") + "_" + PC.GetDayOfMonth(DateTime.Now).ToString("00");
            string strFileName = string.Empty;
            saveFileDialog1.DefaultExt = "BAK";
            saveFileDialog1.FileName = "BackupFile" + strShamsi;
            saveFileDialog1.Filter = @"SQL Backup files (*.BAK) |*.BAK|All files(*.*) |*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Title = "Backup SQL File";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    isWorking = true;
                    btnBackup.Enabled = false;
                    splashScreenManager1.ShowWaitForm();
                    strFileName = saveFileDialog1.FileName;
                    txtPath.Text = strFileName;
                    Cursor.Current = Cursors.WaitCursor;
                    //Connect to DB
                    SqlConnection connect;
                    string con = Properties.Settings.Default.ClassSRMConnectionString;
                    connect = new SqlConnection(con);
                    connect.Open();
                    //Execute
                    SqlCommand command;
                    command = new SqlCommand(@"backup database ClassSRM to disk ='" + strFileName + "' with init,stats=10", connect);
                    command.ExecuteNonQuery();
                    connect.Close();
                    splashScreenManager1.CloseWaitForm();
                    XtraMessageBox.Show("پشتیبان گیری با موفقیت انجام شد", "پشتیبان گیری", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBackup.Enabled = true;
                    isWorking = false;
                    if (isForce)
                    {
                        isForce = false;
                        Close();
                    }
                }
                catch (Exception)
                {
                    splashScreenManager1.CloseWaitForm();
                    XtraMessageBox.Show("لطفا درایو دیگری بجز درایو C انتخاب کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnBackup.Enabled = true;
                    isWorking = false;
                }
            }
        }

        private async void btnRestore_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("آیا برای بازگردانی اطلاعات از فایل پشتیبان اطمینان دارید؟ ", "تایید بازگردانی اطلاعات", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string strFileName = string.Empty;
                Cursor.Current = Cursors.WaitCursor;
                openFileDialog1.Filter = @"SQL Backup files (*.BAK) |*.BAK|All files(*.*) |*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.Title = "Restore SQL File";
                openFileDialog1.FileName = "";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    isWorking = true;
                    btnRestore.Enabled = false;
                    XtraMessageBox.Show("بازگردانی اطلاعات ممکن است مدتی طول بکشد تا پایان عملیات منتظر بمانید!", "بازگردانی", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    strFileName = openFileDialog1.FileName;
                    splashScreenManager1.ShowWaitForm();
                    await RestoreDB(strFileName);
                    splashScreenManager1.CloseWaitForm();
                    XtraMessageBox.Show("بازگردانی با موفقیت انجام شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isWorking = false;
                    btnRestore.Enabled = true;
                }
            }
        }

        private Task RestoreDB(string strFileName)
        {
            var res = Task.Run(() =>
            {
                SqlConnection connect;
                string con = Properties.Settings.Default.ClassSRMConnectionString;
                connect = new SqlConnection(con);
                connect.Open();
                //Excute
                SqlCommand command;
                command = new SqlCommand(@"ALTER DATABASE ClassSRM SET SINGLE_USER WITH ROLLBACK IMMEDIATE USE MASTER RESTORE DATABASE ClassSRM FROM DISK = '" + strFileName + "' WITH REPLACE", connect);
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
                connect.Close();
            });
            return res;
        }

        private void Backup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isWorking)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
            if (isForce)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void Backup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}