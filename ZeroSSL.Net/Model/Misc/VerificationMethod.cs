using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSSL.Net.Model.Misc
{
    public enum VerificationMethod
    {
        EMAIL,
        CNAME_CSR_HASH,
        HTTP_CSR_HASH,
        HTTPS_CSR_HASH
    }
}
