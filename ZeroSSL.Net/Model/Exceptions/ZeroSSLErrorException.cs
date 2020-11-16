using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSSL.Net.Model.Exceptions
{
    public class ZeroSSLErrorException : Exception
    {
        public string Code { get; set; }
        public string Type { get; set; }

        public ZeroSSLErrorException(string code, string type)
        {
            Code = code;
            Type = type;
        }
    }
}
