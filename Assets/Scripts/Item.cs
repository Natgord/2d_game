using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float yRotationSpeed;

    public ParticleSystem selfDestroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nextRotation = new Vector3(0f, 1f, 0f);
        transform.Rotate(nextRotation, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(selfDestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
