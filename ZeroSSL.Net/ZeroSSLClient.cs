﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using ZeroSSL.Net.Model.Input;
using ZeroSSL.Net.Model.Input.GET;
using ZeroSSL.Net.Model.Input.POST;

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

        public void CreateCertificate(CreateCertificatePOST postParameters)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint("certificates");
            SendRequest(absoluteEndpoint, postParameters);
        }

        public void VerifyDomains(string certificateId, VerifyDomainsPOST postParameters)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates/{certificateId}/challenges");
            SendRequest(absoluteEndpoint, postParameters);
        }

        public void DownloadCertificateZIP(string certificateId, string targetFile)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates/{certificateId}/download");

            using (var client = new WebClient())
            {
                client.DownloadFile(absoluteEndpoint, targetFile);
            }
        }

        public void DownloadCertificateInline(string certificateId)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates/{certificateId}/download/return");
            SendRequest(absoluteEndpoint, null);
        }

        public void GetCertificate(string certificateId)
        {
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates/{certificateId}");
            SendRequest(absoluteEndpoint, null);
        }

        public void ListCertificates(ListCertificatesGET getParameters)
        {
            string parameters = $"certificate_status={getParameters.certificate_status}&search={getParameters.search}&limit={getParameters.limit}&page={getParameters.page}";
            var absoluteEndpoint = ConstructAbsoluteEndpoint($"certificates", parameters);
            SendRequest(absoluteEndpoint, null);
        }

        private void SendRequest(string endpointDirectory, InputBasePOST input)
        {
            var requester = WebRequest.Create(endpointDirectory);
            requester.Method = input == null ? "GET" : "POST";

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
