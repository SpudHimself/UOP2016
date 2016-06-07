using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    //movement stuff
    public float mSpeed = 10.0f;
    Vector3 mMoveDirection;

    //component stuff
    Rigidbody mRigidBody;


    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
    }

    //APPLY ANY LOGIC CODE HERE
    void Update()
    {
        //wank way of doing it but works for now, gravity is fucked.
        mMoveDirection = new Vector3(mSpeed * Input.GetAxis("Horizontal"), 0.0f, mSpeed * Input.GetAxis("Vertical"));
    }

    //APPLY ANY MOVEMENT CODE HERE
    void FixedUpdate()
    {
        mRigidBody.velocity = mMoveDirection;
    }
}
