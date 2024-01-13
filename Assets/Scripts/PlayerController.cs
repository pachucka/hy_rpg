using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    private float shootSpeed = 1;
    public int damage = 10;
    private bool isAlive = true;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
            isMoving = rb.velocity.magnitude > 0; // Aktualizuje isMoving w zale¿noœci od prêdkoœci gracza
        }
        else
        {
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
    }

    private void die()
    {
        SceneManager.LoadSceneAsync("DeathScene");
    }

    private void UpdateShootPointPosition()
    {
        Vector3 lookDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        float distanceFromPlayer = 0.5f;
        Vector3 offset = lookDirection * distanceFromPlayer;

        shootPoint.localPosition = offset;
        shootPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void ShootTowardsMouse()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (shootSpeed <= 0)
            {
                Instantiate(projectile, shootPoint.position, shootPoint.rotation);
                shootSpeed = timeBetweenShots;
            }
        }
        shootSpeed -= Time.deltaTime;
    }

    public void speedUp()
    {
        speedActive = true;
    }
}
