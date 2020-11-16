using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using ZeroSSL.Net.Model.Exceptions;
using ZeroSSL.Net.Model.Input.GET;
using ZeroSSL.Net.Model.Input.POST;
using ZeroSSL.Net.Model.Output;

namespace ZeroSSL.Net
{
    public class ZeroSSLClient : IClient
    {
        public ZeroSSLClient(string _accessKey)
        {
            RequestHandler.AccessKey = _accessKey;
        }

        public Certificate CreateCertificate(CreateCertificatePOST postParameters)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint("certificates");
            return RequestHandler.SendRequest<Certificate>(absoluteEndpoint, postParameters, "POST");
        }

        public VerifyDomains VerifyDomains(string certificateId, VerifyDomainsPOST postParameters)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}/challenges");
            return RequestHandler.SendRequest<VerifyDomains>(absoluteEndpoint, postParameters, "POST");
        }

        public void DownloadCertificateZIP(string certificateId, string targetFile)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}/download");

            using (var client = new WebClient())
            {
                client.DownloadFile(absoluteEndpoint, targetFile);
            }
        }

        public DownloadCertificate DownloadCertificateInline(string certificateId)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}/download/return");
            return RequestHandler.SendRequest<DownloadCertificate>(absoluteEndpoint, null, "GET");
        }

        public Certificate GetCertificate(string certificateId)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}");
            return RequestHandler.SendRequest<Certificate>(absoluteEndpoint, null, "GET");
        }

        public ListCertificates ListCertificates(ListCertificatesGET getParameters)
        {
            string parameters = $"certificate_status={getParameters.certificate_status}&search={getParameters.search}&limit={getParameters.limit}&page={getParameters.page}";
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates", parameters);
            return RequestHandler.SendRequest<ListCertificates>(absoluteEndpoint, null, "GET");
        }

        public VerificationStatus VerificationStatus(string certificateId)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}/status");
            return RequestHandler.SendRequest<VerificationStatus>(absoluteEndpoint, null, "GET");
        }

        public SuccessStatus ResendVerificationEmail(string certificateId)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}/challenges/email");
            return RequestHandler.SendRequest<SuccessStatus>(absoluteEndpoint, null, "POST");
        }

        public SuccessStatus CancelCertificate(string certificateId)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}/cancel");
            return RequestHandler.SendRequest<SuccessStatus>(absoluteEndpoint, null, "POST");
        }

        public SuccessStatus DeleteCertificate(string certificateId)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}");
            return RequestHandler.SendRequest<SuccessStatus>(absoluteEndpoint, null, "DELETE");
        }

        public EABCredentials GenerateEABCredentials()
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"acme/eab-credentials");
            return RequestHandler.SendRequest<EABCredentials>(absoluteEndpoint, null, "POST");
        }
    }
}
