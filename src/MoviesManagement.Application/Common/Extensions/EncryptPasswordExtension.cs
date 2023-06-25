using System.Text;

namespace MoviesManagement.Application.Common.Extensions
{
    public static class EncryptPasswordExtension
    {
        public static string Encrypt(string password)
        {
            byte[] passwdBytes = Encoding.UTF8.GetBytes(password);
            string encryptedPassword = Convert.ToBase64String(passwdBytes);

            return encryptedPassword;
        }

    }
}
