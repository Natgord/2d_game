using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public bool goDownFisrt;
    public float maxYTranslate;
    public float minYTranslate;
    public float translateSpeed;

    Vector3[] translations;
    int translateIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 cloudPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        translations = new Vector3[2];
        translations[0] = cloudPos + new Vector3(0f, maxYTranslate, 0f);
        translations[1] = cloudPos - new Vector3(0f, minYTranslate, 0f);

        if (goDownFisrt)
        {
            translateIndex = 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Move the object upward in world space 1 unit/second.
        float step = translateSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, translations[translateIndex], step);

        // Check if need change direction
        if (translations[translateIndex] == transform.position)
        {
            // Change index
            translateIndex = translateIndex * -1 + 1;
        }
    }
}
