using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject Destination;
    public float distance;

    private Transform player_destination;
    private Collider2D m_collision;

    private void Start()
    {
        CheckPriority();
        CheckDestination();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_collision = collision;
        CheckCollision();
    }

    private void CheckPriority() 
    {
        if (gameObject.CompareTag("Portal_Slave")) 
        {
            gameObject.GetComponent<SpriteRenderer>().color = Destination.GetComponent<SpriteRenderer>().color;
        }
    }

    private void CheckDestination()
    {
        player_destination = player_destination = Destination.GetComponent<Transform>();
    }

    private void CheckCollision() 
    {
        if (m_collision.gameObject.CompareTag("Player"))
        {
            CheckColor();
            //GoToDestination();
        }
        else if (m_collision.gameObject.CompareTag("Projectil"))
        {
            CheckColor();
        }
    }

    private void CheckColor()
    {
        if (m_collision.gameObject.GetComponent<SpriteRenderer>().color == gameObject.GetComponent<SpriteRenderer>().color)
        {          
            GoToDestination();
        }
    }

    private void GoToDestination() 
    {
        if (Vector2.Distance(transform.position, m_collision.transform.position) > distance)
        {
            m_collision.transform.position = new Vector2(player_destination.position.x, player_destination.position.y);
        }
    }

}
