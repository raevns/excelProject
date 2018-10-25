namespace combineExport
{
    partial class excelReport
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.imgName = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fileName = new System.Windows.Forms.TextBox();
            this.피벗테이블설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.컬럼설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setForm = new System.Windows.Forms.ToolStripMenuItem();
            this.checkResult = new System.Windows.Forms.ToolStripMenuItem();
            this.checkPivotRange = new System.Windows.Forms.ToolStripMenuItem();
            this.gridTab = new System.Windows.Forms.TabControl();
            this.checkPivotError = new System.Windows.Forms.ToolStripMenuItem();
            this.printImgRow3 = new System.Windows.Forms.ToolStripMenuItem();
            this.printImgRow2 = new System.Windows.Forms.ToolStripMenuItem();
            this.printImgRow1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imgReport = new System.Windows.Forms.ToolStripMenuItem();
            this.printImgRow4 = new System.Windows.Forms.ToolStripMenuItem();
            this.printType = new System.Windows.Forms.ToolStripMenuItem();
            this.filePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.makeFile = new System.Windows.Forms.ToolStripMenuItem();
            this.imgFile = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "그림파일경로";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "파일경로";
            // 
            // imgName
            // 
            this.imgName.Location = new System.Drawing.Point(93, 55);
            this.imgName.Name = "imgName";
            this.imgName.Size = new System.Drawing.Size(314, 21);
            this.imgName.TabIndex = 10;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(93, 28);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(314, 21);
            this.fileName.TabIndex = 9;
            // 
            // 피벗테이블설정ToolStripMenuItem
            // 
            this.피벗테이블설정ToolStripMenuItem.Name = "피벗테이블설정ToolStripMenuItem";
            this.피벗테이블설정ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.피벗테이블설정ToolStripMenuItem.Text = "피벗테이블설정";
            this.피벗테이블설정ToolStripMenuItem.Click += new System.EventHandler(this.피벗테이블설정ToolStripMenuItem_Click);
            // 
            // 컬럼설정ToolStripMenuItem
            // 
            this.컬럼설정ToolStripMenuItem.Name = "컬럼설정ToolStripMenuItem";
            this.컬럼설정ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.컬럼설정ToolStripMenuItem.Text = "컬럼설정";
            this.컬럼설정ToolStripMenuItem.Click += new System.EventHandler(this.컬럼설정ToolStripMenuItem_Click);
            // 
            // setForm
            // 
            this.setForm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.컬럼설정ToolStripMenuItem,
            this.피벗테이블설정ToolStripMenuItem});
            this.setForm.Name = "setForm";
            this.setForm.Size = new System.Drawing.Size(43, 20);
            this.setForm.Text = "설정";
            // 
            // checkResult
            // 
            this.checkResult.Checked = true;
            this.checkResult.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkResult.Name = "checkResult";
            this.checkResult.Size = new System.Drawing.Size(190, 22);
            this.checkResult.Text = "결과요약";
            this.checkResult.Click += new System.EventHandler(this.listMenu_Click);
            // 
            // checkPivotRange
            // 
            this.checkPivotRange.Checked = true;
            this.checkPivotRange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkPivotRange.Name = "checkPivotRange";
            this.checkPivotRange.Size = new System.Drawing.Size(190, 22);
            this.checkPivotRange.Text = "피벗테이블(경간)";
            this.checkPivotRange.Click += new System.EventHandler(this.listMenu_Click);
            // 
            // gridTab
            // 
            this.gridTab.Location = new System.Drawing.Point(8, 82);
            this.gridTab.Name = "gridTab";
            this.gridTab.SelectedIndex = 0;
            this.gridTab.Size = new System.Drawing.Size(981, 377);
            this.gridTab.TabIndex = 13;
            // 
            // checkPivotError
            // 
            this.checkPivotError.Checked = true;
            this.checkPivotError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkPivotError.Name = "checkPivotError";
            this.checkPivotError.Size = new System.Drawing.Size(190, 22);
            this.checkPivotError.Text = "피벗테이블(결함종류)";
            this.checkPivotError.Click += new System.EventHandler(this.listMenu_Click);
            // 
            // printImgRow3
            // 
            this.printImgRow3.Name = "printImgRow3";
            this.printImgRow3.Size = new System.Drawing.Size(180, 22);
            this.printImgRow3.Text = "3줄";
            this.printImgRow3.Click += new System.EventHandler(this.printImgRow_Click);
            // 
            // printImgRow2
            // 
            this.printImgRow2.Checked = true;
            this.printImgRow2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.printImgRow2.Name = "printImgRow2";
            this.printImgRow2.Size = new System.Drawing.Size(180, 22);
            this.printImgRow2.Text = "2줄";
            this.printImgRow2.Click += new System.EventHandler(this.printImgRow_Click);
            // 
            // printImgRow1
            // 
            this.printImgRow1.Name = "printImgRow1";
            this.printImgRow1.Size = new System.Drawing.Size(180, 22);
            this.printImgRow1.Text = "1줄";
            this.printImgRow1.Click += new System.EventHandler(this.printImgRow_Click);
            // 
            // imgReport
            // 
            this.imgReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printImgRow1,
            this.printImgRow2,
            this.printImgRow3,
            this.printImgRow4});
            this.imgReport.Name = "imgReport";
            this.imgReport.Size = new System.Drawing.Size(190, 22);
            this.imgReport.Text = "사진대지";
            // 
            // printImgRow4
            // 
            this.printImgRow4.Name = "printImgRow4";
            this.printImgRow4.Size = new System.Drawing.Size(180, 22);
            this.printImgRow4.Text = "4줄";
            this.printImgRow4.Click += new System.EventHandler(this.printImgRow_Click);
            // 
            // printType
            // 
            this.printType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imgReport,
            this.checkPivotError,
            this.checkPivotRange,
            this.checkResult});
            this.printType.Name = "printType";
            this.printType.Size = new System.Drawing.Size(67, 20);
            this.printType.Text = "출력형태";
            // 
            // filePreview
            // 
            this.filePreview.Name = "filePreview";
            this.filePreview.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.filePreview.Size = new System.Drawing.Size(183, 22);
            this.filePreview.Text = "유효성확인";
            this.filePreview.Click += new System.EventHandler(this.filePreview_Click);
            // 
            // makeFile
            // 
            this.makeFile.Name = "makeFile";
            this.makeFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.makeFile.Size = new System.Drawing.Size(183, 22);
            this.makeFile.Text = "파일생성";
            this.makeFile.Click += new System.EventHandler(this.makeFile_Click);
            // 
            // imgFile
            // 
            this.imgFile.Name = "imgFile";
            this.imgFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.imgFile.Size = new System.Drawing.Size(183, 22);
            this.imgFile.Text = "그림폴더선택";
            this.imgFile.Click += new System.EventHandler(this.imgFile_Click);
            // 
            // fileOpen
            // 
            this.fileOpen.Name = "fileOpen";
            this.fileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.fileOpen.Size = new System.Drawing.Size(183, 22);
            this.fileOpen.Text = "파일열기";
            this.fileOpen.Click += new System.EventHandler(this.fileOpen_Click);
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOpen,
            this.imgFile,
            this.makeFile,
            this.filePreview});
            this.menuFile.Name = "menuFile";
            this.menuFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.menuFile.Size = new System.Drawing.Size(43, 20);
            this.menuFile.Text = "파일";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.printType,
            this.setForm});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1001, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // excelReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 468);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgName);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.gridTab);
            this.Controls.Add(this.menuStrip1);
            this.Name = "excelReport";
            this.Text = "excelReport";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox imgName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.ToolStripMenuItem 피벗테이블설정ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 컬럼설정ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setForm;
        private System.Windows.Forms.ToolStripMenuItem checkResult;
        private System.Windows.Forms.ToolStripMenuItem checkPivotRange;
        private System.Windows.Forms.TabControl gridTab;
        private System.Windows.Forms.ToolStripMenuItem checkPivotError;
        private System.Windows.Forms.ToolStripMenuItem printImgRow3;
        private System.Windows.Forms.ToolStripMenuItem printImgRow2;
        private System.Windows.Forms.ToolStripMenuItem printImgRow1;
        private System.Windows.Forms.ToolStripMenuItem imgReport;
        private System.Windows.Forms.ToolStripMenuItem printImgRow4;
        private System.Windows.Forms.ToolStripMenuItem printType;
        private System.Windows.Forms.ToolStripMenuItem filePreview;
        private System.Windows.Forms.ToolStripMenuItem makeFile;
        private System.Windows.Forms.ToolStripMenuItem imgFile;
        private System.Windows.Forms.ToolStripMenuItem fileOpen;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}