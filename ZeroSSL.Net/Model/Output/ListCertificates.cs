using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSSL.Net.Model.Output
{
    public class ListCertificates : IOutput
    {
        public int total_count { get; set; }
        public int result_count { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public List<CertificateDetails> results { get; set; }
    }
}
