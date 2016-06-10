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
		GameManager.eState state = GameManager.Singleton().GetState();

		timer = GameManager.Singleton().GetGameTime();
		text.text = state == GameManager.eState.Playing ? "Time Remaining: " + timer.ToString( "F2" ) : timer.ToString( "F0" );


        
    }
}