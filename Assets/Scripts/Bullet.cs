using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage = 5.0f;
    [SerializeField] float speed = 20.0f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Setup(bool isFacingRight)
    {
        Vector2 direction2D = new Vector2(isFacingRight ? 1 : -1, 0.25f);
        rb.AddForce(direction2D * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
                        
            collision.gameObject.GetComponent<Enemy>().Inflictdamage(damage);
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject, 1f);
    }
}
