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
		if ( state == GameManager.eState.Playing )
		{
			text.text = "Time Remaining: " + timer.ToString( "F2" );
		}
		else if ( state == GameManager.eState.GameOver )
		{
			Car car = GameManager.Singleton().GetWinningPlayer();
			text.text = car.GetPlayerNumber() + " wins.";
		}
		else if ( state == GameManager.eState.Countdown )
		{
			text.text = timer.ToString( "F0" );
		}
    }
}