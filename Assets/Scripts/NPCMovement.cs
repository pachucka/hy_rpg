using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float detectionRange = 10f;

    void Update()
    {
        // ZnajdŸ wszystkie obiekty w zasiêgu detekcji
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);

        // SprawdŸ, czy wœród obiektów jest coœ z tagiem ró¿nym od "Player"
        bool shouldMove = true;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                shouldMove = false;
                break;
            }
        }

        // Jeœli nie ma obiektu z tagiem "Player" w zasiêgu, poruszaj NPC
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
