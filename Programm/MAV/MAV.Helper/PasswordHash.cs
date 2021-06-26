using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MAV.Helper
{
    /// <summary>
    /// Hilfsklasse um Passwörter zu hashen
    /// </summary>
    public static class PasswordHash
    {
        /// <summary>
        /// hashed Passwort mit vorgegebenen Salt
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static byte[] Hash(string pwd, byte[] salt)
        {
            var hash = new Rfc2898DeriveBytes(pwd, salt, 100);
            return hash.GetBytes(20);
        }

        /// <summary>
        /// hashed Passwort und erstellt dazu zugehörigen Salt
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static byte[] Hash(string pwd, out byte[] salt)
        {
            var hash = new Rfc2898DeriveBytes(pwd, 8, 100);
            salt = hash.Salt;
            return hash.GetBytes(20);
        }
    }
}
