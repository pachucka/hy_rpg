using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float maxDistance;

    private float timeBetweenShots;
    public float start;
    public GameObject projectile;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenShots = start;
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, player.position) < maxDistance)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

            if (timeBetweenShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBetweenShots = start;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        } else
        {
            transform.position = this.transform.position;
            StopShooting();
        }

        if(GameManager.instance.dialogueActive || GameManager.instance.menuOpen)
        {
            transform.position = this.transform.position;
            StopShooting();
        }



    }

    private void StopShooting()
    {
        // Zatrzymaj strzelanie
        timeBetweenShots = start;
    }
}
