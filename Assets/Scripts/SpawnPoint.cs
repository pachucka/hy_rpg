using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the one where Player object is on
        if (scene.name == gameObject.scene.name)
        {
            SetPlayerSpawnPoint();
        }
    }

    private void SetPlayerSpawnPoint()
    {
        // Find player object
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Set player's position to position of the spawn point
            player.transform.position = transform.position;
        }
        else
        {
            Debug.LogError("Player not found");
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
