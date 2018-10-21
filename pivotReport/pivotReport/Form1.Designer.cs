namespace pivotReport
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
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
            this.fileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.priceName = new System.Windows.Forms.TextBox();
            this.gridTab = new System.Windows.Forms.TabControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1158, 24);
            this.menuStrip1.TabIndex = 0;
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
            this.fileOpen.Size = new System.Drawing.Size(176, 22);
            this.fileOpen.Text = "파일열기";
            this.fileOpen.Click += new System.EventHandler(this.fileOpen_Click);
            // 
            // priceOpen
            // 
            this.priceOpen.Name = "priceOpen";
            this.priceOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.priceOpen.Size = new System.Drawing.Size(176, 22);
            this.priceOpen.Text = "가격표열기";
            this.priceOpen.Click += new System.EventHandler(this.priceOpen_Click);
            // 
            // preview
            // 
            this.preview.Name = "preview";
            this.preview.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.preview.Size = new System.Drawing.Size(176, 22);
            this.preview.Text = "유효성검사";
            this.preview.Click += new System.EventHandler(this.preview_Click);
            // 
            // reportPrint
            // 
            this.reportPrint.Name = "reportPrint";
            this.reportPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.reportPrint.Size = new System.Drawing.Size(176, 22);
            this.reportPrint.Text = "보고서출력";
            this.reportPrint.Click += new System.EventHandler(this.reportPrint_Click);
            // 
            // gridcontextmenu
            // 
            this.gridcontextmenu.Name = "contextMenuStrip1";
            this.gridcontextmenu.Size = new System.Drawing.Size(61, 4);
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(79, 37);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(360, 21);
            this.fileName.TabIndex = 2;
            this.fileName.Text = "D:\\cshop\\02-03 가금교 손상현황표2_보고서.xlsx";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "파일경로";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "가격표경로";
            // 
            // priceName
            // 
            this.priceName.Location = new System.Drawing.Point(79, 64);
            this.priceName.Name = "priceName";
            this.priceName.Size = new System.Drawing.Size(360, 21);
            this.priceName.TabIndex = 4;
            this.priceName.Text = "D:\\cshop\\test.json";
            // 
            // gridTab
            // 
            this.gridTab.Location = new System.Drawing.Point(11, 90);
            this.gridTab.Name = "gridTab";
            this.gridTab.SelectedIndex = 0;
            this.gridTab.Size = new System.Drawing.Size(1135, 377);
            this.gridTab.TabIndex = 8;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 475);
            this.Controls.Add(this.gridTab);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.priceName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "단가표출력";
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
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox priceName;
        private System.Windows.Forms.TabControl gridTab;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

