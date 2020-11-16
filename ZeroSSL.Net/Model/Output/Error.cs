using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSSL.Net.Model.Output
{
    public class Error : SuccessStatus, IOutput
    {
        public JObject error { get; set; }
    }
}
