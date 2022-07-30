using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakUI : MonoBehaviour
{
    public Text interactionTxt;
    public Image interactionImg;

    private GameObject player;

    PlayerStatus plyStatus;
    int pressCount = 0;
    int lastPressCount = 0;
    bool talkToGuide = false;

    string[] guideTxts = { "OMGGGG!! This world get its color drain by the lord called by the name of ...",
                            "Sorry we can't say his hame. If so, we'll be cursed for the rest of our lives." +
                            " The world is going to collapse in an dark aera infinitly and I can't do anything for it" +
                            " because I'm stuck here watching this black cloud over your head. But you're a lucky mother fucker!" +
                            " I still have some colors to give you and whit that, you'll be able to start the journey of SAVING THE WORLD!!!!!!",
                            "I only have red color for you my hero and I don't have a lot. Anyway, take it! Here comes 10 points of red!"};


    // Start is called before the first frame update
    void awake()
    {
        lastPressCount = 0;
        pressCount = 0;
        talkToGuide = false;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        plyStatus = player.GetComponent<PlayerStatus>();

        if (talkToGuide)
        {
            if (pressCount > lastPressCount)
            {
                if (pressCount == 4)
                {
                    plyStatus.GainColor(new Color(100f/255f, 0f, 0f, 0f));
                    Destroy(gameObject);
                }
                else
                {
                    interactionTxt.text = guideTxts[lastPressCount];
                }
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            lastPressCount = pressCount;
            pressCount++;
        }
    }

    public void StartDialog(GameObject objectToInteract)
    {
        switch (objectToInteract.name)
        {
            case "Guide":
                pressCount++;
                talkToGuide = true;
                break;
        }
    }
}
