using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSSL.Net.Model.Output
{
    public class VerificationStatus : IOutput
    {
        public bool validation_completed { get; set; }
        public JObject details { get; set; }
    }
}