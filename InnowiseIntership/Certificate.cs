using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace InnowiseIntership;

public interface ICertificate
{
    public RsaSecurityKey PublicKey { get; }
    public RsaSecurityKey PrivateKey { get; }
}

public class Certificate : ICertificate
{
    public Certificate(string path)
    {
        RSA rsa = RSA.Create(2048);

        var publicKeyPath = $"{path}.public.pem";
        var privateKeyPath = $"{path}.private.pem";

        if (File.Exists(publicKeyPath) && File.Exists(privateKeyPath))
        {
            rsa.ImportFromPem(File.ReadAllText(publicKeyPath));
            rsa.ImportFromPem(File.ReadAllText(privateKeyPath));

            PublicKey = new RsaSecurityKey(rsa.ExportParameters(true));
            PrivateKey = new RsaSecurityKey(rsa.ExportParameters(false));
        }

        PublicKey = new RsaSecurityKey(rsa.ExportParameters(true));
        PrivateKey = new RsaSecurityKey(rsa.ExportParameters(false));
        
        File.WriteAllText(publicKeyPath,rsa.ExportRSAPublicKeyPem());
        File.WriteAllText(privateKeyPath,rsa.ExportRSAPrivateKeyPem());
    }

    public RsaSecurityKey PublicKey { get; }
    public RsaSecurityKey PrivateKey { get; }
}