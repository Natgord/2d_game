using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BurstTrail : ActiveSkill
{
    GameObject attack;
    public float speed = 2f;

    List<GameObject> instantiatedAttack;

    public ParticleSystem part;

    private void Awake()
    {
        instantiatedAttack = new List<GameObject>();
        attack = Resources.Load("Prefabs/BurstTrail", typeof(GameObject)) as GameObject;
        coolDown = 4f;
        part = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        UpdateSkill();
    }

    override public void Activate()
    {
        instantiatedAttack.Add(Instantiate(attack));
    }

    //void OnParticleCollision(GameObject other)
    //{
    //    if (other.tag == "Enemey")
    //        Debug.Log(other.tag);
    //}

}

