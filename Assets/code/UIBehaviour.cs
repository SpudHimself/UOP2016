using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBehaviour : MonoBehaviour
{
    public static int score;        // The player's score.


    Text text;                      // Reference to the Text component.


    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        score = 0;
    }


    void Update()
    {
        score++;
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = "Score: " + score;
    }
}


