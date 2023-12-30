using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace ZeusCore.Mnemosyne
{
    public class SaveService
    {
        private bool _useEncryption;
        private string _encryptionKey;


        public SaveService(bool useEncryption, string encryptionKey)
        {
            _useEncryption = useEncryption;
            _encryptionKey = encryptionKey;
        }


        public void SaveData(string key, string data)
        {
            var dataToSave = (_useEncryption) ? EncryptData(data) : data;

            PlayerPrefs.SetString(key, dataToSave);
            PlayerPrefs.Save();
        }


        public string LoadData(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                var data = PlayerPrefs.GetString(key);
                return (_useEncryption) ? DecryptData(data) : data;
            }

            else         
                throw new ArgumentException($"Loading data failed: Key '{key}' not found.");      
        }


        private string EncryptData(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(_encryptionKey);
            aesAlg.IV = new byte[aesAlg.BlockSize / 8];

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new();
            using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                csEncrypt.Write(dataBytes, 0, dataBytes.Length);
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
        }


        private string DecryptData(string encryptedData)
        {
            var cipherText = Convert.FromBase64String(encryptedData);
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(_encryptionKey);
            aesAlg.IV = new byte[aesAlg.BlockSize / 8];

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new(cipherText);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            return srDecrypt.ReadToEnd();
        }


    }
}
