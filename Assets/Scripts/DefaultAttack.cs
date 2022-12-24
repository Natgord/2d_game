using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DefaultAttack : ActiveSkill
{
    GameObject attack;
    public float speed = 2f;

    List<GameObject> instantiatedAttack;

    private void Awake()
    {
        instantiatedAttack = new List<GameObject>();
        attack = Resources.Load("Prefabs/DefaultAttack", typeof(GameObject)) as GameObject;
        coolDown = 2f;
    }

    private void Update()
    {
        UpdateSkill();

        for (int index = 0; index < instantiatedAttack.Count; index++)
        {
            Vector2 direction = enemy.transform.position - instantiatedAttack[index].transform.position;
            direction.Normalize();
            instantiatedAttack[index].GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    override public void Activate()
    {
        instantiatedAttack.Add(Instantiate(attack));
        instantiatedAttack[instantiatedAttack.Count - 1].GetComponent<Rigidbody2D>().AddForce(10, ForceMode2D.Impulse);
    }
}
