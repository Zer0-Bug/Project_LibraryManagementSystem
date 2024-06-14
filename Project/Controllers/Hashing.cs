using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace FrontCode.Libraries
{
    public class Hashing
    {
        /// <summary>
        /// Generate Hash from a given plain text
        /// </summary>
        /// <param name="password"></param>
        /// <returns>return a 256 bytes of hash</returns>
        private static string GenerateHash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(password);
                byte[] resultBytes = sha256.ComputeHash(dataBytes);
                return Convert.ToBase64String(resultBytes);
            }
        }

        /// <summary>
        /// Create Hash for the given password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>return hashed password</returns>
        public static string CreateHash(string password)
        {
            return GenerateHash(password);
        }

        /// <summary>
        /// Verify if the provided password matches the stored hash
        /// </summary>
        /// <param name="inputPassword">Password entered by the user</param>
        /// <param name="storedHash">Hash stored in the database</param>
        /// <returns>True if the password is correct, false otherwise</returns>
        public static string VerifyPassword(string inputPassword)
        {
            // Recompute the hash with the provided password
            string recomputedHash = CreateHash(inputPassword);

            // Compare the recomputed hash with the stored hash
            return recomputedHash;
        }
    }
}

