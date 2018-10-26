using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace combineExport.excel
{
    class pivotService
    {
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public static int ExcelFileType(string XlsFile)
        {
            byte[,] ExcelHeader = {
                { 0xD0, 0xCF, 0x11, 0xE0, 0xA1 }, // XLS  File Header
                { 0x50, 0x4B, 0x03, 0x04, 0x14 }  // XLSX File Header
            };
            // result -2=error, -1=not excel , 0=xls , 1=xlsx
            int result = 0;
            FileStream FS = new FileStream(XlsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            try
            {
                byte[] FH = new byte[5];
                FS.Read(FH, 0, 5);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (FH[j] != ExcelHeader[i, j]) break;
                        else if (j == 4) result = i;
                    }
                    if (result >= 0) break;
                }
            }
            catch (Exception e)
            {
                result = (-2);
                Console.WriteLine(e.StackTrace);
                MessageBox.Show(e.ToString(), "오류발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                FS.Close();
            }
            return result;
        }

        public static Dictionary<string, List<readPivotVO>> ReadExcelData(string fileroot)
        {
            Dictionary<string, List<readPivotVO>> lists = new Dictionary<string, List<readPivotVO>>();

            Excel.Application xlApp = null;
            Excel.Workbook xlWorkBook = null;
            Excel.Worksheet xlWorkSheet = null;
            xlApp = new Excel.Application();

            try
            {
                xlWorkBook = xlApp.Workbooks.Open(fileroot);

                for (int i = 1; i <= xlWorkBook.Worksheets.Count; i++)
                {
                    xlWorkSheet = xlWorkBook.Worksheets[i] as Excel.Worksheet;
                    String sheetName = xlWorkSheet.Name;
                    if (sheetName.Contains("_피벗테이블(결함정보)"))
                    {
                        Excel.Range rng = xlWorkSheet.UsedRange;
                        object[,] data = rng.Value;
                        List<readPivotVO> datas = new List<readPivotVO>();
                        string category = "", sub_position = "", content = "", ea = "";

                        for (int r = 1; r <= data.GetLength(0); r++)
                        {
                            if (data[r, 6] != null && !data[r, 6].ToString().Equals("합계:물량"))
                            {
                                if (data[r, 1] != null)
                                {
                                    if (!data[r, 1].ToString().Equals("총합계"))
                                    {
                                        readPivotVO vo = new readPivotVO();
                                        vo.category = data[r, 1] == null ? category : data[r, 1].ToString();
                                        vo.sub_position = data[r, 2] == null ? sub_position : data[r, 2].ToString();
                                        vo.content = data[r, 3] == null ? content : data[r, 3].ToString();
                                        vo.ea = data[r, 6] == null ? ea : data[r, 4].ToString();
                                        vo.unit = data[r, 4] == null ? "개소없음" : data[r, 5].ToString();
                                        vo.supply = data[r, 5] == null ? "물량없음" : data[r, 6].ToString();
                                        datas.Add(vo);
                                        category = vo.category;
                                        sub_position = vo.sub_position;
                                        content = vo.content;
                                        ea = vo.ea;
                                    }
                                }
                                else
                                {
                                    readPivotVO vo = new readPivotVO();
                                    vo.category = data[r, 1] == null ? category : data[r, 1].ToString();
                                    vo.sub_position = data[r, 2] == null ? sub_position : data[r, 2].ToString();
                                    vo.content = data[r, 3] == null ? content : data[r, 3].ToString();
                                    vo.ea = data[r, 6] == null ? ea : data[r, 4].ToString();
                                    vo.unit = data[r, 4] == null ? "개소없음" : data[r, 5].ToString();
                                    vo.supply = data[r, 5] == null ? "물량없음" : data[r, 6].ToString();
                                    datas.Add(vo);
                                    category = vo.category;
                                    sub_position = vo.sub_position;
                                    content = vo.content;
                                    ea = vo.ea;
                                }
                            }
                        }
                        if (data != null)
                        {
                            lists.Add(sheetName, datas);
                        }
                    }
                    ReleaseExcelObject(xlWorkSheet);
                }

                xlApp.DisplayAlerts = false; // 저장할 것인가 확인하지 않도록 설정
                //xlWorkBook.Close(true, null, null);
                ReleaseExcelObject(xlWorkBook);
                ReleaseExcelProcess(xlApp);

                return lists;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("파일 열기 실패! : " + ex.Message);
                ReleaseExcelProcess(xlApp);
                return lists;
            }
        }


        //엑셀 오브젝트 종료
        static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        //엑셀 프로세스 종료
        static void ReleaseExcelProcess(Excel.Application excelApp)
        {
            if (excelApp != null)
            {
                excelApp.Quit();
                int hWnd = excelApp.Application.Hwnd;
                uint processID;
                GetWindowThreadProcessId((IntPtr)hWnd, out processID);
                Process[] procs = Process.GetProcessesByName("EXCEL");
                foreach (Process p in procs)
                {
                    if (p.Id == processID)
                        p.Kill();
                }
                Marshal.FinalReleaseComObject(excelApp);
            }
        }

        public static void ExportExcel_ForMultiDataGrid(Dictionary<string, object> datagrid, string filename)
        {
            object missingType = Type.Missing;
            Excel.Application objApp;
            Excel.Workbook objBook;
            Excel.Worksheet objSheet;
            objApp = new Excel.Application();
            try
            {
                objBook = objApp.Workbooks.Add();
                foreach (KeyValuePair<string, object> data in datagrid)
                {
                    DataGridView myDataGridView = data.Value as DataGridView;
                    objSheet = objBook.Worksheets.Add();
                    objSheet.Name = data.Key;

                    objSheet.Cells[1, 1].Value = myDataGridView.Columns[0].HeaderText;
                    objSheet.Cells[1, 2].Value = myDataGridView.Columns[1].HeaderText;
                    objSheet.Cells[1, 3].Value = myDataGridView.Columns[2].HeaderText;
                    objSheet.Cells[1, 4].Value = myDataGridView.Columns[3].HeaderText;
                    objSheet.Cells[1, 5].Value = myDataGridView.Columns[4].HeaderText;
                    objSheet.Cells[1, 6].Value = myDataGridView.Columns[5].HeaderText;
                    objSheet.Cells[1, 7].Value = myDataGridView.Columns[6].HeaderText;
                    objSheet.Cells[1, 8].Value = myDataGridView.Columns[7].HeaderText;
                    objSheet.Cells[1, 9].Value = myDataGridView.Columns[8].HeaderText;
                    objSheet.Cells[1, 10].Value = myDataGridView.Columns[9].HeaderText;
                    objSheet.Cells[1, 11].Value = myDataGridView.Columns[10].HeaderText;
                    objSheet.Cells[1, 12].Value = myDataGridView.Columns[11].HeaderText;
                    for (int i = 0; i < myDataGridView.RowCount; i++)
                    {
                        for (int j = 0; j < myDataGridView.ColumnCount; j++)
                        {
                            if (myDataGridView.Rows[i].Cells[j].Value != null)
                                objSheet.Cells[i + 2, j + 1].Value = myDataGridView.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    Excel.Range rs = objSheet.UsedRange;
                    rs.Columns.AutoFit();
                    ReleaseExcelObject(objSheet);
                }

                objApp.DisplayAlerts = false; // 저장할 것인가 확인하지 않도록 설정

                objBook.Worksheets["Sheet1"].Delete();
                objBook.Worksheets["Sheet2"].Delete();
                objBook.Worksheets["Sheet3"].Delete();
                objBook.SaveAs(filename + ".xlsx");
                objBook.Close(true, null, null);

                ReleaseExcelObject(objBook);
                ReleaseExcelProcess(objApp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("파일 생성 실패! : " + ex.Message);
                ReleaseExcelProcess(objApp);
            }
        }
    }


}
