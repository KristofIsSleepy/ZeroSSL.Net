using ZeroSSL.Net;
using ZeroSSL.Net.Model.Input.GET;
using ZeroSSL.Net.Model.Input.POST;
using ZeroSSL.Net.Model.Output;

namespace Tests
{
    public static class TestClientGeneralErrors
    {
        public static void MissingAPIKey()
        {
            RequestHandler.ApiKey = "";
            string dummyEndpoint = RequestHandler.ConstructRequestEndpoint(TestData.dummy_endpoint);
            RequestHandler.SendRequest<CertificateDetails>(dummyEndpoint, null, "GET");
        }

        public static void InvalidAPIKey()
        {
            RequestHandler.ApiKey = "invalid_key";
            string dummyEndpoint = RequestHandler.ConstructRequestEndpoint(TestData.dummy_endpoint);
            RequestHandler.SendRequest<CertificateDetails>(dummyEndpoint, null, "GET");
        }

        public static void InvalidAPIFunction(string apiKey)
        {
            RequestHandler.ApiKey = apiKey;
            string dummyEndpoint = RequestHandler.ConstructRequestEndpoint(TestData.dummy_endpoint);
            RequestHandler.SendRequest<CertificateDetails>(dummyEndpoint, null, "GET");
        }

        /// <summary>
        /// Does not throw expected exception - cannot produce conditions
        /// </summary>
        /// <param name="apiKey"></param>
        public static void IncorrectRequestType(string apiKey)
        {
            RequestHandler.ApiKey = apiKey;
            string dummyEndpoint = RequestHandler.ConstructRequestEndpoint("acme/eab-credentials");
            RequestHandler.SendRequest<CertificateDetails>(dummyEndpoint, null, "GET");
        }

        /// <summary>
        /// Does not throw expected exception - cannot produce conditions
        /// </summary>
        /// <param name="apiKey"></param>
        public static void PermissionDenied(string apiKey)
        {
            RequestHandler.ApiKey = apiKey;
            string dummyEndpoint = RequestHandler.ConstructRequestEndpoint("acme/eab-credentials");
            RequestHandler.SendRequest<CertificateDetails>(dummyEndpoint, null, "GET");
        }

        /// <summary>
        /// Does not throw expected exception - cannot reproduce conditions
        /// </summary>
        /// <param name="apiKey"></param>
        public static void MissingCertificateHash(string apiKey)
        {
            RequestHandler.ApiKey = apiKey;
            string dummyEndpoint = RequestHandler.ConstructRequestEndpoint("certificates//status");
            RequestHandler.SendRequest<CertificateDetails>(dummyEndpoint, null, "GET");
        }

        public static void CertificateNotFound(string apiKey)
        {
            RequestHandler.ApiKey = apiKey;
            string dummyEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{TestData.invalid_certificate}/status");
            RequestHandler.SendRequest<CertificateDetails>(dummyEndpoint, null, "GET");
        }
    }
}
