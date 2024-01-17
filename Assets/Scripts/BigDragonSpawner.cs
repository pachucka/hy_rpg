using System.Collections;
using UnityEngine;

public class BigDragonSpawner : MonoBehaviour
{
    public GameObject dragonPrefab;
    private float spawnInterval;
    public float dragonAttackTime;
    public float mapBorderSize = 10f;

    void Start()
    {
        StartCoroutine(SpawnDragons());
    }

    IEnumerator SpawnDragons()
    {
        while (true)
        {
            spawnInterval = Random.Range(3f, 6f);
            dragonAttackTime = Random.Range(2f, 7f);
            bool spawnAtLeftWall = Random.Range(0, 2) == 0;

            Vector2 spawnPosition = GetBorderPosition(spawnAtLeftWall);

            GameObject dragon = Instantiate(dragonPrefab, spawnPosition, Quaternion.identity);
            Transform fire = dragon.transform.Find("Fire"); // Ustawienie referencji fire na podstawie instancji smoka
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
                Debug.Log("siema");
            }

            yield return new WaitForSeconds(2f);
            Destroy(dragon);

            yield return new WaitForSeconds(spawnInterval);
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
}
