namespace combineExport
{
    partial class pivotReport
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.파ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.priceOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.preview = new System.Windows.Forms.ToolStripMenuItem();
            this.reportPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.gridcontextmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gridTab = new System.Windows.Forms.TabControl();
            this.label2 = new System.Windows.Forms.Label();
            this.priceName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fileName = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1238, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파ToolStripMenuItem
            // 
            this.파ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOpen,
            this.priceOpen,
            this.preview,
            this.reportPrint});
            this.파ToolStripMenuItem.Name = "파ToolStripMenuItem";
            this.파ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.파ToolStripMenuItem.Text = "파일";
            // 
            // fileOpen
            // 
            this.fileOpen.Name = "fileOpen";
            this.fileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.fileOpen.Size = new System.Drawing.Size(180, 22);
            this.fileOpen.Text = "파일열기";
            this.fileOpen.Click += new System.EventHandler(this.fileOpen_Click);
            // 
            // priceOpen
            // 
            this.priceOpen.Name = "priceOpen";
            this.priceOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.priceOpen.Size = new System.Drawing.Size(180, 22);
            this.priceOpen.Text = "가격표열기";
            this.priceOpen.Click += new System.EventHandler(this.priceOpen_Click);
            // 
            // preview
            // 
            this.preview.Name = "preview";
            this.preview.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.preview.Size = new System.Drawing.Size(180, 22);
            this.preview.Text = "유효성검사";
            this.preview.Click += new System.EventHandler(this.preview_Click);
            // 
            // reportPrint
            // 
            this.reportPrint.Name = "reportPrint";
            this.reportPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.reportPrint.Size = new System.Drawing.Size(180, 22);
            this.reportPrint.Text = "보고서출력";
            this.reportPrint.Click += new System.EventHandler(this.reportPrint_Click);
            // 
            // gridcontextmenu
            // 
            this.gridcontextmenu.Name = "contextMenuStrip1";
            this.gridcontextmenu.Size = new System.Drawing.Size(61, 4);
            // 
            // gridTab
            // 
            this.gridTab.Location = new System.Drawing.Point(9, 80);
            this.gridTab.Name = "gridTab";
            this.gridTab.SelectedIndex = 0;
            this.gridTab.Size = new System.Drawing.Size(1217, 377);
            this.gridTab.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "가격표경로";
            // 
            // priceName
            // 
            this.priceName.Location = new System.Drawing.Point(77, 54);
            this.priceName.Name = "priceName";
            this.priceName.Size = new System.Drawing.Size(360, 21);
            this.priceName.TabIndex = 12;
            this.priceName.Text = "D:\\cshop\\test.json";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "파일경로";
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(77, 27);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(360, 21);
            this.fileName.TabIndex = 10;
            this.fileName.Text = "D:\\cshop\\02-03 가금교 손상현황표2_보고서.xlsx";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pivotReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 468);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gridTab);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.priceName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileName);
            this.Name = "pivotReport";
            this.Text = "pivotReport";
            this.Load += new System.EventHandler(this.pivotReport_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 파ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileOpen;
        private System.Windows.Forms.ToolStripMenuItem priceOpen;
        private System.Windows.Forms.ToolStripMenuItem preview;
        private System.Windows.Forms.ToolStripMenuItem reportPrint;
        private System.Windows.Forms.ContextMenuStrip gridcontextmenu;
        private System.Windows.Forms.TabControl gridTab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox priceName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}