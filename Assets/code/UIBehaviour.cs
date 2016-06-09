using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBehaviour : MonoBehaviour
{
    public static int score;        // The player's score.

    public GameObject player;

	private ScoreManager sm;

    Text text;                      // Reference to the Text component.


    void Start()
    {
        // Set up the reference.
        text = GetComponent<Text>();
		sm = player.GetComponent<ScoreManager>();

        // Reset the score.
        score = 0;
    }


    void Update()
    {
        score = sm.Score;
        //score++;
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = "Score: " + score;
    }
}


