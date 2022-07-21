using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectil;

    private Transform firePoint;
    private Animator facialAnimator;

    public float fireDelay = 0.7f;
    private float lastFiredTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        firePoint = transform.Find("FirePoint");
        facialAnimator = GameObject.Find("Facial").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") &&
            (fireDelay < (Time.time - lastFiredTime)))
        {
            facialAnimator.SetBool("isShooting", true);
            GameObject firedProjectile = Instantiate(projectil, firePoint.position, Quaternion.identity);
            if (transform.eulerAngles.y > 0)
            {
                firedProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500f);
            }
            else
            {
                firedProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500f);
            }

            lastFiredTime = Time.time;
            Physics2D.IgnoreCollision(firedProjectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else
        {
            facialAnimator.SetBool("isShooting", false);
        }
    }
}
