using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace combineExport.appAuth
{
    //Authentication은 클라이언트 -> 서버 로 보내는 메세지. json으로 만들어 보낸다.
    class Authentication
    {
        public static String NORMAL = "NORMAL"; //일반 로그인
        public static String ACTIVATE_NEW = "ACTIVATE_NEW"; // 이전 구독이 만료되고, 활성화 가능한 구독이 있을 때 이를 활성화 시키면서 로그인

        public string username { get; set; }
        public string password { get; set; }
        public string type { get; set; } //
        public string clientId { get; set; }
    }

    //AuthencationResult는 서버 -> 클라이언트. Authencation에 대한 답장. json메세지를 이 클래스로 parsing한다.
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

    //활성화된 구독 정보
    class ActivatedSubscription
    {
        public String id { get; set; }
        public String activatedAt { get; set; }
    }
}
