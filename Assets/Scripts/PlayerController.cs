using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    //Settings
    [Header("Settings")]
    [SerializeField] float playerSpeed = 5.0f;
    [SerializeField] float jumpForce = 250f;

    [Header("Collisions")]
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask groundingLayers;

    [Header("Shooting")]
    [SerializeField] Transform firingPoint;
    [SerializeField] Bullet bullet;
    [SerializeField] float fireDelay = .5f;
    private float fireTimer;

    
    bool isFacingRight;

    List<Collider2D> groundColliders;

    Vector3 originalPlayerLocation;
    Vector3 movement;

    //Components
    Rigidbody2D rb;


    private Button nearbyButton;
    private bool isAllowedTomove;

    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        isAllowedTomove = true;
        
        groundColliders = new List<Collider2D>(); // init list
        //Debug.Log("Hello world!");

        // fetch the right rigidbody component
        rb = GetComponent<Rigidbody2D>();
        originalPlayerLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAllowedTomove)
        {


            // are we pressing right
            if (Input.GetKey(KeyCode.D))
            {
                if (!isFacingRight)
                    FlipSprite();


                movement += new Vector3(playerSpeed, 0f, 0f);
            }

            // are we pressing left
            else if (Input.GetKey(KeyCode.A))
            {
                if (isFacingRight)
                    FlipSprite();

                movement += new Vector3(-playerSpeed, 0f, 0f);
            }

            //if (isGrounded == true)
            //{
            //    // are we jumping
            //    if (Input.GetKeyDown(KeyCode.Space))
            //    {
            //        rb.AddForce(new Vector2(0f, jumpForce));
            //    }
            //}

            if (isGrounded && Input.GetKeyDown(KeyCode.W))
            {
                rb.AddForce(new Vector2(0f, jumpForce));
            }

            //Button interaction
            if (Input.GetKeyDown(KeyCode.Space) && nearbyButton)
            {
                nearbyButton.TurnButton();
            }

            if (Input.GetMouseButton(0))
            {
                if(fireTimer >= fireDelay)
                {
                    Instantiate(bullet, firingPoint.position, Quaternion.identity).Setup(isFacingRight);
                    fireTimer = 0;
                }
                
            }

            fireTimer += Time.deltaTime;

        }
        //reseting
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            //transform.position = originalPlayerLocation;
            ResetLevel();
        }
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,
                        transform.localScale.z);
        isFacingRight = !isFacingRight;
    }

    private void ResetLevel()
    {
        //Reload this level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FixedUpdate()
    {
        transform.position += movement * Time.deltaTime;
        movement = Vector3.zero;
    }

    private IEnumerator Dying()
    {
        isAllowedTomove = false;
        rb.AddForce(new Vector3(0, 5, 0), ForceMode2D.Impulse);
        rb.constraints = RigidbodyConstraints2D.None;
        yield return new WaitForSeconds(2f);
        ResetLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // are we touching ground layers?
        if((groundingLayers.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            if(!groundColliders.Contains(collision)) 
            { 
                groundColliders.Add(collision);
            }

            // boxx collider is touching something
            isGrounded = true;
        }

        // is it a platform?
        if(collision.CompareTag("Platform"))
            transform.parent = collision.transform;

        // is it a button?
        if (collision.CompareTag("Button"))
            nearbyButton = collision.gameObject.GetComponent<Button>();

        //is it an enemy?
        if (collision.CompareTag("Enemy"))
            StartCoroutine(Dying());   
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((groundingLayers.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            if (groundColliders.Contains(collision))
            {
                groundColliders.Remove(collision);
            }

            if(groundColliders.Count == 0)
            {
                isGrounded = false;
            }
        }

        if (collision.CompareTag("Platform"))
            transform.parent = null;

        if (collision.CompareTag("Button"))
            nearbyButton = null;

    }
}
