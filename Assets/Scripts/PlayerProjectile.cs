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
        // Poruszaj strza³em w kierunku celu
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ŒledŸ przebyt¹ odleg³oœæ
        distanceTraveled += speed * Time.deltaTime;

        // SprawdŸ, czy strza³ osi¹gn¹³ maksymaln¹ odleg³oœæ
        if (distanceTraveled >= constantDistance)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateTargetPosition()
    {
        // Uzyskaj pozycjê myszy w przestrzeni gry
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //// Ustaw targetPosition na sta³¹ odleg³oœæ od gracza w kierunku myszki
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
