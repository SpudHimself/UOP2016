using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static float time = 90.0f;                 
    
    Text text;                      // Reference to the Text component.

    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        time = 90.0f;
    }


    void Update()
    {
        time -= Time.deltaTime;
        
        text.text = "Time Remaining: " + time.ToString("F2");
    }
}