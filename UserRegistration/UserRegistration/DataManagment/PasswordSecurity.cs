using System.Security.Cryptography;
using System.Text;

namespace UserRegistration.DataManagment
{
    public static class PasswordSecurity
    {
        /// <summary>
        /// Method to hash given value. In this case it used for hashing user password.
        /// </summary>
        /// <param name="value">Value for hashing.</param>
        /// <returns></returns>
        public static string HashSHA1(this string value)
        {
            var sha1 = SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
