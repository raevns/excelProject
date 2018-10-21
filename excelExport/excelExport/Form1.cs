using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using excelExport.excel;

namespace excelExport
{
    public partial class Form1 : Form
    {
        Dictionary<string, List<readVO>> result;
        private Boolean check_imgFIle = true;
        private Boolean click_preview = false;

        private Boolean pivot_content = true;
        private Boolean pivot_position = true;
        private Boolean result_files = true;
        private int row_select = 2;

        public Form1()
        {
            InitializeComponent();
        }

        private void fileOpen_Click(object sender, EventArgs e)
        {
            fileName.Clear();
            String file_path = null;
            openFileDialog1.InitialDirectory = "C:\\";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file_path = openFileDialog1.FileName;

                int result = excelService.ExcelFileType(file_path);
                if (result == 0 || result == 1)
                {
                    fileName.Text = file_path;
                }                 
            }
        }

        private void imgFile_Click(object sender, EventArgs e)
        {
            imgName.Clear();
            String file_path = null;
            folderBrowserDialog1.SelectedPath = "C:\\";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                file_path = folderBrowserDialog1.SelectedPath;

                imgName.Text = file_path;
            }
        }

        private void setForm_Click(object sender, EventArgs e)
        {
            settingForm dlg = new settingForm();
            dlg.ShowDialog();
        }

        private void makeFile_Click(object sender, EventArgs e)
        {
            if (imgName.Text.ToString().Equals("") || fileName.Text.ToString().Equals(""))
            {
                MessageBox.Show("파일혹은 이미지 경로가 지정되지않았습니다.");
            }
            else if (!check_imgFIle)
            {
                click_preview = false;
                check_imgFIle = true;
                MessageBox.Show("이미지 파일이 없는 항목이 있습니다.파일수정 혹은 이미지 파일 추가 후 유효성 검사를 다시 해주세요");
            }
            else if (!click_preview)
            {
                MessageBox.Show("유효성 확인을 하지 않았습니다.");
            }
            else
            {

                folderBrowserDialog1.SelectedPath = "C:\\";
                //            folderBrowserDialog1.SelectedPath = "D:\\cshop";
                String file_path = null;
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    file_path = folderBrowserDialog1.SelectedPath;

                    int check_result = excelService.makeExcelFile(fileName.Text, file_path, imgName.Text.ToString(), result, row_select, pivot_content, pivot_position, result_files);
                    if (check_result == 1)
                    {
                        MessageBox.Show("파일생성완료");
                    }
                    else
                    {
                        MessageBox.Show("파일생성실패");
                    }
                }
            }
        }

        private void filePreview_Click(object sender, EventArgs e)
        {
            if (imgName.Text.ToString().Equals("") || fileName.Text.ToString().Equals(""))
            {
                MessageBox.Show("파일혹은 이미지 경로가 지정되지않았습니다.");
            }
            else
            {
                result = excelService.ReadExcelData(fileName.Text);
                click_preview = true;
                gridTab.TabPages.Clear();
                addTabGrid(result);
                MessageBox.Show("유효성확인완료");
            }
        }

        private void addTabGrid(Dictionary<string, List<readVO>> lists)
        {
            foreach (KeyValuePair<string,List< readVO >> data in lists)
            {
                DataGridView dgw = new DataGridView();

                // dataGridView1
                // 
                dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dgw.Location = new System.Drawing.Point(1, 1);
                dgw.Name = "previewGrid"+ data.Key.ToString();
                dgw.Dock = System.Windows.Forms.DockStyle.Fill;
                dgw.TabIndex = 0;

                List<readVO> insertData = data.Value;
                dgw.DataSource = insertData;

                string title = data.Key.ToString();
                TabPage myTabPage = new TabPage(title);
                myTabPage.Controls.Add(dgw);
                gridTab.TabPages.Add(myTabPage);

                dgw.Columns[0].HeaderText = "구분";
                dgw.Columns[1].HeaderText = "경간";
                dgw.Columns[2].HeaderText = "부재";
                dgw.Columns[3].HeaderText = "결함종류";
                dgw.Columns[4].HeaderText = "개소";
                dgw.Columns[5].HeaderText = "물량";
                dgw.Columns[6].HeaderText = "단위";
                dgw.Columns[7].HeaderText = "사진번호";

                dgw.Columns.RemoveAt(8);
                dgw.Columns.RemoveAt(8);
                List<String> imgList = new List<string>();
                String FolderName = imgName.Text;
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
                string imgExtention = Properties.Settings.Default.settingText9;
                foreach (System.IO.FileInfo File in di.GetFiles())
                {
                    if (File.Extension.ToLower().CompareTo("."+imgExtention) == 0)
                    {
                        String FileNameOnly = File.Name.Substring(0, File.Name.Length - 4);
                        imgList.Add(FileNameOnly);
                    }
                }

                for (int i = 0; i < dgw.RowCount; i++)
                {
                    if (dgw.Rows[i].Cells[7].Value.ToString().Equals("사진없음"))
                    {
                        dgw.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                    }
                    else if (!imgList.Contains(dgw.Rows[i].Cells[7].Value.ToString()))
                    {
                        dgw.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        check_imgFIle = false;
                    }
                }
            }
        }

        private void printImgRow_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            switch (menu.Name.ToString())
            {
                case "printImgRow1":
                    row_select = 1;
                    break;
                case "printImgRow2":
                    row_select = 2;
                    break;
                case "printImgRow3":
                    row_select = 3;
                    break;
                case "printImgRow4":
                    row_select = 4;
                    break;
            }
            if (!menu.Checked)
            {
                foreach (ToolStripMenuItem item in imgReport.DropDownItems)
                {
                    item.Checked = false;
                }
                menu.Checked = true;
            }
        }

        private void listMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            switch (menu.Name.ToString())
            {
                case "checkPivotError":
                    if (!menu.Checked)
                    {
                        pivot_content = true;
                        menu.Checked = true;
                    }
                    else
                    {
                        pivot_content = false;
                        menu.Checked = false;
                    }
                    break;
                case "checkPivotRange":
                    if (!menu.Checked)
                    {
                        pivot_position= true;
                        menu.Checked = true;
                    }
                    else
                    {
                        pivot_position = false;
                        menu.Checked = false;
                    }
                    break;
                case "checkResult":
                    if (!menu.Checked)
                    {
                        result_files= true;
                        menu.Checked = true;
                    }
                    else
                    {
                        result_files = false;
                        menu.Checked = false;
                    }
                    break;
            }
            
        }

        private void gridTab_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 피벗테이블설정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 dlg = new Form3();
            dlg.ShowDialog();
        }
    }
}
