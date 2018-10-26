using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace combineExport.excel
{
    class readPivotVO
    {
        public String category { set; get; }//구분 추가함, 결과정리페이지용
        public String sub_position { set; get; } //부재
        public String content { set; get; } // 결함종류
        public String ea { set; get; } //단위
        public String unit { set; get; } // 개소
        public String supply { set; get; } // 물량
    }
}
