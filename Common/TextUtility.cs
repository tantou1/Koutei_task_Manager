using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TextUtility
    {
        public static string DecryptData_Henkou(string encryptpwd)
        {
            //string strDecryptpwd = string.Empty;
            //UTF8Encoding encodepwd = new UTF8Encoding();
            //Decoder Decode = encodepwd.GetDecoder();
            //byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            //int intCharCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            //char[] decoded_char = new char[intCharCount];
            //Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            //strDecryptpwd = new String(decoded_char);
            //return strDecryptpwd;
            return DESEncryp.Decrypt(encryptpwd, "demo20", "");
        }
    }
}
