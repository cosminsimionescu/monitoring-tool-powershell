namespace MonitoringTool.Core.Settings;

/// <summary>Encrypts secrets before they are written to disk (DPAPI on Windows).</summary>
public interface ISecretProtector
{
    string Protect(string plainText);

    /// <exception cref="System.Security.Cryptography.CryptographicException">The value cannot be decrypted by this user.</exception>
    string Unprotect(string protectedText);
}
