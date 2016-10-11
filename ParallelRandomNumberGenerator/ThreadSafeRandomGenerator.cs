using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelRandomNumberGenerator
{
    public static class ThreadSafeRandomGenerator
    {
        private static int GetCryptoRandom()
        {
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[4];
                rng.GetBytes(bytes);
                return BitConverter.ToInt32(bytes, 0);
            }
        }

        private static readonly ThreadLocal<Random> _local = new ThreadLocal<Random>(() => new Random(GetCryptoRandom()));
 
        public static int GenerateWithInBounds(int minBound, int maxBound)
        {
           return _local.Value.Next(minBound, maxBound);
        }
    }
}
