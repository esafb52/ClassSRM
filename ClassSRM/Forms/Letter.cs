﻿
/****************************** ghost1372.github.io ******************************\
*	Module Name:	Letter.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 7, 26, 01:30 ب.ظ
*	
***********************************************************************************/

using DevExpress.XtraEditors;
using Microsoft.Office.Interop.Word;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ClassSRM
{
    public partial class Letter : DevExpress.XtraEditors.XtraForm
    {
        public Letter()
        {
            InitializeComponent();
        }

        private void frmLetterAll_Load(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            tblSchoolBindingSource.DataSource = dc.SelectSchool();
            cmbClass.ItemIndex = 0;
            txtDate.EditValue = DateTime.Now;
        }

        private static object tempPath = System.IO.Path.GetTempPath();

        private object docPath = tempPath + @"\sample.doc";
        private string pdfPath = tempPath + @"\sample.pdf";

        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            var dc = new ClassSRMDataContext(Config.connection);

            tblStudentBindingSource.DataSource = dc.SelectStudentByClassIdNoIMG((int)cmbClass.EditValue);
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount == 0)
            {
                XtraMessageBox.Show("دانش آموزی وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                pdfViewer1.CloseDocument();
                splashScreenManager1.ShowWaitForm();
                Object oMissing = System.Reflection.Missing.Value;
                Object oTemplatePath;
                if (cmbTem.SelectedIndex == 0)
                {
                    oTemplatePath = System.Windows.Forms.Application.StartupPath + @"\Sample\sample.dot";
                }
                else
                {
                    oTemplatePath = System.Windows.Forms.Application.StartupPath + @"\Sample\sample2.dot";
                }

                Object oTrue = true;
                Object oFalse = false;

                Microsoft.Office.Interop.Word.Application oWord = null;
                Microsoft.Office.Interop.Word.Document oWordDoc = null;

                oWord = new Microsoft.Office.Interop.Word.Application();
                oWordDoc = new Microsoft.Office.Interop.Word.Document();

                oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing); int iTotalFields = 0;

                var schoolName = cmbClass.GetColumnValue("SchName");
                var className = cmbClass.GetColumnValue("SchClass");
                var stuName = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "StuName") + " " + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "StuLName");

                foreach (Microsoft.Office.Interop.Word.Field myMergeField in oWordDoc.Fields)
                {
                    iTotalFields++;
                    Microsoft.Office.Interop.Word.Range rngFieldCode = myMergeField.Code;
                    String fieldText = rngFieldCode.Text;

                    if (fieldText.StartsWith(" MERGEFIELD"))
                    {
                        string fieldName = fieldText.Substring(12, fieldText.IndexOf(" ", 12) - 12);

                        switch (fieldName)
                        {
                            case "stuname":
                                myMergeField.Select();
                                oWord.Selection.TypeText(stuName.ToString());
                                break;

                            case "date":
                                myMergeField.Select();
                                oWord.Selection.TypeText(txtDate.Text);
                                break;

                            case "day":
                                myMergeField.Select();
                                oWord.Selection.TypeText(cmbDay.Text);
                                break;

                            case "teacher":
                                myMergeField.Select();
                                oWord.Selection.TypeText(className + " " + txtTeacher.Text);
                                break;

                            case "school":
                                myMergeField.Select();
                                oWord.Selection.TypeText(schoolName.ToString());
                                break;

                            case "body":
                                myMergeField.Select();
                                oWord.Selection.TypeText(txtBody.Text ?? " ");
                                break;

                            case "quet":
                                myMergeField.Select();
                                oWord.Selection.TypeText(cmbQuet.Text);
                                break;
                        }
                    }
                }

                oWordDoc.SaveAs(docPath);
                oWordDoc.Close(ref oFalse, ref oMissing, ref oMissing);

                oWord.Quit(ref oMissing, ref oMissing, ref oMissing);

                ExportPDF();
                pdfViewer1.LoadDocument(pdfPath);
                splashScreenManager1.CloseWaitForm();

                try
                {
                    foreach (Process proc in Process.GetProcessesByName("WINWORD"))
                    {
                        proc.Kill();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void ExportPDF()
        {
            Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
            wordDocument = appWord.Documents.Open(docPath);
            wordDocument.ExportAsFixedFormat(pdfPath, WdExportFormat.wdExportFormatPDF);
            wordDocument.Close();
        }

        public Microsoft.Office.Interop.Word.Document wordDocument { get; set; }

        private void frmLetter_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            pdfViewer1.CloseDocument();
            System.IO.File.Delete(pdfPath);
            System.IO.File.Delete(docPath.ToString());
        }

        private void cmbTem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTem.SelectedIndex == 0)
            {
                lyBody.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                lyBody.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void Letter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}