using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 5f; // Prêdkoœæ poruszania siê strza³u
    public float constantDistance = 20f; // Sta³a odleg³oœæ od gracza

    private Vector2 initialPosition; // Pozycja pocz¹tkowa strza³u
    private Vector2 targetPosition; // Pozycja celu

    private float distanceTraveled = 0f; // Dodaj zmienn¹ do œledzenia przebytej odleg³oœci

    private void Start()
    {
        initialPosition = transform.position;
        UpdateTargetPosition();
    }

    private void Update()
    {
        // Move towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Track the distance traveled
        distanceTraveled += speed * Time.deltaTime;

        // Check if the projectile has reached the maximum distance
        if (distanceTraveled >= constantDistance)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateTargetPosition()
    {
        // Get the mouse position in the game space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Set targetPosition to a constant distance from the player towards the mouse
        targetPosition = (mousePosition - (Vector2)initialPosition).normalized * constantDistance + (Vector2)initialPosition;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Item"))
        {
            if (other.CompareTag("SmallDragon"))
            {
                other.GetComponent<Enemy>().TakeDamage(PlayerController.instance.damage);
            }
            if (other.CompareTag("Bat"))
            {
                other.GetComponent<EnemyBat>().TakeDamage(PlayerController.instance.damage);
            }
            Destroy(gameObject);
        }
    }
}
