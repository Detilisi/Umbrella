using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Common.Services;

public class AesEncryptionService : IEncryptionService
{
    private static readonly string _encrptionKey = "mysecretkey1234567890123456";
    public string Encrypt(string plainText)
    {
        using var md5 = MD5.Create();
        using var des = TripleDES.Create();
        des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(_encrptionKey));
        des.Mode = CipherMode.ECB;
        des.Padding = PaddingMode.PKCS7;

        //Encryption process
        using var encryptor = des.CreateEncryptor();
        var encryptedArray = Encoding.UTF8.GetBytes(plainText);
        var cryptoArray = encryptor.TransformFinalBlock(encryptedArray, 0, encryptedArray.Length);

        return Convert.ToBase64String(cryptoArray, 0, cryptoArray.Length);
    }
    public string Decrypt(string cipherText)
    {
        using var md5 = MD5.Create();
        using var des = TripleDES.Create();
        des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(_encrptionKey));
        des.Mode = CipherMode.ECB;
        des.Padding = PaddingMode.PKCS7;

        //Decryption process
        using var encryptor = des.CreateDecryptor();
        var decryptArray = Convert.FromBase64String(cipherText);
        var cryptoArray = encryptor.TransformFinalBlock(decryptArray, 0, decryptArray.Length);

        des.Clear();

        return Encoding.UTF8.GetString(cryptoArray);
    }
}
