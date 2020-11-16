using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSSL.Net.Model.Exceptions
{
    public class EmptyAPIKeyException : Exception
    {
        public EmptyAPIKeyException() : base("API Key is empty or null.")
        {
        }
    }
}
