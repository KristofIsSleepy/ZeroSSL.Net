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
    public class ZeroSSLClient
    {
        public string BaseEndpoint { private get; set; } = "api.zerossl.com";
        private string accessKey { get; set; }

        public ZeroSSLClient(string _accessKey)
        {
            accessKey = _accessKey;
        }

        public Certificate CreateCertificate(CreateCertificatePOST postParameters)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint("certificates");
            return SendRequest<Certificate>(absoluteEndpoint, postParameters, "POST");
        }

        public VerifyDomains VerifyDomains(string certificateId, VerifyDomainsPOST postParameters)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates/{certificateId}/challenges");
            return SendRequest<VerifyDomains>(absoluteEndpoint, postParameters, "POST");
        }

        public void DownloadCertificateZIP(string certificateId, string targetFile)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates/{certificateId}/download");

            using (var client = new WebClient())
            {
                client.DownloadFile(absoluteEndpoint, targetFile);
            }
        }

        public DownloadCertificate DownloadCertificateInline(string certificateId)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates/{certificateId}/download/return");
            return SendRequest<DownloadCertificate>(absoluteEndpoint, null, "GET");
        }

        public Certificate GetCertificate(string certificateId)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates/{certificateId}");
            return SendRequest<Certificate>(absoluteEndpoint, null, "GET");
        }

        public ListCertificates ListCertificates(ListCertificatesGET getParameters)
        {
            string parameters = $"certificate_status={getParameters.certificate_status}&search={getParameters.search}&limit={getParameters.limit}&page={getParameters.page}";
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates", parameters);
            return SendRequest<ListCertificates>(absoluteEndpoint, null, "GET");
        }

        public VerificationStatus VerificationStatus(string certificateId)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates/{certificateId}/status");
            return SendRequest<VerificationStatus>(absoluteEndpoint, null, "GET");
        }

        public SuccessStatus ResendVerificationEmail(string certificateId)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates/{certificateId}/challenges/email");
            return SendRequest<SuccessStatus>(absoluteEndpoint, null, "POST");
        }

        public SuccessStatus CancelCertificate(string certificateId)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates/{certificateId}/cancel");
            return SendRequest<SuccessStatus>(absoluteEndpoint, null, "POST");
        }

        public SuccessStatus DeleteCertificate(string certificateId)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates/{certificateId}");
            return SendRequest<SuccessStatus>(absoluteEndpoint, null, "DELETE");
        }

        public EABCredentials GenerateEABCredentials()
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"acme/eab-credentials");
            return SendRequest<EABCredentials>(absoluteEndpoint, null, "POST");
        }

        private T SendRequest<T>(string endpointDirectory, InputBasePOST input, string method) where T : IOutput
        {
            var requester = WebRequest.Create(endpointDirectory);
            requester.Method = method;

            if (requester.Method == "POST")
            {
                requester.ContentType = "application/json";

                //Gather data we want to post into JSON string then Byte array
                var postData = JsonConvert.SerializeObject(input);
                Byte[] bytes = new ASCIIEncoding().GetBytes(postData);

                //Write the bytes to the stream
                using (var stream = requester.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            var response = requester.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();

                //Check for errors first
                var errorObject = JsonConvert.DeserializeObject<Error>(content);

                if (errorObject.error != null)
                {
                    string code = "";
                    string type = "";

                    foreach(var obj in errorObject.error)
                    {
                        switch(obj.Key)
                        {
                            case "code":
                                code = obj.Value.ToString();
                                break;
                            case "type":
                                type = obj.Value.ToString();
                                break;
                            default:
                                throw new Exception("Unexpected token in error response.");
                        }
                    }

                    if (code == "" || type == "")
                    {
                        throw new Exception("Could not set code and/or type. Has the API definition changed?");
                    }
                    else
                    {
                        throw new ZeroSSLErrorException(code, type);
                    }
                }
                else
                {
                    var responseObject = JsonConvert.DeserializeObject<T>(content);
                    return responseObject;
                }
            }
        }

        private string ConstructAbsoluteEndpoint(string endpointDirectory)
        {
            return $@"https://{BaseEndpoint}/{endpointDirectory}?access_key={accessKey}";
        }

        private string ConstructAbsoluteEndpoint(string endpointDirectory, string additionalParameters)
        {
            return $@"https://{BaseEndpoint}/{endpointDirectory}?access_key={accessKey}&{additionalParameters}";
        }
    }
}
