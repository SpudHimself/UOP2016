using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public enum eState
	{
		Playing,
		GameOver,
		Paused,
	}
	private eState mState;

	public const float GAME_TIME_DEFAULT = 15f; // This will probably get tweaked all the time.
	private float mGameTimer;

	// Singleton.
	private static GameManager sSingleton;
	public static GameManager Singleton()
	{
		return sSingleton;
	}

	void Awake()
	{
		sSingleton = this;

		mGameTimer = GAME_TIME_DEFAULT;
	}

	void Update()
	{
		switch ( mState )
		{
			case eState.Playing:
				StatePlaying();
				break;

			case eState.GameOver:
				StateGameOver();
				break;

			case eState.Paused:
				StatePaused();
				break;
		}
	}

	public void SetState( eState state )
	{
		mState = state;

		// Ideally only set stuff in here.
		// State functions that are called per-frame is done in Update().
		switch ( state )
		{
			case eState.Playing:
				break;

			case eState.GameOver:
				print( "Game over!" );
				// Press start to play again or some bullshit...
				break;

			case eState.Paused:
				break;
		}
	}

	private void StatePlaying()
	{
		mGameTimer = Mathf.Max( mGameTimer - Time.deltaTime, 0f );
		//print( mGameTimer );
		if ( mGameTimer <= 0f )
		{
			SetState( eState.GameOver );
		}
	}

	private void StateGameOver()
	{

	}

	private void StatePaused()
	{

	}
}