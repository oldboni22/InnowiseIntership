using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace InnowiseIntership;

public interface ICertificate
{
    RsaSecurityKey Rsa { get; }
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
            try
            {
                rsa.ImportFromPem(File.ReadAllText(publicKeyPath));
                rsa.ImportFromPem(File.ReadAllText(privateKeyPath));
                
                Rsa = new RsaSecurityKey(rsa);
                return;
            }
            catch
            {
            }
        }
        
        File.WriteAllText(publicKeyPath,rsa.ExportRSAPublicKeyPem());
        File.WriteAllText(privateKeyPath,rsa.ExportRSAPrivateKeyPem());

        Rsa = new RsaSecurityKey(rsa);
    }
    public RsaSecurityKey Rsa { get; }
}