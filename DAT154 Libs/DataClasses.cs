using System;
using System.Data.Linq.Mapping;
using System.Security.Cryptography;

namespace DAT154_Libs {
    [Table(Name = "room")]
    public class Room {
        [Column(IsPrimaryKey = true, CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int id { get; set; }

        [Column(CanBeNull = false)]
        public int room_number { get; set; }

        [Column(CanBeNull = false)]
        public int beds { get; set; }

        [Column(CanBeNull = false)]
        public int room_size { get; set; }

        [Column(CanBeNull = false)]
        public int quality { get; set; }
    }

    [Table(Name = "user")]
    public class User {
        [Column(IsPrimaryKey = true, CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int id { get; set; }

        [Column(CanBeNull = false)]
        public int type { get; set; }

        [Column(CanBeNull = false)]
        public string name { get; set; }

        [Column(CanBeNull = false)]
        public string email { get; set; }

        [Column(CanBeNull = false)]
        public string passhash { get; private set; }

        public string password {
            set {
                this.passhash = this.hashPassword(value);
            }
        }

        private string hashPassword(string password) {
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

        public bool verifyPassword(string password) {
            byte[] hashBytes = Convert.FromBase64String(this.passhash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i=0; i < 20; i++)
                if (hashBytes[i+16] != hash[i])
                    return false;
            return true;
        }
    }

    [Table(Name = "booking")]
    public class Booking {
        public readonly int STATUS_CANCELED = -1;
        public readonly int STATUS_BOOKED = 0;
        public readonly int STATUS_PAID = 1;
        public readonly int STATUS_CHECKEDIN = 2;
        public readonly int STATUS_COMPLETE = 3;

        [Column(IsPrimaryKey = true, CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int id { get; set; }

        [Column(CanBeNull = false)]
        public int room_id { get; set; }

        [Column(CanBeNull = false)]
        public int user_id { get; set; }

        [Column(CanBeNull = false)]
        public DateTime start_date { get; set; }

        [Column(CanBeNull = false)]
        public DateTime end_date { get; set; }

        [Column(CanBeNull = false)]
        public int booking_status { get; set; }
    }

    [Table(Name = "task")]
    public class Task {
        [Column(IsPrimaryKey = true, CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int id { get; set; }

        [Column(CanBeNull = false)]
        public int room_id { get; set; }

        [Column(CanBeNull = false)]
        public int status { get; set; }

        [Column(CanBeNull = false)]
        public int category { get; set; }

        [Column(CanBeNull = false)]
        public string notes { get; set; }
    }
}