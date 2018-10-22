﻿using System.Security.Cryptography;

namespace Phnx.Security
{
    /// <summary>
    /// A 4096 bit RSA asymmetric encryption algorithm
    /// </summary>
    public class RsaEncryption : IAsymmetricEncryption
    {
        /// <summary>
        /// The key size of the RSA Encryption used
        /// </summary>
        public const int KeyLength = 4096;

        /// <summary>
        /// The size of the public key that's generated by <see cref="CreateRandomKeys"/>
        /// </summary>
        public const int PublicKeySize = 532;

        /// <summary>
        /// The size of the private key that's generated by <see cref="CreateRandomKeys"/>
        /// </summary>
        public const int PrivateKeySize = 2324;

        /// <summary>
        /// Create random secure keys for use by <see cref="Encrypt"/> and <see cref="Decrypt"/>
        /// </summary>
        /// <param name="publicKey">The generated public key</param>
        /// <param name="privateKey">The generated private key</param>
        public void CreateRandomKeys(out byte[] publicKey, out byte[] privateKey)
        {
            var provider = new RSACryptoServiceProvider(KeyLength);

            publicKey = provider.ExportCspBlob(false);

            privateKey = provider.ExportCspBlob(true);
        }

        /// <summary>
        /// Encrypt data
        /// </summary>
        /// <param name="data">The data to encrypt</param>
        /// <param name="publicKey">The public key to use when encrypting the data</param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] data, byte[] publicKey)
        {
            var rsaServiceProvider = new RSACryptoServiceProvider();
            rsaServiceProvider.ImportCspBlob(publicKey);

            return rsaServiceProvider.Encrypt(data, true);
        }

        /// <summary>
        /// Decrypt data
        /// </summary>
        /// <param name="encryptedData">The data to decrypt</param>
        /// <param name="privateKey">The private key to use when decrypting the data</param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] encryptedData, byte[] privateKey)
        {
            var rsaServiceProvider = new RSACryptoServiceProvider();
            rsaServiceProvider.ImportCspBlob(privateKey);

            return rsaServiceProvider.Decrypt(encryptedData, true);
        }
    }
}
