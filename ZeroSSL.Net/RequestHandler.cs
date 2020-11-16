using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZeroSSL.Net.Model.Exceptions;
using ZeroSSL.Net.Model.Input.POST;
using ZeroSSL.Net.Model.Output;

namespace ZeroSSL.Net
{
    public static class RequestHandler
    {
        public static string BaseEndpoint { get; set; } = "api.zerossl.com";
        public static string AccessKey { get; set; }

        public static T SendRequest<T>(string endpointDirectory, InputBasePOST input, string method) where T : IOutput
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
                    var zeroSSLException = new ZeroSSLErrorException();

                    foreach (var obj in errorObject.error)
                    {
                        switch (obj.Key)
                        {
                            case "code":
                                zeroSSLException.Code = obj.Value.ToString();
                                break;
                            case "type":
                                zeroSSLException.Type = obj.Value.ToString();
                                break;
                            case "info":
                                zeroSSLException.Info = obj.Value.ToString();
                                break;
                            default:
                                throw new Exception("Unexpected token in error response.");
                        }
                    }

                    if (zeroSSLException.Code == "" || zeroSSLException.Type == "")
                    {
                        throw new Exception("Could not set code and/or type. Has the API definition changed?");
                    }
                    else
                    {
                        throw zeroSSLException;
                    }
                }
                else
                {
                    var responseObject = JsonConvert.DeserializeObject<T>(content);
                    return responseObject;
                }
            }
        }

        public static string ConstructRequestEndpoint(string endpointDirectory)
        {
            //Verify that the consumer has set the AccessKey
            if(String.IsNullOrEmpty(AccessKey))
            {
                throw new EmptyAPIKeyException();
            }

            return $@"https://{BaseEndpoint}/{endpointDirectory}?access_key={AccessKey}";
        }

        public static string ConstructRequestEndpoint(string endpointDirectory, string additionalParameters)
        {
            string intermediateAddress = ConstructRequestEndpoint(endpointDirectory);

            return $@"{intermediateAddress}&{additionalParameters}";
        }
    }
}
