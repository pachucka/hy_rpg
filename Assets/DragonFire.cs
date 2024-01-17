using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFire : MonoBehaviour
{
    private bool playerInside = false;
    private float damageInterval = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("W œrodku");
            playerInside = true;
            StartCoroutine(DealDamageOverTime());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Poza");
            playerInside = false;
        }
    }

    IEnumerator DealDamageOverTime()
    {
        while (playerInside)
        {
            PlayerController playerController = PlayerController.instance;

            if (playerController != null)
            {
                playerController.health -= 10;
            }

            yield return new WaitForSeconds(damageInterval);
        }
    }
}
