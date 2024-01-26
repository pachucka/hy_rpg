using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public int xp;
    public float[] position;
    public string scene;
    public int[] items;

    public PlayerData(PlayerController player, InventoryManager inventory)
    {
        level = player.lvl;
        health = player.health;
        xp = player.xp;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        items = new int[inventory.Items.Count];
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            items[i] = inventory.Items[i].id;
        }

        scene = SceneManager.GetActiveScene().name;
        Debug.Log(scene);
    }
}
