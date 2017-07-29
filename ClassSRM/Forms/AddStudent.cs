using DevExpress.XtraEditors;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM.Forms
{
    public partial class AddStudent : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext dc = new ClassSRMDataContext();

        public AddStudent()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] imageData = ReadImageFile(img.GetLoadedImageLocation());
                dc.InsertStudent((int)cmbClass.EditValue, txtName.Text, txtLName.Text, txtFName.Text, cmbGender.Text, imageData);
                XtraMessageBox.Show("دانش آموز موردنظر با موفقیت ثبت شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Text = string.Empty;
                txtFName.Text = string.Empty;
                txtLName.Text = string.Empty;
                img.Image = null;
                txtName.Select();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ClassSRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //تبدیل عکس به بایت برای ذخیره در دیتابیس
        public byte[] ReadImageFile(string imageLocation)
        {
            byte[] imageData = null;
            FileInfo fileInfo = new FileInfo(imageLocation);
            long imageFileLength = fileInfo.Length;
            FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageData = br.ReadBytes((int)imageFileLength);
            return imageData;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            img.LoadImage();
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            tblSchoolBindingSource.DataSource = from v in dc.tbl_Schools select v;
        }

        private void AddStudent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}