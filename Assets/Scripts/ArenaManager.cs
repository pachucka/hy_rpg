using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    public GameObject[] enemies;
    public int[] numOfEnemies;
    private int enemiesLeftToSpawn = 0;

    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;

    public float minX = -10, maxX = 10, minY = -10, maxY = 10;

    void Start()
    {
        for(int i = 0; i < numOfEnemies.Length; i++)
        {
            enemiesLeftToSpawn += numOfEnemies[i];
        }
        InvokeRepeating("SpawnRandomEnemy", 1f, Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    void SpawnRandomEnemy()
    {
        if (!GameManager.instance.menuOpen)
        {
            if(SceneManager.GetActiveScene().name == "BossFight")
            {
                if(BigDragonSpawner.instance.hp <= 0)
                {
                    Invoke("EndArena", 5f);
                }
            }
            if (enemies.Length == 0 || numOfEnemies.Length == 0)
            {
                Debug.LogError("No enemies to spawn");
                return;
            }

            if (enemiesLeftToSpawn > 0)
            {
                int randomEnemyIndex = Random.Range(0, enemies.Length);
                while (numOfEnemies[randomEnemyIndex] <= 0)
                {
                    randomEnemyIndex = Random.Range(0, enemies.Length);
                }

                numOfEnemies[randomEnemyIndex]--;
                enemiesLeftToSpawn--;

                GameObject enemyPrefab = enemies[randomEnemyIndex];

                Vector3 spawnPosition = GetRandomSpawnPosition();
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            } else if(SceneManager.GetActiveScene().name != "BossFight")
            {
                Invoke("EndArena", 5f);
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(minX, maxX); // Zakres wspó³rzêdnych x
        float randomZ = Random.Range(minY, maxY); // Zakres wspó³rzêdnych z

        return new Vector3(randomX, 0f, randomZ);
    }

    public void EndArena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
