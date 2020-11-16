using ZeroSSL.Net.Model.Misc;

namespace ZeroSSL.Net.Model.Input.POST
{
    public class VerifyDomainsPOST : InputBasePOST
    {
        public VerificationMethod validation_method { get; set; }
        public string validation_email { get; set; }

        public VerifyDomainsPOST(VerificationMethod validationMethod)
        {
            validation_method = validationMethod;
        }
    }
}
