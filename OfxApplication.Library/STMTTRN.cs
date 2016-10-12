using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfxApplication.Library
{
    public class STMTTRN
    {
        public Guid id { get; set; }
        public string type { get; set; }
        public string dtPosted { get; set; }
        public string trnamt { get; set; }
        public string fitid { get; set; }
        public string memo { get; set; }
    }
}
