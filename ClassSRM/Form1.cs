﻿using ArazUpdater;
using ClassSRM.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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

        //Data Context
        private ClassSRMDataContext dc = new ClassSRMDataContext();

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
            Config.AddUpdateAppSettings("Default School", cmbClass.EditValue.ToString());
            LoadStudent();
        }

        //Custome Persian Date
        private void CreateCustomDate()
        {
            strCurMonth = pc.GetMonth(DateTime.Now).ToString("00");
            strYear = pc.GetYear(DateTime.Now).ToString("0000");
            PrevMonth = Convert.ToInt32(strCurMonth) - 1;
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

                    
                    SplashScreenManager.ShowForm(typeof(WaitForm1));
                    int id = (int)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                    var qActivity = (from v in dc.tbl_ActPoints where v.StudentId == id select v.Score).Sum().GetValueOrDefault(0); //دریافت مجموع امتیاز فعالیت ها
                    var qbitAct = (from v in dc.tbl_Checks where v.StudentId == id && v.Exist == true select v.Exist).Count();  //دریافت حضور فعال
                    var qbitDeAct = (from v in dc.tbl_Checks where v.StudentId == id && v.Exist == false select v.Exist).Count();   //دریافت حضور غیرفعال

                    var qEvaPoint = (from v in dc.tbl_EvaPoints where v.StudentId == id select v.Score).Sum(); //دریافت مجموع امتیاز درس ها

                    var queryWin = (from T in ((from tbl_ActPoints in dc.tbl_ActPoints
                                                select new
                                                {
                                                    tbl_ActPoints.StudentId,
                                                    tbl_ActPoints.Score
                                                }))
                                    group T by new
                                    {
                                        T.StudentId
                                    } into g
                                    orderby
                                      ((System.Int32?)g.Sum(p => p.Score).GetValueOrDefault(0) ?? (System.Int32?)0) descending
                                    select new
                                    {
                                        g.Key.StudentId,
                                        HighScore = ((System.Int32?)g.Sum(p => p.Score).GetValueOrDefault(0) ?? (System.Int32?)0)
                                    }).Take(1);

                    var qFarsi = from v in dc.SumEvas where v.Book == "بخوانیم و بنویسیم" && v.StudentId == id select v;
                    if (qFarsi.Any() == false)
                    {
                        prgFarsi.Value = 0;
                        lblFarsi.Text = "0";
                    }
                    else
                    {
                        prgFarsi.Value = qFarsi.FirstOrDefault().HighScoreUser;
                        lblFarsi.Text = qFarsi.FirstOrDefault().HighScoreUser.ToString();
                    }
                    var qWork = from v in dc.SumEvas where v.Book == "کار و فناوری" && v.StudentId == id select v;
                    if (qWork.Any() == false)
                    {
                        prgWorkLife.Value = 0;
                        lblWorkLife.Text = "0";
                    }
                    else
                    {
                        prgWorkLife.Value = qWork.FirstOrDefault().HighScoreUser;
                        lblWorkLife.Text = qWork.FirstOrDefault().HighScoreUser.ToString();
                    }
                    var qQuran = from v in dc.SumEvas where v.Book == "قرآن" && v.StudentId == id select v;
                    if (qQuran.Any() == false)
                    {
                        prgQuran.Value = 0;
                        lblQuran.Text = "0";
                    }
                    else
                    {
                        prgQuran.Value = qQuran.FirstOrDefault().HighScoreUser;
                        lblQuran.Text = qQuran.FirstOrDefault().HighScoreUser.ToString();
                    }

                    var qLife = from v in dc.SumEvas where v.Book == "مهارت های زندگی و تربیتی" && v.StudentId == id select v;
                    if (qLife.Any() == false)
                    {
                        prgLife.Value = 0;
                        lblLife.Text = "0";
                    }
                    else
                    {
                        prgLife.Value = qLife.FirstOrDefault().HighScoreUser;
                        lblLife.Text = qLife.FirstOrDefault().HighScoreUser.ToString();
                    }
                    var qTafakor = from v in dc.SumEvas where v.Book == "تفکر" && v.StudentId == id select v;
                    if (qTafakor.Any() == false)
                    {
                        prgTafakor.Value = 0;
                        lblTafakor.Text = "0";
                    }
                    else
                    {
                        prgTafakor.Value = qTafakor.FirstOrDefault().HighScoreUser;
                        lblTafakor.Text = qTafakor.FirstOrDefault().HighScoreUser.ToString();
                    }

                    var qHedye = from v in dc.SumEvas where v.Book == "هدیه های آسمانی" && v.StudentId == id select v;
                    if (qHedye.Any() == false)
                    {
                        prgHedye.Value = 0;
                        lblHedye.Text = "0";
                    }
                    else
                    {
                        prgHedye.Value = qHedye.FirstOrDefault().HighScoreUser;
                        lblHedye.Text = qHedye.FirstOrDefault().HighScoreUser.ToString();
                    }

                    var qEmla = from v in dc.SumEvas where v.Book == "املا/انشا" && v.StudentId == id select v;
                    if (qEmla.Any() == false)
                    {
                        prgEmla.Value = 0;
                        lblEmla.Text = "0";
                    }
                    else
                    {
                        prgEmla.Value = qEmla.FirstOrDefault().HighScoreUser;
                        lblEmla.Text = qEmla.FirstOrDefault().HighScoreUser.ToString();
                    }

                    var qEjtemayi = from v in dc.SumEvas where v.Book == "اجتماعی" && v.StudentId == id select v;
                    if (qEjtemayi.Any() == false)
                    {
                        prgEjtemayi.Value = 0;
                        lblEjtemayi.Text = "0";
                    }
                    else
                    {
                        prgEjtemayi.Value = qEjtemayi.FirstOrDefault().HighScoreUser;
                        lblEjtemayi.Text = qEjtemayi.FirstOrDefault().HighScoreUser.ToString();
                    }

                    var qOlom = from v in dc.SumEvas where v.Book == "علوم" && v.StudentId == id select v;
                    if (qOlom.Any() == false)
                    {
                        prgOlom.Value = 0;
                        lblOlom.Text = "0";
                    }
                    else
                    {
                        prgOlom.Value = qOlom.FirstOrDefault().HighScoreUser;
                        lblOlom.Text = qOlom.FirstOrDefault().HighScoreUser.ToString();
                    }

                    var qRiazi = from v in dc.SumEvas where v.Book == "ریاضی" && v.StudentId == id select v;
                    if (qRiazi.Any() == false)
                    {
                        prgRiazi.Value = 0;
                        lblRiazi.Text = "0";
                    }
                    else
                    {
                        prgRiazi.Value = qRiazi.FirstOrDefault().HighScoreUser;
                        lblRiazi.Text = qRiazi.FirstOrDefault().HighScoreUser.ToString();
                    }
                    SplashScreenManager.CloseDefaultWaitForm();

                    var diffrence = (100 - (qActivity * 100) / (queryWin.FirstOrDefault().HighScore)).ToString();

                    lblDifference.Text = diffrence;
                    if (lblDifference.Text == "0")
                        lblDifference.Text = "برتر";
                    prgDifference.Value = Convert.ToInt32(diffrence);
                    lblActive.Text = qbitAct.ToString();
                    prgActive.Value = qbitAct;
                    lblDeAct.Text = qbitDeAct.ToString();
                    prgDeActive.Value = qbitDeAct;

                    if (qEvaPoint == null)
                    {
                        lblEvaPoint.Text = "0";
                        prgActiveClass.Value = 0;
                    }
                    else
                    {
                        lblEvaPoint.Text = qEvaPoint.ToString();
                        prgActiveClass.Value = qEvaPoint.Value;
                    }
                    //رفع خطای خالی بودن امتیاز ها
                    if (qActivity.ToString() == null)
                    {
                        prgTotalScore.Value = 0;
                        lblActivity.Text = "0";
                    }
                    else
                    {
                        lblActivity.Text = qActivity.ToString();
                        prgTotalScore.Value = (float)qActivity;
                    }

                    CreateCustomDate();
                    sumEvaBindingSource.DataSource = dc.SelectSumEvaDate(id, strYear + "/" + strCurMonth + "/" + "01", strYear + "/" + strCurMonth + "/" + "31");
                    sumEvaBindingSource1.DataSource = dc.SelectSumEvaDate(id, strYear + "/" + PrevMonth.ToString("00") + "/" + "01", strYear + "/" + PrevMonth.ToString("00") + "/" + "31");
                }
            }
            catch (Exception)
            {
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
            int id = Convert.ToInt32(Config.ReadSetting("Default School"));
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            tblStudentBindingSource.DataSource = from v in dc.tbl_Students where v.StuClassId == id select v;
            SplashScreenManager.CloseDefaultWaitForm();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tblSchoolBindingSource.DataSource = dc.SelectSchool();
            cmbClass.ItemIndex = Convert.ToInt32(Config.ReadSetting("Default School"));
            LoadStudent();
            EnableAnim();
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
            System.Diagnostics.Process.Start("https://github.com/ghost1372");
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
            Backup.isForce = true;
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
            Backup.isForce = true;
            new Backup().ShowDialog();
            Update up = new Update();
            if (up.UpdateFromDisk(Application.ExecutablePath) == true)
                Application.Exit();
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