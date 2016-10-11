using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelRandomNumberGenerator
{
    /// <summary>
    /// Thread safe Class to generate Random Number
    /// </summary>
    public static class ThreadSafeRandomGenerator
    {
        /// <summary>
        /// Method to generate GetCryptoRandom
        /// </summary>
        /// <returns></returns>
        private static int GetCryptoRandom()
        {
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[4];
                rng.GetBytes(bytes);
                return BitConverter.ToInt32(bytes, 0);
            }
        }

        /// <summary>
        /// Thread safe access to create instance of Random class
        /// </summary>
        private static readonly ThreadLocal<Random> _local = new ThreadLocal<Random>(() => new Random(GetCryptoRandom()));
 
        /// <summary>
        /// Generate integer Random number
        /// </summary>
        /// <param name="minBound">Minimum bound to generate no.</param>
        /// <param name="maxBound">Maximum bound to generate no.</param>
        /// <returns>Random number generated</returns>
        public static int GenerateWithInBounds(int minBound, int maxBound)
        {
           return _local.Value.Next(minBound, maxBound);
        }
    }
}
