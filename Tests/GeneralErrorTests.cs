using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZeroSSL.Net;
using ZeroSSL.Net.Model.Exceptions;
using ZeroSSL.Net.Model.Input.GET;
using ZeroSSL.Net.Model.Input.POST;
using ZeroSSL.Net.Model.Misc;

namespace Tests
{
    [TestClass]
    public class GeneralErrorTests
    {
        string apiKey = "726d3895dc963c3922358e5bb032044a";

        [TestMethod]
        public void MissingAccessKey()
        {
            bool wasSuccessful = false;

            try
            {
                TestClientGeneralErrors.MissingAPIKey();
            }
            catch (EmptyAPIKeyException ex)
            {
                wasSuccessful = true;
            }

            Assert.IsTrue(wasSuccessful);
        }

        [TestMethod]
        public void InvalidAccessKey()
        {
            bool wasSuccessful = false;

            try
            {
                TestClientGeneralErrors.InvalidAPIKey();
            }
            catch (ZeroSSLErrorException ex)
            {
                wasSuccessful = (ex.Code == "101" && ex.Type == "invalid_access_key" && ex.Info == "You have not supplied a valid API Access Key. [Technical Support: support@apilayer.com]");
            }

            Assert.IsTrue(wasSuccessful);
        }

        [TestMethod]
        public void InvalidApiFunction()
        {
            bool wasSuccessful = false;

            try
            {
                TestClientGeneralErrors.InvalidAPIFunction(apiKey);
            }
            catch (ZeroSSLErrorException ex)
            {
                wasSuccessful = (ex.Code == "103" && ex.Type == "invalid_api_function" && ex.Info == "This API Function does not exist.");
            }

            Assert.IsTrue(wasSuccessful);
        }

        /// <summary>
        /// Cannot produce conditions to make this succeed
        /// </summary>
        [TestMethod]
        [Ignore]
        public void IncorrectRequestType()
        {
            bool wasSuccessful = false;

            try
            {
                TestClientGeneralErrors.IncorrectRequestType(apiKey);
            }
            catch (ZeroSSLErrorException ex)
            {
                wasSuccessful = (ex.Code == "2800" && ex.Type == "incorrect_request_type");
            }

            Assert.IsTrue(wasSuccessful);
        }

        /// <summary>
        /// Cannot produce conditions to make this succeed
        /// </summary>
        [TestMethod]
        [Ignore]
        public void PermissionDenied()
        {
            bool wasSuccessful = false;

            try
            {
                TestClientGeneralErrors.PermissionDenied(apiKey);
            }
            catch (ZeroSSLErrorException ex)
            {
                wasSuccessful = (ex.Code == "2800" && ex.Type == "incorrect_request_type");
            }

            Assert.IsTrue(wasSuccessful);
        }

        /// <summary>
        /// Cannot produce conditions to make this succeed
        /// </summary>
        [TestMethod]
        [Ignore]
        public void MissingCertificateHash()
        {
            bool wasSuccessful = false;

            try
            {
                TestClientGeneralErrors.MissingCertificateHash(apiKey);
            }
            catch (ZeroSSLErrorException ex)
            {
                wasSuccessful = (ex.Code == "2800" && ex.Type == "incorrect_request_type");
            }

            Assert.IsTrue(wasSuccessful);
        }

        [TestMethod]
        public void CertificateNotFound()
        {
            bool wasSuccessful = false;

            try
            {
                TestClientGeneralErrors.CertificateNotFound(apiKey);
            }
            catch (ZeroSSLErrorException ex)
            {
                wasSuccessful = (ex.Code == "2803" && ex.Type == "certificate_not_found" && ex.Info == null);
            }

            Assert.IsTrue(wasSuccessful);
        }
    }
}