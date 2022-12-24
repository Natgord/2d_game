using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[System.Serializable]
public class ActiveSkill : MonoBehaviour
{
    [SerializeField]
    public float coolDown { set; get; } = 0f;
    public GameObject enemy { set; get; } = null;

    float lastActiveTime = 0f;

    // Update is called once per frame
    public void UpdateSkill()
    {
        if (CheckConditions())
        {
            Activate();
            lastActiveTime = Time.time;
        }
    }

    public bool CheckConditions()
    {
        // Declare local variables
        bool succeed = true;

        // Check if the delay to spawn an enemy is reached
        succeed &= CheckCoolDown();

        // Check an enemy exists
        succeed &= CheckEnemy();

        // Return true if everything passed
        return succeed;
    }

    public bool CheckCoolDown()
    {
        // Check if the delay to spawn an enemy is reached
        if (coolDown <= (Time.time - lastActiveTime))
            return true;

        return false;
    }

    private bool CheckEnemy()
    {
        // Declare local variables
        enemy = GameObject.FindGameObjectWithTag("Enemey");

        // Check if an Enemy exists in the world
        if (null != enemy)
            return true;

        return false;

    }

    new public virtual void Activate() {}
}
