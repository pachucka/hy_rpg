using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public GameObject[] healthBarElements;

    private void Update()
    {
        int health = PlayerController.instance.health;

        // Wy³¹cz wszystkie elementy pasków ¿ycia
        foreach (GameObject element in healthBarElements)
        {
            element.SetActive(false);
        }

        // W³¹cz odpowiedni¹ iloœæ elementów pasków ¿ycia na podstawie aktualnego zdrowia
        int healthIndex = Mathf.Clamp((health - 1) / 10, 0, healthBarElements.Length - 1);

        for (int i = 0; i <= healthIndex; i++)
        {
            healthBarElements[i].SetActive(true);
        }
    }
}
