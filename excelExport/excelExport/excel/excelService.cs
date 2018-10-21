using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using excelExport.excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Windows.Forms;


namespace excelExport
{
    public class excelService
    {
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        private static int settingText7 = GetExcelColumnToInt(Properties.Settings.Default.settingText7);
        private static int settingText8 = GetExcelColumnToInt(Properties.Settings.Default.settingText8);
        private static String settingText9 = Properties.Settings.Default.settingText9;

        private static Excel.Application excelApp = null;

        public static int ExcelFileType(string XlsFile)
        {
            byte[,] ExcelHeader = {
                { 0xD0, 0xCF, 0x11, 0xE0, 0xA1 }, // XLS  File Header
                { 0x50, 0x4B, 0x03, 0x04, 0x14 }  // XLSX File Header
            };
            // result -2=error, -1=not excel , 0=xls , 1=xlsx
            int result = 0;
            FileStream FS = new FileStream(XlsFile,FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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
                MessageBox.Show(e.ToString(), "오류발생",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                FS.Close();
            }
            return result;
        }

        // 1 구분 2 경간 3 부재 4 결함종류 5 개소 6 물량 7 단위 8 사진번호
        public static Dictionary<string, List<readVO>> ReadExcelData(string path)
        { 
            int settingText1 = GetExcelColumnToInt(Properties.Settings.Default.settingText1);
            int settingText2 = GetExcelColumnToInt(Properties.Settings.Default.settingText2);
            int settingText3 = GetExcelColumnToInt(Properties.Settings.Default.settingText3);
            int settingText4 = GetExcelColumnToInt(Properties.Settings.Default.settingText4);
            int settingText5 = GetExcelColumnToInt(Properties.Settings.Default.settingText5);
            int settingText6 = GetExcelColumnToInt(Properties.Settings.Default.settingText6);

            Dictionary<string, List<readVO>> dictionary = new Dictionary<string, List<readVO>>();
            
           
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                excelApp = new Excel.Application();
                wb = excelApp.Workbooks.Open(path);
                for (int i=1; i <= wb.Worksheets.Count; i++) {
                    ws = wb.Worksheets.get_Item(i) as Excel.Worksheet;
                    Excel.Range rng = ws.UsedRange;
                    object[,] data = rng.Value;
                    List<readVO> list = new List<readVO>();
                    for (int r = 1; r <= data.GetLength(0); r++)
                    {
                        if (!(data[r, settingText1] == null))
                        {
                            if (data[r, settingText1].ToString().Equals("구분") || data[r, settingText1].ToString().Equals(""))
                            {
                                continue;
                            }
                            else
                            {
                                readVO vo = new readVO();
                                vo.category = data[r, settingText1] == null ? "구분없음" : data[r, settingText1].ToString();
                                vo.position = data[r, settingText2] == null ? "경간없음" : data[r, settingText2].ToString();
                                vo.sub_position = data[r, settingText3] == null ? "부재없음" : data[r, settingText3].ToString();
                                vo.content = data[r, settingText4] == null ? "결함내용없음" : data[r, settingText4].ToString();
                                vo.unit = data[r, settingText5] == null ? "개소없음" : data[r, settingText5].ToString();
                                vo.supply = data[r, settingText6] == null ? "물량없음" : data[r, settingText6].ToString();
                                vo.ea = data[r, settingText7] == null ? "단위없음" : data[r, settingText7].ToString();
                                vo.pictureFileNameInExcel = data[r, settingText8] == null ? "사진없음" : data[r, settingText8].ToString();
                                vo.orignalImgCell = data[r, settingText8] == null ? 0 : r;
                                vo.sheetnum = i;
                                list.Add(vo);
                            }
                        }
                    }
                    if (list != null)
                    {
                        dictionary.Add(ws.Name, list);
                    }
                    ReleaseExcelObject(rng);
                    ReleaseExcelObject(ws);
                }

                wb.Close(true,null,null);
                ReleaseExcelObject(wb);

                excelApp.DisplayAlerts = false; // 저장할 것인가 확인하지 않도록 설정
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
                return dictionary;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.ToString(), "오류발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dictionary;
            }
        }

        //성공 1 실패 0 기존파일 복사
        public static int makeExcelFile(string originalpath,string newpath ,string imgpath, Dictionary<string, List<readVO>> lists,int row_select,Boolean pivot_content, Boolean pivot_position, Boolean result_files)
        {
           
            Excel.Workbook wb = null;
            try
            {
                //오리지널 불러오기
                Excel.Application oriExcel = new Excel.Application();
                Excel.Workbook oriWb = oriExcel.Workbooks.Open(originalpath);



                //같은파일 존재시 코딩할곳
                string _Filestr = newpath + "\\" + oriWb.Name.ToString().Replace(".xlsx", "").Replace(".xls", "") + "_보고서.xlsx";
                System.IO.FileInfo fi = new System.IO.FileInfo(_Filestr);
                if (fi.Exists)
                {
                    MessageBox.Show("같은 파일이름 원본파일_보고서.xlsx가 존재합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 0;
                }

                oriWb.SaveAs(newpath + "\\" + oriWb.Name.ToString().Replace(".xlsx", "").Replace(".xls", "") + "_보고서.xlsx");
                oriWb.Close(true, null, null);
                ReleaseExcelObject(oriWb);
                if (oriExcel != null)
                {
                    oriExcel.Quit();
                    int hWnd = oriExcel.Application.Hwnd;
                    uint processID;
                    GetWindowThreadProcessId((IntPtr)hWnd, out processID);
                    Process[] procs = Process.GetProcessesByName("EXCEL");
                    foreach (Process p in procs)
                    {
                        if (p.Id == processID)
                            p.Kill();
                    }
                    Marshal.FinalReleaseComObject(oriExcel);
                }

                excelApp = new Excel.Application();
                excelApp.DisplayAlerts = false; // 저장할 것인가 확인하지 않도록 설정
                wb = excelApp.Workbooks.Open(_Filestr);
                int sheetcount = wb.Worksheets.Count;

                //사진대지
                int check_result = newExcelFile(wb, imgpath, lists, row_select);
                if (check_result == 0)
                {
                    ReleaseExcelProcess(excelApp);
                    return 0;
                }

                //피벗(결함)
                if (pivot_content)
                {
                    check_result = makePivotContent(wb, lists, Properties.Settings.Default.settingpivotSt, Properties.Settings.Default.settingpivotEd);
                    if (check_result == 0)
                    {
                        ReleaseExcelProcess(excelApp);
                        return 0;
                    }
                }

                //피벗(결함)
                if (pivot_position)
                {
                    check_result = makePivotPosition(wb, lists, Properties.Settings.Default.settingpivotSt, Properties.Settings.Default.settingpivotEd);
                    if (check_result == 0)
                    {
                        ReleaseExcelProcess(excelApp);
                        return 0;
                    }
                }

                //결과요약
                //if (result_files)
                //{
                //    check_result = makePivotPosition(wb, lists, Properties.Settings.Default.settingpivotSt, Properties.Settings.Default.settingpivotEd);

                //}

                wb.SaveAs(_Filestr, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,Type.Missing, Type.Missing);

                wb.Close(true, null, null);
                ReleaseExcelObject(wb);
                ReleaseExcelProcess(excelApp);
                return 1;
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex);
                ReleaseExcelProcess(excelApp);
                MessageBox.Show(ex.ToString(), "오류발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            
        }

        //성공 1 실패 0 만든 기존파일에 내용추가 사진대지
        static int newExcelFile(Excel.Workbook wb,String imgpath ,Dictionary<string, List<readVO>> lists,int row_select)
        {
            try
            {
                Excel.Worksheet copyws = null;
                Excel.Worksheet imgws = null;
                foreach (KeyValuePair<string, List<readVO>> data in lists)
                {
                    string sheet_name = null;
                    copyws = wb.Worksheets[data.Key] as Excel.Worksheet;
                    sheet_name = copyws.Name;
                    imgws = wb.Sheets.Add(After: wb.Sheets[wb.Sheets.Count]);

                    imgws.Name = sheet_name + "_사진대지";
                    List<readVO> orignalData = data.Value;

                    makeImgCell(copyws, imgws, imgpath, orignalData, row_select);
                    ReleaseExcelObject(copyws);
                    ReleaseExcelObject(imgws);
                }
                return 1;
            }
            catch (Exception ex)
            {
                ReleaseExcelProcess(excelApp);
                Console.WriteLine(ex);
                return 0;
            }

        }

        static void makeImgCell(Excel.Worksheet copyws, Excel.Worksheet imgws, String imgpath, List<readVO> data, int row_select)
        {
            int startrow = 1;
            int startcolum = 1;
            int settingText8 = GetExcelColumnToInt(Properties.Settings.Default.settingText8);
            string settingText9 = Properties.Settings.Default.settingText9;
            List<readVO> checkList = new List<readVO>();
            foreach (readVO vo in data)
            {
                if (!vo.pictureFileNameInExcel.Equals("사진없음")) checkList.Add(vo);
            }

            if (row_select == 1)
            {
                for (int i = 0; i <= checkList.Count; i++)
                {
                    readVO vo = checkList[i];
                    makeimgCell_common(startrow, startcolum, imgws, copyws, imgpath, vo, 0);
                    startrow += 19;
                }
            }
            else if (row_select == 2)
            {
                for (int i = 0; i < checkList.Count;i++)
                {
                    readVO vo = checkList[i];
                    if (i % 2 == 0) makeimgCell_common(startrow, startcolum, imgws, copyws, imgpath, vo, 0);
                    else
                    {
                        makeimgCell_common(startrow, startcolum, imgws, copyws, imgpath, vo, 10);
                        startrow += 19;
                    }
                }
            }
            else if (row_select == 3)
            {
                for (int i = 0; i < checkList.Count; i++)
                {
                    readVO vo = checkList[i];
                    if (data.IndexOf(vo) % 3 == 0) makeimgCell_common(startrow, startcolum, imgws, copyws, imgpath, vo, 0);
                    else if (data.IndexOf(vo) % 3 == 1) makeimgCell_common(startrow, startcolum, imgws, copyws, imgpath, vo, 10);
                    else
                    {
                        makeimgCell_common(startrow, startcolum, imgws, copyws, imgpath, vo, 20);
                        startrow += 19;
                    }
                }
            }
            else if (row_select == 4)
            {
                for (int i = 0; i < checkList.Count; i++)
                {
                    readVO vo = checkList[i];
                    if (data.IndexOf(vo) % 4 == 0) makeimgCell_common(startrow, startcolum, imgws, copyws, imgpath, vo, 0);
                    else if (data.IndexOf(vo) % 4 == 1) makeimgCell_common(startrow, startcolum, imgws, copyws, imgpath, vo, 10);
                    else if (data.IndexOf(vo) % 4 == 2) makeimgCell_common(startrow, startcolum, imgws, copyws, imgpath, vo, 20);
                    else
                    {
                        makeimgCell_common(startrow, startcolum, imgws, copyws, imgpath, vo, 30);
                        startrow += 19;
                    }
                }
            }       
        }

        static void makeimgCell_common(int startrow, int startcolum, Excel.Worksheet imgws, Excel.Worksheet copyws, String imgpath, readVO vo, int addcolum)
        {
            int afterrow = startrow;
            Excel.Range selectCell = null;
            //Insert Image
            Excel.Range imgRange = (Excel.Range)imgws.Cells[startrow + 1, startcolum + 1 + addcolum];
            float Left = (float)((double)imgRange.Left);
            float Top = (float)((double)imgRange.Top);
            imgws.Shapes.AddPicture(imgpath + "\\" + vo.pictureFileNameInExcel + "." + settingText9, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, (float)432, (float)246.8976);
            selectCell = imgws.Range[imgws.Cells[startrow, startcolum + addcolum], imgws.Cells[startrow + 17 - 1, startcolum + 9 + addcolum]];
            selectCell.Merge();
            selectCell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium);
            selectCell = (Excel.Range)copyws.Cells[vo.orignalImgCell, settingText8];
            copyws.Hyperlinks.Add(selectCell, "#'" + imgws.Name + "'!" + ExcelColumnIndexToName(startcolum+addcolum) + (startrow), Type.Missing, Type.Missing, Type.Missing);
            //1열
            selectCell = imgws.Range[imgws.Cells[startrow + 17, startcolum + addcolum], imgws.Cells[startrow + 17, startcolum + 1 + addcolum]];
            selectCell.RowHeight = 24;
            selectCell.Merge();
            selectCell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium);
            selectCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            selectCell.Value = "위  치";
            selectCell = imgws.Range[imgws.Cells[startrow + 17, startcolum + 2 + addcolum], imgws.Cells[startrow + 17, startcolum + 9 + addcolum]];
            selectCell.Merge();
            selectCell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium);
            selectCell.IndentLevel = 1;
            selectCell.Value = vo.sub_position + "(" + vo.position + ")";
            //2열
            selectCell = imgws.Range[imgws.Cells[startrow + 18, startcolum + addcolum], imgws.Cells[startrow + 18, startcolum + 1 + addcolum]];
            selectCell.RowHeight = 24;
            selectCell.Merge();
            selectCell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium);
            selectCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            selectCell.Value = "내  용";
            selectCell = imgws.Range[imgws.Cells[startrow + 18, startcolum + 2 + addcolum], imgws.Cells[startrow + 18, startcolum + 5 + addcolum]];
            selectCell.Merge();
            selectCell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium);
            selectCell.IndentLevel = 1;
            selectCell.Value = vo.content;
            selectCell = imgws.Range[imgws.Cells[startrow + 18, startcolum + 6 + addcolum], imgws.Cells[startrow + 18, startcolum + 9 + addcolum]];
            selectCell.Merge();
            selectCell.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium);
            selectCell.IndentLevel = 1;
            selectCell.Value = vo.supply + " / " + vo.ea + " / " + vo.unit + "EA";
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
        
        // 엑셀 영문자 컬럼을 순서 숫자로 변경  
        public static int GetExcelColumnToInt(string colName)
        {
            int[] digits = new int[colName.Length];
            for (int i = 0; i < colName.Length; ++i)
            {
                digits[i] = Convert.ToInt32(colName[i]) - 64;
            }
            int mul = 1; int res = 0;
            for (int pos = digits.Length - 1; pos >= 0; --pos)
            {
                res += digits[pos] * mul;
                mul *= 26;
            }
            return res;
        }

        //엑셀 숫자컬럼을 영문자로변경
        private static string ExcelColumnIndexToName(int Index)
        {
            string range = string.Empty;
            if (Index < 0) return range;
            int a = 26;
            int x = (int)Math.Floor(Math.Log((Index) * (a - 1) / a + 1, a));
            Index -= (int)(Math.Pow(a, x) - 1) * a / (a - 1);
            for (int i = x + 1; Index + i > 0; i--)
            {
                range = ((char)(65 + Index % a)).ToString() + range;
                Index /= a;
            }
            return range;
        }

        //성공 1 실패 0 만든 기존파일에 내용추가 피벗 결함
        static int makePivotContent(Excel.Workbook wb, Dictionary<string, List<readVO>> lists, String Start, String End)
        {
            try
            {
                Excel.Worksheet copyws = null;
                Excel.Worksheet pivotws = null;
                foreach (KeyValuePair<string, List<readVO>> data in lists)
                {
                    string sheet_name = null;
                    copyws = wb.Worksheets[data.Key] as Excel.Worksheet;
                    sheet_name = copyws.Name;
                    pivotws = wb.Sheets.Add(After: wb.Sheets[wb.Sheets.Count]);
                    pivotws.Name = sheet_name + "_피벗테이블(결함정보)";

                    Excel.Range oRange = copyws.get_Range(Start, End);

                    // specify first cell for pivot table
                    Excel.Range oRange2 = pivotws.get_Range("B2", "B2");

                    // create Pivot Cache and Pivot Table
                    Excel.PivotCache oPivotCache = (Excel.PivotCache)wb.PivotCaches().Add(Excel.XlPivotTableSourceType.xlDatabase, oRange);

                    // I have error on this line
                    Excel.PivotTable oPivotTable = (Excel.PivotTable)pivotws.PivotTables().Add(oPivotCache, oRange2, "Summary");

                    // create Pivot Field, note that name will be the same as column name on sheet one
                    Excel.PivotField oPivotField = (Excel.PivotField)oPivotTable.PivotFields(1);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlRowField;
                    int count = oPivotTable.PivotFields(1).PivotItems.Count;
                    oPivotField.PivotItems(count).visible = false;
                    oPivotField.Subtotals[1] = true;
                    oPivotField.Subtotals[1] = false;


                    oPivotField = (Excel.PivotField)oPivotTable.PivotFields(3);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlRowField;
                    count = oPivotTable.PivotFields(3).PivotItems.Count;
                    oPivotField.PivotItems(count).visible = false;
                    oPivotField.Subtotals[3] = true;
                    oPivotField.Subtotals[3] = false;


                    oPivotField = (Excel.PivotField)oPivotTable.PivotFields(5);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlRowField;
                    count = oPivotTable.PivotFields(5).PivotItems.Count;
                    oPivotField.PivotItems(count).visible = false;
                    oPivotField.Subtotals[5] = true;
                    oPivotField.Subtotals[5] = false;

                    oPivotField = (Excel.PivotField)oPivotTable.PivotFields(11);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlRowField;
                    count = oPivotTable.PivotFields(11).PivotItems.Count;
                    oPivotField.PivotItems(count).visible = false;

                    oPivotField = (Excel.PivotField)oPivotTable.PivotFields(9);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlDataField;
                    oPivotField.Function = Excel.XlConsolidationFunction.xlSum;
                    oPivotField.Value = "합계:개소";

                    oPivotField = (Excel.PivotField)oPivotTable.PivotFields(10);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlDataField;
                    oPivotField.Function = Excel.XlConsolidationFunction.xlSum;
                    oPivotField.Value = "합계:물량";

                    Excel.PivotField dataField = oPivotTable.DataPivotField;
                    dataField.Orientation = Excel.XlPivotFieldOrientation.xlColumnField;

                    Excel.Range rs = pivotws.UsedRange;

                    rs.Columns.AutoFit();
                    oPivotTable.SubtotalHiddenPageItems = true;

                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ReleaseExcelProcess(excelApp);
                MessageBox.Show(ex.ToString(), "오류발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        //성공 1 실패 0 만든 기존파일에 내용추가 피벗 경간
        static int makePivotPosition(Excel.Workbook wb, Dictionary<string, List<readVO>> lists, String Start, String End)
        {
            try
            {
                Excel.Worksheet copyws = null;
                Excel.Worksheet pivotws = null;
                foreach (KeyValuePair<string, List<readVO>> data in lists)
                {
                    string sheet_name = null;
                    copyws = wb.Worksheets[data.Key] as Excel.Worksheet;
                    sheet_name = copyws.Name;
                    pivotws = wb.Sheets.Add(After: wb.Sheets[wb.Sheets.Count]);
                    pivotws.Name = sheet_name + "_피벗테이블(경간)";

                    Excel.Range oRange = copyws.get_Range(Start, End);

                    // specify first cell for pivot table
                    Excel.Range oRange2 = pivotws.get_Range("B2", "B2");

                    // create Pivot Cache and Pivot Table
                    Excel.PivotCache oPivotCache = (Excel.PivotCache)wb.PivotCaches().Add(Excel.XlPivotTableSourceType.xlDatabase, oRange);

                    // I have error on this line
                    Excel.PivotTable oPivotTable = (Excel.PivotTable)pivotws.PivotTables().Add(oPivotCache, oRange2, "Summary");

                    // create Pivot Field, note that name will be the same as column name on sheet one
                    Excel.PivotField oPivotField = (Excel.PivotField)oPivotTable.PivotFields(1);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlRowField;
                    int count = oPivotTable.PivotFields(1).PivotItems.Count;
                    oPivotField.PivotItems(count).visible = false;
                    oPivotField.Subtotals[1] = true;
                    oPivotField.Subtotals[1] = false;

                    oPivotField = (Excel.PivotField)oPivotTable.PivotFields(3);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlRowField;
                    count = oPivotTable.PivotFields(3).PivotItems.Count;
                    oPivotField.PivotItems(count).visible = false;
                    oPivotField.Subtotals[3] = true;
                    oPivotField.Subtotals[3] = false;

                    oPivotField = (Excel.PivotField)oPivotTable.PivotFields(2);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlRowField;
                    count = oPivotTable.PivotFields(2).PivotItems.Count;
                    oPivotField.PivotItems(count).visible = false;
                    oPivotField.Subtotals[2] = true;
                    oPivotField.Subtotals[2] = false;

                    oPivotField = (Excel.PivotField)oPivotTable.PivotFields(5);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlRowField;
                    count = oPivotTable.PivotFields(5).PivotItems.Count;
                    oPivotField.PivotItems(count).visible = false;
                    oPivotField.Subtotals[5] = true;
                    oPivotField.Subtotals[5] = false;

                    oPivotField = (Excel.PivotField)oPivotTable.PivotFields(11);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlRowField;
                    count = oPivotTable.PivotFields(11).PivotItems.Count;
                    oPivotField.PivotItems(count).visible = false;

                    oPivotField = (Excel.PivotField)oPivotTable.PivotFields(9);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlDataField;
                    oPivotField.Function = Excel.XlConsolidationFunction.xlSum;
                    oPivotField.Value = "합계:개소";

                    oPivotField = (Excel.PivotField)oPivotTable.PivotFields(10);
                    oPivotField.Orientation = Excel.XlPivotFieldOrientation.xlDataField;
                    oPivotField.Function = Excel.XlConsolidationFunction.xlSum;
                    oPivotField.Value = "합계:물량";

                    Excel.PivotField dataField = oPivotTable.DataPivotField;
                    dataField.Orientation = Excel.XlPivotFieldOrientation.xlColumnField;

                    Excel.Range rs = pivotws.UsedRange;

                    rs.Columns.AutoFit();
                    oPivotTable.SubtotalHiddenPageItems = true;

                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ReleaseExcelProcess(excelApp);
                MessageBox.Show(ex.ToString(), "오류발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

        }

        //성공 1 실패 0 만든 기존파일에 내용추가 결과 요약
        static int makeResultFiles(Excel.Workbook wb, Dictionary<string, List<readVO>> lists)
        {
            try
            {
                Excel.Worksheet copyws = null;
                Excel.Worksheet resultws = null;
                foreach (KeyValuePair<string, List<readVO>> data in lists)
                {
                    string sheet_name = null;
                    copyws = wb.Worksheets[data.Key] as Excel.Worksheet;
                    sheet_name = copyws.Name;
                    resultws = wb.Sheets.Add(After: wb.Sheets[wb.Sheets.Count]);

                    resultws.Name = sheet_name + "_결과요약";
                    List<readVO> orignalData = data.Value;

                    ReleaseExcelObject(copyws);
                    ReleaseExcelObject(resultws);
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.ToString(), "오류발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
    }
}
