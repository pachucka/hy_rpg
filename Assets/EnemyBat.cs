using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    private int Hp = 10;
    public float speed;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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
    }
}
