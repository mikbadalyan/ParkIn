using System;
using System.Security.Cryptography;
using System.Text;
public class TokenService
{
    private readonly byte[] key;

    public TokenService(string encryptionKey)
    {
        if (encryptionKey.Length != 32)
        {
            throw new ArgumentException("Encryption key must be 32 characters long.");
        }
        key = Encoding.UTF8.GetBytes(encryptionKey);
    }

    public string GenerateToken()
    {
        return Guid.NewGuid().ToString();
    }

    public string EncryptToken(string token)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = new byte[16]; // Initialization vector

        var buffer = Encoding.UTF8.GetBytes(token);
        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var result = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
        return Convert.ToBase64String(result);
    }

    public string DecryptToken(string encryptedToken)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = new byte[16]; // Initialization vector

        var buffer = Convert.FromBase64String(encryptedToken);
        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        var result = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
        return Encoding.UTF8.GetString(result);
    }
}
