namespace ClassSRM.Forms
{
    partial class Ask
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ask));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStuClassId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStuName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStuLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStuFName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.cmbBook = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbClass = new DevExpress.XtraEditors.LookUpEdit();
            this.tblSchoolBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.bsStudent = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBook.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblSchoolBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsStudent)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.cmbBook);
            this.layoutControl1.Controls.Add(this.cmbClass);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(594, 265, 450, 400);
            this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1022, 633);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bsStudent;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridControl1.Location = new System.Drawing.Point(16, 96);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1});
            this.gridControl1.Size = new System.Drawing.Size(990, 521);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colStuClassId,
            this.colStuName,
            this.colStuLName,
            this.colStuFName});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsImageLoad.AnimationType = DevExpress.Utils.ImageContentAnimationType.SegmentedFade;
            this.gridView1.OptionsImageLoad.DesiredThumbnailSize = new System.Drawing.Size(60, 60);
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowHeight = 60;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colStuClassId
            // 
            this.colStuClassId.FieldName = "StuClassId";
            this.colStuClassId.Name = "colStuClassId";
            // 
            // colStuName
            // 
            this.colStuName.Caption = "نام";
            this.colStuName.FieldName = "StuName";
            this.colStuName.Name = "colStuName";
            this.colStuName.Visible = true;
            this.colStuName.VisibleIndex = 0;
            this.colStuName.Width = 126;
            // 
            // colStuLName
            // 
            this.colStuLName.Caption = "نام خانوادگی";
            this.colStuLName.FieldName = "StuLName";
            this.colStuLName.Name = "colStuLName";
            this.colStuLName.Visible = true;
            this.colStuLName.VisibleIndex = 1;
            this.colStuLName.Width = 166;
            // 
            // colStuFName
            // 
            this.colStuFName.Caption = "نام پدر";
            this.colStuFName.FieldName = "StuFName";
            this.colStuFName.Name = "colStuFName";
            this.colStuFName.Visible = true;
            this.colStuFName.VisibleIndex = 2;
            this.colStuFName.Width = 159;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.EnableLODImages = true;
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.OptionsMask.MaskType = DevExpress.XtraEditors.Controls.PictureEditMaskType.Circle;
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSave.ImageOptions.SvgImage")));
            this.btnSave.Location = new System.Drawing.Point(382, 16);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 48);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 6;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbBook
            // 
            this.cmbBook.EditValue = "اجتماعی";
            this.cmbBook.Location = new System.Drawing.Point(448, 16);
            this.cmbBook.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbBook.Name = "cmbBook";
            this.cmbBook.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBook.Properties.Items.AddRange(new object[] {
            "اجتماعی",
            "املا/انشا",
            "بخوانیم و بنویسیم",
            "تفکر",
            "ریاضی",
            "علوم",
            "قرآن",
            "کار و فناوری",
            "مهارت های زندگی و تربیتی",
            "هدیه های آسمانی"});
            this.cmbBook.Properties.Sorted = true;
            this.cmbBook.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbBook.Size = new System.Drawing.Size(188, 22);
            this.cmbBook.StyleController = this.layoutControl1;
            this.cmbBook.TabIndex = 5;
            this.cmbBook.SelectedIndexChanged += new System.EventHandler(this.cmbBook_SelectedIndexChanged);
            // 
            // cmbClass
            // 
            this.cmbClass.Location = new System.Drawing.Point(726, 16);
            this.cmbClass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbClass.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 33, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SchName", "مدرسه", 57, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SchClass", "کلاس/پایه", 55, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SchDate", "سال تحصیلی", 53, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.cmbClass.Properties.DataSource = this.tblSchoolBindingSource;
            this.cmbClass.Properties.DisplayMember = "SchClass";
            this.cmbClass.Properties.NullText = "مدرسه را انتخاب کنید";
            this.cmbClass.Properties.ValueMember = "Id";
            this.cmbClass.Size = new System.Drawing.Size(196, 22);
            this.cmbClass.StyleController = this.layoutControl1;
            this.cmbClass.TabIndex = 4;
            // 
            // tblSchoolBindingSource
            // 
            this.tblSchoolBindingSource.DataSource = typeof(ClassSRM.tbl_School);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1022, 633);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbClass;
            this.layoutControlItem1.Location = new System.Drawing.Point(710, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(286, 54);
            this.layoutControlItem1.Text = "مدرسه/کلاس";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(81, 17);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbBook;
            this.layoutControlItem2.Location = new System.Drawing.Point(432, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(278, 54);
            this.layoutControlItem2.Text = "درس";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(81, 17);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSave;
            this.layoutControlItem3.Location = new System.Drawing.Point(366, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(66, 54);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(366, 54);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 54);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(996, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(996, 527);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // Ask
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 633);
            this.Controls.Add(this.layoutControl1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Ask";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "پرسش از دانش آموزان";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Ask_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ask_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBook.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblSchoolBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsStudent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbBook;
        private DevExpress.XtraEditors.LookUpEdit cmbClass;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colStuClassId;
        private DevExpress.XtraGrid.Columns.GridColumn colStuName;
        private DevExpress.XtraGrid.Columns.GridColumn colStuLName;
        private DevExpress.XtraGrid.Columns.GridColumn colStuFName;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private System.Windows.Forms.BindingSource tblSchoolBindingSource;
        private System.Windows.Forms.BindingSource bsStudent;
    }
}