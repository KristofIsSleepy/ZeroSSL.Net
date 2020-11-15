using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroSSL.Net.Model.Input.POST
{
    public class CreateCertificatePOST : InputBasePOST
    {
        public string certificate_domains { get; set; }
        public string certificate_validity_days { get; set; }
        public string certifice_csr { get; set; }
    }
}
