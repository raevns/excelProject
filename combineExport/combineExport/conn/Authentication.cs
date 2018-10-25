using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace combineExport.conn
{
    class Authentication
    {
        public static String NORMAL = "NORMAL"; //일반 로그인
        //public static String ENFORCED = "ENFORCED"; // 중복로그인 -> 강제로그인 없앰
        public static String ACTIVATE_NEW = "ACTIVATE_NEW"; // 이전 구독이 만료되고, 활성화 가능한 구독이 있을 때 이를 활성화 시키면서 로그인

        public string username { get; set; }
        public string password { get; set; }
        public string type { get; set; } //
        public string clientId { get; set; }
    }
}
