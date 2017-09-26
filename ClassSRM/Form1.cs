/****************************** ghost1372.github.io ******************************\
*	Module Name:	Form1.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:29 ب.ظ
*
***********************************************************************************/

using ArazUpdater;
using ClassSRM.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassSRM
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        //Get Persian Date
        private PersianCalendar pc = new PersianCalendar();

        private string strCurMonth, strYear;
        private int PrevMonth;
        private int id;

        //Data Context
        private ClassSRMDataContext dc = new ClassSRMDataContext(Config.connection);

        public Form1()
        {
            InitializeComponent();

            //Get Skin from Config and App Version
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(sRGB, true, true);
            sRGB.GalleryItemClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(rgbi_GalleryItemClick);
            if (Config.ReadSetting("Skin") != "0")
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Config.ReadSetting("Skin"));

            this.Text += ProductVersion;
        }

        //Set Skin Name to Config
        private void rgbi_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            Config.AddUpdateAppSettings("Skin", DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName);
        }

        //Load Student and Set Default School to Config
        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            Config.AddUpdateAppSettings("Default School", cmbClass.ItemIndex.ToString());
            LoadStudent();
            gridView1_FocusedRowChanged(null, null);
        }

        //Custome Persian Date
        private void CreateCustomDate()
        {
            strCurMonth = pc.GetMonth(DateTime.Now).ToString("00");
            strYear = pc.GetYear(DateTime.Now).ToString("0000");
            PrevMonth = Convert.ToInt32(strCurMonth) - 1;
        }

        private void initScheduler()
        {
            DBAppointmentList apts = new DBAppointmentList();
            apts.AddRange(dc.DBAppointments.ToArray());

            List<DBResource> resources = new List<DBResource>();
            resources.AddRange(dc.DBResources.ToArray());

            AppointmentStorage aptStorage = schedulerStorage1.Appointments;
            ResourceStorage resStorage = schedulerStorage1.Resources;

            aptStorage.Mappings.AllDay = "AllDay";
            aptStorage.Mappings.Description = "Description";
            aptStorage.Mappings.End = "EndTime";
            aptStorage.Mappings.Label = "Label";
            aptStorage.Mappings.Location = "Location";
            aptStorage.Mappings.RecurrenceInfo = "RecurrenceInfo";
            aptStorage.Mappings.ReminderInfo = "ReminderInfo";
            aptStorage.Mappings.ResourceId = "CarIdShared";
            aptStorage.Mappings.Start = "StartTime";
            aptStorage.Mappings.Status = "Status";
            aptStorage.Mappings.Subject = "Subject";
            aptStorage.Mappings.Type = "EventType";

            resStorage.Mappings.Id = "ID";
            resStorage.Mappings.Caption = "Model";
            resStorage.Mappings.Image = "Picture";

            resStorage.DataSource = resources;
            aptStorage.DataSource = apts;
            schedulerControl1.GoToToday();
        }

        //Get Student Statictic Scores and Data
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridView1.RowCount > 0)
                {
                    byte[] data = ((System.Data.Linq.Binary)(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StuImage"))).ToArray();
                    var stream = new MemoryStream(data);
                    imgStudent.Image = Image.FromStream(stream);

                    id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");

                    var qActivity = dc.SelectQActivity(id).First().Score; //دریافت مجموع امتیاز فعالیت ها
                    lblActivity.Text = qActivity.ToString();
                    prgActiveClass.Value = qActivity;

                    var Exist = dc.SelectExistCheck(id).First().Count;  //دریافت حضور فعال
                    lblActive.Text = Exist.ToString();
                    prgActive.Value = Exist;

                    var NotExist = dc.SelectNOTExistCheck(id).First().Count;   //دریافت حضور غیرفعال
                    lblDeAct.Text = NotExist.ToString();
                    prgDeActive.Value = NotExist;

                    var qEvaPoint = dc.SelectSumEvaPoint(id).First().SUMEVA; //دریافت مجموع امتیاز درس ها

                    lblEvaPoint.Text = qEvaPoint.ToString();
                    prgTotalScore.Value = qEvaPoint;

                    var queryWin = dc.SelectQueryWin().First();
                    if (qActivity != 0 && queryWin.HighScore != 0)
                    {
                        var diffrence = (100 - (qActivity * 100) / (queryWin.HighScore));
                        lblDifference.Text = diffrence.ToString();
                        if (lblDifference.Text == "0")
                            lblDifference.Text = "برتر";
                        prgDifference.Value = diffrence;
                    }
                    else
                    {
                        lblDifference.Text = "0";
                        if (lblDifference.Text == "0")
                            lblDifference.Text = "برتر";
                        prgDifference.Value = 0;
                    }

                    var qFarsi = dc.SelectSumBook(id, "بخوانیم و بنویسیم").First();
                    prgFarsi.Value = qFarsi.HighScoreUser;
                    lblFarsi.Text = qFarsi.HighScoreUser.ToString();

                    var qWork = dc.SelectSumBook(id, "کار و فناوری").First();
                    prgWorkLife.Value = qWork.HighScoreUser;
                    lblWorkLife.Text = qWork.HighScoreUser.ToString();

                    var qQuran = dc.SelectSumBook(id, "قرآن").First();
                    prgQuran.Value = qQuran.HighScoreUser;
                    lblQuran.Text = qQuran.HighScoreUser.ToString();

                    var qLife = dc.SelectSumBook(id, "مهارت های زندگی و تربیتی").First();
                    prgLife.Value = qLife.HighScoreUser;
                    lblLife.Text = qLife.HighScoreUser.ToString();

                    var qTafakor = dc.SelectSumBook(id, "تفکر").First();
                    prgTafakor.Value = qTafakor.HighScoreUser;
                    lblTafakor.Text = qTafakor.HighScoreUser.ToString();

                    var qHedye = dc.SelectSumBook(id, "هدیه های آسمانی").First();
                    prgHedye.Value = qHedye.HighScoreUser;
                    lblHedye.Text = qHedye.HighScoreUser.ToString();

                    var qEmla = dc.SelectSumBook(id, "املا/انشا").First();
                    prgEmla.Value = qEmla.HighScoreUser;
                    lblEmla.Text = qEmla.HighScoreUser.ToString();

                    var qEjtemayi = dc.SelectSumBook(id, "اجتماعی").First();
                    prgEjtemayi.Value = qEjtemayi.HighScoreUser;
                    lblEjtemayi.Text = qEjtemayi.HighScoreUser.ToString();

                    var qOlom = dc.SelectSumBook(id, "علوم").First();
                    prgOlom.Value = qOlom.HighScoreUser;
                    lblOlom.Text = qOlom.HighScoreUser.ToString();

                    var qRiazi = dc.SelectSumBook(id, "ریاضی").First();
                    prgRiazi.Value = qRiazi.HighScoreUser;
                    lblRiazi.Text = qRiazi.HighScoreUser.ToString();

                    drawChart(id);
                }
            }
            catch (InvalidOperationException ex) { XtraMessageBox.Show(ex.Message); }
            //catch (TaskCanceledException ex) { XtraMessageBox.Show(ex.Message); }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }

        private void drawChart(int id)
        {
            CreateCustomDate();
            if (strCurMonth.Equals("01"))
            {
                int nyear = Convert.ToInt32(strYear) - 1;
                sumEvaBindingSource.DataSource = dc.SelectSumEvaDate(id, strYear + "/" + strCurMonth + "/" + "01", strYear + "/" + strCurMonth + "/" + "31");
                sumEvaBindingSource1.DataSource = dc.SelectSumEvaDate(id, nyear + "/" + "12" + "/" + "01", nyear + "/" + "12" + "/" + "31");
            }
            else
            {
                sumEvaBindingSource.DataSource = dc.SelectSumEvaDate(id, strYear + "/" + strCurMonth + "/" + "01", strYear + "/" + strCurMonth + "/" + "31");
                sumEvaBindingSource1.DataSource = dc.SelectSumEvaDate(id, strYear + "/" + PrevMonth.ToString("00") + "/" + "01", strYear + "/" + PrevMonth + "/" + "31");
            }
        }

        // isOne => if month is 01 we must set prevMonth 12 and prevYear - 1
        private void drawChart(int id, string Date1, string Date2, bool isOne)
        {
            if (isOne)
            {
                int nyear = Convert.ToInt32(strYear) - 1;
                sumEvaBindingSource.DataSource = dc.SelectSumEvaDate(id, strYear + "/" + Date1 + "/" + "01", strYear + "/" + Date1 + "/" + "31");
                sumEvaBindingSource1.DataSource = dc.SelectSumEvaDate(id, nyear + "/" + Date2 + "/" + "01", nyear + "/" + Date2 + "/" + "31");
            }
            else
            {
                sumEvaBindingSource.DataSource = dc.SelectSumEvaDate(id, strYear + "/" + Date1 + "/" + "01", strYear + "/" + Date1 + "/" + "31");
                sumEvaBindingSource1.DataSource = dc.SelectSumEvaDate(id, strYear + "/" + Date2 + "/" + "01", strYear + "/" + Date2 + "/" + "31");
            }
        }

        //Enable ProgressBar Animation
        private void EnableAnim()
        {
            prgActiveClass.EnableAnimation = true;
            prgTotalScore.EnableAnimation = true;
            prgActive.EnableAnimation = true;
            prgDeActive.EnableAnimation = true;
            prgDifference.EnableAnimation = true;
            prgFarsi.EnableAnimation = true;
            prgRiazi.EnableAnimation = true;
            prgOlom.EnableAnimation = true;
            prgHedye.EnableAnimation = true;
            prgQuran.EnableAnimation = true;
            prgTafakor.EnableAnimation = true;
            prgWorkLife.EnableAnimation = true;
            prgEjtemayi.EnableAnimation = true;
            prgEmla.EnableAnimation = true;
            prgLife.EnableAnimation = true;
        }

        //Load Student
        private void LoadStudent()
        {
            int count = (cmbClass.Properties.DataSource as IList).Count;
            if (count > 0)
            {
                int id = (int)cmbClass.EditValue;
                splashScreenManager2.ShowWaitForm();
                tblStudentBindingSource.DataSource = from v in dc.tbl_Students where v.StuClassId == id select v;
                splashScreenManager2.CloseWaitForm();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tblSchoolBindingSource.DataSource = dc.SelectSchool();
            cmbClass.ItemIndex = Convert.ToInt32(Config.ReadSetting("Default School"));
            LoadStudent();
            EnableAnim();
            pCalendar.DateTime = DateTime.Now;
            initScheduler();
            txtDate1.DateTime = DateTime.Now;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnSettings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new Settings().ShowDialog();
        }

        private void btnBackup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new Backup().ShowDialog();
        }

        private void btnDeveloper_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Splash.isAbout = true;
            try
            {
                SplashScreenManager.CloseForm();
            }
            catch { }
            SplashScreenManager.ShowForm(typeof(Splash));
        }

        private void btnLic_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraMessageBox.Show("کاربر عزیز،شما می توانید جهت حمایت از برنامه نویس این نرم افزار و پیشرفت بیشتر این برنامه\n با پرداخت مبالغ دلخواه از این برنامه حمایت کنید.با حمایت های شما تیم توسعه دلگرم شده و\n ویژگی های بیشتر و بهتری را به ارمغان خواهد آورد.", "حمایت مالی", MessageBoxButtons.OK, MessageBoxIcon.Information);
            System.Diagnostics.Process.Start("http://ipfile.ir/donation");
        }

        private void btnGit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraMessageBox.Show("در گیت هاب میتوانید پروژه های رایگان و متن باز دیگر ما را مشاهده کنید و در صورت داشتن توانایی، در توسعه بیشتر نرم افزار ها به ما کمک کنید.", "پروژه های توسعه دهنده", MessageBoxButtons.OK, MessageBoxIcon.Information);
            System.Diagnostics.Process.Start("https://github.com/ghost1372/ClassSRM");
        }

        private void btnTaskListKanban_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Task.Run(() => { new KanBanTask().ShowDialog(); });
        }

        private void btnLetterSingle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new Letter().ShowDialog();
        }

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraMessageBox.Show("قبل از بروزرسانی نرم افزار لازم است تا از اطلاعات\nنرم افزار فایل پشتیبان تهیه کنید.بعد از تایید\nپنجره پشتیبان گیری باز شده و اقدام به پشتیبان گیری کنید.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Backup.isUpdate = true;
            new Backup().ShowDialog();
            Update up = new Update();
            String txt = "http://ipfile.ir/ClassSRMServer/ClassSRM.txt";
            String ver = Application.ProductVersion;
            if (up.UpdateFromServer(txt, ver, Application.ExecutablePath) == true)
                Application.Exit();
        }

        private void btnOfflineUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraMessageBox.Show("قبل از بروزرسانی نرم افزار لازم است تا از اطلاعات\nنرم افزار فایل پشتیبان تهیه کنید.بعد از تایید\nپنجره پشتیبان گیری باز شده و اقدام به پشتیبان گیری کنید.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Backup.isUpdate = true;
            new Backup().ShowDialog();
            Update up = new Update();
            if (up.UpdateFromDisk(Application.ExecutablePath) == true)
                Application.Exit();
        }

        private void btnHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new History().ShowDialog();
        }

        private void btnAddSchool_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new AddSchool().ShowDialog();
            btnRefresh_Click(null, null);
        }

        private void btnAddStudent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new AddStudent().ShowDialog();
            btnRefresh_Click(null, null);
        }

        private void btnAddUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new UserAcount().ShowDialog();
        }

        private void btnDailyCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new DailyCheck().ShowDialog();
        }

        private void btnBookEva_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new PointBook().ShowDialog();
        }

        private void btnActPoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new PointActivity().ShowDialog();
        }

        private void btnQuastion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new Ask().ShowDialog();
        }

        private void btnTop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new TopStudent().ShowDialog();
        }

        private void btnGifts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new Gifts().ShowDialog();
        }

        private void btnProccess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new Process().ShowDialog();
        }

        private void btnCompare_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new Compare().ShowDialog();
        }

        private void btnStuChart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new CompareBarChart().ShowDialog();
        }

        private void btnEditUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new EditAccount().ShowDialog();
        }

        private void btnSchoolList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new Schoollist().ShowDialog();
            btnRefresh_Click(null, null);
        }

        private void btnStudentList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new StudentList().ShowDialog();
            btnRefresh_Click(null, null);
        }

        private void btnLstCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new DailyCheckList().ShowDialog();
            btnRefresh_Click(null, null);
        }

        private void btnActPointList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new PointActivityList().ShowDialog();
            btnRefresh_Click(null, null);
        }

        private void btnEvaPointList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new PointBookList().ShowDialog();
            btnRefresh_Click(null, null);
        }

        private void btnWebsite_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ipfile.ir/classsrm/");
        }

        private void btnShedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new Shedule().ShowDialog();
            Form1_Load(null, null);
        }

        #region "Scheduler Handler"

        private void schedulerStorage1_AppointmentsInserted(object sender, PersistentObjectsEventArgs e)
        {
            foreach (Appointment apt in e.Objects)
            {
                DBAppointment dbApt = (DBAppointment)apt.GetSourceObject(schedulerStorage1);
                dc.DBAppointments.InsertOnSubmit(dbApt);
                dc.SubmitChanges();
            }
        }

        private void schedulerStorage1_AppointmentsChanged(object sender, PersistentObjectsEventArgs e)
        {
            dc.SubmitChanges();
        }

        private void schedulerStorage1_AppointmentDeleting(object sender, PersistentObjectCancelEventArgs e)
        {
            Appointment apt = (Appointment)e.Object;
            DBAppointment dbApt = (DBAppointment)apt.GetSourceObject(schedulerStorage1);
            dc.DBAppointments.DeleteOnSubmit(dbApt);
            dc.SubmitChanges();
        }

        #endregion "Scheduler Handler"

        private void chkMFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMFilter.Checked)
            {
                txtDate1.Enabled = true;
                txtDate1_EditValueChanged(null, null);
            }
            else
            {
                txtDate1.Enabled = false;
                drawChart(id);
            }
        }

        private void txtDate1_EditValueChanged(object sender, EventArgs e)
        {
            string month = txtDate1.Text.Substring(5, 2);
            int prev;
            if (month.Equals("01"))
            {
                prev = 12;
                drawChart(id, month, prev.ToString("00"), true);
            }
            else
            {
                prev = Convert.ToInt32(month) - 1;
                drawChart(id, month, prev.ToString("00"), false);
            }
        }

        private void dockManager1_Expanded(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            int count = (cmbClass.Properties.DataSource as IList).Count;
            if (count > 0)
            {
                cmbClass.ItemIndex = Convert.ToInt32(Config.ReadSetting("Default School"));
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tblSchoolBindingSource.DataSource = dc.SelectSchool();
            cmbClass.ItemIndex = Convert.ToInt32(Config.ReadSetting("Default School"));
            LoadStudent();
            gridView1_FocusedRowChanged(null, null);
        }

        //Draw Persian Holiday to Calendar
        private void pCalendar_CustomDrawDayNumberCell(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            if (e.View != DevExpress.XtraEditors.Controls.DateEditCalendarViewType.MonthInfo) return;
            //return if a given date is not a holiday
            //in this case the default drawing will be performed (e.Handled is false)
            if (!Config.IsHoliday(e.Date)) return;
            //highlight the selected date
            if (e.Selected)
            {
                e.Graphics.FillRectangle(e.Style.GetBackBrush(e.Cache), e.Bounds);
            }
            //the brush for painting days
            Brush brush = (e.Inactive ? Brushes.LightPink : Brushes.OrangeRed);
            //specify formatting attributes for drawing text
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;
            //draw the day number

            DateTime dt = e.Date;
            var day = Config.PersianDate(dt).Substring(8, 2);
            e.Graphics.DrawString(day, e.Style.Font, brush, e.Bounds, strFormat);
            e.Handled = true;
        }
    }
}