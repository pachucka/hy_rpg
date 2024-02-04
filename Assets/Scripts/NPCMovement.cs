using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float detectionRange = 10f;

    void Update()
    {
        // Znajd� wszystkie obiekty w zasi�gu detekcji
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);

        // Sprawd�, czy w�r�d obiekt�w jest co� z tagiem r�nym od "Player"
        bool shouldMove = true;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                shouldMove = false;
                break;
            }
        }

        // Je�li nie ma obiektu z tagiem "Player" w zasi�gu, poruszaj NPC
        if (shouldMove)
        {
            MoveNPC();
        }
    }

    void MoveNPC()
    {
        // Poruszaj NPC w kierunku przeciwnym
        transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
    }
}
