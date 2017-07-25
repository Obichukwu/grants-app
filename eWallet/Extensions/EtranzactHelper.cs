using System.Security.Cryptography;
using System.Text;

namespace eWallet.Extensions
{
    public class OnlinePaymenttHelper
    {
        public static string GetEtranzactCheckSum(string amount, string terminalId, string transactionId, string responseUrl,
            string secretKey) {
            var combination = amount + terminalId + transactionId + responseUrl + secretKey;
            return CreateMd5Hash(combination);
        }

        public static string GetEtranzactFinalCheckSum(string success, string amount, string terminalId, string transactionId, string responseUrl,
            string secretKey)
        {
            var combination = success + amount + terminalId + transactionId + responseUrl + secretKey;
            return CreateMd5Hash(combination);
        }

        public static string GetBankItCheckSum(string amount, string terminalId, string transactionId, string responseUrl,
            string secretKey)
        {
            var combination = amount + terminalId + transactionId + responseUrl + secretKey;
            return CreateSha256Hash(combination);
        }

        public static string GetBankItFinalCheckSum(string success, string amount, string terminalId, string transactionId, string responseUrl,
            string secretKey)
        {
            var combination = amount + terminalId + transactionId + responseUrl + secretKey;
            return CreateSha256Hash(combination);
        }

        public static string CreateMd5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);
            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();
            foreach (var t in hashBytes) {
                sb.Append(t.ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static string CreateSha256Hash(string input)
        {
            // Use input string to calculate MD5 hash
            var sha256 = SHA256.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = sha256.ComputeHash(inputBytes);
            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();
            foreach (byte t in hashBytes) {
                sb.Append(t.ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}