using System.Security.Cryptography;

namespace HTCCL.Utils;

public class Checksum
{
    public static string GetChecksum(byte[] bytes)
    {
        return BitConverter.ToString(MD5.Create().ComputeHash(bytes));
    }
}