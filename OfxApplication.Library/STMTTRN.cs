using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfxApplication.Library
{
    public class STMTTRN : Object
    {
        public string type { get; set; }
        public string dtPosted { get; set; }
        public string trnamt { get; set; }
        public string fitid { get; set; }
        public string memo { get; set; }
        public Guid id { get; set; }
        public string hash { get; set; }

        //create a hash with md5 using: type + trnamt + fitid
        public string GetHashCode()
        {
            string checksum = "";
            hash = type + trnamt + fitid;

            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            checksum = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(hash))).Replace("-", String.Empty);

            return checksum;
        }
    }
}
