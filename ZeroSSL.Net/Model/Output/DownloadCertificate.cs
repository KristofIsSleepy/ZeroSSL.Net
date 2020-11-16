using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSSL.Net.Model.Output
{
    public class DownloadCertificate : IOutput
    {
        [JsonProperty("certificate.crt")]
        public string certificate { get; set; }
        [JsonProperty("ca_bundle.crt")]
        public string ca_bundle { get; set; }
    }
}
