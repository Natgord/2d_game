using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ParticleSystem selfDestroyEffect;

    public float lifeTime = 1f;

    private float startTime = 0;

    // Start is called before the first frame update
    void Awake()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTime <= (Time.time - startTime))
        {
            Instantiate(selfDestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
