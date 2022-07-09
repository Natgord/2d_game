using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Global classes
    Rigidbody2D rb;

    // Global public variables
    public float movementSpeed = 6f;
    public float jumpForce = 5f;

    // Global private variables
    private bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        // Attach classes to components/objects
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get A or D inputs
        // TODO : add controller inputs
        float horizontalInput = Input.GetAxis("Horizontal"); // Can be -1, 0 or 1

        // Add force on Player on the X axis
        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);

        // Check if space bar is pressed and if the player has its feet on objects he can jump from
        // TODO : add controller input
        if (Input.GetButtonDown("Jump") && canJump)
        {
            // Add force on the Y axis
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
                canJump = true;
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Get the tag name of the collision object in the game
        string tag = collision.gameObject.tag;

        // Switch in tag
        switch (tag)
        {
            case "Ground":
                canJump = false;
                break;
        }
    }
}
