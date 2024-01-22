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
        // Sprawd�, czy za�adowana scena jest t�, na kt�rej znajduje si� obiekt SpawnPoint
        if (scene.name == gameObject.scene.name)
        {
            SetPlayerSpawnPoint();
        }
    }

    private void SetPlayerSpawnPoint()
    {
        // Znajd� obiekt gracza
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Ustaw pozycj� gracza na pozycj� spawnpointa
            player.transform.position = transform.position;
        }
        else
        {
            Debug.LogError("Nie znaleziono obiektu gracza.");
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
