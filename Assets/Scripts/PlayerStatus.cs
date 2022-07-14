using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    SpriteRenderer spriteRend;
    public ParticleSystem LightUpPS;    

    private ParticleSystem lightUpEffect;

    private Transform centerBoddyPlayer;

    private int fadeSpeed = 230;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        string name = collision.gameObject.name;

        char firstLetter = name[0];
        char secondLetter = name[1];

        if (firstLetter == 'C')
        {
            if (secondLetter == 'G')
            {
                Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;
                Color nextColor = currentColor + collision.gameObject.GetComponent<SpriteRenderer>().color / 2f;

                spriteRend.color = nextColor;
                if (lightUpEffect == null)
                {
                    lightUpEffect = Instantiate(LightUpPS, transform.position, Quaternion.identity);
                    lightUpEffect.transform.parent = gameObject.transform;
                }
            }
            else if (secondLetter == 'L')
            {
                Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;
                Color nextColor = currentColor - collision.gameObject.GetComponent<SpriteRenderer>().color / 2f;
                nextColor += new Color(0f, 0f, 0f, 1f);
                spriteRend.color = nextColor;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        string name = collision.gameObject.name;

        char firstLetter = name[0];
        char secondLetter = name[1];

        if (secondLetter == 'G')
        {
            Destroy(lightUpEffect);
        }     
    }
}
