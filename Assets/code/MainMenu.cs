using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button creditsButton;
    public Button helpButton;
    public Button quitButton;
    public Button currentButton;
    public EventSystem eventSystem;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    public void LoadLevel(string filename)
    {
        Application.LoadLevel(filename);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CheckInput()
    {
        if (currentButton == startButton)
        {
            if ((Input.GetButton("down")))
            {
                helpButton.Select();
            }

            if ((Input.GetButton("up")))
            {
                quitButton.Select();
            }
        }

        else if (currentButton == helpButton)
        {
            if ((Input.GetButton("down")))
            {
                creditsButton.Select();
            }

            if ((Input.GetButton("up")))
            {
                startButton.Select();
            }
        }

        else if (currentButton == creditsButton)
        {
            if ((Input.GetButton("down")))
            {
                quitButton.Select();
            }

            if ((Input.GetButton("up")))
            {
                helpButton.Select();
            }
        }

        else if (currentButton == quitButton)
        {
            if ((Input.GetButton("down")))
            {
                startButton.Select();
            }

            if ((Input.GetButton("up")))
            {
                creditsButton.Select();
            }
        }
    }
}