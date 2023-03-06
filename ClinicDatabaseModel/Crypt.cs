using System.Security.Cryptography;
using System.Text;

namespace ClinicDatabaseModel
{
    internal static class Crypt
    {
        internal static string GetMD5Hash(string data)
        {
            byte[] tmpSource = Encoding.ASCII.GetBytes(data);
            byte[] tmpHash = MD5.HashData(tmpSource);
            var output = new StringBuilder(tmpHash.Length);
            for (int i = 0; i < tmpHash.Length; i++)
            {
                output.Append(tmpHash[i].ToString("X2"));
            }
            return output.ToString();
        }

        internal static bool Compare(string input, string existing)
        {
            var inputHash = GetMD5Hash(input);
            return inputHash == existing;
        }
    }
}
