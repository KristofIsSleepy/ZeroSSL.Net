using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSSL.Net.Model.Output
{
    public class EABCredentials : SuccessStatus, IOutput
    {
        public string eab_kid { get; set; }
        public string eab_hmac_key { get; set; }
    }
}
