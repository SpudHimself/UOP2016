using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
	public enum eState
	{
		Waiting,
		Alive,
		Dead
	}
	private eState mState;

	// These 2 will have to be changed.
	private float mRoamRadius = 15f;
	private float mMoveTimer;
    private float mDeadTimer = 5.0f;

	private NavMeshAgent mAgent;
    private MeshRenderer mRenderer;

	void Awake()
	{
		tag = Tags.NPC;
        mRenderer = this.GetComponent<MeshRenderer>();
	}

	void Start()
	{
		GameManager.Singleton().AddNPC( this );

		mMoveTimer = 0f;

		mAgent = gameObject.AddComponent<NavMeshAgent>();
		mAgent.Resume();
		mAgent.destination = transform.position;
	}

	void Update()
	{
		switch ( mState )
		{
			case eState.Waiting:
				break;

			case eState.Alive:
				StateAlive();
				break;

			case eState.Dead:
				StateDead();
				break;
		}
	}

    void OnCollisionEnter(Collision col)
    {
        float value = 50.0f;

        if (col.gameObject.CompareTag(Tags.PLAYER))
        {
            Car car = col.gameObject.GetComponent<Car>();

            Vector3 dir = col.contacts[0].point;
            dir.y = 5.0f;

            Debug.Log(dir);

            SetState(eState.Dead);
            this.GetComponent<Rigidbody>().AddForce(dir * (car.Motor / value));
        }
    }

	public eState GetState()
	{
		return mState;
	}

	public void SetState( eState state )
	{
		mState = state;

		switch ( mState )
		{
			case eState.Waiting:
				break;

			case eState.Alive:
				break;

			case eState.Dead:
                Destroy(this.GetComponent<NavMeshAgent>());
				break;
		}
	}

	private void StateAlive()
	{
		mMoveTimer = Mathf.Max( mMoveTimer - Time.deltaTime, 0f );
		if ( mMoveTimer <= 0f && mAgent.remainingDistance < 0.25f )
		{
			mMoveTimer = Random.Range( 3f, 5f );
			MakeNewMovement();
		}
	}

	private void StateDead()
	{
		// Fade out or something?
        if (mDeadTimer >= 0.0f)
        {
            mDeadTimer -= Time.deltaTime;
        }
        else
        {
            Color color = mRenderer.material.color;

            color.a -= Time.deltaTime;

            mRenderer.material.color = color;

            if (color.a <= 0.0f)
            {
                Destroy(this.gameObject);
            }
        }
	}

	// Find a random new position within mRoamRadius to move towards.
	private void MakeNewMovement()
	{
		Vector2 unitCircle = Random.insideUnitCircle;
		Vector3 circle = new Vector3( unitCircle.x, 0f, unitCircle.y ) * mRoamRadius;
		circle += transform.position;

		NavMeshHit hit;
		NavMesh.SamplePosition( circle, out hit, mRoamRadius, 1 );

		if ( Vector3.Distance( transform.position, hit.position ) < 1f )
		{
			MakeNewMovement();
			return;
		}

		mAgent.destination = hit.position;

		//print( "NPC is moving: " + mAgent.destination );
	}
}