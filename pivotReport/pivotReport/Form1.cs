using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using pivotReport;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace pivotReport
{
    public partial class Form1 : Form
    {
        Dictionary<string, List<readVO>> lists = new Dictionary<string, List<readVO>>();

        String select_price = "";
        DataGridView select_dgw;
        DataGridViewCellStateChangedEventArgs check_e;
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
                if (file_path.Contains("보고서"))
                {
                    if (result == 0 || result == 1)
                    {
                        fileName.Text = file_path;
                    }
                }
                else
                {
                    MessageBox.Show("엑셀보고서 프로그램으로 만든 파일이 아닙니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void priceOpen_Click(object sender, EventArgs e)
        {
            priceName.Clear();
            String price_path = null;
            openFileDialog1.InitialDirectory = "C:\\";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                price_path = openFileDialog1.FileName;

                int result = excelService.ExcelFileType(price_path);
                if (price_path.Contains(".json"))
                {
                    if (result == 0 || result == 1)
                    {
                        priceName.Text = price_path;
                    }
                }
                else
                {
                    MessageBox.Show("가격표 파일이 아닙니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void preview_Click(object sender, EventArgs e)
        {
            if (priceName.Text.ToString().Equals("") || fileName.Text.ToString().Equals(""))
            {
                MessageBox.Show("파일혹은 이미지 경로가 지정되지않았습니다.");
            }
            else
            {
                lists = excelService.ReadExcelData(fileName.Text);
                gridTab.TabPages.Clear();
                addTabGrid(lists);
                for (int i = 0; i <gridcontextmenu.Items.Count; i++)
                {
                    gridcontextmenu.Items.RemoveAt(i);
                }
                addcontextMenu(priceName.Text);
                MessageBox.Show("유효성확인완료");
            }
        }

        private void reportPrint_Click(object sender, EventArgs e)
        {

        }

        private void addTabGrid(Dictionary<string, List<readVO>> lists)
        {
            foreach (KeyValuePair<string, List<readVO>> data in lists)
            {
                DataGridView dgw = new DataGridView();

                // dataGridView1
                dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dgw.Location = new System.Drawing.Point(1, 1);
                dgw.Name = "previewGrid" + data.Key.ToString();
                dgw.Dock = System.Windows.Forms.DockStyle.Fill;
                dgw.TabIndex = 0;

                List<readVO> insertData = data.Value;
                dgw.DataSource = insertData;

                string title = data.Key.ToString();
                TabPage myTabPage = new TabPage(title);
                myTabPage.Controls.Add(dgw);
                gridTab.TabPages.Add(myTabPage);
                
                dgw.Columns[0].HeaderText = "구분";
                dgw.Columns[1].HeaderText = "부재";
                dgw.Columns[2].HeaderText = "결함종류";
                dgw.Columns[3].HeaderText = "단위";
                dgw.Columns[4].HeaderText = "개소";
                dgw.Columns[5].HeaderText = "물량";
                dgw.Columns.Add("priceList", "단가표");
                dgw.Columns.Add("price", "단가");
                dgw.Columns.Add("sum", "합계");

                dgw.CellStateChanged += grid_CellStateChanged;
                dgw.Rows[0].Cells[0].Selected = false;

            }
        }

        private void grid_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            // For any other operation except, StateChanged, do nothing
            if (!e.Cell.Selected || e.Cell.ColumnIndex != 6) return;
            select_dgw = sender as DataGridView;
            gridcontextmenu.Show(select_dgw.PointToScreen(select_dgw.GetCellDisplayRectangle(e.Cell.ColumnIndex, e.Cell.RowIndex, false).Location));
            select_dgw.Rows[e.Cell.RowIndex].Selected = true;
            check_e = e;
        }

        private void addcontextMenu(string path)
        {
            // read JSON directly from a file
            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o1 = (JObject)JToken.ReadFrom(reader);
                Dictionary <string,Dictionary<string, Dictionary<string, Dictionary<string, int>>>> ValueList = 
                        JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, int>>>>>(o1.ToString());

                foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, Dictionary<string, int>>>> pair in ValueList)
                {
                    ToolStripMenuItem newitem = new ToolStripMenuItem();
                    newitem.Text = pair.Key;
                    foreach (KeyValuePair<string, Dictionary<string, Dictionary<string,int>>> pair2 in ValueList[pair.Key])
                    {
                        ToolStripMenuItem newitem_sub = new ToolStripMenuItem();
                        newitem_sub.Text = pair2.Key;
                        newitem.DropDownItems.Add(newitem_sub);
                        foreach (KeyValuePair<string, Dictionary<string,int>> pair3 in pair2.Value)
                        {
                            ToolStripMenuItem newitem_sub_sub = new ToolStripMenuItem();
                            newitem_sub_sub.Text = pair3.Key;
                            newitem_sub.DropDownItems.Add(newitem_sub_sub);
                            foreach (KeyValuePair<string, int> pair4 in pair3.Value)
                            {
                                ToolStripMenuItem newitem_sub_sub_price = new ToolStripMenuItem();
                                newitem_sub_sub_price.Text = pair4.Key+" | "+pair4.Value+"원";
                                newitem_sub_sub_price.Click += contextClick;
                                newitem_sub_sub.DropDownItems.Add(newitem_sub_sub_price);
                            }
                        }
                    }
                    gridcontextmenu.Items.Add(newitem);
                }
            }
        }

        private void contextClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            select_price = item.Text;
            if (!select_price.Equals(""))
            {
                string[] data = select_price.Split('|');
                select_dgw.Rows[check_e.Cell.RowIndex].Cells[check_e.Cell.ColumnIndex].Value = data[0];
                select_dgw.Rows[check_e.Cell.RowIndex].Cells[check_e.Cell.ColumnIndex + 1].Value = data[1].Trim().Substring(0, data[1].Length - 2);
            }
        }
    }
}
