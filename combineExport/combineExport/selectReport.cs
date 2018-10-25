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
    public partial class selectReport : Form
    {
        public selectReport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            excelReport exReport = new excelReport();
            exReport.Show();
            exReport.FormClosing += report_Closing;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            pivotReport pvReport = new pivotReport();
            pvReport.Show();
            pvReport.FormClosing += report_Closing;
        }

        private void report_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }
    }
}
