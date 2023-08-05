using System.Security.Cryptography;

namespace CutInLine.Services
{
    public class Encripty
    {
        public static string GenerateSmallHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256Hash.ComputeHash(inputBytes);

                // Converta os primeiros 8 bytes do hash em uma string hexadecimal
                string token = BitConverter.ToString(hashBytes, 0, 8).Replace("-", "");
                return token;
            }
        }
    }
}
