using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSSL.Net.Model.Input.GET
{
    public class ListCertificatesGET : InputBaseGET
    {
        public string certificate_status { get; set; }
        public string search { get; set; }
        public int limit { get; set; } = 100;
        public int page { get; set; } = 1;
    }
}
