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
using System.Xml.Linq;
using System.Xml;

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
            if (rdCore.SelectedIndex==1)
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
                rdCore.SelectedIndex = 1;
            else
                rdCore.SelectedIndex = 0;
            if (font == "Custome")
                chkFont.Checked = true;

            fontEdit1.Text = fontFamily;
            fSize.Value = Convert.ToInt32(fontSize);
            if (!System.IO.File.Exists("Activity.xml"))
                emptyXml();
        }
        void emptyXml()
        {
            new XDocument(
            new XElement("Activity",
                new XElement("ActivityPoint")
                )
        )
                .Save("Activity.xml");
        }
        private void Settings_Load(object sender, EventArgs e)
        {
            CheckConfig();
            getAllItemsName();
        }

        private void chkFont_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFont.Checked)
            {
                fontEdit1.Enabled = true;
                fSize.Enabled = true;
                Config.AddUpdateAppSettings("Font", "Custome");
                DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new Font(fontEdit1.Text, fSize.Value);
            }
            else
            {
                fontEdit1.Enabled = false;
                fSize.Enabled = false;
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
        private void getAllItemsName()
        {
            XDocument doc = XDocument.Load("Activity.xml");
            var items = doc
                .Element(XName.Get("Activity"))
                .Elements(XName.Get("ActivityPoint"));
            var itemName = items.Select(ele => ele.Element(XName.Get("Name")).Value);
            foreach (string name in itemName)
            {
                cmbActivity.Properties.Items.Add(name);
            }
            cmbActivity.SelectedIndex = 0;
        }

        private void createActivity(string pID, string pName, string pValue)
        {
            XDocument doc = XDocument.Load("Activity.xml");
            XElement item = doc.Element("Activity");
            item.Add(
            new XElement(
                        new XElement("ActivityPoint",
                            new XAttribute("id", pID),
                            new XAttribute("Name", pName),
                            new XElement("Name", pName),
                            new XElement("Value", pValue),
                            new XElement("id", pID)
                        )
           ));
            doc.Save("Activity.xml");
        }
        private int getLastId()
        {
            XDocument doc = XDocument.Load("Activity.xml");
            var res = (from v in doc.Descendants("id")
                       select v).LastOrDefault();
            if (res != null)
                return Convert.ToInt32(res.Value);
            else
                return 0;
        }
       
        private int getValeofItem(string itemName)
        {
            XDocument doc = XDocument.Load("Activity.xml");
            var values = doc.Descendants("ActivityPoint")
                .Where(i => i.Element("Name").Value == itemName)
                .Select(i => i.Element("Value").Value).FirstOrDefault();
            if (values != null)
                return Convert.ToInt32(values);
            else
                return 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            createActivity((getLastId()+1).ToString(), txtTitle.Text, txtValue.Value.ToString());
            cmbActivity.Properties.Items.Clear();
            getAllItemsName();
            XtraMessageBox.Show("فعالیت موردنظر با موفقیت اضافه شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            XDocument doc = XDocument.Load("Activity.xml");
            var q = from node in doc.Descendants("ActivityPoint")
                    let attr = node.Attribute("Name")
                    where attr != null && attr.Value == cmbActivity.Text
                    select node;
            if (q != null)
            {
                q.ToList().ForEach(x => x.Remove());
                cmbActivity.Properties.Items.Clear();
                getAllItemsName();
                XtraMessageBox.Show("فعالیت موردنظر با موفقیت حذف شد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                doc.Save("Activity.xml");
            }
            else
            {
                XtraMessageBox.Show("فعالیتی برای حذف وجود ندارد", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void cmbActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValue.Value = getValeofItem(cmbActivity.Text);
        }
    }
}