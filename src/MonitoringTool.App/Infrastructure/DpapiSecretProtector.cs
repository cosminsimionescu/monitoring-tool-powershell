using System.Security.Cryptography;
using System.Text;
using MonitoringTool.Core.Settings;

namespace MonitoringTool.App.Infrastructure;

/// <summary>Windows DPAPI: only the same Windows user on the same machine can decrypt.</summary>
internal sealed class DpapiSecretProtector : ISecretProtector
{
    private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("MonitoringTool.Smtp.v2");

    public string Protect(string plainText) =>
        Convert.ToBase64String(ProtectedData.Protect(Encoding.UTF8.GetBytes(plainText), Entropy, DataProtectionScope.CurrentUser));

    public string Unprotect(string protectedText) =>
        Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(protectedText), Entropy, DataProtectionScope.CurrentUser));
}
