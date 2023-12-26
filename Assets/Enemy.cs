using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int Hp = 50;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float maxDistance;

    private float timeBetweenShots;
    public float shootSpeed;
    public GameObject projectile;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenShots = shootSpeed;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < maxDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                FlipSprite(direction.x);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                FlipSprite(-direction.x); // Obracaj w przeciwnym kierunku
            }

            if (timeBetweenShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBetweenShots = shootSpeed;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }
        else
        {
            transform.position = this.transform.position;
            StopShooting();
        }

        if (GameManager.instance.dialogueActive || GameManager.instance.menuOpen)
        {
            transform.position = this.transform.position;
            StopShooting();
        }

        if (Hp <= 0)
        {
            die();
        }
    }


    private void StopShooting()
    {
        // Zatrzymaj strzelanie
        timeBetweenShots = shootSpeed;
    }

    private void die()
    {

    }

    private void FlipSprite(float direction)
    {
        if (direction > 0)
        {
            // Obróæ w prawo
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (direction < 0)
        {
            // Obróæ w lewo
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        // Nie zmieniaj skali, gdy poruszasz siê pionowo
    }
}