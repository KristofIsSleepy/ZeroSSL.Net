using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSSL.Net.Model.Misc
{
    public enum CertificateType
    {
        NinetyDay = 1,
        NinetyDayWildCard = 2,
        NinetyDayMultiDomain = 3,
        Year = 4,
        YearWildCard = 5,
        YearMultiDomain = 6
    }
}
