using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerController player, InventoryManager inventory)
    {
        // Create a binary formatter
        BinaryFormatter formatter = new BinaryFormatter();

        // Define the save path for the player data
        string path = Application.persistentDataPath + "/player.fun";

        // Open a file stream to the specified path, creating the file if it doesn't exist
        FileStream stream = new FileStream(path, FileMode.Create);

        // Serialize the player data using the formatter and write it to the stream
        formatter.Serialize(stream, GameManager.instance.data);

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

                // Deserialize the player data from the stream and cast it to PlayerData
                PlayerData data = formatter.Deserialize(stream) as PlayerData;

                // Close the stream
                stream.Close();

                // Return the loaded player data
                return data;
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

}
