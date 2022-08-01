using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeUI : MonoBehaviour
{
    public GameObject[] addColorButtons;
    private int buttonIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Awake()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(addColorButtons[buttonIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            buttonIndex++;

            if (buttonIndex >= addColorButtons.Length)
            {
                buttonIndex = 0;
            }

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(addColorButtons[buttonIndex]);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            buttonIndex--;

            if (buttonIndex < 0)
            {
                buttonIndex = addColorButtons.Length - 1;
            }
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(addColorButtons[buttonIndex]);
        }


        if (Input.GetButtonDown("Jump"))
        {
            switch (EventSystem.current.currentSelectedGameObject.name)
            {
                case "AddRedButton":
                    GameObject.Find("Player").GetComponent<SpriteRenderer>().color += new Color(30f / 255f, 0f, 0f, 0f);
                    break;
                case "AddGreenButton":
                    GameObject.Find("Player").GetComponent<SpriteRenderer>().color += new Color(0f, 30f / 255f, 0f, 0f);
                    break;
                case "AddBlueButton":
                    GameObject.Find("Player").GetComponent<SpriteRenderer>().color += new Color(0f, 0f, 30f / 255f, 0f);
                    break;
            }

            gameObject.SetActive(false);
        }
    }
}
