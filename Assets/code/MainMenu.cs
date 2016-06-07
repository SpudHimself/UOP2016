using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;

    public AudioSource audioClip;

    // Use this for initialization
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {       
    }

    public void LoadLevel(string filename)
    {
        Application.LoadLevel(filename);
    }

    public void Quit()
    {
        Application.Quit();
    }
}