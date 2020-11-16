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
        public ZeroSSLClient(string _apiKey)
        {
            RequestHandler.ApiKey = _apiKey;
        }

        /// <summary>
        /// Create a certificate.
        /// </summary>
        /// <param name="postParameters">View ZeroSSL Api Documentation</param>
        /// <returns>The created certificate's details.</returns>
        public CertificateDetails CreateCertificate(CreateCertificatePOST postParameters)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint("certificates");
            return RequestHandler.SendRequest<CertificateDetails>(absoluteEndpoint, postParameters, "POST");
        }

        /// <summary>
        /// Initiate verification process for a specific certificate.
        /// </summary>
        /// <param name="certificateId">Certificate hash</param>
        /// <param name="postParameters">View ZeroSSL Api Documentation</param>
        /// <returns></returns>
        public VerifyDomains VerifyDomains(string certificateId, VerifyDomainsPOST postParameters)
        {
            throw new NotImplementedException("Response object not created.");
            //var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}/challenges");
            //return RequestHandler.SendRequest<VerifyDomains>(absoluteEndpoint, postParameters, "POST");
        }

        /// <summary>
        /// Write a specific certificate .ZIP file to disk.
        /// </summary>
        /// <param name="certificateId">Certificate hash</param>
        /// <param name="targetFile">Absolute file path to write .ZIP file to. e.g. C:\temp\output.zip</param>
        public void DownloadCertificateZIP(string certificateId, string targetFile)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}/download");

            using (var client = new WebClient())
            {
                client.DownloadFile(absoluteEndpoint, targetFile);
            }
        }

        /// <summary>
        /// Retrieve a specific certificate's contents as strings.
        /// </summary>
        /// <param name="certificateId">Certificate hash</param>
        /// <returns>A specific certificate's contents as strings.</returns>
        public InlineCertificate DownloadCertificateInline(string certificateId)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}/download/return");
            return RequestHandler.SendRequest<InlineCertificate>(absoluteEndpoint, null, "GET");
        }

        /// <summary>
        /// Retrieve a specific certificate's details.
        /// </summary>
        /// <param name="certificateId">Certificate hash</param>
        /// <returns>A specific certificate's details.</returns>
        public CertificateDetails GetCertificate(string certificateId)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}");
            return RequestHandler.SendRequest<CertificateDetails>(absoluteEndpoint, null, "GET");
        }

        /// <summary>
        /// Search for certificate details.
        /// </summary>
        /// <param name="getParameters">Search specification.</param>
        /// <returns>Total number found, number returned, page, limit per page, and details of each one.</returns>
        public ListCertificates ListCertificates(ListCertificatesGET getParameters)
        {
            string parameters = $"certificate_status={getParameters.certificate_status}&search={getParameters.search}&limit={getParameters.limit}&page={getParameters.page}";
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates", parameters);
            return RequestHandler.SendRequest<ListCertificates>(absoluteEndpoint, null, "GET");
        }

        /// <summary>
        /// Get the verification status of a specified certificate.
        /// </summary>
        /// <param name="certificateId">Certificate hash</param>
        /// <returns>The verification status of a certificate.</returns>
        public VerificationStatus VerificationStatus(string certificateId)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}/status");
            return RequestHandler.SendRequest<VerificationStatus>(absoluteEndpoint, null, "GET");
        }

        /// <summary>
        /// Attempts to resend a specific certificate's verification email. Only available for certificates that are attempting to verify via Email.
        /// </summary>
        /// <param name="certificateId">Certificate hash</param>
        /// <returns>The success status.</returns>
        public SuccessStatus ResendVerificationEmail(string certificateId)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}/challenges/email");
            return RequestHandler.SendRequest<SuccessStatus>(absoluteEndpoint, null, "POST");
        }

        /// <summary>
        /// Cancel a certificate.
        /// </summary>
        /// <param name="certificateId">Certificate hash</param>
        /// <returns>The success status.</returns>
        public SuccessStatus CancelCertificate(string certificateId)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}/cancel");
            return RequestHandler.SendRequest<SuccessStatus>(absoluteEndpoint, null, "POST");
        }

        /// <summary>
        /// Delete a certificate.
        /// </summary>
        /// <param name="certificateId">Certificate hash</param>
        /// <returns>The success status.</returns>
        public SuccessStatus DeleteCertificate(string certificateId)
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"certificates/{certificateId}");
            return RequestHandler.SendRequest<SuccessStatus>(absoluteEndpoint, null, "DELETE");
        }

        /// <summary>
        /// Generates one set of ACME EAB credentials.
        /// </summary>
        /// <returns>One set of ACME EAB credentials.</returns>
        public EABCredentials GenerateEABCredentials()
        {
            var absoluteEndpoint = RequestHandler.ConstructRequestEndpoint($"acme/eab-credentials");
            return RequestHandler.SendRequest<EABCredentials>(absoluteEndpoint, null, "POST");
        }
    }
}
