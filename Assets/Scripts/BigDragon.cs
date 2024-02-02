using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigDragon : MonoBehaviour
{
    private int damage;
    private bool dealDamage = false;


    private void Update()
    {
        if (dealDamage)
        {
            StartCoroutine(DealDamage());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dealDamage = true;
        }
        if (other.CompareTag("PlayerProjectile") && SceneManager.GetActiveScene().name == "BossFight")
        {
            BigDragonSpawner.instance.takeDamage(PlayerController.instance.damage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dealDamage = false;
        }
    }

    IEnumerator DealDamage()
    {
        PlayerController.instance.health -= 10;
        yield return new WaitForSeconds(0.5f);
    }

}
