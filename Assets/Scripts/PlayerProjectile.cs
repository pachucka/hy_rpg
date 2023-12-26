using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField]private float speed;
    private bool hit;
    private CircleCollider2D circleCollider;
    private float direction;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (hit)
        {
            return;
        }

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        hit = true;
        circleCollider.enabled = false;
        Deactivate();
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;

        gameObject.SetActive(true);
        hit = false;
        circleCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }



    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
