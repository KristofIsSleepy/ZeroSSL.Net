using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroSSL.Net.Model.Misc;

namespace ZeroSSL.Net.Model.Output
{
    public class Certificate : IOutput
    {
        public string id { get; set; }
        public CertificateType type { get; set; }
        public string common_name { get; set; }
        public string additional_domains { get; set; }
        public string created { get; set; }
        public string expires { get; set; }
        public CertificateStatus status { get; set; }
        public VerificationMethod validation_type { get; set; }
        public string validation_emails { get; set; }
        public string replacement_for { get; set; }
        public Validation validation { get; set; }

        public class Validation
        {
            public JObject email_validation { get; set; }
            public JObject other_methods { get; set; }
        }
    }
}
