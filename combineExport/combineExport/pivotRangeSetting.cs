using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace combineExport
{
    public partial class pivotRangeSetting : Form
    {
        public pivotRangeSetting()
        {
            InitializeComponent();
        }

        private void pivotRangeSetting_Load(object sender, EventArgs e)
        {
            //불러오기
            this.settingpivotSt.Text = Properties.Settings.Default.settingpivotSt.ToString();
            this.settingpivotEd.Text = Properties.Settings.Default.settingpivotEd.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //저장하기
            Properties.Settings.Default.settingpivotSt = this.settingpivotSt.Text;
            Properties.Settings.Default.settingpivotEd = this.settingpivotEd.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
