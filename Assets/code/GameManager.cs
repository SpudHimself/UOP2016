﻿using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public enum eState
	{
		Countdown,
		Playing,
		GameOver,
		Paused,
	}
	private eState mState;

	public const float GAME_TIME_PLAYING = 15f; // This will probably get tweaked all the time. 15 for testing. Maybe 45 for real thing?
	public const float GAME_TIME_COUNTDOWN = 3f;
	private float mGameTimer;

	private List<Car> mPlayers = new List<Car>();
	private List<Transform> mSpawns = new List<Transform>();

	// Singleton.
	private static GameManager sSingleton;
	public static GameManager Singleton()
	{
		return sSingleton;
	}

	void Awake()
	{
		sSingleton = this;

		mPlayers = new List<Car>();

		// This is a really shit way of doing it but it's the only way in Unity I know how.
		// Because we need the Transforms not the GameObjects.
		GameObject[] spawnGameObjects = GameObject.FindGameObjectsWithTag( Tags.PLAYER_SPAWN );
		foreach ( GameObject go in spawnGameObjects )
		{
			mSpawns.Add( go.transform );
		}

		// Keep this last.
		SetState( eState.Countdown );
	}

	void Update()
	{
		switch ( mState )
		{
			case eState.Countdown:
				StateCountdown();
				break;

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

	public eState GetState()
	{
		return mState;
	}

	public void SetState( eState state )
	{
		mState = state;

		// Ideally only set stuff in here.
		// State functions that are called per-frame is done in Update().
		switch ( state )
		{
			case eState.Countdown:
				print( "Get ready!" );

				foreach ( Car car in mPlayers ) {
					car.SetState( Car.eState.Countdown );
				}

				mGameTimer = GAME_TIME_COUNTDOWN;
				break;

			case eState.Playing:
				mGameTimer = GAME_TIME_PLAYING;
				foreach ( Car car in mPlayers ) {
					car.SetState( Car.eState.Playing );
				}
				print( "GO!" );
				break;

			case eState.GameOver:
				print( "Game over! Press Enter (temporary) to restart game." );
				foreach ( Car car in mPlayers ) {
					car.SetState( Car.eState.GameOver );
				}
				break;

			case eState.Paused:
				break;
		}
	}

	private void StateCountdown()
	{
		mGameTimer = Mathf.Max( mGameTimer - Time.deltaTime, 0f );
// 		print( mGameTimer );
		if ( mGameTimer <= 0f )
		{
			SetState( eState.Playing );
		}
	}

	private void StatePlaying()
	{
		mGameTimer = Mathf.Max( mGameTimer - Time.deltaTime, 0f );
// 		print( mGameTimer );
		if ( mGameTimer <= 0f )
		{
			SetState( eState.GameOver );
		}
	}

	private void StateGameOver()
	{
		if ( Input.GetKey( KeyCode.Return ) )
		{
			Application.LoadLevel( Application.loadedLevelName );
		}
	}

	private void StatePaused()
	{
		// Press escape or start to unpause or whatever...
	}

	public void AddPlayer( Car car )
	{
		mPlayers.Add( car );
	}

	public List<Car> GetPlayers()
	{
		return mPlayers;
	}

	/// <summary>
	/// <para>Returns the player from the list of players.</para>
	/// </summary>
	/// <param name="index">The zero-oriented player number.</param>
	/// <returns>The player from the list of players.</returns>
	public Car GetPlayer( int index )
	{
		return mPlayers[index - 1];
	}

	public Transform GetPlayerSpawn( int index )
	{
		return mSpawns[index - 1];
	}
}