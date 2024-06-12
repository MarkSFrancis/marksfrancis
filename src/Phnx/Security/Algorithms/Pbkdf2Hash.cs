﻿using System;
using System.Security.Cryptography;

namespace Phnx.Security.Algorithms
{
    /// <summary>
    /// A PBKDF2 hashing algorithm. Suitable for passwords, but generally too slow for checksums. Consider using <see cref="Sha256Hash"/> if you're generating checksums
    /// </summary>
    public class Pbkdf2Hash : IHashWithSalt
    {
        /// <summary>
        /// The length of the hash generated by the algorithm
        /// </summary>
        public const int HashBytesLength = 128;

        /// <summary>
        /// The length of the salt required by the algorithm
        /// </summary>
        public int SaltBytesLength { get; }

        /// <summary>
        /// Create a new <see cref="Pbkdf2Hash"/>
        /// </summary>
        public Pbkdf2Hash()
        {
            SaltBytesLength = 24;
        }

        /// <summary>
        /// Generate a salt for use with this algorithm
        /// </summary>
        /// 
        public byte[] GenerateSalt()
        {
            return SecureRandomBytes.Generate(SaltBytesLength);
        }

        /// <summary>
        /// Hash data using a salt
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <param name="salt">The salt to use. This must have the same length as <see cref="SaltBytesLength"/></param>
        /// <param name="numberOfIterations">The number of times the algorithm is ran on <paramref name="data"/></param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="data"/> or <paramref name="salt"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="numberOfIterations"/> is less than zero</exception>
        public byte[] Hash(byte[] data, byte[] salt, int numberOfIterations = 1)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (salt is null)
            {
                throw new ArgumentNullException(nameof(salt));
            }
            if (numberOfIterations < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfIterations));
            }

            if (numberOfIterations == 0)
            {
                return data;
            }

            using var pbkdf2 = new Rfc2898DeriveBytes(data, salt, numberOfIterations, HashAlgorithmName.SHA1);

            return pbkdf2.GetBytes(HashBytesLength);
        }
    }
}
