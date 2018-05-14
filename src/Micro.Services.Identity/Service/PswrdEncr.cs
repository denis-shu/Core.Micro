using System;
using System.Security.Cryptography;

namespace Micro.Services.Identity.Service
{
    public class PswrdEncr : IPswrdEncr
    {
        private static readonly int saltLength = 33;
        private static readonly int bitesCount = 10000;

        public string GetHash(string val, string saly)
        {
            var res = new Rfc2898DeriveBytes(val, GetBytes(saly), bitesCount);
            return Convert.ToBase64String(res.GetBytes(saltLength));
        }

        public string GetSalt(string val)
        {
            var r = new Random();
            //var saltB=new byte[44];
            var saltB = new byte[saltLength];
            var rGen = RandomNumberGenerator.Create();
            rGen.GetBytes(saltB);

            return Convert.ToBase64String(saltB);

        }

        private static byte[] GetBytes(string val)
        {
            var bytes = new byte[val.Length * sizeof(char)];
            Buffer.BlockCopy(val.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}