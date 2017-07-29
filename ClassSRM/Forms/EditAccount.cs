using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class EditAccount : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext dc = new ClassSRMDataContext();

        private SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider();
        private string Hash = string.Empty;
        private Byte[] byte1;
        private Byte[] byte2;

        public EditAccount()
        {
            InitializeComponent();
        }

        private void EditAccount_Load(object sender, EventArgs e)
        {
            tblUserBindingSource.DataSource = from v in dc.tbl_Users select v;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.RowCount != 0)
            {
                txtUserName.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Name").ToString();
                txtPass.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Pass").ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount != 0)
            {
                int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                byte1 = UTF8Encoding.UTF8.GetBytes(txtPass.Text);
                byte2 = sha.ComputeHash(byte1);
                Hash = BitConverter.ToString(byte2);
                dc.UpdateUser(id, txtUserName.Text, Hash);
                tblUserBindingSource.EndEdit();
                dc = new ClassSRMDataContext();
                XtraMessageBox.Show("اطلاعات با موفقیت بروزرسانی شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EditAccount_Load(null, null);
            }
            else
            {
                XtraMessageBox.Show("حسابی برای ویرایش وجود ندارد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount != 0)
            {
                int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                dc.DeleteUser(id);
                txtPass.Text = string.Empty;
                txtUserName.Text = string.Empty;
                XtraMessageBox.Show("حساب کاربری با موفقیت حذف شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EditAccount_Load(null, null);
            }
            else
            {
                XtraMessageBox.Show("حسابی برای حذف وجود ندارد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}