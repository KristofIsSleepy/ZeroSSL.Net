using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZeroSSL.Net;
using ZeroSSL.Net.Model;
using ZeroSSL.Net.Model.Input.GET;
using ZeroSSL.Net.Model.Input.POST;

namespace Tests
{
    [TestClass]
    public class ZeroSSLTests
    {
        [TestMethod]
        public void CreateCertificate()
        {
            var certificateInput = new CreateCertificatePOST();

            var client = new ZeroSSLClient("9918e4ec1bf76ae109b3b4077eec6942");

            client.CreateCertificate(certificateInput);
        }

        [TestMethod]
        public void VerifyDomains()
        {
            var verifyDomainsInput = new VerifyDomainsPOST(ValidationMethod.EMAIL);

            var client = new ZeroSSLClient("9918e4ec1bf76ae109b3b4077eec6942");

            client.VerifyDomains("227c21d7deba60807d9dc76d3c1c5e4f", verifyDomainsInput);
        }

        [TestMethod]
        public void DownloadCertificateZIP()
        {
            var client = new ZeroSSLClient("9918e4ec1bf76ae109b3b4077eec6942");
            client.DownloadCertificateZIP("227c21d7deba60807d9dc76d3c1c5e4f", @"C:\temp\1.zip");
        }

        [TestMethod]
        public void DownloadCertificateInline()
        {
            var client = new ZeroSSLClient("9918e4ec1bf76ae109b3b4077eec6942");
            client.DownloadCertificateInline("227c21d7deba60807d9dc76d3c1c5e4f");
        }

        [TestMethod]
        public void GetCertificate()
        {
            var client = new ZeroSSLClient("9918e4ec1bf76ae109b3b4077eec6942");
            client.GetCertificate("227c21d7deba60807d9dc76d3c1c5e4f");
        }

        [TestMethod]
        public void ListCertificates()
        {
            var client = new ZeroSSLClient("9918e4ec1bf76ae109b3b4077eec6942");
            client.ListCertificates(new ListCertificatesGET());
        }
    }
}
