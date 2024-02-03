using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

public static class SaveSystem
{
    // Key and initialization vector for data encryption
    private static readonly string encryptionKey = "EncryptionKey123"; 
    private static readonly string initializationVector = "InitVector123456"; 

    public static void SavePlayer(PlayerController player, InventoryManager inventory)
    {
        // Create a binary formatter
        BinaryFormatter formatter = new BinaryFormatter();

        // Define the save path for the player data
        string path = Application.persistentDataPath + "/player.fun";

        // Open a file stream to the specified path, creating the file if it doesn't exist
        FileStream stream = new FileStream(path, FileMode.Create);

        // Create a MemoryStream to store encrypted data
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Serialize the player data using the formatter to the memory stream
            formatter.Serialize(memoryStream, GameManager.instance.data);

            // Encrypt data in memory
            byte[] encryptedData = Encrypt(memoryStream.ToArray());

            // Write encrypted data to the file stream
            stream.Write(encryptedData, 0, encryptedData.Length);
        }

        // Close the stream
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        // Check if a GameManager instance exists
        if (GameManager.instance != null)
        {
            // Define the path to the save file
            string path = Application.persistentDataPath + "/player.fun";

            // Check if the save file exists
            if (File.Exists(path))
            {
                // Create a binary formatter
                BinaryFormatter formatter = new BinaryFormatter();

                // Open a file stream to the specified path
                FileStream stream = new FileStream(path, FileMode.Open);

                // Read encrypted data from the file stream
                byte[] encryptedData = new byte[stream.Length];
                stream.Read(encryptedData, 0, encryptedData.Length);

                // Decrypt data
                byte[] decryptedData = Decrypt(encryptedData);

                // Create a MemoryStream with decrypted data
                using (MemoryStream memoryStream = new MemoryStream(decryptedData))
                {
                    // Deserialize player data from memory
                    PlayerData data = formatter.Deserialize(memoryStream) as PlayerData;

                    // Close the stream
                    stream.Close();

                    // Return the loaded player data
                    return data;
                }
            }
            else
            {
                // Log an error if the save file is not found
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }
        else
        {
            // Log an error if no GameManager instance is found
            Debug.LogError("No GameManager instance");
            return null;
        }
    }

    // Data encryption function
    private static byte[] Encrypt(byte[] data)
    {
        // Create an instance of the Advanced Encryption Standard (AES) algorithm
        using (Aes aesAlg = Aes.Create())
        {
            // Set the encryption key and initialization vector (IV) for the AES algorithm
            aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(encryptionKey);
            aesAlg.IV = System.Text.Encoding.UTF8.GetBytes(initializationVector);

            // Create an encryptor using the AES algorithm and specified key and IV
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create a MemoryStream to store the encrypted data
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                // Create a CryptoStream that links the MemoryStream and the encryptor
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    // Write the input data to the CryptoStream, encrypting it
                    csEncrypt.Write(data, 0, data.Length);

                    // Flush the final block of data through the CryptoStream
                    csEncrypt.FlushFinalBlock();

                    // Return the encrypted data as an array of bytes
                    return msEncrypt.ToArray();
                }
            }
        }
    }


    // Data decryption function
    private static byte[] Decrypt(byte[] data)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(encryptionKey);
            aesAlg.IV = System.Text.Encoding.UTF8.GetBytes(initializationVector);

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream())
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                {
                    csDecrypt.Write(data, 0, data.Length);
                    csDecrypt.FlushFinalBlock();
                    return msDecrypt.ToArray();
                }
            }
        }
    }
}
