using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace combineExport.conn
{
    class AuthenticationResult
    {
        public static String AUTHORIZED = "AUTHORIZED"; //인증성공
        public static String UNAUHORIZED = "UNAUHORIZED"; //인증실패
        public static String ERROR = "ERROR"; //에러 , message 필드에 에러 원인이 입력되있다.
        public static String NEED_TO_ACTIVATE_NEW = "NEED_TO_ACTIVATE_NEW"; // 새 구독 활성화 필요
        public static String DUPLICATED = "DUPLICATED"; // 로그인 중복, 강제 로그인 해야함

        public string resultCode { get; set; } // authorized, unauthorized, needToActivateNew
        public ActivatedSubscription activatedSubscription { get; set; }    //현재 활성화된 구독 정보
        public ActivatedSubscription message { get; set; } // 메세지.. 에러 메세지로만 쓰일듯
    }
}
