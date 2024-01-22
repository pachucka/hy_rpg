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

    public PlayerData(PlayerController player)
    {
        level = player.lvl;
        health = player.health;
        xp = player.xp;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        scene = SceneManager.GetActiveScene().name;
    }
}
