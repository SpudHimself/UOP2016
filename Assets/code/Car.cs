﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Car : MonoBehaviour
{
	public enum eState
	{
		Countdown,
		Playing,
		GameOver
	}
	private eState mState;

    [Range(1.0f, 4.0f)]
    public int mPlayerNumber;
    public bool mKeyboardUser;

    public List<AxleInfo> mAxleInfos;
    public float mMaxMotorTorque;
    public float mMaxSteeringAngle;
    public float mBrakeTorque;

	private ScoreManager mScoreManager;

	private GameObject mScorePlumPrefab;

    public float Motor { get; set; }
	void Awake()
	{
		tag = Tags.PLAYER;

        GameManager.Singleton().GetPlayers().Add(this);

		gameObject.SetTagRecursively( Tags.PLAYER );
	}

    void OnLevelWasLoaded()
    {
        GameManager.Singleton().GetPlayers().Add(this);
    }

    // Use this for initialization
    void Start()
    {
		mScoreManager = gameObject.AddComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if ( Input.GetButtonDown( "Start_1" ) )
        //{
        //    GameManager.Singleton().TogglePause();
        //}

        //input testing
        if (Input.GetButtonDown("Powerup_" + mPlayerNumber))
        {
            Debug.Log("Player " + mPlayerNumber + ": Powerup pressed");
            //firePowerup()
        }

        if (Input.GetButtonDown("Start_" + mPlayerNumber))
        {
            Debug.Log("Player " + mPlayerNumber + ": Start pressed");
            GameManager.Singleton().TogglePause();
        }

        if (Input.GetButtonDown("Reset_" + mPlayerNumber))
        {
            Debug.Log("Player " + mPlayerNumber + ": Reset pressed");
            ResetPosition();
        }
        
    }

    public void FixedUpdate()
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
		}
    }

    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
    }

	public void SetState( eState state )
	{
		mState = state;
	}

	private void StateCountdown()
	{
	}

	private void StatePlaying()
	{
		UpdateCar();
	}

	private void StateGameOver()
	{
		if ( Input.GetKeyDown( KeyCode.Return ) || Input.GetButtonDown("Start_1") )
		{
			Application.LoadLevel( Application.loadedLevelName );
		}
	}

	private void UpdateCar()
	{
		//for multiple people
        //float motor = mMaxMotorTorque * Input.GetAxis("Vertical_" + mPlayerNumber);
        //float steering = mMaxSteeringAngle * Input.GetAxis("Horizontal_" + mPlayerNumber);
        float steering;


        //fuckery for testing, wont be needed end game
        if (mKeyboardUser)
        {
            Motor = mMaxMotorTorque * Input.GetAxis("Vertical");
            steering = mMaxSteeringAngle * Input.GetAxis("Horizontal");
        }
        else
        {
            Motor = mMaxMotorTorque * Input.GetAxis("Acceleration_" + mPlayerNumber);
            steering = mMaxSteeringAngle * Input.GetAxis("Steering_" + mPlayerNumber);
        }

        foreach (AxleInfo axle in mAxleInfos)
        {
            if (axle.steering)
            {
                axle.leftWheel.steerAngle = steering;
                axle.rightWheel.steerAngle = steering;
            }

            if (axle.motor)
            {
                axle.leftWheel.motorTorque = Motor;
                axle.rightWheel.motorTorque = Motor;

                //handbrake
                if (Input.GetButton("Handbrake_" + mPlayerNumber))
                {
                    Debug.Log("Player " + mPlayerNumber + ": Brake applied");
                    axle.leftWheel.brakeTorque = mBrakeTorque;
                    axle.rightWheel.brakeTorque = mBrakeTorque;
                }
                else
                {
                    axle.leftWheel.brakeTorque = 0.0f;
                    axle.rightWheel.brakeTorque = 0.0f;
                }
            }
        }
	}

	public ScoreManager GetScoreManager()
	{
		return mScoreManager;
	}

	public int GetPlayerNumber()
	{
		return mPlayerNumber;
	}

    void FirePowerup()
    {
        //fire the powerup yo
    }

    //wouldnt mind cleaning this one up
    void ResetPosition()
    {
        Vector3 currentPosition = this.transform.position;
        Quaternion currentRotation = this.transform.rotation;

        //set y pos to +2
        //set z rot to 0
        currentRotation = new Quaternion(currentRotation.x, currentRotation.y, 0.0f, 1.0f);
        currentPosition.y += 2.0f;

        this.transform.rotation = currentRotation;
        this.transform.position = currentPosition;

    }
}