using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{

    public Text whatToDoText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lookAtPlayer();
    }

    void lookAtPlayer()
    {
        GameObject player = GameObject.Find("Player");
        if (player.transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            GameObject.Find("GuideEyes").GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            GameObject.Find("GuideEyes").GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Get the tag of the collsion
        string collisionTag = other.gameObject.tag;

        if (collisionTag == "Player")
        {
            whatToDoText.enabled = true;
            whatToDoText.text = "Press B to interact with the Guide";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Get the tag of the collsion
        string collisionTag = other.gameObject.tag;

        if (collisionTag == "Player")
        {
            whatToDoText.text = "";
            whatToDoText.enabled = false;
        }
    }
}
