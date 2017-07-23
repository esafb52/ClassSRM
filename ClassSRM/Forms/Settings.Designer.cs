namespace ClassSRM.Forms
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.label1 = new System.Windows.Forms.Label();
            this.rdCore = new DevExpress.XtraEditors.RadioGroup();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAutoRun = new DevExpress.XtraEditors.CheckEdit();
            this.chkTopM = new DevExpress.XtraEditors.CheckEdit();
            this.chkLogin = new DevExpress.XtraEditors.CheckEdit();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
            this.tabPane1.SuspendLayout();
            this.tabNavigationPage1.SuspendLayout();
            this.tabNavigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdCore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoRun.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTopM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLogin.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPane1
            // 
            this.tabPane1.Controls.Add(this.tabNavigationPage1);
            this.tabPane1.Controls.Add(this.tabNavigationPage2);
            this.tabPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPane1.Location = new System.Drawing.Point(0, 0);
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage2,
            this.tabNavigationPage1});
            this.tabPane1.RegularSize = new System.Drawing.Size(369, 374);
            this.tabPane1.SelectedPage = this.tabNavigationPage2;
            this.tabPane1.Size = new System.Drawing.Size(369, 374);
            this.tabPane1.TabIndex = 0;
            this.tabPane1.Text = "tabPane1";
            // 
            // tabNavigationPage1
            // 
            this.tabNavigationPage1.Caption = "اصلی";
            this.tabNavigationPage1.Controls.Add(this.rdCore);
            this.tabNavigationPage1.Controls.Add(this.label2);
            this.tabNavigationPage1.Controls.Add(this.label1);
            this.tabNavigationPage1.Name = "tabNavigationPage1";
            this.tabNavigationPage1.Size = new System.Drawing.Size(369, 374);
            // 
            // tabNavigationPage2
            // 
            this.tabNavigationPage2.Caption = "عمومی";
            this.tabNavigationPage2.Controls.Add(this.label3);
            this.tabNavigationPage2.Controls.Add(this.chkLogin);
            this.tabNavigationPage2.Controls.Add(this.chkTopM);
            this.tabNavigationPage2.Controls.Add(this.chkAutoRun);
            this.tabNavigationPage2.Name = "tabNavigationPage2";
            this.tabNavigationPage2.Size = new System.Drawing.Size(351, 329);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 68);
            this.label1.TabIndex = 0;
            this.label1.Text = "لطفا سیستم ارزشیابی نرم افزار را از بین توصیفی یا عددی انتخاب کنید\r\n(بصورت پیشفرض" +
    " توصیفی فعال می باشد)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rdCore
            // 
            this.rdCore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdCore.Location = new System.Drawing.Point(9, 85);
            this.rdCore.Name = "rdCore";
            this.rdCore.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rdCore.Properties.Appearance.Options.UseBackColor = true;
            this.rdCore.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "سیستم عددی"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "سیستم توصیفی")});
            this.rdCore.Size = new System.Drawing.Size(350, 96);
            this.rdCore.TabIndex = 1;
            this.rdCore.SelectedIndexChanged += new System.EventHandler(this.rdCore_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(9, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(353, 47);
            this.label2.TabIndex = 0;
            this.label2.Text = "تنظیمات بصورت خودکار ذخیره می شود";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkAutoRun
            // 
            this.chkAutoRun.Location = new System.Drawing.Point(20, 19);
            this.chkAutoRun.Name = "chkAutoRun";
            this.chkAutoRun.Properties.AllowFocused = false;
            this.chkAutoRun.Properties.Caption = "اجرای خودکار با روشن شدن ویندوز";
            this.chkAutoRun.Size = new System.Drawing.Size(314, 19);
            this.chkAutoRun.TabIndex = 0;
            this.chkAutoRun.CheckedChanged += new System.EventHandler(this.chkAutoRun_CheckedChanged);
            // 
            // chkTopM
            // 
            this.chkTopM.Location = new System.Drawing.Point(19, 59);
            this.chkTopM.Name = "chkTopM";
            this.chkTopM.Properties.AllowFocused = false;
            this.chkTopM.Properties.Caption = "بالا بودن پنجره اصلی روی پنجره های دیگر";
            this.chkTopM.Size = new System.Drawing.Size(315, 19);
            this.chkTopM.TabIndex = 0;
            this.chkTopM.CheckedChanged += new System.EventHandler(this.chkTopM_CheckedChanged);
            // 
            // chkLogin
            // 
            this.chkLogin.Location = new System.Drawing.Point(19, 103);
            this.chkLogin.Name = "chkLogin";
            this.chkLogin.Properties.AllowFocused = false;
            this.chkLogin.Properties.Caption = "ورود به نرم افزار با استفاده از رمز عبور";
            this.chkLogin.Size = new System.Drawing.Size(315, 19);
            this.chkLogin.TabIndex = 0;
            this.chkLogin.CheckedChanged += new System.EventHandler(this.chkLogin_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(9, 275);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(335, 47);
            this.label3.TabIndex = 1;
            this.label3.Text = "تنظیمات بصورت خودکار ذخیره می شود";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 374);
            this.Controls.Add(this.tabPane1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تنظیمات برنامه";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
            this.tabPane1.ResumeLayout(false);
            this.tabNavigationPage1.ResumeLayout(false);
            this.tabNavigationPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdCore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoRun.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTopM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLogin.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.RadioGroup rdCore;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.CheckEdit chkTopM;
        private DevExpress.XtraEditors.CheckEdit chkAutoRun;
        private DevExpress.XtraEditors.CheckEdit chkLogin;
        private System.Windows.Forms.Label label3;
    }
}