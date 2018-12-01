using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace combineExport.appAuth
{
    class Me
    {
        public Me() {
            username = "";
            encryptedPassword = "";
        }
        public Me(String username, String plainPassword)
        {
            this.username = username;
            this.encryptedPassword = Crypto.Encrypt(plainPassword);
        }
        public String username { get; set; }
        public String encryptedPassword
        {
            get; set;
        }

        public void setEncryptedPasswordByPalinPassword(string plainPassword)
        {
            encryptedPassword = Crypto.Encrypt(plainPassword);
        }
        public string getDecryptedPassword()
        {
            return Crypto.Decrypt(encryptedPassword);
        }
    }
}
