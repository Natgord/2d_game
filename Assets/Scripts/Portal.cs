using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform player_destination;

    public GameObject Destination;
    public float distance;

    private void Start()
    {
        player_destination = Destination.GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Vector2.Distance(transform.position, collision.transform.position) > distance) 
        {
            collision.transform.position = new Vector2(player_destination.position.x, player_destination.position.y);
            
        }
    }
}
