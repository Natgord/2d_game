using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{


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
}
