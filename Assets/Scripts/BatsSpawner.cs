using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatsSpawner : MonoBehaviour
{
    public GameObject batPrefab;
    public Transform[] batSpawnPoints;
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 5f;
    public int batsToSpawn = 6;

    private int batsSpawned = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InvokeRepeating("SpawnBat", 0f, Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    void SpawnBat()
    {
        if (batsSpawned < batsToSpawn)
        {
            int randomSpawnIndex = Random.Range(0, batSpawnPoints.Length);
            Transform spawnPoint = batSpawnPoints[randomSpawnIndex];

            Instantiate(batPrefab, spawnPoint.position, Quaternion.identity);

            batsSpawned++;
        }
        else
        {
            CancelInvoke("SpawnBat");
        }
    }
}
