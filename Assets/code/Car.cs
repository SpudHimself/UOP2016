using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Car : MonoBehaviour
{
    [Range(1.0f, 4.0f)]
    public int mPlayerNumber;
    public bool mSinglePlayer;

    public List<AxleInfo> mAxleInfos;
    public float mMaxMotorTorque;
    public float mMaxSteeringAngle;
    public float mBrakeTorque;

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
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void FixedUpdate()
    {
        //for multiple people
        //float motor = mMaxMotorTorque * Input.GetAxis("Vertical_" + mPlayerNumber);
        //float steering = mMaxSteeringAngle * Input.GetAxis("Horizontal_" + mPlayerNumber);
        float motor;
        float steering;


        //fuckery for testing, wont be needed end game
        if(mSinglePlayer)
        {
            motor = mMaxMotorTorque * Input.GetAxis("Acceleration");
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

    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
    }
}
