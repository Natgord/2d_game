using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemey : MonoBehaviour
{
    GameObject player;

    public int health;
    public float speed;

    public bool followXPlayer;
    public bool followXYPlayer;
    public bool followXYPlayerWithDelay;
    public float followDelay;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (followXYPlayer)
        {
            //transform.LookAt(player.transform);
            Vector3 targetDirection = (player.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(targetDirection * speed);
        }

        if (followXPlayer)
        {
            //transform.LookAt(player.transform);
            Vector3 targetDirection = (player.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(targetDirection.x, 0f) * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectil")
        {
            health -= 1;
        }

        if (collision.gameObject.tag == "ShortAttack")
        {
            health -= 2;
        }
    }
}
