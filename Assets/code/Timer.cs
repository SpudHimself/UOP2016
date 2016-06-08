using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    //public static float time = 90.0f;                 

    Text text;                      // Reference to the Text component.
    private static float timer;

    void Start()
    {
        // Set up the reference.
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (GameManager.Singleton().GetState() == GameManager.eState.Playing)
        {
            timer = GameManager.Singleton().GetGameTime();
            text.text = "Time Remaining: " + timer.ToString("F2");
        }

    }
}