using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigDragonSpawner : MonoBehaviour
{
    public GameObject dragonPrefab;
    private float spawnInterval;
    private float dragonAttackTime;
    public float mapBorderSize = 10f;
    public int hp = 500;

    public GameObject boss;

    public static BigDragonSpawner instance;

    void Start()
    {
        if(BigDragonSpawner.instance == null)
        {
            instance = this;
        }

        if(SceneManager.GetActiveScene().name == "BossFight")
        {
            StartCoroutine(BossFight());
        } else
        {
            StartCoroutine(SpawnDragons());
        }
    }

    IEnumerator SpawnDragons()
    {
        while (true)
        {
            // Dodaj sprawdzenie czy menu nie jest otwarte
            if (!GameManager.instance.menuOpen)
            {
                spawnInterval = Random.Range(3f, 6f);
                dragonAttackTime = Random.Range(2f, 7f);
                bool spawnAtLeftWall = Random.Range(0, 2) == 0;

                Vector2 spawnPosition = GetBorderPosition(spawnAtLeftWall);

                GameObject dragon = Instantiate(dragonPrefab, spawnPosition, Quaternion.identity);
                Transform fire = dragon.transform.Find("Fire");

                if (fire != null)
                {
                    fire.gameObject.SetActive(false);
                }

                if (!spawnAtLeftWall)
                {
                    dragon.transform.rotation *= Quaternion.Euler(0, 180, 0);
                }

                yield return new WaitForSeconds(2f);

                if (fire != null)
                {
                    fire.gameObject.SetActive(true);
                }

                yield return new WaitForSeconds(dragonAttackTime);

                if (fire != null)
                {
                    fire.gameObject.SetActive(false);
                }

                yield return new WaitForSeconds(2f);
                Destroy(dragon);

                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    IEnumerator BossFight()
    {
        while (hp > 0)
        {
            if (!GameManager.instance.menuOpen)
            {
                spawnInterval = Random.Range(0.1f, 0.5f);
                dragonAttackTime = Random.Range(2f, 3f);
                bool spawnAtLeftWall = Random.Range(0, 2) == 0;

                Vector2 spawnPosition = GetBorderPosition(spawnAtLeftWall);

                GameObject dragon = Instantiate(dragonPrefab, spawnPosition, Quaternion.identity);
                Transform fire = dragon.transform.Find("Fire");

                if (fire != null)
                {
                    fire.gameObject.SetActive(false);
                }

                if (!spawnAtLeftWall)
                {
                    dragon.transform.rotation *= Quaternion.Euler(0, 180, 0);
                }

                yield return new WaitForSeconds(1f);

                if (fire != null)
                {
                    fire.gameObject.SetActive(true);
                }

                yield return new WaitForSeconds(dragonAttackTime);

                if (fire != null)
                {
                    fire.gameObject.SetActive(false);
                }

                yield return new WaitForSeconds(0.5f);
                Destroy(dragon);

                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }



    }

    Vector2 GetBorderPosition(bool spawnAtLeftWall)
    {
        float randomY = Random.Range(-10, 4);

        if (spawnAtLeftWall)
        {
            return new Vector2(-mapBorderSize, randomY);
        }
        else
        {
            return new Vector2(mapBorderSize, randomY);
        }
    }

    public void takeDamage(int dmg)
    {
        hp -= dmg;
    }
}
