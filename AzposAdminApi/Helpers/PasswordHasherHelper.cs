using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace AzposAdminApi.Helpers
{
    public sealed class HashingOptions
    {
        public int Iterations { get; set; } = 10000;
    }

    public interface IPasswordHasher
    {
        string Hash(string password);

        (bool Verified, bool NeedsUpgrade) Check(string hash, string password);
    }

    public sealed class PasswordHasherHelper : IPasswordHasher
    {
        private const int SaltSize = 16; // 128 bit 
        private const int KeySize = 32; // 256 bit

        public PasswordHasherHelper(IOptions<HashingOptions> options)
        {
            Options = options.Value;
        }

        public PasswordHasherHelper()
        {
            Options = new HashingOptions();
        }

        private HashingOptions Options { get; }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              SaltSize,
              Options.Iterations,
              HashAlgorithmName.SHA512))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{salt}.{key}";
            }
        }

        public bool IsValidPassword(string hash, string password)
        {
            var parts = hash.Split('.', 2);

            if (parts.Length != 2)
            {
                return false;
            }

            var iterations = Options.Iterations;
            var salt = Convert.FromBase64String(parts[0]);
            var key = Convert.FromBase64String(parts[1]);

            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              iterations,
              HashAlgorithmName.SHA512))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var isValid = keyToCheck.SequenceEqual(key);

                return isValid;
            }
        }

        public (bool Verified, bool NeedsUpgrade) Check(string hash, string password)
        {
            var parts = hash.Split('.', 2);

            if (parts.Length != 2)
            {
                //throw new FormatException("Unexpected hash format. " +
                //  "Should be formatted as `{iterations}.{salt}.{hash}`");
                return (false, false);
            }

            var iterations = Options.Iterations;
            var salt = Convert.FromBase64String(parts[0]);
            var key = Convert.FromBase64String(parts[1]);

            var needsUpgrade = iterations != Options.Iterations;

            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              iterations,
              HashAlgorithmName.SHA512))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                return (verified, needsUpgrade);
            }
        }
    }
}