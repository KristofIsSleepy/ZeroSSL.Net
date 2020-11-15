using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSSL.Net.Model.Input.POST
{
    public class VerifyDomainsPOST : InputBasePOST
    {
        public VerificationMethod validation_method { get; set; }
        public string validation_email { get; set; }

        public VerifyDomainsPOST(VerificationMethod validationMethod)
        {
            validation_method = validationMethod;
        }
    }
}
