using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cumulo.ninja.Infrastructure.Identity
{
    internal static class Rfc6238AuthenticationService
    {
        private static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly Encoding _encoding = (Encoding)new UTF8Encoding(false, true);
        private static TimeSpan _timestep = TimeSpan.FromMinutes(1.0);

        static Rfc6238AuthenticationService()
        {
        }

        private static int ComputeTotp(HashAlgorithm hashAlgorithm, ulong timestepNumber, string modifier)
        {
            byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((long)timestepNumber));
            byte[] hash = hashAlgorithm.ComputeHash(Rfc6238AuthenticationService.ApplyModifier(bytes, modifier));
            int index = (int)hash[hash.Length - 1] & 15;
            return (((int)hash[index] & (int)sbyte.MaxValue) << 24 | ((int)hash[index + 1] & (int)byte.MaxValue) << 16 | ((int)hash[index + 2] & (int)byte.MaxValue) << 8 | (int)hash[index + 3] & (int)byte.MaxValue) % 1000000;
        }

        private static byte[] ApplyModifier(byte[] input, string modifier)
        {
            if (string.IsNullOrEmpty(modifier)) return input;
            byte[] bytes = Rfc6238AuthenticationService._encoding.GetBytes(modifier);
            byte[] numArray = new byte[checked(input.Length + bytes.Length)];
            Buffer.BlockCopy((Array)input, 0, (Array)numArray, 0, input.Length);
            Buffer.BlockCopy((Array)bytes, 0, (Array)numArray, input.Length, bytes.Length);
            return numArray;
        }

        private static ulong GetCurrentTimeStepNumber(int validResetTokenMinutes)
        {
            Rfc6238AuthenticationService._timestep = TimeSpan.FromMinutes(validResetTokenMinutes);
            return (ulong)((DateTime.UtcNow - Rfc6238AuthenticationService._unixEpoch).Ticks / Rfc6238AuthenticationService._timestep.Ticks);
        }

        public static int GenerateCode(int validResetTokenMinutes, SecurityToken securityToken, string modifier = null)
        {
            if (securityToken == null) throw new ArgumentNullException("securityToken");
            ulong currentTimeStepNumber = Rfc6238AuthenticationService.GetCurrentTimeStepNumber(validResetTokenMinutes);
            using (HMACSHA1 hmacshA1 = new HMACSHA1(securityToken.GetDataNoClone()))
            {
                int code = Rfc6238AuthenticationService.ComputeTotp((HashAlgorithm)hmacshA1, currentTimeStepNumber, modifier);
                return code;
            }
        }

        public static bool ValidateCode(int validResetTokenMinutes, SecurityToken securityToken, int code, string modifier = null)
        {
            if (securityToken == null) throw new ArgumentNullException("securityToken");
            ulong currentTimeStepNumber = Rfc6238AuthenticationService.GetCurrentTimeStepNumber(validResetTokenMinutes);
            using (HMACSHA1 hmacshA1 = new HMACSHA1(securityToken.GetDataNoClone()))
            {
                for (int index = -2; index <= 2; ++index)
                {
                    int verifyCode = Rfc6238AuthenticationService.ComputeTotp((HashAlgorithm)hmacshA1, currentTimeStepNumber + (ulong)index, modifier);
                    if (verifyCode == code) return true;
                }
            }
            return false;
        }
    }
}
