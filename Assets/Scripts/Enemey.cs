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
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), speed);
        }

        if (followXPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed);
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
