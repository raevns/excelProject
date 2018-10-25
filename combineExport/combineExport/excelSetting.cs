using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace combineExport
{
    public partial class excelSetting : Form
    {
        public excelSetting()
        {
            InitializeComponent();
        }

        private void excelSetting_Load(object sender, EventArgs e)
        {
            //불러오기
            this.settingText1.Text = Properties.Settings.Default.settingText1.ToString();
            this.settingText2.Text = Properties.Settings.Default.settingText2.ToString();
            this.settingText3.Text = Properties.Settings.Default.settingText3.ToString();
            this.settingText4.Text = Properties.Settings.Default.settingText4.ToString();
            this.settingText5.Text = Properties.Settings.Default.settingText5.ToString();
            this.settingText6.Text = Properties.Settings.Default.settingText6.ToString();
            this.settingText7.Text = Properties.Settings.Default.settingText7.ToString();
            this.settingText8.Text = Properties.Settings.Default.settingText8.ToString();
            this.settingText9.Text = Properties.Settings.Default.settingText9.ToString().ToLower();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //저장하기
            Properties.Settings.Default.settingText1 = this.settingText1.Text;
            Properties.Settings.Default.settingText2 = this.settingText2.Text;
            Properties.Settings.Default.settingText3 = this.settingText3.Text;
            Properties.Settings.Default.settingText4 = this.settingText4.Text;
            Properties.Settings.Default.settingText5 = this.settingText5.Text;
            Properties.Settings.Default.settingText6 = this.settingText6.Text;
            Properties.Settings.Default.settingText7 = this.settingText7.Text;
            Properties.Settings.Default.settingText8 = this.settingText8.Text;
            Properties.Settings.Default.settingText9 = this.settingText9.Text.ToLower();
            Properties.Settings.Default.Save();
            this.Close();
        }
        
        private void settingText1_Leave(object sender, EventArgs e)
        {
            Regex emailregex = new Regex(@"[a-zA-Z]");
            Boolean ismatch = emailregex.IsMatch(settingText1.Text);
            if (!ismatch)
            {
                MessageBox.Show("영문자만 입력해 주세요.");
                settingText1.Text = "";
                settingText1.Focus();

            }
        }

        private void buttonClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
