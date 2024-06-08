using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2.0f;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] Transform groundChecker;
    [SerializeField] float health = 10.0f;

    private Rigidbody2D rb;
    private bool isMovingRight;
    Vector3 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = new Vector3(isMovingRight ? movementSpeed : - movementSpeed, 0, 0);

        Vector3 groundDirection = groundChecker.position - transform.position;

        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, groundDirection, groundDirection.magnitude, groundLayers);
        
        Vector3 wallCheckDirection = isMovingRight ? transform.right : - transform.right;

        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, wallCheckDirection, 1.0f, groundLayers);

        Debug.DrawLine(transform.position, groundChecker.position, Color.yellow);
        Debug.DrawRay(transform.position, wallCheckDirection, Color.red);

        //did we hit empthy air?
        if(groundHit.collider == null || wallHit.collider != null)
        {
            if(isMovingRight)
            {
                FlipSprite();
                isMovingRight = false;
            }
            else
            {
                FlipSprite();
                isMovingRight = true;
            }
        }
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1,
                    transform.localScale.y, transform.localScale.z);
    }

    private void FixedUpdate()
    {
        transform.position += movement * Time.deltaTime;
    }

    public void Inflictdamage(float damageToInflict)
    {
        health -=damageToInflict;
        if(health <= 0)
            Destroy(this.gameObject);
    }

}
