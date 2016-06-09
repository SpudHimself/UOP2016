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

    public Animator animator;

	private ScoreManager mScoreManager;

	private GameObject mScorePlumPrefab;

	public AudioClip mMotor;
	public AudioSource mAudioSource;

    private float mDistToGround;
    public BoxCollider mBoxCollider;
    private Rigidbody mRigidBody;
    private List<WheelCollider> mWheelsColliders;

    public float Motor { get; set; }

	private int mNumItems = 0;

	private GameObject mItemPrefab;

	void Awake()
	{
		tag = Tags.PLAYER;

		gameObject.SetTagRecursively( Tags.PLAYER );

		mItemPrefab = (GameObject) Resources.Load( "prefabs/Item" );

		mScoreManager = gameObject.AddComponent<ScoreManager>();
	}

    void OnLevelWasLoaded()
    {
        GameManager.Singleton().GetPlayers().Add(this);
    }

    // Use this for initialization
    void Start()
    {
		// Leave this first.
		GameManager.Singleton().GetPlayers().Add(this);

		mAudioSource.playOnAwake = false;
		mAudioSource.loop = true;
		mAudioSource.clip = mMotor;
		mAudioSource.Play();

        mBoxCollider = this.GetComponent<BoxCollider>();
        mDistToGround = mBoxCollider.bounds.extents.y;
        //Debug.Log(mDistToGround);

        mRigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if ( Input.GetButtonDown( "Start_1" ) )
        //{
        //    GameManager.Singleton().TogglePause();
        //}

        mDistToGround = GetComponentInChildren<BoxCollider>().bounds.extents.y;
        Debug.DrawLine(this.transform.position, -this.transform.up, Color.black);
        //Debug.Log(mDistToGround);

        //input testing
        if (Input.GetButtonDown("Powerup_" + mPlayerNumber))
        {
            Debug.Log("Player " + mPlayerNumber + ": Powerup pressed");
            //firePowerup()
        }

        if (Input.GetButtonDown("Start_" + mPlayerNumber) || Input.GetKeyDown( KeyCode.Escape ))
        {
            Debug.Log("Player " + mPlayerNumber + ": Start pressed");
            GameManager.Singleton().TogglePause();
        }

        if (Input.GetButtonDown("Reset_" + mPlayerNumber) && !this.isGrounded())
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
		UpdateCarNoise();
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

	private float mVerticalAxis;

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

			mVerticalAxis = Input.GetAxis( "Vertical" );

			//SoundManager.Singleton().SetPitch( "motor_fatman", Input.GetAxis("Vertical") );			
        }
        else
        {
            Motor = mMaxMotorTorque * Input.GetAxis("Acceleration_" + mPlayerNumber);
            steering = mMaxSteeringAngle * Input.GetAxis("Steering_" + mPlayerNumber);

			mVerticalAxis = Input.GetAxis("Acceleration_" + mPlayerNumber);

			//SoundManager.Singleton().SetPitch( "motor_fatman", Input.GetAxis("Acceleration_" + mPlayerNumber) );
			//SoundManager.Singleton().PlaySound( "motor_fatman" );
        }

        animator.SetFloat("Steering", steering);

		UpdateCarNoise();

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
        currentRotation = new Quaternion(0.0f, currentRotation.y, 0.0f, 1.0f);
        currentPosition.y += 0.5f;

        this.transform.rotation = currentRotation;
        this.transform.position = currentPosition;

        mRigidBody.velocity = Vector3.zero;
    }

	private void UpdateCarNoise()
	{
        float vol = mVerticalAxis * (Time.deltaTime * 2.5f);
        mAudioSource.volume = Mathf.Lerp(0f, 1f, vol);

        //AudioListener.volume = Mathf.Lerp(0f, 1f, Time.deltaTime);
	}

	private void DropItems()
	{
		for ( int i = 0; i < mNumItems; i++ )
		{
			Vector3 randomCircle = Random.insideUnitCircle;
			randomCircle += transform.position;

			Instantiate( mItemPrefab, new Vector3( randomCircle.x, transform.position.y + 3f, randomCircle.y ), Quaternion.identity );
		}

		mNumItems = 0;
	}

	public int GetItems()
	{
		return mNumItems;
	}

	public void SetItems( int items )
	{
		mNumItems = items;
	}

	public void AddItems( int items )
	{
		mNumItems += items;
	}

	private void OnCollisionEnter( Collision col )
	{
		switch ( col.gameObject.tag )
		{
			case Tags.PLAYER:
				{
					Vector3 thisVel = GetComponent<Rigidbody>().velocity;
					Vector3 otherVel = col.gameObject.GetComponent<Rigidbody>().velocity;

					if ( thisVel.magnitude > otherVel.magnitude )
					{
						col.gameObject.GetComponent<Car>().DropItems();
					}
					else if ( thisVel.magnitude < otherVel.magnitude )
					{
                        animator.SetBool("Collided", true);
						DropItems();
                        animator.SetBool("Collider", false);
					}
				}
				break;
		}
	}

    bool isGrounded()
    {
        Debug.DrawRay(transform.position, -Vector3.up, Color.red);
        Ray ray = new Ray(transform.position, -Vector3.up);

        return Physics.Raycast(ray, 1.0f);
    }
}