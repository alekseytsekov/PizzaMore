namespace PizzaMore.Utility
{
    using System;
    using System.Security.Cryptography;

    public class PasswordHasher
    {
        private string Hash { get;  set; }
        private string Salt { get;  set; }

        public string[] SaltHash(string password)
        {
            var saltBytes = new byte[32];

            using (var provider = new RNGCryptoServiceProvider())
                provider.GetNonZeroBytes(saltBytes);

            this.Salt = Convert.ToBase64String(saltBytes);
            this.Hash = ComputeHash(this.Salt, password);

            var result = new string[2];

            result[0] = this.Salt;
            result[1] = this.Hash;

            return result;
        }

        static string ComputeHash(string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000))
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
        }

        public static bool Verify(string salt, string hash, string password)
        {
            return hash == ComputeHash(salt, password);
        }
    }
}
