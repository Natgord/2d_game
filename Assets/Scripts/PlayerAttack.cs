using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAttack : MonoBehaviour
{
    // Classes
    public GameObject projectil;
    public GameObject shortAttack;

    private Transform firePoint;
    private Animator facialAnimator;
    private Transform shortAttackPoint;
    private GameObject shortAtt;

    // Public variables
    public float fireDelay = 0.7f;
    public bool canShoot = false;
    public bool canShortAttack = true;

    // Private variables
    private float lastFiredTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Assign classes
        shortAttackPoint = transform.Find("ShortAttackPoint");
        firePoint = transform.Find("FirePoint");
        facialAnimator = GameObject.Find("Facial").GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player can and have to shoot
        if (canShoot && (fireDelay <= (Time.time - lastFiredTime)) && Input.GetButtonDown("Fire1"))
        {
            facialAnimator.SetBool("isShooting", true);
            shoot(projectil);
            lastFiredTime = Time.time;
        }
        else
        {
            facialAnimator.SetBool("isShooting", false);
        }

        if (canShortAttack && Input.GetButtonDown("ShortAttack"))
        {
            short_attack();
        }

        else
        {
            if (shortAtt != null && (shortAtt.transform.childCount == 0))
            {
                Destroy(shortAtt);
            }
        }
    }

    void short_attack()
    {
        // Get the effect of short attack
        shortAtt = Instantiate(shortAttack, shortAttackPoint.position, Quaternion.identity);

        // Set colors
        Color currentPlayerColor = GetComponent<SpriteRenderer>().color;
        shortAtt.GetComponent<SpriteRenderer>().color = currentPlayerColor;
        shortAtt.GetComponentInChildren<ParticleSystem>().startColor = currentPlayerColor;
        ParticleSystem.TrailModule trails = shortAtt.GetComponentInChildren<ParticleSystem>().trails;
        trails.inheritParticleColor = true;
    }

    void shoot(GameObject projectilToShoot)
    {
        // Instantiate a projectil
        GameObject firedProjectile = Instantiate(projectilToShoot, firePoint.position, Quaternion.identity);

        // Check if the player is facing right
        if (transform.eulerAngles.y > 0)
        {
            // Five velocity to the projectil in right direction
            firedProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500f);
        }
        else
        {
            // // Five velocity to the projectil in left direction
            firedProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500f);
        }

        // Ignore the collisions with the player
        Physics2D.IgnoreCollision(firedProjectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
