# ZeroSSL.Net

Initialise a client with a string:
```
var client = new ZeroSSLClient(apiKey);
```

## Method Parameters

*Most* of the parameter objects are defined in the ZeroSSL [API documentation](https://zerossl.com/documentation/api/cancel-certificate/)

Otherwise they are documented in code.

## Calling Methods

Client instance method names correspond to API endpoint usage, i.e.

'Create Certificate' can be called with;
```
client.CreateCertificate(CreateCertificatePOST);
```

'Cancel Certificate' can be called with;
```
client.CancelCertificate(certificateId);
```