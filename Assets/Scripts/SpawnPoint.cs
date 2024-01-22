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
        // SprawdŸ, czy za³adowana scena jest t¹, na której znajduje siê obiekt SpawnPoint
        if (scene.name == gameObject.scene.name)
        {
            SetPlayerSpawnPoint();
        }
    }

    private void SetPlayerSpawnPoint()
    {
        // ZnajdŸ obiekt gracza
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Ustaw pozycjê gracza na pozycjê spawnpointa
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
