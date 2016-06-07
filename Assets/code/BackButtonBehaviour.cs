using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class BackButtonBehaviour : MonoBehaviour
{
    public Button currentButton;
    public EventSystem eventSystem;

    // Use this for initialization
    void Start()
    {
        currentButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadLevel(string filename)
    {
        Application.LoadLevel(filename);
    }
}
