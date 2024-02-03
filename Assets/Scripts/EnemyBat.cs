using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    private int Hp = 10;
    private int xpToGive = 5;
    public float speed;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Check if the in-game menu is not open
        if (!GameManager.instance.menuOpen)
        {
            // Calculate the direction from the current position to the player's position
            Vector2 direction = (player.position - transform.position).normalized;


            // Move the object towards the player's position based on the calculated direction and speed
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerController.instance.health -= 10;
        }

        if(Hp <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int hp)
    {
        Hp -= hp;
    }

    private void Die()
    {
        Destroy(gameObject);
        PlayerController.instance.xp += xpToGive;
    }
}
