using UnityEngine;
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
    public bool mSinglePlayer;

    public List<AxleInfo> mAxleInfos;
    public float mMaxMotorTorque;
    public float mMaxSteeringAngle;

	private ScoreManager mScoreManager;

	void Awake()
	{
		tag = Tags.PLAYER;

        GameManager.Singleton().AddPlayer(this);

		gameObject.SetTagRecursively( Tags.PLAYER );
	}

    void OnLevelWasLoaded()
    {
        GameManager.Singleton().AddPlayer(this);
    }

    // Use this for initialization
    void Start()
    {
		mScoreManager = gameObject.AddComponent<ScoreManager>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

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
		if ( Input.GetKeyDown( KeyCode.Return ) )
		{
			Application.LoadLevel( Application.loadedLevelName );
		}
	}

	private void UpdateCar()
	{
		//for multiple people
        //float motor = mMaxMotorTorque * Input.GetAxis("Vertical_" + mPlayerNumber);
        //float steering = mMaxSteeringAngle * Input.GetAxis("Horizontal_" + mPlayerNumber);
        float motor;
        float steering;

        //fuckery for testing, wont be needed end game
        if(mSinglePlayer)
        {
            motor = mMaxMotorTorque * Input.GetAxis("Vertical");
            steering = mMaxSteeringAngle * Input.GetAxis("Horizontal");
        }
        else
        {
            motor = mMaxMotorTorque * Input.GetAxis("Vertical_" + mPlayerNumber);
            steering = mMaxSteeringAngle * Input.GetAxis("Horizontal_" + mPlayerNumber);
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
                axle.leftWheel.motorTorque = motor;
                axle.rightWheel.motorTorque = motor;
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
}