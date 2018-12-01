using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using combineExport.conn;
using System.Net.NetworkInformation;
using Newtonsoft.Json; // install-package Newtonsoft.Json
using RabbitMQ.Client;
using EasyNetQ;
using combineExport.appAuth;

namespace combineExport
{
    public partial class login : Form
    {
        AppAuthContext appAuthContext;

        public login()
        {
            InitializeComponent();
            appAuthContext = new AppAuthContext(this);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_ID.Text.ToString().Equals(""))
            {
                MessageBox.Show("아이디를 입력해주세요");
            }
            else if (textBox_PWD.Text.ToString().Equals(""))
            {
                MessageBox.Show("비밀번호를 입력해주세요");
            }
            else
            {
                try
                {
                    appAuthContext.authenticate(textBox_ID.Text.ToString(), textBox_PWD.Text.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void login_Load(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.settingUserId.ToString().Equals(""))
            {                
                this.textBox_ID.Text = Properties.Settings.Default.settingUserId.ToString();
                this.textBox_PWD.Text = Properties.Settings.Default.settingUserPwd.ToString();
                checkBox1.Checked = true;
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
            else
            {
                return;
            }
        }

        private void selReport_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        public void setStateMessage(String stateMessage)
        {
            
            MethodInvoker methodInvokerDelegate = delegate ()
            { this.StateMessageLabel.Text = stateMessage; };

            //This will be true if Current thread is not UI thread.
            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
                
        }

        public void onAuthorized()
        {

            MethodInvoker methodInvokerDelegate = delegate ()
            {
                if (checkBox1.Checked)
                {
                    Properties.Settings.Default.settingUserId = this.textBox_ID.Text;
                    Properties.Settings.Default.settingUserPwd = this.textBox_PWD.Text;
                    Properties.Settings.Default.Save();
                }
                this.Hide();
                selectReport selReport = new selectReport();
                selReport.Show();
                selReport.FormClosing += selReport_Closing;
            };

            //This will be true if Current thread is not UI thread.
            if (this.InvokeRequired)
                this.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();

        }

    }
}
