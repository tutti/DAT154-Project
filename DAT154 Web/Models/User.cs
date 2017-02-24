using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

using DAT154_Libs;

namespace DAT154_Web.Models
{
    /*public class User
    {
        public int id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string passhash { get; private set; }

        public string password
        {
            set
            {
                this.passhash = this.hashPassword(value);
            }
        }

        private string hashPassword(string password)
        {
            // Code shamelessly stolen from http://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public bool verifyPassword(string password)
        {
            byte[] hashBytes = Convert.FromBase64String(this.passhash);
            // Get the salt
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            // Compute the hash on the password the user entered
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            // Compare the results
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }
    }
    */

    public class UserDBContext : DbContext
    {
        public DbSet<User> Movies { get; set; }
    }
}