using System;
using System.Text;
using System.Security.Cryptography;

namespace FG.FFTT.Api.Client
{
    /// <summary>
    /// Access parameters for SmartPing APIs
    /// </summary>
    public class FfttCredentials
    {
        #region properties

        /// <summary>
        /// An application identifier passed as an "id" in each API request.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Password used to encrypt identification parameters.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Serial number of the user sending the request
        /// </summary>
        public string SerialNumber { get; private set; } = null;

        #endregion properties

        #region constructor

        public FfttCredentials(string id, string password, string serialNumber = null)
        {
            Id = id;
            Password = password;
            SerialNumber = serialNumber ?? GenerateSerialNumber();
        }

        #endregion constructor

        #region public methods

        /// <summary>
        /// Calculating the hash key from the password.
        /// </summary>
        /// <returns></returns>
        public string GetHashKeyFromPassword()
        {
            byte[] hash = null;
            using (var md5 = new MD5CryptoServiceProvider())
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Password));

            StringBuilder sb = new StringBuilder();
            foreach (var b in hash)
                sb.Append(b.ToString("x2").ToLower());

            return sb.ToString();
        }

        /// <summary>
        /// Encodes the timestamp in Sha1 with the hash key of the password.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public string EncodeTimestampToSha1(string timestamp)
        {
            byte[] hash = null;
            using (var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(GetHashKeyFromPassword())))
                hash = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(timestamp));

            StringBuilder sb = new StringBuilder();
            foreach (var b in hash)
                sb.Append(b.ToString("x2").ToLower());

            return sb.ToString();
        }

        /// <summary>
        /// Generates a unique serial number for the user. This number will need to be passed in every API request.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public string GenerateSerialNumber(int size = 15)
        {
            Random random = new Random();
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < size; i++)
                sb.Append(alphabet[random.Next(alphabet.Length)]);
            
            return sb.ToString();
        }

        #endregion public methods
    }
}