using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZeroSSL.Net;
using ZeroSSL.Net.Model.Exceptions;
using ZeroSSL.Net.Model.Input.GET;
using ZeroSSL.Net.Model.Input.POST;
using ZeroSSL.Net.Model.Misc;

namespace Tests
{
    [TestClass]
    public class ZeroSSLTests
    {
        string apiKey = "";

        [TestMethod]
        public void CreateCertificate()
        {
            var certificateInput = new CreateCertificatePOST();

            var client = new ZeroSSLClient(apiKey);

            try
            {
                var certificate = client.CreateCertificate(certificateInput);
            }
            catch (ZeroSSLErrorException ex)
            {

            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public void VerifyDomains()
        {
            var verifyDomainsInput = new VerifyDomainsPOST(VerificationMethod.EMAIL);

            var client = new ZeroSSLClient(apiKey);

            client.VerifyDomains("227c21d7deba60807d9dc76d3c1c5e4f", verifyDomainsInput);
        }

        [TestMethod]
        public void DownloadCertificateZIP()
        {
            var client = new ZeroSSLClient(apiKey);
            client.DownloadCertificateZIP("227c21d7deba60807d9dc76d3c1c5e4f", @"C:\temp\1.zip");
        }

        [TestMethod]
        public void DownloadCertificateInline()
        {
            var client = new ZeroSSLClient(apiKey);
            client.DownloadCertificateInline("227c21d7deba60807d9dc76d3c1c5e4f");
        }

        [TestMethod]
        public void GetCertificate()
        {
            var client = new ZeroSSLClient(apiKey);
            client.GetCertificate("227c21d7deba60807d9dc76d3c1c5e4f");
        }

        [TestMethod]
        public void ListCertificates()
        {
            var client = new ZeroSSLClient(apiKey);
            client.ListCertificates(new ListCertificatesGET());
        }

        [TestMethod]
        public void VerificationStatus()
        {
            var client = new ZeroSSLClient(apiKey);
            client.VerificationStatus("227c21d7deba60807d9dc76d3c1c5e4f");
        }

        [TestMethod]
        public void ResendVerificationEmail()
        {
            var client = new ZeroSSLClient(apiKey);
            client.ResendVerificationEmail("227c21d7deba60807d9dc76d3c1c5e4f");
        }

        [TestMethod]
        public void CancelCertificate()
        {
            var client = new ZeroSSLClient(apiKey);
            client.CancelCertificate("227c21d7deba60807d9dc76d3c1c5e4f");
        }
    }
}
