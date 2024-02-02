using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    public float shootSpeed = 1;
    public int damage = 10;
    private bool isAlive = true;
    public int xp = 0;
    public int lvl = 1;

    public bool speedActive = false;
    private float speedTime = 5f;
    private float originalMoveSpeed;

    private Rigidbody2D rb;
    public float moveSpeed;
    public float timeBetweenShots;
    public GameObject projectile;

    private Animator animator;

    public static PlayerController instance;

    public string areaTransitionName;

    public Vector2 initialPos;

    public bool canMove = true;

    public Transform shootPoint;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        shootSpeed = timeBetweenShots;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        originalMoveSpeed = moveSpeed;

        Debug.Log("Initial Level: " + lvl);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player can move
        if (canMove)
        {
            // If the player is allowed to move, set the rigidbody's velocity based on input
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

            // Update isMoving based on the player's velocity magnitude
            isMoving = rb.velocity.magnitude > 0;
        }
        else
        {
            // If movement is not allowed, set the rigidbody's velocity to zero
            rb.velocity = Vector2.zero;
        }

        animator.SetFloat("moveX", rb.velocity.x);
        animator.SetFloat("moveY", rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        if (isMoving)
        {
            UpdateShootPointPosition();
        }

        ShootTowardsMouse();
        if (speedActive)
        {
            Debug.Log(speedTime);
            if (speedTime <= 0)
            {
                speedActive = false;
                moveSpeed = originalMoveSpeed;
            }
            speedTime -= Time.deltaTime;
        }

        if(health <= 0 && isAlive)
        {
            isAlive = false;
            die();
        }

        if(xp == 100)
        {
            LevelUp();
        }
    }

    private void die()
    {
        SceneManager.LoadSceneAsync("DeathScene");
    }

    private void UpdateShootPointPosition()
    {
        Vector3 lookDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized;

        // Calculate the angle of the shoot direction in degrees
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        float distanceFromPlayer = 0.5f;

        // Calculate the position of the shoot point
        Vector3 offset = lookDirection * distanceFromPlayer;
        shootPoint.localPosition = offset;

        // Rotate the shoot point to face the calculated angle
        shootPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void ShootTowardsMouse()
    {
        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Check if enough time has passed since the last shot
            if (shootSpeed <= 0)
            {
                // Instantiate a projectile at the shoot point's position and rotation
                Instantiate(projectile, shootPoint.position, shootPoint.rotation);

                // call Shooting SFX
                AudioManager.instance.PlayEffects("Shoot");

                //Reset the shoot speed to the time between shots
                shootSpeed = timeBetweenShots;
            }
        }
        // Decrease the shoot speed over time
        shootSpeed -= Time.deltaTime;
    }

    public void speedUp()
    {
        speedActive = true;
    }

    public void resetStats()
    {
        health = 100;
        shootSpeed = 1;
        damage = 10;
        isAlive = true;
        xp = 0;
        lvl = 1;
    }

    private void LevelUp()
    {
        Debug.Log(lvl);
        Debug.Log("LevelUP");
        xp = 0;
        if(lvl % 5 == 0)
        {
            Debug.Log("Increased strength on every 5th level");
            damage += 10;
        }
        if(timeBetweenShots > 0 && lvl % 3 == 0)
        {
            timeBetweenShots -= 0.1f;
        }
        lvl++;
    }
}
