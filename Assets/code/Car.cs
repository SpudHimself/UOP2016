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
    public bool mKeyboardUser;

    public List<AxleInfo> mAxleInfos;
    public float mMaxMotorTorque;
    public float mMaxSteeringAngle;
    public float mBrakeTorque;

	private ScoreManager mScoreManager;

	private GameObject mScorePlumPrefab;

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
		if ( Input.GetKeyDown( KeyCode.Return ) || Input.GetButtonDown("Start") )
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
        if (mKeyboardUser)
        {
<<<<<<< HEAD
            //motor = mMaxMotorTorque * Input.GetAxis("Acceleration");
			motor = mMaxMotorTorque * Input.GetAxis("Vertical");
=======
            motor = mMaxMotorTorque * Input.GetAxis("Vertical");
>>>>>>> d2a4857cc248608b3af16e14d40423ea11d4749e
            steering = mMaxSteeringAngle * Input.GetAxis("Horizontal");
        }
        else
        {
            motor = mMaxMotorTorque * Input.GetAxis("Acceleration_" + mPlayerNumber);
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
                axle.leftWheel.motorTorque = motor;
                axle.rightWheel.motorTorque = motor;

                //handbrake
                if (Input.GetButton("Fire2"))
                {
                    Debug.Log("Brake applied");
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
}