using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroSSL.Net.Model.Input.GET;
using ZeroSSL.Net.Model.Input.POST;
using ZeroSSL.Net.Model.Output;

namespace ZeroSSL.Net
{
    public interface IClient
    {
        CertificateDetails CreateCertificate(CreateCertificatePOST postParameters);
        VerifyDomains VerifyDomains(string certificateId, VerifyDomainsPOST postParameters);
        void DownloadCertificateZIP(string certificateId, string targetFile);
        InlineCertificate DownloadCertificateInline(string certificateId);
        CertificateDetails GetCertificate(string certificateId);
        ListCertificates ListCertificates(ListCertificatesGET getParameters);
        VerificationStatus VerificationStatus(string certificateId);
        SuccessStatus ResendVerificationEmail(string certificateId);
        SuccessStatus CancelCertificate(string certificateId);
        SuccessStatus DeleteCertificate(string certificateId);
        EABCredentials GenerateEABCredentials();
    }
}
