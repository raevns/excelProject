namespace combineExport
{
    partial class pivotRangeSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.settingpivotEd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.settingpivotSt = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(186, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "피벗 끝위치";
            // 
            // settingpivotEd
            // 
            this.settingpivotEd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.settingpivotEd.Location = new System.Drawing.Point(257, 12);
            this.settingpivotEd.Name = "settingpivotEd";
            this.settingpivotEd.Size = new System.Drawing.Size(79, 21);
            this.settingpivotEd.TabIndex = 31;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 12);
            this.label9.TabIndex = 30;
            this.label9.Text = "피벗 시작위치";
            // 
            // settingpivotSt
            // 
            this.settingpivotSt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.settingpivotSt.Location = new System.Drawing.Point(97, 12);
            this.settingpivotSt.Name = "settingpivotSt";
            this.settingpivotSt.Size = new System.Drawing.Size(79, 21);
            this.settingpivotSt.TabIndex = 29;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(196, 46);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 24);
            this.buttonClose.TabIndex = 28;
            this.buttonClose.Text = "닫기";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(84, 46);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 24);
            this.buttonSave.TabIndex = 27;
            this.buttonSave.Text = "저장";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // pivotRangeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 84);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.settingpivotEd);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.settingpivotSt);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Name = "pivotRangeSetting";
            this.Text = "피벗 범위 설정";
            this.Load += new System.EventHandler(this.pivotRangeSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox settingpivotEd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox settingpivotSt;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
    }
}