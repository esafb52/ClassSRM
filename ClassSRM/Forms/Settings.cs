using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

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
            if (rdCore.SelectedIndex == 1)
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

            if (login == "true")
                chkLogin.Checked = true;
            if (startup == "true")
                chkAutoRun.Checked = true;
            if (tosifi == "true")
                rdCore.SelectedIndex = 1;
            else
                rdCore.SelectedIndex = 0;

            if (!System.IO.File.Exists("Activity.xml"))
                emptyXml();
        }

        private void emptyXml()
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
            createActivity((getLastId() + 1).ToString(), txtTitle.Text, txtValue.Value.ToString());
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

        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void btnConServer_Click(object sender, EventArgs e)
        {
            new ConfigServer().ShowDialog();
        }
    }
}