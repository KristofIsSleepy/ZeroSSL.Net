using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSSL.Net.Model.Misc
{
    public enum CertificateStatus
    {
        draft,
        pending_validation,
        issued,
        cancelled,
        expiring_soon,
        expired
    }
}
