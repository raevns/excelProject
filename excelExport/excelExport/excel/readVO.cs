using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace excelExport.excel
{
    public class readVO
    {
        public String category { set; get; }//구분 추가함, 결과정리페이지용
        public String position { set; get; } //경간
        public String sub_position { set; get; } //부재
        public String content { set; get; } // 결함종류
        public String unit { set; get; } // 개소
        public String supply { set; get; } // 물량
        public String ea { set; get; } //단위
        public String pictureFileNameInExcel { set; get; } //사진번호
        public int sheetnum { set; get; }//시트넘버 쓰려고 추가한거
        public int orignalImgCell { set; get; } //원본시트의 사진번호 셀
        //private File pictureFile { set; get; }
    }
}
