using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    // Global classes
    Rigidbody2D rb;
    SpriteRenderer spriteRend;
    public ParticleSystem dashEffect;
    private Animator facialAnimator;
    public AudioSource mouvementSound;

    // Global public variables
    public float movementSpeed = 6f;
    public float jumpForce = 5f;
    public float dashForce = 10;
    public float startDashTimer;
    public int maxJump = 1;
    public int maxDash = 1;

    // Global private variables
    private bool canJump = false;
    private float xSize;
    private bool canDash = false;
    private bool isDashing = false;
    private float currentDashTimer;
    private int jumpCount = 0;
    private int dashCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Attach classes to components/objects
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        facialAnimator = GameObject.Find("Facial").GetComponent<Animator>();

        xSize = transform.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {
        checkMovement();
        checkJump();
        checkDashAbility();

        GetComponent<TrailRenderer>().startColor = spriteRend.color;
    }

    private void checkMovement()
    {
        // Get A or D inputs
        // TODO : add controller inputs
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            // Set run animation
            facialAnimator.SetBool("isMoving", true);
            if (mouvementSound.isPlaying != true)
            {
                mouvementSound.Play();
            }
            
        }
        else if (horizontalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            // Set run animation
            facialAnimator.SetBool("isMoving", true);
            if (mouvementSound.isPlaying != true)
            {
                mouvementSound.Play();
            }
        }
        else
        {
            facialAnimator.SetBool("isMoving", false);
            mouvementSound.Stop();
        }

        // Add force on Player on the X axis
        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
    }

    private void checkJump()
    {
        // Check if space bar is pressed and if the player has its feet on objects he can jump from
        if (Input.GetButtonDown("Jump") && canJump)
        {
            // Add force on the Y axis
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Increment jump counter
            jumpCount++;

            if (jumpCount >= maxJump)
            {
                // Set jump bool
                canJump = false;
            }
        }
    }

    private void checkDashAbility()
    {
        if (canDash && (dashCount < maxDash))
        {
            Vector3 dashDirection = transform.right;
            if (Input.GetButtonDown("Dash"))
            {
                isDashing = true;
                currentDashTimer = startDashTimer;
                rb.velocity = Vector2.zero;
                if (Mathf.Abs(transform.eulerAngles.y) == 180)
                {
                    dashDirection = transform.right * -1;
                }
            }

            if (isDashing)
            {
                ParticleSystem dashPS = Instantiate(dashEffect, transform.position, Quaternion.identity);
                dashPS.startColor = spriteRend.color;
                rb.velocity = dashDirection * dashForce;
                currentDashTimer -= Time.deltaTime;
                if (currentDashTimer <= 0)
                {
                    dashCount++;
                    isDashing = false;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Get the tag name of the collision object in the game
        string tag = collision.gameObject.tag;

        // Switch in tag
        switch (tag)
        {
            case "Ground":
            case "Platforme":
                dashCount = 0;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the tag name of the collision object in the game
        string tag = collision.gameObject.tag;

        // Switch in tag
        switch (tag)
        {
            case "Ground":
            case "Platforme":
                canJump = true;
                jumpCount = 0;
                break;

            case "Item":

                // Get the name of the game object
                string name = collision.gameObject.name;

                // If it's the Dash ability
                if (name == "DashAbility")
                {
                    // The player can dash now
                    canDash = true;
                }
                // If it's the double jump ability
                else if (name == "DoubleJumpAbility")
                {
                    // The player can double jump now
                    maxJump = 2;
                }
                break;
        }
    }

}
