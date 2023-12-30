using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 5f; // Pr�dko�� poruszania si� strza�u
    public float constantDistance = 20f; // Sta�a odleg�o�� od gracza

    private Vector2 initialPosition; // Pozycja pocz�tkowa strza�u
    private Vector2 targetPosition; // Pozycja celu

    private float distanceTraveled = 0f; // Dodaj zmienn� do �ledzenia przebytej odleg�o�ci

    private void Start()
    {
        initialPosition = transform.position;
        UpdateTargetPosition();
    }

    private void Update()
    {
        // Poruszaj strza�em w kierunku celu
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // �led� przebyt� odleg�o��
        distanceTraveled += speed * Time.deltaTime;

        // Sprawd�, czy strza� osi�gn�� maksymaln� odleg�o��
        if (distanceTraveled >= constantDistance)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateTargetPosition()
    {
        // Uzyskaj pozycj� myszy w przestrzeni gry
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //// Ustaw targetPosition na sta�� odleg�o�� od gracza w kierunku myszki
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
