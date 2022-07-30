using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    const int MAX_COLOR_COUNT = 60;

    SpriteRenderer spriteRend;
    public ParticleSystem LightUpPS;
    public ParticleSystem DrainPS;
    public UnityEngine.UI.Text redColor;
    public UnityEngine.UI.Text greenColor;
    public UnityEngine.UI.Text blueColor;

    public Color[] colorBuffer;
    int colorBufferIndex = 0;


    private ParticleSystem lightUpEffect;
    private ParticleSystem drainEffectDown;
    private ParticleSystem drainEffectLeft;
    private ParticleSystem drainEffectRight;
    private ParticleSystem drainEffectUp;

    private float fadeSpeed = 0.001f;

    Color touchColor;

    // Start is called before the first frame update
    void Start()
    {
        // Assign classes
        spriteRend = GetComponent<SpriteRenderer>();

        // Allocate memory for the buffer
        colorBuffer = new Color[MAX_COLOR_COUNT];
    }

    // Update is called once per frame
    void Update()
    {
        updateColor();

        if (lightUpEffect != null)
        {
            var forceOverLife = lightUpEffect.forceOverLifetime;
            forceOverLife.x = Mathf.Abs(Input.GetAxis("Horizontal")) * -50f;
        }

        // Update the UI
        updateColorHealth();
    }

    private void setColorHealthTexts(Color color)
    {
        // Set all the color in the UI
        redColor.text = "RED      " + Mathf.RoundToInt(color.r * 255);
        greenColor.text = "GREEN  " + Mathf.RoundToInt(color.g * 255);
        blueColor.text = "BLUE    " + Mathf.RoundToInt(color.b * 255);
    }

    private void updateColorHealth()
    {
        // Get the current color of the player
        colorBuffer[colorBufferIndex++] = spriteRend.color;

        // Check if counter reach max index
        if (MAX_COLOR_COUNT <= colorBufferIndex)
        {
            // Reset buffer index
            colorBufferIndex = 0;

            // Compute mean of the buffer for each color
            Color meanColor = Color.clear;
            for (int index = 0; index < MAX_COLOR_COUNT; index++)
            {
                meanColor += colorBuffer[index] / MAX_COLOR_COUNT;
            }

            // Set the color texts UI
            setColorHealthTexts(meanColor);
        }
    }

    private void updateColor()
    {
        // Set the new color the same as the current color of the player
        Color newColor = spriteRend.color;

        // Compute the new color
        newColor = spriteRend.color + touchColor;
        newColor.a = 1f;
        newColor = LimitColorValue(newColor);

        // Update color
        spriteRend.color = Color.Lerp(spriteRend.color, newColor, fadeSpeed);

        // If the player gain colors
        if (touchColor.r > 0f || touchColor.g > 0f || touchColor.b > 0f)
        {
            // Create regeneration of colors effect is it's not created yet
            if (lightUpEffect == null)
            {
                // Instantiate the effect and place it as a child of the player. Will follow player
                lightUpEffect = Instantiate(LightUpPS, transform.position, transform.rotation);
                lightUpEffect.transform.parent = gameObject.transform;
            }
            return;
        }

        Destroy(lightUpEffect);
    }

    private Color LimitColorValue(Color colorToLimit)
    {
        // Store Red Green and Blue in array
        float[] rgb = { colorToLimit.r, colorToLimit.g, colorToLimit.b };

        // Loop in red, green and blue
        for (int rgbIndex = 0; rgbIndex < rgb.Length; rgbIndex++)
        {
            // If it`s higher than one
            if (rgb[rgbIndex] > 1f)
            {
                // Set the limit
                rgb[rgbIndex] = 1f;
            }
            // If it`s lower than 0
            else if (rgb[rgbIndex] < 0f)
            {
                // Set the limit
                rgb[rgbIndex] = 0f;
            }
        }

        // Get the limited color
        Color r_limitedColor = Color.white;
        r_limitedColor.r = rgb[0];
        r_limitedColor.g = rgb[1];
        r_limitedColor.b = rgb[2];
        return r_limitedColor;
    }

    public void GainColor(Color colorToAdd)
    {
        spriteRend.color += colorToAdd;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Get the name of the collided object
        string name = collision.gameObject.name;

        // Get the current color of the player
        Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;

        // Get first two letters in string
        string firstLetters = "" + name[0] + name[1];

        if (collision.gameObject.tag == "Ground")
        {
            if (drainEffectDown == null)
            {
                // Instantiate the effect and place it as a child of the player. Will follow player
                drainEffectDown = Instantiate(DrainPS, transform.position + new Vector3(0f, -0.9f, 0f), transform.rotation);
                drainEffectDown.transform.parent = gameObject.transform;
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            if (drainEffectLeft == null)
            {
                // Instantiate the effect and place it as a child of the player. Will follow player
                int flip = -1;
                if (spriteRend.flipX == true)
                    flip = 1;
                drainEffectLeft = Instantiate(DrainPS, transform.position + new Vector3(0.9f* flip, 0.0f, 0f), transform.rotation);
                drainEffectLeft.transform.Rotate(new Vector3(0f, 0f, 90f * flip));
                drainEffectLeft.transform.parent = gameObject.transform;
            }
        }

        // If it's a "C"olor which the player will "G"ain
        if (firstLetters == "CG")
        {
            // Get the color of the collided object
            touchColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
        }
        // If it's a "C"olor which the player will "L"oose
        else if (firstLetters == "CL")
        {
            // Get the color of the collided object
            touchColor = collision.gameObject.GetComponent<SpriteRenderer>().color * -1f;
        }
        else
        {
            // The Player will not loose or gain color
            touchColor = new Color(0f, 0f, 0f, 1f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Get the name of the collided object
        string name = collision.gameObject.name;

        // Get first two letters in string
        string firstLetters = "" + name[0] + name[1];

        if (collision.gameObject.tag == "Ground")
        {
            if (drainEffectDown != null)
            {
                Destroy(drainEffectDown);
            }
        }
        else if (collision.gameObject.tag == "Wall")
        {
            if (drainEffectLeft != null)
            {
                Destroy(drainEffectLeft);
            }
        }
        // If it's a "C"olor which the player will "G"ain
        if (firstLetters == "CG" || firstLetters == "CL")
        {
            // Get the color of the collided object
            touchColor = Color.black;
        }
    }
}
