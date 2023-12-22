using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 100;

    private Rigidbody2D rb;
    public float moveSpeed;
    public float timeBetweenShots;
    private float shootSpeed;
    public GameObject projectile;

    private Animator animator;

    public static PlayerController instance;

    public string areaTransitionName;

    public Vector2 initialPos;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        shootSpeed = timeBetweenShots;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        } else
        {
            rb.velocity = Vector2.zero;
        }

        animator.SetFloat("moveX", rb.velocity.x);
        animator.SetFloat("moveY", rb.velocity.y);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if(canMove)
            {
                animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        ShootTowardsMouse();
    }

    private void ShootTowardsMouse()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (shootSpeed <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                shootSpeed = timeBetweenShots;
            }
        }
        shootSpeed -= Time.deltaTime;
    }
}
