using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    const int MAX_WEAPONS = 6;

    GameObject[] weapons;
    ActiveSkill[] activeSkills;

    // Start is called before the first frame update
    void Start()
    {
        weapons = new GameObject[MAX_WEAPONS];

        weapons[0] = new GameObject();
        weapons[0].transform.parent = gameObject.transform;
        weapons[0].AddComponent<DefaultAttack>();

        weapons[1] = new GameObject();
        weapons[1].transform.parent = gameObject.transform;
        weapons[1].AddComponent<BurstTrail>();


    }

    // Update is called once per frame
    void Update()
    {

    }
}
